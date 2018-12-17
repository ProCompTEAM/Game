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
		
		public bool JoinPlayer(Player p)
		{
			if(Players.IndexOf(p) == -1) {
				Players.Add(p);
				
				Data.SendToLog("Player " + p.Name + " join to game in level '" + LevelName + "'", Data.Log_Info, ConsoleColor.Yellow);
				
				return true;
			}
			return false;
		}
		
		public bool LeavePlayer(Player p)
		{
			Data.SendToLog("Player " + p.Name + " leave the game", Data.Log_Info, ConsoleColor.DarkYellow);
			
			return Players.Remove(p);
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
	}
}
