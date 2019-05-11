using System;

namespace GameServer.ui.form
{
	public class ContentText : Control
	{
		public readonly string Content;
		
		public ContentText(string content) : base(TYPE_CONTENT)
		{
			Content = GameServer.utils.TextUtil.ClearForPacket(content.Replace(',', _REPLACE));
		}
		
		public override string ToString()
		{
			return Type.ToString() + "," + Content;
		}

	}
}