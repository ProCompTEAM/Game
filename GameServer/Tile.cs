using System;

namespace GameServer
{
	public class Tile
	{
		utils.Position Position;
		
		public int Code;
		
		public readonly int[] BUILDINGS = { 3, 4, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
		public readonly int[] ROADS = { 5, 6, 7, 8, 9, 10, 11, 12, 21, 22 };
		
		public Tile(int TileCode)
		{
			Code = TileCode;
			
			Position = new utils.Position(0, 0);
		}
		
		public Tile(int TileCode, int x, int y)
		{
			Code = TileCode;
			
			Position = new utils.Position(x, y);
		}
		
		public utils.Position GetPosition()
		{
			return Position;
		}
	}
}
