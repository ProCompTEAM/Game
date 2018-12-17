using System;
using System.Linq;

namespace GameServer.generator
{
    class GBuilding
    {
        public void Buildings(int n, int[,] massive)
        {
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (massive[i, j] == 20)
                    {
                        massive[i, j] = rand.Next(20, 30);
                    }
                }
            }
        }
    }
}
