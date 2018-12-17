using System;
using System.Linq;

namespace GameServer.generator
{
    class GCity
    {
        
        public void City_1(int[,] massive)
        {
            Random rand = new Random();

            int gpoint_1 = rand.Next(40, 70);
            int gpoint_2 = rand.Next(25, 35);

            int i = gpoint_1;
            int j = gpoint_2;

            int size;
            int random;
            int build;

            for (int cycle = 0; cycle < 5; cycle++)
            {
                size = rand.Next(5) + 2;
                random = rand.Next(2) + 1;

                switch (random)
                {
                    case 1:
                        massive[i, j] = 1;
                        random = rand.Next(2) + 1;

                        switch (random)
                        {
                            case 1:
                                for (int count = 1; count < 5; count++)
                                {

                                    massive[i + count, j] = 1;
                                    build = rand.Next(2) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + count, j + 1] = 20;
                                            break;
                                        case 2:
                                            massive[i + count, j - 1] = 20;
                                            break;
                                        default:
                                            break;
                                    }

                                }
                                i += 5;
                                break;
                            case 2:
                                for (int count = 5; count > 0; count--)
                                {
                                    massive[i - count, j] = 1;
                                    build = rand.Next(3) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i - count, j + 1] = 20;
                                            break;
                                        case 2:
                                            massive[i - count, j - 1] = 20;
                                            break;
                                        default:
                                            break;
                                    }

                                }
                                i -= 5;
                                break;
                        }
                        break;

                    case 2:
                        massive[i, j] = 1;
                        random = rand.Next(2) + 1;

                        switch (random)
                        {
                            case 1:
                                for (int count = 1; count < 5; count++)
                                {
                                    massive[i, j + count] = 1;
                                    build = rand.Next(3) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + 1, j + count] = 20;
                                            break;
                                        case 2:
                                            massive[i - 1, j + count] = 20;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                j += 5;
                                break;
                            case 2:
                                for (int count = 5; count > 0; count--)
                                {
                                    massive[i, j - count] = 1;
                                    build = rand.Next(3) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + 1, j - count] = 20;
                                            break;
                                        case 2:
                                            massive[i - 1, j - count] = 20;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                j -= 5;
                                break;
                        }
                        break;
                }
                massive[gpoint_1, gpoint_2] = 3;
            }

            massive[gpoint_1, gpoint_2] = 3;
        }


        public void City_2(int[,] massive)
        {
            Random rand = new Random();
           
            int gpoint_1 = rand.Next(40, 70);
            int gpoint_2 = rand.Next(65, 75);


            int i = gpoint_1;
            int j = gpoint_2;

            int size;
            int random;
            int build;

            

            for (int cycle = 0; cycle < 5; cycle++)
            {
                size = rand.Next(5) + 2;
                random = rand.Next(2) + 1;

                switch (random)
                {
                    case 1:
                        massive[i, j] = 1;
                        random = rand.Next(2) + 1;

                        switch (random)
                        {
                            case 1:
                                for (int count = 1; count < 5; count++)
                                {

                                    massive[i + count, j] = 1;
                                    build = rand.Next(2) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + count, j + 1] = 20;
                                            break;
                                        case 2:
                                            massive[i + count, j - 1] = 20;
                                            break;
                                        default:
                                            break;
                                    }

                                }
                                i += 5;
                                break;
                            case 2:
                                for (int count = 5; count > 0; count--)
                                {
                                    massive[i - count, j] = 1;
                                    build = rand.Next(2) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i - count, j + 1] = 20;
                                            break;
                                        case 2:
                                            massive[i - count, j - 1] = 20;
                                            break;
                                        default:
                                            break;
                                    }

                                }
                                i -= 5;
                                break;
                        }
                        break;

                    case 2:
                        massive[i, j] = 1;
                        random = rand.Next(2) + 1;

                        switch (random)
                        {
                            case 1:
                                for (int count = 1; count < 5; count++)
                                {
                                    massive[i, j + count] = 1;
                                    build = rand.Next(2) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + 1, j + count] = 20;
                                            break;
                                        case 2:
                                            massive[i - 1, j + count] = 20;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                j += 5;
                                break;
                            case 2:
                                for (int count = 5; count > 0; count--)
                                {
                                    massive[i, j - count] = 1;
                                    build = rand.Next(2) + 1;

                                    switch (build)
                                    {
                                        case 1:
                                            massive[i + 1, j - count] = 20;
                                            break;
                                        case 2:
                                            massive[i - 1, j - count] = 20;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                j -= 5;
                                break;
                        }
                        break;
                }
            }

            massive[gpoint_1, gpoint_2] = 3;
        }

    }
}
