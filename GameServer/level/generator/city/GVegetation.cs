using System;
using System.Linq;

namespace GameServer.level.generator.city
{
    class GVegetation
    {
        public void Vegetation(int n, int[,] massive)
        {
            Random rand = new Random();
            int x = rand.Next(4, 16);
            int y = x;
            
            for (int count = 0; count < rand.Next(3,6) + 1; count++)
            {
                for (int i = 0; i < rand.Next(3) + 1; i++)
                {
                    if (y + i < 20)
                        if (massive[x, y + i] == 0)
                            massive[x, y + i] = 17;
                    else
                        break;
                }

                for (int i = rand.Next(3) + 1; i > 0; i--)
                {
                    if (y - i > 0)
                        if (massive[x, y - i] == 0)
                            massive[x, y - i] = 17;
                    else
                        break;
                }

                x++;
            }

            massive[y + rand.Next(3) + 1,y + rand.Next (3) + 1 ] = 16;

            x = rand.Next(24, 36);
            y = x;

            for (int count = 0; count < rand.Next(3, 6) + 1; count++)
            {
                for (int i = 0; i < rand.Next(3) + 1; i++)
                {
                    if (y + i < 40)
                        if (massive[x, y + i] == 0)
                            massive[x, y + i] = 17;
                    else
                        break;
                }

                for (int i = rand.Next(3) + 1; i > 0; i--)
                {
                    if(y - i > 20)
                        if (massive[x, y - i] == 0)
                            massive[x, y - i] = 17;
                    else
                        break;
                }

                x++;
            }

            massive[y + rand.Next(2) + 1, y + rand.Next(2) + 1] = 16;

        }
    }
}

