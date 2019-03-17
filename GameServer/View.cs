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
		
		
		public int ToInt32()
		{
			return Identifier;
		}
	}
}
