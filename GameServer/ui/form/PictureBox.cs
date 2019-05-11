using System;

namespace GameServer.ui.form
{
	public class PictureBox : Control
	{
		public readonly string ImageLink;
		
		public PictureBox(string imageLink) : base(TYPE_PICTURE)
		{
			ImageLink = GameServer.utils.TextUtil.ClearForPacket(imageLink.Replace(',', _REPLACE));
		}
		
		public override string ToString()
		{
			return Type.ToString() + "," + ImageLink;
		}

	}
}