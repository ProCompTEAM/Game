using System;

namespace GameServer.ui.form
{
	public class Button : Control
	{
		public readonly string Label;
		
		public Button(string label) : base(TYPE_BUTTON)
		{
			Label = GameServer.utils.TextUtil.ClearForPacket(label.Replace(',', _REPLACE));
		}
		
		public override string ToString()
		{
			return Type.ToString() + "," + Label;
		}

	}
}
