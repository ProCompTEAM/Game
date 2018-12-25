using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
		
		public static string HandleCommand(string[] Args, string ownerName = "Console")
		{
			events.ConsoleCommandEvent ev = new events.ConsoleCommandEvent(Args);
			events.Events.CallEvent(ev);
			if(ev.Cancelled) return "Cancelled!";
			
			switch(Args[0])
			{
				case "?": case "help": ShowHelpText(); return string.Join(", ", HelpCommandLines);
				case "exit": Server.Exit(); return "Closing...";
				case "stop": Server.ServerStop(); return "Closing...";
				case "resume": Server.ServerResume(); return "Resumed.";
				case "online":
				Data.SendToLog("Online: " + Server.CurrentLevel.GetOnlinePlayers().Length.ToString());
					foreach(player.Player p in Server.CurrentLevel.GetOnlinePlayers())
					{
						Data.SendToLog("- " + p.ToString());
					}
				return string.Join(", ", Server.CurrentLevel.GetOnlinePlayersStr());
				case "ban":
					if(Args.Length < 2) return "bad argument";
				
					string ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9‌​]{2}|2[0-4][0-9]|25[0-5])$";
			        Regex r = new Regex(ValidIpAddressRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			        
			        if (r.Match(Args[1]).Success)
			        	player.control.Ban.BanIP(Args[1], ownerName);
			        else player.control.Ban.BanByName(Args[1], ownerName);
				return "ok";
				case "pardon":
					if(Args.Length < 2) return "bad argument";
				
					ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9‌​]{2}|2[0-4][0-9]|25[0-5])$";
			        r = new Regex(ValidIpAddressRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			        
			        if (r.Match(Args[1]).Success)
			        	player.control.Ban.PardonIP(Args[1], ownerName);
			        else player.control.Ban.PardonByName(Args[1], ownerName);
				return "ok";
				
				default: Data.SendToLog("Unknown command!", Data.Log_Warning); return "Unknow command, type /help";
			}
		}
		
		public static void InitializeDafaultLines()
		{
			HelpCommandLines.Add("exit - stop server and exit from app");
			HelpCommandLines.Add("stop - stop server listeners and network");
			HelpCommandLines.Add("resume - resume(if stopped) server");
			HelpCommandLines.Add("online - online players of server");
			HelpCommandLines.Add("ban <name/ip> - ban player");
			HelpCommandLines.Add("pardon <name/ip> - unblock player");
		}
		
		public static void ShowHelpText()
		{
			Console.WriteLine("> List of avaliable commands: ");
			
			foreach(string line in HelpCommandLines) Console.WriteLine("* " + line);
			
			Console.WriteLine();
		}
	}
}
