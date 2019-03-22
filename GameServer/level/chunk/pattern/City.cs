
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
                                    Content[x + i, y] = 1;
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
                                    Content[x - i, y] = 1;
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
                                    Content[x, y + i] = 1;
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
                                    Content[x, y - i] = 1;
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
                    if (Content[i,j] == 1)
                    {
                        if (Content[i,j] > 0 && Content[i,j] < 16)
                        {
                        	if (Content[i + 1, j] > 0 && Content[i + 1, j] < 16)
                                bottom = true;
                        	if (Content[i - 1, j] > 0 && Content[i - 1, j] < 16)
                                top = true;
                        	if (Content[i, j + 1] > 0 && Content[i ,j + 1] < 16)
                                right = true;
                            if (Content[i, j - 1] > 0 && Content[i, j - 1] < 16)
                                left = true;
                        }
                        
                        if (bottom && !top && !right && !left)
                        	Content[i, j] = 1;
                        if (!bottom && top && !right && !left)
                        	Content[i, j] = 1;
                        if (!bottom && !top && right && !left)
                        	Content[i, j] = 2;
                        if (!bottom && !top && !right && left)
                        	Content[i, j] = 2;
                        if (bottom && top && !right && !left)
                            Content[i, j] = 1;
                        if (bottom && !top && right && !left)
                            Content[i, j] = 3;
                        if (bottom && !top && !right && left)
                            Content[i, j] = 4;
                        if (!bottom && top && right && !left)
                            Content[i, j] = 5;
                        if (!bottom && top && !right && left)
                            Content[i, j] = 6;
                        if (!bottom && !top && right && left)
                            Content[i, j] = 2;
                        if (bottom && !top && right && left)
                        	Content[i, j] = 14;
                        if (bottom && top && !right && left)
                        	Content[i, j] = 12;
                        if (bottom && top && right && !left)
                        	Content[i, j] = 11;
                        if (!bottom && top && right && left)
                        	Content[i, j] = 13;
                        if (bottom && top && right && left)
                            Content[i, j] = 15;

                        bottom = false; top = false; right = false; left = false;
                    }
                }
            }
        }

    public override int[,] Generate()
		{
			Block_Generator();
			Road_Creation();
            //Build_Creation();

			return Content;
		}

	}
}
