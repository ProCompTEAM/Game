using System;

namespace GameServer.level.chunk.pattern
{
	public class Forest : Pattern
	{
        public void Forest_Creation()
        {
            Random rand = new Random();
            int Long;

            for (int x = 0; x < Size; x++)
            {
                int y;
                y = rand.Next(2, 9);
                Long = rand.Next(9, 14);
                if (y > Long)
                {
                    for (; y > Long; y--)
                    {
                        if (y >= 0 && y < Size)
                        {
                            Content[x, y] = View.ID_FOREST;
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
                        if (y >= 0 && y < Size)
                        {
                            Content[x, y] = View.ID_FOREST;
                        }
                        else
                            break;
                    }
                }
            }

            for (int y = 0; y < Size; y++)
            {
                int x;
                x = rand.Next(2, 9);
                Long = rand.Next(9, 14);
                if (x > Long)
                {
                    for (; x > Long; x--)
                    {
                        if (x >= 0 && x < Size)
                        {
                            Content[x, y] = View.ID_FOREST;
                        }
                        else
                            break;
                    }
                }
                else
                if (x < Long)
                {
                    for (; x < Long; x++)
                    {
                        if (x >= 0 && x < Size)
                        {
                            Content[x, y] = View.ID_FOREST;
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