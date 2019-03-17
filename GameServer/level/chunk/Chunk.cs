using System;
using GameServer.level.chunk.pattern;

namespace GameServer.level.chunk
{
	public class Chunk
	{
		public const int SIZE = 16;
		
		public readonly int OffsetX, OffsetY;
		
		public int[,] Content;
		
		public Chunk(ushort offsetX, ushort offsetY, Pattern pattern)
		{
			OffsetX = offsetX;
			OffsetY = offsetY;
			
			Content = pattern.Generate();
		}
		
		public int Mass
		{
			get
			{
				int mass = 0;
				
				for(int i = 0; i < SIZE; i++)
					for(int j = 0; j < SIZE; j++)
						mass += Content[i, j];
				
				return mass;
			}
		}
	}
}
