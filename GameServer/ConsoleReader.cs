using System;
using System.Collections.Generic;

namespace GameServer
{
	public static class ConsoleReader
	{
		public static List<string> HelpCommandLines = new List<string>();
		
		public static void Read()
		{
			while(true)
			{
				HandleCommand(Console.ReadLine().Split(' '));
			}
		}
		
		public static void HandleCommand(string[] Args)
		{
			events.ConsoleCommandEvent ev = new events.ConsoleCommandEvent(Args);
			events.Events.CallEvent(ev);
			if(ev.Cancelled) return;
			
			switch(Args[0])
			{
				case "?": case "help": ShowHelpText(); break;
				case "exit": Server.Exit(); break;
				case "stop": Server.ServerStop(); break;
				case "resume": Server.ServerResume(); break;
				
				default: Data.SendToLog("Unknown command!", Data.Log_Warning); break;
			}
		}
		
		public static void InitializeDafaultLines()
		{
			HelpCommandLines.Add("exit - stop server and exit from app");
			HelpCommandLines.Add("stop - stop server listeners and network");
			HelpCommandLines.Add("resume - resume(if stopped) server");
		}
		
		public static void ShowHelpText()
		{
			Console.WriteLine("> List of avaliable commands: ");
			
			foreach(string line in HelpCommandLines) Console.WriteLine("* " + line);
			
			Console.WriteLine();
		}
	}
}
