using System;

namespace GameServer
{
	public static class Data
	{
		public const string Log_Info = "Log/Info";
		public const string Log_Warning = "Log/Warning";
		public const string Log_Error = "Log/Error";
		public const string Log_Critical = "Log/Critical";	
		
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
			Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + Type + "] " + Message);
		}
		
		
	}
}
