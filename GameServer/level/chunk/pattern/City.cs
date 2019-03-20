
using System;

namespace GameServer.level.chunk.pattern
{
	/// <summary>
	/// Description of City.
	/// </summary>
	public class City:Pattern
	{
		Random rand = new Random();
		public City()
		{
			
			
		}
		
		public void Road()
		{
			int x, y;
            int way;
            int build;
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
                            if (x >= 1 && x <= Size)
                            {
                                Content[x + count, y] = 1;
                                countt++;

                                build = rand.Next(3) + 1;

                                switch (build)
                                {
                                    case 1:
                                        Content[x + count, y + 1] = 20;
                                        break;
                                    case 2:
                                        Content[x + count, y - 1] = 20;
                                        break;
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
                            if (x >= 1 && x <= Size)
                            {
                                Content[x - count, y] = 1;
                                countt++;

                                build = rand.Next(3) + 1;

                                switch (build)
                                {
                                    case 1:
                                        Content[x - count, y + 1] = 20;
                                        break;
                                    case 2:
                                        Content[x - count, y - 1] = 20;
                                        break;
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
                            if (y >= 1 && y <= Size-1)
                            {
                                Content[x, y + count] = 1;
                                countt++;

                                build = rand.Next(3) + 1;

                                switch (build)
                                {
                                    case 1:
                                        Content[x + 1, y + count] = 20;
                                        break;
                                    case 2:
                                        Content[x - 1, y + count] = 20;
                                        break;
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
                            if (y >= 1 && y <= Size)
                            {
                                Content[x, y - count] = 1;
                                countt++;

                                build = rand.Next(3) + 1;

                                switch (build)
                                {
                                    case 1:
                                        Content[x + 1, y - count] = 20;
                                        break;
                                    case 2:
                                        Content[x - 1, y - count] = 20;
                                        break;
                                }
                            }
                        }

                        y -= countt;
                        break;
                }
            }

            Content[x, y] = 3;
		}
		
		public void road()
        {

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] < 1 || Content[i - 1, j] > 15)
                        {
                            if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                            {
                                if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                {
                                    if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                    {
                                        Content[i, j] = 2;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i, j - 1] < 1 || Content[i, j - 1] > 15)
                            {
                                if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 3;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i, j + 1] < 1 || Content[i, j + 1] > 15)
                            {
                                if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                {
                                    if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                    {
                                        Content[i, j] = 4;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i, j - 1] < 1 || Content[i, j - 1] > 15)
                            {
                                if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 5;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i, j + 1] < 1 || Content[i, j + 1] > 15)
                            {
                                if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                {
                                    if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                    {
                                        Content[i, j] = 6;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] < 1 || Content[i - 1, j] > 15)
                        {
                            if (Content[i, j - 1] < 1 || Content[i, j - 1] > 15)
                            {
                                if (Content[i, j + 1] < 1 || Content[i, j + 1] > Size)
                                {
                                    if (Content[i + 1, j] > 0 && Content[i + 1, j] < Size)
                                    {
                                        Content[i, j] = 7;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i, j - 1] < 1 || Content[i, j - 1] > 15)
                            {
                                if (Content[i, j + 1] < 1 || Content[i, j + 1] > Size)
                                {
                                    if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                    {
                                        Content[i, j] = 8;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] < 1 || Content[i - 1, j] > 15)
                        {
                            if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                            {
                                if (Content[i, j - 1] < 1 || Content[i, j - 1] > Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 9;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] < 1 || Content[i - 1, j] > 15)
                        {
                            if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                            {
                                if (Content[i, j + 1] < 1 || Content[i, j + 1] > Size)
                                {
                                    if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                    {
                                        Content[i, j] = 10;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i, j - 1] < 1 || Content[i, j - 1] > 15)
                        {
                            if (Content[i + 1, j] > 0 && Content[i + 1, j] < Size)
                            {
                                if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 11;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i, j + 1] < 1 || Content[i, j + 1] > 15)
                        {
                            if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                            {
                                if (Content[i + 1, j] > 0 && Content[i + 1, j] < Size)
                                {
                                    if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                    {
                                        Content[i, j] = 12;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i + 1, j] < 1 || Content[i + 1, j] > 15)
                        {
                            if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                            {
                                if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 13;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] < 1 || Content[i - 1, j] > 15)
                        {
                            if (Content[i + 1, j] > 0 && Content[i + 1, j] < Size)
                            {
                                if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 14;
                                    }
                                }
                            }
                        }
                    }

                    if (Content[i, j] == 1)
                    {
                        if (Content[i - 1, j] > 0 && Content[i - 1, j] < Size)
                        {
                            if (Content[i + 1, j] > 0 && Content[i + 1, j] < Size)
                            {
                                if (Content[i, j - 1] > 0 && Content[i, j - 1] < Size)
                                {
                                    if (Content[i, j + 1] > 0 && Content[i, j + 1] < Size)
                                    {
                                        Content[i, j] = 15;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
		
		public override int[,] Generate()
		{
			Road();
			road();
			return Content;
		}

	}
}
