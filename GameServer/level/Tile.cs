using System;

namespace GameServer
{
	public class Tile : View
	{
		string Meta;
		
		public Tile(int id, string meta = "")
		{
			Id = id;
			
			Meta = meta;
		}
		
		public string Metadata
		{
			get { return Meta; }
		}
	}
}
