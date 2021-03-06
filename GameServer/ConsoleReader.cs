﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GameServer.locale;

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
			if(ev.Cancelled) return ev.Metadata;
			
			switch(Args[0])
			{
				case "?": case "help": ShowHelpText(); return string.Join(", ", HelpCommandLines);
				case "exit": Server.Exit(); return Strings.From("cmd.closing");
				case "stop": Server.ServerStop(); return Strings.From("cmd.closing");
				case "resume": Server.ServerResume(); return Strings.From("cmd.resumed");
				case "online":
				Data.SendToLog("Online: " + Server.GetOnlinePlayers().Length.ToString());
					foreach(player.Player p in Server.GetOnlinePlayers())
					{
						Data.SendToLog("- " + p.ToString());
					}
				return string.Join(", ", Server.GetOnlinePlayersStr());
				case "ban":
					if(Args.Length < 2) return Strings.From("cmd.badargument");
				
					string ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9‌​]{2}|2[0-4][0-9]|25[0-5])$";
			        Regex r = new Regex(ValidIpAddressRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			        
			        if (r.Match(Args[1]).Success)
			        	player.control.Ban.BanIP(Args[1], ownerName);
			        else player.control.Ban.BanByName(Args[1], ownerName);
				return "ok";
				case "pardon":
					if(Args.Length < 2) return Strings.From("cmd.badargument");
				
					ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9‌​]{2}|2[0-4][0-9]|25[0-5])$";
			        r = new Regex(ValidIpAddressRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			        
			        if (r.Match(Args[1]).Success)
			        	player.control.Ban.PardonIP(Args[1], ownerName);
			        else player.control.Ban.PardonByName(Args[1], ownerName);
				return "ok";
				case "ip":
				string ip = "(server) " + Server.GetFullAddress();
					if(Args.Length > 1 && Server.GetPlayer(Args[1]) != null)
						ip = Server.GetPlayer(Args[1]).Connection.Address;
					Data.SendToLog("IP = " + ip);
				return "IP = " + ip;
				
				default: Data.SendToLog(Strings.From("command.unknown"), Data.Log_Warning); return Strings.From("command.unknown");
			}
		}
		
		public static void InitializeDafaultLines()
		{
			HelpCommandLines.Add("exit - " + Strings.From("cmd.exit"));
			HelpCommandLines.Add("stop - " + Strings.From("cmd.stop"));
			HelpCommandLines.Add("resume - " + Strings.From("cmd.resume"));
			HelpCommandLines.Add("online - " + Strings.From("cmd.online"));
			HelpCommandLines.Add("ban <name/ip> - " + Strings.From("cmd.ban"));
			HelpCommandLines.Add("pardon <name/ip> - " + Strings.From("cmd.pardon"));
		}
		
		public static void ShowHelpText()
		{
			Console.WriteLine(Strings.From("command.list"));
			
			foreach(string line in HelpCommandLines) Console.WriteLine("* " + line);
			
			Console.WriteLine();
		}
	}
}
