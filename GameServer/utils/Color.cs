using System;

namespace GameServer.utils
{
	public class Color
	{	
		public const char WHITE = 'f';
		public const char BLACK = '0';
		public const char GREEN  = '1';
		public const char BLUE = '2';
		public const char AQUA = '3';
		public const char BRICK = '4';
		public const char PURPLE = '5';
		public const char ORANGE = '6';
		public const char SMOKE = '7';
		public const char GRAY = '8';
		public const char OCEAN = '9';
		public const char LIME = 'a';
		public const char SKY = 'b';
		public const char RED = 'c';
		public const char PINK = 'd';
		public const char YELLOW = 'e';
		
		public const char SIGNATURE = '§';
		
		public readonly char Code;
		
		public Color()
		{
			Code = WHITE;
		}
		
		public Color(char code)
		{
			Code = code;
		}
		
		
	}
}
