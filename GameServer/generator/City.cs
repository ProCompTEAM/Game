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
            GCity gcity = new GCity();
            GRoad groad = new GRoad();
            GBuild gbuild = new GBuild();

            int[,] city = new int[MATRIX_SIZE, MATRIX_SIZE];

            gcity.City_1(city);
            Thread.Sleep(10);
            gcity.City_2(city);
            groad.road(MATRIX_SIZE, city);
            gbuild.Build(MATRIX_SIZE, city);
            this.matrix = city;
        }
    }
}
