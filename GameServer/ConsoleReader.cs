using System;

namespace GameServer
{
	public static class ConsoleReader
	{
		public static void Read()
		{
			while(true)
			{
				HandleCommand(Console.ReadLine().Split(' '));
			}
		}
		
		public static void HandleCommand(string[] Args)
		{
			switch(Args[0])
			{
				case "exit": Server.Exit(); break;
				case "stop": Server.ServerStop(); break;
				case "resume": Server.ServerResume(); break;
				case "restart": Server.ServerRestart(); break;
				default: Data.SendToLog("Unknown command!", Data.Log_Warning); break;
			}
		}
	}
}
