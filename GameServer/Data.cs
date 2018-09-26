using System;
using System.IO;
using GameServer.utils;

namespace GameServer
{
	public static class Data
	{
		public const int DEFAULT_SERVER_PORT = 48888;
		
		public const string Log_Info = "Log/Info";
		public const string Log_Warning = "Log/Warning";
		public const string Log_Error = "Log/Error";
		public const string Log_Critical = "Log/Critical";

		public static string LOG_FILE = "server-log.txt";
		
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
			string line = "[" + DateTime.Now.ToLongTimeString() + "] [" + Type + "] " + Message;
			
			Console.WriteLine(line);
			
			if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON)
				File.AppendAllText(LOG_FILE, Environment.NewLine + line);
		}
		
		
	}
}
