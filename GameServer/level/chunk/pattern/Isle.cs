using System;

namespace GameServer.level.chunk.pattern
{
    public class Isle : Pattern
    {
        public void Ocean_Creation()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Content[x, y] = View.ID_OCEAN;
                }
            }
        }
        public void Isle_Creation()
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
                        if (y >= View.ID_ROAD1 && y <= View.ID_ROAD11)
                        {
                            Content[x, y] = View.ID_DESERT;
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
                        if (y >= View.ID_ROAD1 && y <= View.ID_ROAD11)
                        {
                            Content[x, y] = View.ID_DESERT;
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
                if (y > Long)
                {
                    for (; y > Long; y--)
                    {
                        if (y >= View.ID_ROAD1 && y <= View.ID_ROAD11)
                        {
                            Content[x, y] = View.ID_DESERT;
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
                        if (y >= View.ID_ROAD1 && y <= View.ID_ROAD11)
                        {
                            Content[x, y] = View.ID_DESERT;
                        }
                        else
                            break;
                    }
                }
            }
        }
        public override int[,] Generate()
        {
            Ocean_Creation();
            return Content;
        }
    }
}
