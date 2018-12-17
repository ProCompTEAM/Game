using System;
using System.Linq;

namespace GameServer.generator
{
    class GBorder
    {
        public void Border(int n, int[,] massive)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 || i == n - 1 || j == 0 || j == n - 1)
                        massive[i, j] = 9;
                }
            }

        }
    }
}