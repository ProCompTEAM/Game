using System;
using GameServer.level.chunk;
using GameServer.level.chunk.pattern;

namespace GameServer.level
{
	public static class Creator
	{
		public static void CreateSimple(Level level, ushort width, ushort height)
		{
			for(ushort i = 0; i < width; i++)
			{
				for(ushort j = 0; j < height; j++)
				{
					level.SetChunk(new Chunk(i, j, new Empty()));
				}
			}
		}
		
		public static void CreateMesh(Level level, ushort width, ushort height)
		{
			Random rnd = new Random();
			
			for(ushort i = 0; i < width; i++)
			{
				for(ushort j = 0; j < height; j++)
				{
					int r = rnd.Next(1, 7);
					System.Threading.Thread.Sleep(10);
					
					switch(r)
					{
						case 1:
						case 2:
								level.SetChunk(new Chunk(i, j, new City()));
						break;
						
						case 3:
						case 4:
							level.SetChunk(new Chunk(i, j, new Forest()));
						break;
						
						default:
							level.SetChunk(new Chunk(i, j, new Empty()));
						break;
					}
				}
			}
		}
	}
}
