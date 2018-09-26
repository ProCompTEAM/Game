using System;

namespace GameServer.entity
{
	public class Entity
	{
		public int EntityType;
		public utils.Position Position;
		
		const int Type_Tile = 0x00;
		const int Type_Road = 0x01;
		const int Type_Building = 0x02;
		const int Type_Station = 0x03;
			
		public Entity(utils.Position Pos, int Type = Type_Tile)
		{
			EntityType = Type;
			Position = Pos;
		}
	}
}
