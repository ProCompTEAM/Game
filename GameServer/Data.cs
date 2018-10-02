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
			string line = string.Format("[{0}][{1}] {2}", DateTime.Now.ToLongTimeString(), Type, Message);
			
			Console.WriteLine(line);
			
			if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON)
				File.AppendAllText(LOG_FILE, Environment.NewLine + line);
		}
		
		public static void SetTitle(string Title)
		{
			Console.Title = GetGameName() + " v." + GetGameVersion() + " | " + Title;
		}
		
		public static void Crash(Exception Ex, bool StopServer = false)
		{
			string crash = "<-=================- C R A S H -=================->";
			
			Console.WriteLine(Environment.NewLine + crash);
			
			Data.SendToLog("Detected server crash", Data.Log_Critical);
			
			Console.WriteLine(" * " + Ex.ToString());
			Console.WriteLine(" * Source: " + Ex.Source);
			Console.WriteLine(crash + Environment.NewLine);
			
			if(StopServer) Server.ServerStop();
			
			if(Server.Properties.GetProperty("save-crashes") == Config.SWITCH_ON)
			{
				string dt = DateTime.Now.ToShortDateString().Replace('.', '-') + "_" + DateTime.Now.ToLongTimeString().Replace(':', '-');
				File.AppendAllText("crashdump_" + dt + ".txt", Ex.ToString());
			}
		}
	}
}
