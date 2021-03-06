﻿using System;
using System.Collections.Generic;
using System.IO;
using GameServer.locale;

namespace GameServer.player.control
{
	public static class Ban
	{
		const string FILE1 = "banned.txt";
		const string FILE2 = "banned-ips.txt";
		
		static List<string> Banned = new List<string>();
		static List<string> BannedIPs = new List<string>();
		
		public static void InitializeAll()
		{
			if(!File.Exists(FILE1) && !File.Exists(FILE2)) Save();
			
			Load();
		}
		
		public static void BanByName(string playerName, string ownerName = "")
		{	
			if(!IsBanned(playerName))
			{
				Banned.Add(playerName.ToLower());
				
				Server.BroadcastMessage(Strings.From("players.banned") + playerName + " : " + ownerName);
				
				if(Server.IsOnline(playerName))
					Server.GetPlayer(playerName).Close();
				
				Save();
			}
		}
		
		public static void PardonByName(string playerName, string ownerName = "")
		{	
			if(IsBanned(playerName))
			{
				Banned.Remove(playerName.ToLower());
				
				Server.BroadcastMessage(Strings.From("players.unblocked") + playerName + " : " + ownerName);
				
				Save();
			}
		}
		
		public static bool IsBanned(string playerName)
		{
			return (Banned.IndexOf(playerName.ToLower()) != -1);
		}
		
		public static void BanIP(string ip, string ownerName = "")
		{	
			if(!IsIPBanned(ip))
			{
				BannedIPs.Add(ip);
				
				Server.BroadcastMessage(Strings.From("player.ipban") + ip + " : " + ownerName);
				
				Save();
			}
		}
		
		public static void PardonIP(string ip, string ownerName = "")
		{	
			if(IsIPBanned(ip))
			{
				Banned.Remove(ip);
				
				Server.BroadcastMessage(Strings.From("player.unblocked") + ip + " : " + ownerName);
				
				Save();
			}
		}
		
		public static bool IsIPBanned(string ip)
		{
			return (BannedIPs.IndexOf(ip) != -1);
		}
		
		internal static void Load()
		{
			Banned.AddRange(File.ReadAllLines(FILE1));
			Banned.AddRange(File.ReadAllLines(FILE2));
		}
		
		internal static void Save()
		{
			File.WriteAllLines(FILE1, Banned);
			File.WriteAllLines(FILE2, BannedIPs);
		}
	}
}
