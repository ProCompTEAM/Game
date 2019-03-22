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
	16.12.2018 BuildingRace
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
	result = "";
	
	var lines = data.split(';');
	
	for(i = 0; i < lines.length; i++)
	{
		var chars = lines[i].split(' ');
		
		for(j = 0; j < chars.length; j++)
		{
			var parts = chars[j].split('[');
			
			result += parts[0] + " ";

			if(isset_arr(parts, 1))
			{
				for(c = 0; c < Number.parseInt(parts[1]); c++) result += parts[0] + " ";
				
				result.substring(0, result.length - 1);
			}
		}
		
		result += ";";
	}
	
	return result.substring(0, result.length - 1);
}

var update_level = function(current_level_cache)
{	
	//FORMAT: oX,oY:[id,id,id,id .. id];oX,oY:[id,id,id,id .. id]

	//LEVEL_CACHE = decompress_level(current_level_cache);
	LEVEL_CACHE = current_level_cache;
	
	//console.log(LEVEL_CACHE);
	
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

var INVENTORY_MASS = 0;
var INVENTORY_SELECTED = 0;

var inventory_update = function(raw)
{return;
	var el = document.getElementById("inventory_content");
	el.innerHTML = "";
	
	var items = raw.split(';');
	
	for(i = 0; i < items.length; i++)
	{
		var id    = items[i].split(',')[0];
		var count = items[i].split(',')[1];
		var label = items[i].split(',')[2];
		
		el.innerHTML += 
			'<div class="item" onclick="INVENTORY_SELECTED = ' + Number.parseInt(id) + '">' + 
				'<img src="' + gameObjects[Number.parseInt(id)].frameName + '">' +
				'<span class="count">' + count + 'x</span>' + 
				'<span class="label">' + label + '</span>' + 
			'</div>'
		;
	}
}

var click_level = function(x, y, id)
{
	net_request_inventory(PLAYER_TOKEN, x, y, INVENTORY_SELECTED);
	//net_request_level(PLAYER_TOKEN);
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
			document.getElementById("label").innerText = SERVER_NAME;
			
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
				document.getElementById("bar").innerHTML = packet['bar'];
			
			check_level(packet['mass']);
		break;
		
		//Packet 'Chat Response Packet' : 0x07
		case 0x08:
			show_status("Чат обновлен...");
			
			var lines = packet['raw'].split(';');
			
			document.getElementById("chat").innerHTML = "";
			
			for(i = 0; i < lines.length; i++)
				document.getElementById("chat").innerHTML += lines[i] + "<br>";
		break;
		
		//Packet 'Inventory Response Packet' : 0x09
		case 0x09:
			inventory_update(packet['items']);
			
			show_status("Инвентарь обновлен...");
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
	api_request("p=7+token=" + token);
}

//Packet 'Chat Packet Request' : 0x08
var net_request_chat = function(token, message = "")
{
	if(message == "") api_request("p=8+token=" + token);
	else api_request("p=8+token=" + token + "+msg=" + message);
}

//Packet 'Inventory Packet Request' : 0x09
var net_request_inventory = function(token, x = -1, y = -1, id = -1)
{
	if(x == -1 && y == -1 && id == -1) api_request("p=9+token=" + token);
	else api_request("p=9+token=" + token + "+set=" + x + "," + y + "," + id);
}