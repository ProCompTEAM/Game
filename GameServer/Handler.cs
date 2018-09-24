using System;

namespace GameServer
{
	public static class Handler
	{
		/*
		 * @return string param1=value1+param2=value2+param3=value3... 
		 **/
		public static string SendRequest(string[] Items)
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
			
			return "status=ok";
		}
	}
}
