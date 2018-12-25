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
		public const string Log_Chat = "Log/Chat";

		public static string LOG_FILE = "server-log.txt";
		
		public static string GetGameName()
		{
			return "Building Race";
		}
		
		public static double GetGameVersion()
		{
			return 1.0;
		}
		
		public static void SendToLog(string Message, string Type = Log_Info, ConsoleColor cl = ConsoleColor.White)
		{
			string line = string.Format("[{0}][{1}] {2}", DateTime.Now.ToLongTimeString(), Type, Message);
			
			if(cl == ConsoleColor.White || 
			   Server.Properties.GetProperty("console-colors") != Config.SWITCH_ON) Console.WriteLine(line);
			else
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("[{0}][{1}] ", DateTime.Now.ToLongTimeString(), Type);
				Console.ForegroundColor = cl;
				Console.WriteLine(Message);
				Console.ForegroundColor = ConsoleColor.White;
			}
			
			
			if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON)
				File.AppendAllText(LOG_FILE, Environment.NewLine + line);
		}
		
		public static void Debug(string Message)
		{
			if(Server.Properties.GetProperty("debug-logging") == Config.SWITCH_ON)
				SendToLog("[DEBUG] " + Message, Log_Info, ConsoleColor.DarkGray);
		}
		
		public static void SetTitle(string Title)
		{
			Console.Title = GetGameName() + " v." + GetGameVersion() + " | " + Title;
		}
		
		public static void Crash(Exception Ex, bool StopServer = false)
		{
			string crash = "<-=================- C R A S H -=================->";
			
			if(Server.Properties.GetProperty("console-colors") == Config.SWITCH_ON)
				Console.ForegroundColor = ConsoleColor.Red;
			
			Console.WriteLine(Environment.NewLine + crash);
			
			Data.SendToLog("Detected server crash", Data.Log_Critical);
			
			Console.WriteLine(" * " + Ex.ToString());
			Console.WriteLine(" * Source: " + Ex.Source);
			Console.WriteLine(crash + Environment.NewLine);
			
			if(Server.Properties.GetProperty("console-colors") == Config.SWITCH_ON)
				Console.ForegroundColor = ConsoleColor.White;
			
			if(StopServer) Server.ServerStop();
			
			if(Server.Properties.GetProperty("save-crashes") == Config.SWITCH_ON)
			{
				string dt = DateTime.Now.ToShortDateString().Replace('.', '-') + "_" + DateTime.Now.ToLongTimeString().Replace(':', '-');
				File.AppendAllText("crashdump_" + dt + ".txt", Ex.ToString());
			}
		}
	}
}
