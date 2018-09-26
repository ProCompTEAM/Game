using System;
using GameServer.network;

namespace GameServer
{
	public static class Handler
	{
		/*
		 * @return string param1=value1+param2=value2+param3=value3... 
		 **/
		public static string SendRequest(string RawData, string Address)
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
			Packet request = Network.HandleRequest(RawData, Address);
			
<<<<<<< HEAD
			//call events and more
			events.Events.CallEvent(new events.PacketRequestEvent(request));
			
			//server reply
			Packet response = Network.ConvertToResponse(request);
			events.Events.CallEvent(new events.PacketResponseEvent(response));
			
=======
			
			//call events and more
			//TODO >> ...
			
			
			//server reply
			Packet response = Network.ConvertToResponse(request);
>>>>>>> 4b586fa00d77513ad2e104cee463030542846d82
			Data.SendToLog("(packets) " + request.GetName() + " >> " + response.GetName());
			
			return response.TransformToRawData();
		}
	}
}
