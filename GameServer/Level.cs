using System;
using System.Collections.Generic;

using GameServer.locale;
using GameServer.player;	
using GameServer.generator.city;
	
namespace GameServer
{
	public class Level
	{
		string LevelName;
		
		List<Player> Players;
		
		public generator.Generator Generator;
		
		public Level(string name)
		{
			LevelName = name.ToLower();
			
			Players = new List<Player>();
			
			Generator = new City();
			
			events.Events.CallEvent(new events.LevelLoadedEvent(this));
			
			Data.SendToLog(Strings.From("level.loading") + LevelName);
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
				
			Data.SendToLog(Strings.From("player") + p.Name + Strings.From("level.join") + "'" + LevelName + "'", Data.Log_Info, ConsoleColor.Yellow);
			
			BroadcastMessage(Strings.From("player") + p.Name + Strings.From("player.joined"));
		}
		
		public void LeavePlayer(Player p)
		{
			Data.SendToLog(Strings.From("player") + p.Name + Strings.From("player.left"), Data.Log_Info, ConsoleColor.DarkYellow);
			
			Players.Remove(p);
			
			BroadcastMessage(Strings.From("player") + p.Name + Strings.From("player.left"));
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
			Data.SendToLog(Strings.From("level.generation") + LevelName);

			Generator.Generate();
		}
		
		public void SetTile(Tile tile)
		{
			Generator.Set(tile.GetPosition().X, tile.GetPosition().Y, tile.ToInt32());
		}
		
		public Tile GetTile(utils.Position position)
		{
			return new Tile(Generator.Get(position.X, position.Y), position.X, position.Y);
		}
		
		public void BroadcastMessage(string messageText)
		{
			foreach(Player p in GetOnlinePlayers()) p.CurrentChat.SendMessage(messageText);
			
			Data.SendToLog(messageText, Data.Log_Chat, ConsoleColor.Magenta);
		}
		
		public void BroadcastBar(string messageText)
		{
			foreach(Player p in GetOnlinePlayers()) p.Bar(messageText);
		}
	}
}
