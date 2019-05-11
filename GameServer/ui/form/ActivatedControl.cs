using System;

namespace GameServer.ui.form
{
	public class ActivatedControl
	{
		public readonly int Id;
		public string Data;
		
		public ActivatedControl(int id, string data)
		{
			Id = id;
			Data = data;
		}
		
		public ActivatedControl(Control control)
		{
			Id = -1;
			Data = control.GetType().ToString();
		}
		
		public static ActivatedControl From(Control control)
		{
			return new ActivatedControl(control);
		}
	}
}
