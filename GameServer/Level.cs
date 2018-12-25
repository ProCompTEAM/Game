using System;
using System.Collections.Generic;

using GameServer.player;	
using GameServer.generator;
	
namespace GameServer
{
	public class Level
	{
		string LevelName;
		
		List<Player> Players;
		
		public Generator LevelGenerator;
		
		public Level(string name)
		{
			LevelName = name.ToLower();
			
			Players = new List<Player>();
			
			LevelGenerator = new City();
			
			events.Events.CallEvent(new events.LevelLoadedEvent(this));
			
			Data.SendToLog("Loading '" + LevelName + "' level...");
		}
			
		
		public Player[] GetOnlinePlayers()
		{
			return Players.ToArray();
		}
		
		public string[] GetOnlinePlayersStr()
		{
			string cache = "";
			
			foreach(Player p in GetOnlinePlayers()) cache += "+" + p.Name;
			
			return cache.Split('+');
		}
		
		public void JoinPlayer(Player p)
		{
			Players.Add(p);
				
			Data.SendToLog("Player " + p.Name + " joined the game in level '" + LevelName + "'", Data.Log_Info, ConsoleColor.Yellow);
			
			BroadcastMessage("Player " + p.Name + " joined the game");
		}
		
		public void LeavePlayer(Player p)
		{
			Data.SendToLog("Player " + p.Name + " left the game", Data.Log_Info, ConsoleColor.DarkYellow);
			
			Players.Remove(p);
			
			BroadcastMessage("Player " + p.Name + " left the game");
		}
		
		public bool IsOnline(string playerName)
		{
			return (Array.IndexOf(GetOnlinePlayersStr(), playerName) != -1);
		}
		
		public Player GetPlayer(string playerName)
		{
			foreach(Player p in GetOnlinePlayers()) 
				if(p.Name == playerName) return p;
			return null;
		}
		
		public void Generate()
		{
			Data.SendToLog("Generation of '" + LevelName + "' level...");

			LevelGenerator.Generate();
		}
		
		public void SetTile(Tile tile)
		{
			LevelGenerator.Set(tile.GetPosition().X, tile.GetPosition().Y, tile.Code);
		}
		
		public Tile GetTile(utils.Position position)
		{
			return new Tile(LevelGenerator.Get(position.X, position.Y), position.X, position.Y);
		}
		
		public void BroadcastMessage(string messageText)
		{
			foreach(Player p in GetOnlinePlayers()) p.CurrentChat.SendMessage(messageText);
			
			Data.SendToLog(messageText, Data.Log_Chat, ConsoleColor.Magenta);
		}
	}
}
