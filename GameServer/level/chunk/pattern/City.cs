
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
		
		public void Block_Generator()
		{
            int count = 0;
            int x = rand.Next(1,15), y = rand.Next(1,15);
            for (int cycle = 0; cycle < 10; cycle++)
            {
                count = 0;
                switch (rand.Next(1, 5))
                {
                    case 1:
                        for (int i = 1; i < 4; i++)
                        {
                            if (x > 0 && x < Size - 2)
                            {
                                if ((x + i) < 14)
                                {
                                    Content[x + i, y] = View.ID_ROAD1;
                                    count++;
                                }
                            }
                            else
                                break;
                        }
                        x += count;
                        break;
                    case 2:
                        for (int i = 4; i > 0; i--)
                        {
                            if (x > 0 && x < Size - 2)
                            {
                                if ((x - i) > 1)
                                {
                                    Content[x - i, y] = View.ID_ROAD1;
                                    count++;
                                }

                            }
                            else
                                break;
                        }
                        x -= count;
                        break;
                    case 3:
                        for (int i = 1; i < 4; i++)
                        {
                            if (y > 0 && y < Size - 2)
                            {
                                if ((y + i) < 14)
                                {
                                    Content[x, y + i] = View.ID_ROAD1;
                                    count++;
                                }

                            }
                            else
                                break;
                        }
                        y += count;
                        break;
                    case 4:
                        for (int i = 4; i > 0; i--)
                        {
                            if (y > 0 && y < Size - 2)
                            {
                                if ((y - i) > 1)
                                {
                                    Content[x, y - i] = View.ID_ROAD1;
                                    count++;
                                }
                            }
                        }
                        y -= count;
                        break;
                }
            }
        }
		
		public void Road_Creation()
        {
            bool left = false, right = false, top = false, bottom = false;
            
            for (int i = 1; i < Size - 1; i++)
            {
                for (int j = 1; j < Size - 1; j++)
                {
                    if (Content[i, j] == View.ID_ROAD1)
                    {
                        if (Content[i, j] >= View.ID_ROAD1 && Content[i, j] <= View.ID_ROAD11)
                        {
                            if (Content[i + 1, j] >= View.ID_ROAD1 && Content[i + 1, j] <= View.ID_ROAD11)
                                bottom = true;
                            if (Content[i - 1, j] >= View.ID_ROAD1 && Content[i - 1, j] <= View.ID_ROAD11)
                                top = true;
                            if (Content[i, j + 1] >= View.ID_ROAD1 && Content[i, j + 1] <= View.ID_ROAD11)
                                right = true;
                            if (Content[i, j - 1] >= View.ID_ROAD1 && Content[i, j - 1] <= View.ID_ROAD11)
                                left = true;
                        }

                        if (!bottom && !top && right && !left)
                            Content[i, j] = View.ID_ROAD1;
                        if (!bottom && !top && !right && left)
                            Content[i, j] = View.ID_ROAD1;
                        if (!bottom && !top && right && left)
                            Content[i, j] = View.ID_ROAD1;
                        if (bottom && !top && !right && !left)
                            Content[i, j] = View.ID_ROAD2;
                        if (!bottom && top && !right && !left)
                        	Content[i, j] = View.ID_ROAD2;
                        if (bottom && top && !right && !left)
                            Content[i, j] = View.ID_ROAD2;
                        if (!bottom && top && !right && left)
                            Content[i, j] = View.ID_ROAD3;
                        if (bottom && !top && !right && left)
                            Content[i, j] = View.ID_ROAD4;
                        if (!bottom && top && right && !left)
                            Content[i, j] = View.ID_ROAD5;
                        if (bottom && !top && right && !left)
                            Content[i, j] = View.ID_ROAD6;
                        if (!bottom && top && right && left)
                            Content[i, j] = View.ID_ROAD7;
                        if (bottom && top && !right && left)
                            Content[i, j] = View.ID_ROAD8;
                        if (bottom && top && right && !left)
                            Content[i, j] = View.ID_ROAD9;
                        if (bottom && !top && right && left)
                            Content[i, j] = View.ID_ROAD10;
                        if (bottom && top && right && left)
                            Content[i, j] = View.ID_ROAD11;

                        bottom = false; top = false; right = false; left = false;
                    }
                }
            }
        }
        
        void Building(int number, int i, int j)
        {
            switch (number)
            {
                case 1:
                    Content[i + 1, j] = rand.Next(View.ID_HOUSE1, View.ID_HOUSE11);
                    break;
                case 2:
                    Content[i - 1, j] = rand.Next(View.ID_HOUSE1, View.ID_HOUSE11);
                    break;
                case 3:
                    Content[i, j + 1] = rand.Next(View.ID_HOUSE1, View.ID_HOUSE11);
                    break;
                case 4:
                    Content[i, j - 1] = rand.Next(View.ID_HOUSE1, View.ID_HOUSE11);
                    break;
            }
        }
        void Build_Creation()
        {
            bool left = false, right = false, top = false, bottom = false;

            for (int i = 1; i < Size - 1; i++)
            {
                for (int j = 1; j < Size - 1; j++)
                {
                    if (Content[i, j] == View.ID_ROAD1)
                    {
                        if (Content[i, j] >= View.ID_ROAD1 && Content[i, j] <= View.ID_ROAD11)
                        {
                            if (Content[i + 1, j] >= View.ID_ROAD1 && Content[i + 1, j] <= View.ID_ROAD11)
                                bottom = true;
                            if (Content[i - 1, j] >= View.ID_ROAD1 && Content[i - 1, j] <= View.ID_ROAD11)
                                top = true;
                            if (Content[i, j + 1] >= View.ID_ROAD1 && Content[i, j + 1] <= View.ID_ROAD11)
                                right = true;
                            if (Content[i, j - 1] >= View.ID_ROAD1 && Content[i, j - 1] <= View.ID_ROAD11)
                                left = true;
                        }

                        if (bottom && !top && !right && !left)
                            switch (rand.Next(1,7))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(3, i, j);
                                    break;
                                case 3:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && top && !right && !left)
                            switch (rand.Next(1, 7))
                            {
                                case 1:
                                    Building(2, i, j);
                                    break;
                                case 2:
                                    Building(3, i, j);
                                    break;
                                case 3:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && !top && right && !left)
                            switch (rand.Next(1, 7))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(2, i, j);
                                    break;
                                case 3:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && !top && !right && left)
                            switch (rand.Next(1, 7))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(2, i, j);
                                    break;
                                case 3:
                                    Building(3, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && top && !right && !left)
                            switch (rand.Next(1, 6))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(2, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && !top && right && !left)
                            switch (rand.Next(1, 6))
                            {
                                case 1:
                                    Building(3, i, j);
                                    break;
                                case 2:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && !top && !right && left)
                            switch (rand.Next(1, 6))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(3, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && top && right && !left)
                            switch (rand.Next(1, 6))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && top && !right && left)
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    Building(2, i, j);
                                    break;
                                case 2:
                                    Building(3, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && !top && right && left)
                            switch (rand.Next(1, 6))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                case 2:
                                    Building(2, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && !top && right && left)
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    Building(1, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && top && !right && left)
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    Building(3, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (bottom && top && right && !left)
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    Building(4, i, j);
                                    break;
                                default:
                                    break;
                            }
                        if (!bottom && top && right && left)
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    Building(2, i, j);
                                    break;
                                default:
                                    break;
                            }

                        bottom = false; top = false; right = false; left = false;
                    }
                }
            }
        }
    public override int[,] Generate()
		{
			Block_Generator();
			Road_Creation();
            Build_Creation();

			return Content;
		}

	}
}
