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

const SERVER_ADDRESS = "127.0.0.1:48888";

var SERVER_RESPONCE = "";
var request = new XMLHttpRequest();
request.responseType = "text";

var api_request = function(line_params)
{	
	SERVER_RESPONCE = "";
	
	request.open('GET', "http://" + SERVER_ADDRESS + "/" + line_params, true);
	
	request.onload = function() 
	{
		net_reply_handler(this.responseText);
	}
	
	request.send();
}

var show_status = function(message)
{
	document.getElementById("gs").innerHTML = message;
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
	
	return arr;
}

/*
	16.12.2018 BuildingRace
*/

var SERVER_NAME = "";
var PLAYER_NAME = "TestPlayer";
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

var update_level = function(current_level_cache)
{	
	LEVEL_CACHE = current_level_cache;
	
	alert(current_level_cache);
	
	//drawTileIso(4, 0, 0);
	
	const SZ = 40;
	
	var lines = LEVEL_CACHE.split(';');
	
	for(i = 0; i < SZ; i++)
	{
		chs = lines[i].split(' ');
		
		for(j = 0; j < SZ; j++) 
		{	
			drawTile(Number.parseInt(chs[j]), i, j);
		
			//if(chs[j] != 0) alert(lines[i] + "\n j " + j + " " + chs[j]);
		}
	}
}

var net_reply_handler = function(raw_data)
{
	show_status("Получены данные от сервера!");
	
	var packet = get_packet(raw_data);
	
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
			
			net_request_auth(PLAYER_NAME);
		break;
		
		//Packet 'Auth Response Packet' : 0x05
		case 0x05:
			PLAYER_TOKEN = packet['token'];
			
			setInterval(function() {
			  net_request_gs(PLAYER_TOKEN);
			}, 1000);
		break;
		
		//Packet 'Level Response Packet' : 0x07
		case 0x06:
			show_status("Обновление игрового мира...");
			
			update_level(packet['raw']);
		break;
		
		//Packet 'Gamestatus Response Packet' : 0x07
		case 0x07:
			show_status(packet['online'] + " " + packet['mass']);
			
			check_level(packet['mass']);
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