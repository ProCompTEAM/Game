using System;
using System.Linq;

namespace GameServer.generator
{
    class GRoad
    {
        Random rand = new Random();
        public void road(int n, int[,] massive)
        {

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (massive[i, j] == 1)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                            {
                                if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        massive[i, j] = 2;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 3;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        massive[i, j] = 4;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 5;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                	if (massive[i,j-1] > 0 && massive[i,j-1] < 16)
                                    {
                                        massive[i, j] = 6;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                        {
                            if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 11;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                        {
                            if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        massive[i, j] = 12;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                            {
                                if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 13;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                            {
                                if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 14;
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] == 1)
                    {
                        if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                        {
                            if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                            {
                                if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        massive[i, j] = 15;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
