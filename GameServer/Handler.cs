using System;
using GameServer.network;

namespace GameServer
{
	public static class Handler
	{
		/*
		 * @return string param1=value1+param2=value2+param3=value3... 
		 **/
		public static string SendRequest(string RawData)
		{
			//format string Item: param=value
			/*
			 * foreach (string Item in Items)
			 * {
			 * 		string[] block = Item.Split('=');
			 * 		string param = block[0];
			 * 		string value = block[1];
			 * }
			 * */
			
			//received a request
			Packet request = Network.HandleRequest(RawData);
			Data.SendToLog("Initialized packet '" + request.GetName() + "'");
			
			//call events and more
			//TODO >> ...
			
			
			//server reply
			Packet response = Network.ConvertToResponse(request);
			Data.SendToLog("Packet '" + response.GetName() + "' sent back");
			
			return response.TransformToRawData();
		}
	}
}
