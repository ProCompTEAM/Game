using System;

namespace GameServer.ui.form
{
	public class TextBox : Control
	{
		public readonly string DefaultValue;
		
		public TextBox(string defaultValue = "") : base(TYPE_TEXTBOX)
		{
			DefaultValue = GameServer.utils.TextUtil.ClearForPacket(defaultValue.Replace(',', _REPLACE));
		}
		
		public override string ToString()
		{
			return Type.ToString() + "," + DefaultValue;
		}

	}
}