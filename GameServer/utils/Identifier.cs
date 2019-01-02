using System;

namespace GameServer.utils
{
	public class Identifier
	{
		public const int iSimple = 0;
		public const int iRace1 = 2;
		
		protected int Code;
		
		public Identifier(int code)
		{
			Code = code;
		}
		
		public int ToInt32()
		{
			return Code;
		}
		
		public override string ToString()
		{
			return Code.ToString();
		}
	}
}
