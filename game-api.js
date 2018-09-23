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

const SERVER_ADDRESS = "http://mcrpg.ru/";

var SERVER_RESPONCE = "";
var request = new XMLHttpRequest();

var api_request = function(line_params)
{	
	SERVER_RESPONCE = "";
	
	request.open('GET', SERVER_ADDRESS + line_params, true);
	
	request.onload = function() 
	{
		SERVER_RESPONCE = this.responseText;
	}
	
	request.send();
}