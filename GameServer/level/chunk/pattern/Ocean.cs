using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.level.chunk.pattern
{
    class Ocean : Pattern
    {
        public void Ocean_Creation()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0;y < Size; y++)
                {
                    Content[x, y] = View.ID_OCEAN;
                }
            }
        }
        public override int[,] Generate()
        {
            Ocean_Creation();
            return Content;
        }
    }
}
