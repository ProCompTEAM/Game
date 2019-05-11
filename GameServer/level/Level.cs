using System;
using System.Collections.Generic;

using GameServer.locale;
using GameServer.player;
using GameServer.level.chunk;
	
namespace GameServer.level
{
	public class Level
	{
		public readonly string Name;
		
		public Player[] Players
		{
			get
			{
				List<Player> players = new List<Player>();
				
				foreach(Player player in Server.GetOnlinePlayers())
				{
					if(player.Level == this) players.Add(player);
				}
				
				return players.ToArray();
			}
		}
		
		List<Chunk> Chunks;
		
		public string CompressedData;
		
		public Level(string name)
		{
			Name = name.ToLower();
			
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
			foreach(Chunk c in Chunks.ToArray())
			{
				if(c.OffsetX == chunk.OffsetX && c.OffsetY == chunk.OffsetY) Chunks.Remove(c);
			}
			
			Chunks.Add(chunk);
			
			CompressedData = Compressor.Compress(RawData);
		}
		
		public bool UnsetChunk(int offsetX, int offsetY)
		{
			foreach(Chunk c in Chunks.ToArray())
			{
				if(c.OffsetX == offsetX && c.OffsetY == offsetY) 
				{
					Chunks.Remove(c);
					
					return true;
				}
			}
			
			CompressedData = Compressor.Compress(RawData);
			
			return false;
		}
		
		public Chunk[] GetChunks()
		{
			return Chunks.ToArray();
		}
		
		public Chunk GetChunk(ushort offsetX = 0, ushort offsetY = 0)
		{
			foreach(Chunk ch in Chunks)
			{
				if(ch.OffsetX == offsetX && ch.OffsetY == offsetY) return ch;
			}
			
			return null;
		}
		
		public void Save()
		{
			LevelsProvider.Save(this);
		}
	}
}
