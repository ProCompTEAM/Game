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
	}
}
