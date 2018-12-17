using System;
using System.Linq;
using System.Threading;

namespace GameServer.generator
{
	class City : Generator
    {
        public override void Generate()
        {
            Random rand = new Random();
            GBorder gborder = new GBorder();
            GCity gcity = new GCity();
            GBuilding gbuilding = new GBuilding();
            GRoad groad = new GRoad();

            int[,] city = new int[MATRIX_SIZE, MATRIX_SIZE];

            gborder.Border(MATRIX_SIZE, city);
            gcity.City_1(city);
            Thread.Sleep(10);
            gcity.City_2(city);
            gbuilding.Buildings(MATRIX_SIZE, city);
            groad.road(MATRIX_SIZE, city);

            this.matrix = city;
        }
    }
}
