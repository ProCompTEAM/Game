using System;
using System.Linq;

namespace GameServer.generator
{
    class GBuild
    {
        Random rand = new Random();
        public void Build(int n, int[,] massive)
        {
            int build;
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                        {
                            if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i , j - 1] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i , j + 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i,j] < 16)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                            {
                                if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i - 1, j] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i + 1, j] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i - 1, j] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i, j - 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i - 1, j] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i, j + 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i + 1, j] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i, j - 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i + 1, j] = rand.Next(20, 30);
                                                break;
                                            case 2:
                                                massive[i, j + 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i, j - 1] < 1 || massive[i, j - 1] > 15)
                        {
                            if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                            {
                                if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i, j - 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i, j + 1] < 1 || massive[i, j + 1] > 15)
                        {
                            if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                            {
                                if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                                {
                                    if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i, j + 1] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i + 1, j] < 1 || massive[i + 1, j] > 15)
                        {
                            if (massive[i - 1, j] > 0 && massive[i - 1, j] < 16)
                            {
                                if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i + 1, j] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (massive[i, j] > 0 && massive[i, j] < 16)
                    {
                        if (massive[i - 1, j] < 1 || massive[i - 1, j] > 15)
                        {
                            if (massive[i + 1, j] > 0 && massive[i + 1, j] < 16)
                            {
                                if (massive[i, j - 1] > 0 && massive[i, j - 1] < 16)
                                {
                                    if (massive[i, j + 1] > 0 && massive[i, j + 1] < 16)
                                    {
                                        build = rand.Next(2) + 1;
                                        switch (build)
                                        {
                                            case 1:
                                                massive[i - 1, j] = rand.Next(20, 30);
                                                break;
                                            default:
                                                break;
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
}
