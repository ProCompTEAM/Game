using System;

namespace GameServer.ui.form
{
	public abstract class Control
	{
		public const int TYPE_SIMPLE = 0x00;
		public const int TYPE_BUTTON = 0x01;
		public const int TYPE_LIST = 0x02;
		public const int TYPE_TEXTBOX = 0x03;
		public const int TYPE_CONTENT = 0x04;
		public const int TYPE_PICTURE = 0x05;
		
		public const char _REPLACE = '~';
		
		public readonly int Type;
		
		protected Control(int controlType = TYPE_SIMPLE)
		{
			Type = controlType;
		}
		
		/*
		 * STRING : <type>,<element1>,<element2> .. <element№>
		 */
		
		public override string ToString()
		{
			return Type.ToString();
		}
	}
}
