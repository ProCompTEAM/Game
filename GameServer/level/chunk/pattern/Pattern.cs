using System;

namespace GameServer.level.chunk.pattern
{
	public abstract class Pattern
	{
		protected int[,] Content;
		
		public int Size
		{
			get { return Chunk.SIZE; }
		}
		
		protected Pattern()
		{
			Content = new int[Size, Size];
			
	        for (int i = 0; i < Size; i++)
	            for (int j = 0; j < Size; j++)
	        		Content[i, j] = View.ID_GRASS;
		}
		
		public abstract int[,] Generate();
	}
}
