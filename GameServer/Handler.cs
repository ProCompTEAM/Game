using System;
<<<<<<< HEAD
=======
using GameServer.network;
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1

namespace GameServer
{
	public static class Handler
	{
		/*
		 * @return string param1=value1+param2=value2+param3=value3... 
		 **/
<<<<<<< HEAD
		public static string SendRequest(string[] Items)
=======
		public static string SendRequest(string RawData, string Address)
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1
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
			
<<<<<<< HEAD
			return "status=ok";
=======
			//received a request
			Packet request = Network.HandleRequest(RawData, Address);
			
			
			//call events and more
			//TODO >> ...
			
			
			//server reply
			Packet response = Network.ConvertToResponse(request);
			Data.SendToLog("(packets) " + request.GetName() + " >> " + response.GetName());
			
			return response.TransformToRawData();
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1
		}
	}
}
