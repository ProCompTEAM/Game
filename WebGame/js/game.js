//API JavaScript File

/*
	@возвращает текущую версию API игры в виде числа
*/
var api_version = function()
{
	return 1;
}

/*
	@регистрирует GET запрос на главный сервер и записывает результат через некоторое время в SERVER_RESPONCE
	
	@var line_params - строка в формате "параметр1=значение1+параметр2=значение2 ..";
*/



var SERVER_RESPONCE = "";
var request = new XMLHttpRequest();
request.responseType = "text";

var api_request = function(line_params)
{	
	var SERVER_RESPONCE = "";

	if(window.location.protocol == "http:" || window.location.protocol == "file:")
		request.open('GET', "http://" + SERVER_ADDRESS + "/" + line_params, true);
	else 
	{
		show_status("Ретрансляция (proxy)...");
		
		line_params = line_params.replaceAll('+', '[plus]');
		line_params = line_params.replaceAll('&', '[and]');
		
		request.open('GET', "/proxy.php?r=http://" + SERVER_ADDRESS + "/" + line_params, true);
		
		//console.log("/proxy.php?r=http://" + SERVER_ADDRESS + "/" + line_params);
	}
	
	request.onload = function() 
	{
		net_reply_handler(this.responseText);
	}
	
	request.send();
}

String.prototype.replaceAll = function(search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

var show_status = function(message)
{
	document.getElementById("gs").innerHTML = message;
}

var isset_arr = function(arr, index)
{
	return (typeof(arr[index]) != 'undefined');
}

var get_packet = function(raw_data)
{
	var arr = [];
	
	arr['p'] = -1;
	
	parts = raw_data.split('+');
	
	parts.forEach(function(line) 
	{
		packet_option = line.split('=')[0];
		packet_value  = line.split('=')[1];
		
		arr[packet_option] = packet_value;
	});
	
	if(isset_arr(arr, 'error'))
		alert("Получена ошибка сервера: \n" + arr['error']);
	
	return arr;
}

/*
	BuildingRace
*/

var SERVER_NAME = "";
var PLAYER_TOKEN = "";


var LEVEL_MASS = 0;
var LEVEL_CACHE = "";

var check_level = function(current_level_mass)
{
	if(current_level_mass != LEVEL_MASS) 
	{
		net_request_level(PLAYER_TOKEN);
		LEVEL_MASS = current_level_mass;
	}
}

var decompress_level = function(data)
{
	data = decompressor(data);
	return data;
}

var read_level = function(current_level_cache)
{
	LEVEL_CACHE = decompress_level(current_level_cache);
	
	var lines = LEVEL_CACHE.split(';');
	
	const sz = 16; //chunk size
	const maxsz = 256; //chunk square
	
	lines.forEach(
		function(item, i, lines) 
		{
			var parts = item.split(':');
			
			var offsetX = parts[0].split(',')[0];
			var offsetY = parts[0].split(',')[1];
			
			var line = parts[1].substr(1, parts[1].length - 2);
			
			var ids = line.split(',');
			
			var x = -1;
			var y = 0;
			
			for(var i = 0; i < maxsz; i++)
			{
				if(x == sz - 1) 
				{
					y++;
					x = 0;
				}
				else x++;
				
				//console.log("ox = " + offsetX + " oy = " + offsetY + " x = " + x + " y = " + y);
				
				tile_set(offsetX, offsetY, x, y, Number.parseInt(ids[i]));
			}
		}
	);
}

var update_level = function(current_level_cache)
{	
	var oldSum = chunksSumLevel();

	read_level(current_level_cache);
	var newSum = chunksSumLevel();
	
	if(newSum != oldSum) 
	{
		
		clearChunksLevel();
		read_level(current_level_cache);
		drawChunks(0,0);
		displayMap();
		console.log("dss");
	}
}

var INVENTORY_MASS = 0;
var INVENTORY_SELECTED = 1;

var show_inventory = function()
{
	var el = document.getElementById("inventory_content");
	el.innerHTML = "";
	
	for(i = 0; i < items.length; i++)
	{
		var id    = items[i].split(',')[0];
		var count = items[i].split(',')[1];
		var label = items[i].split(',')[2];
			
		if(label.toLowerCase().includes(document.getElementById("textbox").value.toLowerCase()) || document.getElementById("textbox").value==""){
			el.innerHTML += 
			'<div class="item" onclick="INVENTORY_SELECTED = ' + Number.parseInt(id) + '">' + 
				'<img src="assets/' +  ( gameObjects[Number.parseInt(id)] ) + '.png">' +
				'<span class="count">' + count + 'x</span>' + 
				'<span class="label">' + label + '</span>' + 
			'</div>'
			;
		}
	}
}

var inventory_update = function(raw)
{
	items = raw.split(';');
	
	show_inventory();
}

var click_level = function(ox, oy, x, y)
{
	net_request_inventory(PLAYER_TOKEN, ox, oy, y, x, INVENTORY_SELECTED);
}

var send_chat = function(message)
{
	net_request_chat(PLAYER_TOKEN, message.replace('=', ':').replace('+', '&'));
}

var net_reply_handler = function(raw_data)
{
	show_status("Получены данные от сервера!");
	
	var packet = get_packet(raw_data);
	
	if(raw_data.indexOf('p=') == -1)
	{
		alert("Исключение: " + raw_data);
		window.location.href = "/";
	}
	
	switch(Number.parseInt(packet['p']))
	{
		//Packet 'Ping Response Packet' : 0x01
		case 0x01:
			show_status("Получение имени сервера...");
			
			net_request_name();
		break;
		
		//Packet 'Named Response Packet' : 0x02
		case 0x02:
			SERVER_NAME = packet['name'];
			
			show_status(SERVER_NAME);
			document.title = SERVER_NAME;
			document.getElementById("label").innerHTML = str_color_format(SERVER_NAME);
			
			net_request_auth(PLAYER_NAME);
		break;
		
		//Packet 'Auth Response Packet' : 0x05
		case 0x05:
			PLAYER_TOKEN = packet['token'];
			
			setInterval(function() {
			  net_request_gs(PLAYER_TOKEN);
			}, 1000);
			
			setInterval(function() {
			  net_request_chat(PLAYER_TOKEN);
			}, 3000);
			
			net_request_level(PLAYER_TOKEN);
			net_request_inventory(PLAYER_TOKEN);
		break;
		
		//Packet 'Level Response Packet' : 0x07
		case 0x06:
			show_status("Обновление игрового мира...");
			
			update_level(packet['raw']);
		break;
		
		//Packet 'Gamestatus Response Packet' : 0x07
		case 0x07:
			show_status("Онлайн: " + packet['online']);
			
			if(INVENTORY_MASS != packet['inv'])
			{
				net_request_inventory(PLAYER_TOKEN);
				INVENTORY_MASS = packet['inv'];
			}
			
			if(isset_arr(packet, 'message'))
				alert("Сообщение от сервера: \n" + packet['message']);
			
			if(isset_arr(packet, 'bar'))
				document.getElementById("bar").innerHTML = str_color_format(packet['bar']);
			
			if(isset_arr(packet, 'mu'))
				document.getElementById("money").innerHTML = str_color_format(packet['mu']);
			
			if(isset_arr(packet, 'pu'))
				document.getElementById("pipls").innerHTML = str_color_format(packet['pu']);
			
			if(isset_arr(packet, 'fq'))
			{
				if(Number.parseInt(packet['fq']) > 0) 
					net_request_form(PLAYER_TOKEN);
			}
			
			if(isset_arr(packet, 'wurl'))
				window.open(packet['wurl'], '_blank');
			
			check_level(packet['mass']);
		break;
		
		//Packet 'Chat Response Packet' : 0x07
		case 0x08:
			show_status("Чат обновлен...");
			
			var lines = packet['raw'].split(';');
			
			document.getElementById("chat").innerHTML = "";
			
			for(i = 0; i < lines.length; i++)
				document.getElementById("chat").innerHTML += lines[i] + "<br>";
			
			document.getElementById("chat").innerHTML = str_color_format(document.getElementById("chat").innerHTML);
		break;
		
		//Packet 'Inventory Response Packet' : 0x09
		case 0x09:
			inventory_update(packet['items']);
			
			show_status("Инвентарь обновлен...");
		break;
		
		//Packet 'Form Response Packet' : 0x0A
		case 0x0A:
			create_form_ui(packet['raw']);
		break;
	}
}



//Packet 'Ping Packet Request' : 0x01
var net_request_ping = function()
{
	api_request("p=1");
}

//Packet 'Named Packet Request' : 0x02
var net_request_name = function()
{
	api_request("p=2");
}

//Packet 'Auth Packet Request' : 0x05
var net_request_auth = function(name)
{
	api_request("p=5+uid=" + name);
}

//Packet 'Level Packet Request' : 0x06
var net_request_level = function(token)
{
	api_request("p=6+token=" + token);
}

//Packet 'Gamestatus Packet Request' : 0x07
var net_request_gs = function(token)
{
	api_request("p=7+token=" + token + "+si=" + INVENTORY_SELECTED);
}

//Packet 'Chat Packet Request' : 0x08
var net_request_chat = function(token, message = "")
{
	if(message == "") api_request("p=8+token=" + token);
	else api_request("p=8+token=" + token + "+msg=" + message);
}

//Packet 'Inventory Packet Request' : 0x09
var net_request_inventory = function(token, ox = -1, oy = -1, x = -1, y = -1, id = -1)
{
	if(x == -1 && y == -1 && id == -1) api_request("p=9+token=" + token);
	else api_request("p=9+token=" + token + "+ox=" + ox + "+oy=" + oy + "+x=" + x + "+y=" + y + "+id=" + id);
}

//Packet 'Form Packet Request' : 0x0A
var net_request_form = function(token, activatedData = "")
{
	if(activatedData != "") api_request("p=10+token=" + token + "+af=" + activatedData);
	else api_request("p=10+token=" + token);
}

//Packet 'Settings Packet Request' : 0x0B
var net_request_settings = function(token, actId)
{
	api_request("p=11+token=" + token + "+act=" + actId);
}