using System;

namespace GameServer
{
	public class Tile : utils.Identifier
	{
		utils.Position Position;
		string Meta;
		
		public Tile(int id, string meta = "") : base(id)
		{
			Position = new utils.Position(0, 0);
			
			Meta = meta;
		}
		
		public Tile(int id, int x, int y) : base(id)
		{
			Position = new utils.Position(x, y);
		}
		
		public utils.Position GetPosition()
		{
			return Position;
		}
		
		public string Metadata
		{
			get { return Meta; }
		}
	}
}
