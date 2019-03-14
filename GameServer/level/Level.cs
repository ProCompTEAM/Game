using System;
using System.Collections.Generic;

using GameServer.locale;
using GameServer.player;	
using GameServer.level.generator;
using GameServer.level.generator.city;
	
namespace GameServer.level
{
	public class Level
	{
		public readonly string Name;
		
		public List<Player> Players;
		
		public generator.Generator Generator;
		
		public Level(string name)
		{
			Name = name.ToLower();
			
			Players = new List<Player>();
			
			Generator = new City();
			
			events.Events.CallEvent(new events.LevelLoadedEvent(this));
			
			Data.SendToLog(Strings.From("level.loading") + Name);
		}
		
		public string[] GetOnlinePlayersStr()
		{
			string cache = "";
			
			foreach(Player p in Players) cache += "+" + p.Name;
			
			return cache.Split('+');
		}
		
		public bool IsInLevel(string playerName)
		{
			return (Array.IndexOf(GetOnlinePlayersStr(), playerName) != -1);
		}
		
		public Player GetPlayer(string playerName)
		{
			foreach(Player p in Players) 
				if(p.Name == playerName) return p;
			return null;
		}
		
		public void Generate()
		{
			Data.SendToLog(Strings.From("level.generation") + Name);

			Generator.Generate();
		}
		
		public void SetTile(Tile tile)
		{
			if(tile.GetPosition().X >= 0 && tile.GetPosition().X < Generator.MATRIX_SIZE
			   && tile.GetPosition().Y >= 0 && tile.GetPosition().Y < Generator.MATRIX_SIZE)
				Generator.Set(tile.GetPosition().X, tile.GetPosition().Y, tile.ToInt32());
		}
		
		public Tile GetTile(utils.Position position)
		{
			return new Tile(Generator.Get(position.X, position.Y), position.X, position.Y);
		}
		
		public void BroadcastMessage(string messageText)
		{
			foreach(Player p in Players) p.CurrentChat.SendMessage(messageText);
			
			Data.SendToLog(messageText, Data.Log_Chat, ConsoleColor.Magenta);
		}
		
		public void BroadcastBar(string messageText)
		{
			foreach(Player p in Players) p.Bar(messageText);
		}
		
		public override string ToString()
		{
			return Name;
		}
	}
}
