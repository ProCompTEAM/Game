using System;
using System.Linq;

namespace GameServer.generator
{
    class GCity
    {
        Random rand = new Random();

        public void City_1(int[,] massive)
        {
            int x, y;
            int way;
            int countt = 0;
            x = rand.Next(5, 35);
            y = rand.Next(5, 15);


            for (int cycle = 0; cycle < 5; cycle++)
            {
                way = rand.Next(4) + 1;
                countt = 0;
                switch (way)
                {
                    case 1:
                        for (int count = 1; count < 4; count++)
                        {
                            if (x >= 1 && x <= 39)
                            {
                                if ((x + count) <= 38)
                                {
                                    massive[x + count, y] = 1;
                                    countt++;
                                }
                            }
                            else
                                break;
                        }

                        x += countt;
                        break;
                    case 2:
                        for (int count = 4; count > 0; count--)
                        {
                            if (x >= 1 && x <= 39)
                            {
                                if ((x - count) >= 2)
                                {
                                    massive[x - count, y] = 1;
                                    countt++;
                                }

                            }
                            else
                                break;
                        }

                        x -= countt;
                        break;
                    case 3:
                        for (int count = 1; count < 4; count++)
                        {
                            if (y >= 1 && y <= 19)
                            {
                                if ((y + count) <= 38)
                                {
                                    massive[x, y + count] = 1;
                                    countt++;
                                }

                            }
                            else
                                break;
                        }

                        y += countt;
                        break;
                    case 4:
                        for (int count = 4; count > 0; count--)
                        {
                            if (y >= 1 && y <= 19)
                            {
                                if ((y - count) >= 2)
                                {
                                    massive[x, y - count] = 1;
                                    countt++;
                                }
                            }
                        }

                        y -= countt;
                        break;
                }
            }

            massive[x, y] = 3;
        }


        public void City_2(int[,] massive)
        {
            int x, y;
            int way;
            int countt = 0;
            x = rand.Next(5, 35);
            y = rand.Next(25, 35);


            for (int cycle = 0; cycle < 5; cycle++)
            {
                way = rand.Next(4) + 1;
                countt = 0;
                switch (way)
                {
                    case 1:
                        for (int count = 1; count < 4; count++)
                        {
                            if (x >= 1 && x <= 39)
                            {
                                if ((x + count) <= 38)
                                {
                                    massive[x + count, y] = 1;
                                    countt++;
                                }
                            }
                            else
                                break;
                        }

                        x += countt;
                        break;
                    case 2:
                        for (int count = 4; count > 0; count--)
                        {
                            if (x >= 1 && x <= 39)
                            {
                                if ((x - count) >= 2)
                                {
                                    massive[x - count, y] = 1;
                                    countt++;
                                }

                            }
                            else
                                break;
                        }

                        x -= countt;
                        break;
                    case 3:
                        for (int count = 1; count < 4; count++)
                        {
                            if (y >= 21 && y <= 39)
                            {
                                if (y + count <= 38)
                                {
                                    massive[x, y + count] = 1;
                                    countt++;
                                }
                            }
                            else
                                break;
                        }

                        y += countt;
                        break;
                    case 4:
                        for (int count = 4; count > 0; count--)
                        {
                            if (y >= 21 && y <= 39)
                            {
                                if ((y - count) >= 2)
                                {
                                    massive[x, y - count] = 1;
                                    countt++;
                                }
                            }
                        }

                        y -= countt;
                        break;
            }
                }
            }

	}
}
