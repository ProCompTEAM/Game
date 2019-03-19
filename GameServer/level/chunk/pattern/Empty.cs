using System;

namespace GameServer.level.chunk.pattern
{
	public class Empty : Pattern
	{
		public override int[,] Generate()
		{
			return Content;
		}
	}
}