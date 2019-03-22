using System;

namespace GameServer.level.chunk.pattern
{
	public class Forest : Pattern
	{
        public void Forest_Creation()
        {
            Random rand = new Random();
            int Long;
            int y;
            for (int x = 0; x < Size; x++)
            {
                y = rand.Next(2, 9);
                Long = rand.Next(9, 14);
                if (y > Long)
                {
                    for (; y > Long; y--)
                    {
                    	if (y >= 0 && y < 16)
                    	{
                            Content[x, y] = 17;
                            Content[y, x] = 17;
                    	}
                        else
                            break;
                    }
                }
                else
                if (y < Long)
                {
                    for (; y < Long; y++)
                    {
                    	if (y >= 0 && y < 16)
                    	{
                            Content[x, y] = 17;
                            Content[y, x] = 17;
                    	}
                        else
                            break;
                    }
                }
            }
            
        }
		public override int[,] Generate()
		{
            Forest_Creation();
			return Content;
		}
	}
}