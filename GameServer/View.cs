using System;

namespace GameServer
{
	public abstract class View
	{
		protected int Id;
		
		public int Identifier
		{
			get { return Id; }
		}

        public const int ID_EMPTY = 0;
        public const int ID_GRASS = 1;
        public const int ID_OCEAN = 2;
        public const int ID_DESERT = 3;
        public const int ID_FOREST = 4;
        public const int ID_ROAD1 = 10;
        public const int ID_ROAD2 = 11;
        public const int ID_ROAD3 = 12;
        public const int ID_ROAD4 = 13;
        public const int ID_ROAD5 = 14;
        public const int ID_ROAD6 = 15;
        public const int ID_ROAD7 = 16;
        public const int ID_ROAD8 = 17;
        public const int ID_ROAD9 = 18;
        public const int ID_ROAD10 = 19;
        public const int ID_ROAD11 = 20;
        public const int ID_HOUSE1 = 21;
        public const int ID_HOUSE2 = 22;
        public const int ID_HOUSE3 = 23;
        public const int ID_HOUSE4 = 24;
        public const int ID_HOUSE5 = 25;
        public const int ID_HOUSE6 = 26;
        public const int ID_HOUSE7 = 27;
        public const int ID_HOUSE8 = 28;
        public const int ID_HOUSE9 = 29;
        public const int ID_HOUSE10 = 30;
        public const int ID_HOUSE11 = 31;
        public const int ID_SHOP1 = 51;
        public const int ID_SHOP2 = 52;
        public const int ID_SHOP3 = 53;
        public const int ID_SHOP4 = 54;
        
        public static int[] IDs
		{
			get
			{
				return new int[]
				{
					ID_EMPTY,
			        ID_GRASS,
			        ID_OCEAN,
			        ID_DESERT,
			        ID_FOREST,
			        ID_ROAD1,
			        ID_ROAD2,
			        ID_ROAD3,
			        ID_ROAD4,
			        ID_ROAD5,
			        ID_ROAD6,
			        ID_ROAD7,
			        ID_ROAD8,
			        ID_ROAD9,
			        ID_ROAD10,
			        ID_ROAD11,
			        ID_HOUSE1,
			        ID_HOUSE2,
			        ID_HOUSE3,
			        ID_HOUSE4,
			        ID_HOUSE5,
			        ID_HOUSE6,
			        ID_HOUSE7,
			        ID_HOUSE8,
			        ID_HOUSE9,
			        ID_HOUSE10,
			        ID_HOUSE11,
			        ID_SHOP1,
			        ID_SHOP2,
			        ID_SHOP3,
			        ID_SHOP4
				};
			}
		}

        public int ToInt32()
		{
			return Identifier;
		}
	}
}
