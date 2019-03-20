using System;
using System.Collections.Generic;

using GameServer.level.chunk.pattern;
using GameServer.locale;
using GameServer.player;
using GameServer.level.chunk;
	
namespace GameServer.level
{
	public class Level
	{
		public readonly string Name;
		
		public List<Player> Players;
		
		List<Chunk> Chunks;
		
		public Level(string name)
		{
			Name = name.ToLower();
			
			Players = new List<Player>();
			
			Chunks = new List<Chunk>();
			
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
		
		public void SetTile(Tile tile)
		{
			//TODO SetTile
		}
		
		public Tile GetTile(utils.Position position)
		{
			return new Tile(0, position.X, position.Y); //TODO GetTile
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
		
		public int Mass
		{
			get
			{
				int mass = 1;
				
				foreach(Chunk c in Chunks) mass += c.Mass;
				
				return mass;
			}
		}
		
		public string RawData
		{
			//oX,oY:[id,id,id,id .. id];oX,oY:[id,id,id,id .. id]
			get
			{
				if(Chunks.Count > 0)
				{
					string result = "";
					
					foreach(Chunk c in Chunks)
					{
						string data = "";
						
						for(int i = 0; i < Chunk.SIZE; i++)
							for(int j = 0; j < Chunk.SIZE; j++)
								data += ("," + c.Content[i, j]);
											
						result += string.Format(";{0},{1}:[{2}]", c.OffsetX, c.OffsetY, data.Substring(1));
					}
					
					
					
					return result.Substring(1);
				}
				else return "";
			}
		}
		
		public void SetChunk(Chunk chunk)
		{
			foreach(Chunk c in Chunks)
			{
				if(c.OffsetX == chunk.OffsetX && c.OffsetY == chunk.OffsetY) Chunks.Remove(c);
			}
			
			Chunks.Add(chunk);
		}
		
		public bool UnsetChunk(int offsetX, int offsetY)
		{
			foreach(Chunk c in Chunks)
			{
				if(c.OffsetX == offsetX && c.OffsetY == offsetY) 
				{
					Chunks.Remove(c);
					
					return true;
				}
			}
			
			return false;
		}
		
		public Chunk[] GetChunks()
		{
			return Chunks.ToArray();
		}
	}
}
