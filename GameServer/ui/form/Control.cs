using System;
using System.Collections.Generic;

namespace GameServer.ui.form
{
	public class Control
	{
		public const int DEFAULT_WIDTH = 600;
		public const int DEFAULT_HEIGHT = 400;
		
		public int Width, Height;
		
		public string Name;
		
		public object Tag;
		
		IFormAction ActionListener = null;
		
		public Control(int width, int height, string name, object tag = null)
		{
			Width = width;
			Height = height;
			Name = name;
			Tag = tag;
		}
		
		public Control(string name, object tag = null)
		{
			Width = DEFAULT_WIDTH;
			Height = DEFAULT_HEIGHT;
			Name = name;
			Tag = tag;
		}
		
		public void SetActionListener(IFormAction ifa)
		{
			ActionListener = ifa;
		}
		
		public void Action(Form form)
		{
			if(ActionListener != null)
				ActionListener.FormAction(form.ID, "Default", Name);
		}
		
		public void Action(string formID)
		{
			if(ActionListener != null)
				ActionListener.FormAction(formID, "Default", Name);
		}
		
		/*
		 * FORMAT > <type>,<width>,<height>,<name>
		*/
		
		public override string ToString()
		{
			return "control," + Width.ToString() + "," + Height.ToString() + "," + Name;
		}
	}
}
