using System;

namespace GameServer.ui.form
{
	public class ListGroup : Control
	{
		public readonly string[] Lines;
		
		public ListGroup(string[] lines) : base(TYPE_LIST)
		{
			for(int i = 0; i < lines.Length; i++)
				lines[i] = lines[i].Replace(',', _REPLACE);
			
			Lines = lines;
		}
		
		public override string ToString()
		{
			return Type.ToString() + "," + string.Join(",", Lines);
		}

	}
}