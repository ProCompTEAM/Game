using System;
<<<<<<< HEAD
=======
using System.IO;
using GameServer.utils;
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1

namespace GameServer
{
	public static class Data
	{
<<<<<<< HEAD
		public const string Log_Info = "Log/Info";
		public const string Log_Warning = "Log/Warning";
		public const string Log_Error = "Log/Error";
		public const string Log_Critical = "Log/Critical";	
=======
		public const int DEFAULT_SERVER_PORT = 48888;
		
		public const string Log_Info = "Log/Info";
		public const string Log_Warning = "Log/Warning";
		public const string Log_Error = "Log/Error";
		public const string Log_Critical = "Log/Critical";

		public static string LOG_FILE = "server-log.txt";
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1
		
		public static string GetGameName()
		{
			return "Building Race";
		}
		
		public static double GetGameVersion()
		{
			return 1.0;
		}
		
		public static void SendToLog(string Message, string Type = Log_Info)
		{
<<<<<<< HEAD
			Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + Type + "] " + Message);
=======
			string line = "[" + DateTime.Now.ToLongTimeString() + "] [" + Type + "] " + Message;
			
			Console.WriteLine(line);
			
			if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON)
				File.AppendAllText(LOG_FILE, Environment.NewLine + line);
>>>>>>> 4232a856c76727beecb118792a3f95ce6b770ac1
		}
		
		
	}
}
