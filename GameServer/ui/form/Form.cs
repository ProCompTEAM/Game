using System;
using System.Collections.Generic;

namespace GameServer.ui.form
{
	public class Form
	{
		public readonly string Title;
		protected readonly Dictionary<int, Control> Controls = new Dictionary<int, Control>();
		
		public Form(string title = "")
		{
			Title = title;
		}
		
		public Control GetControl(int id)
		{
			if(Controls.ContainsKey(id)) return Controls[id];
			
			return null;
		}
		
		public int AddControl(Control control)
		{
			int id = Controls.Count;
			
			if(!Controls.ContainsKey(id))
				Controls[id] = control;
			
			return id;
		}
		
		public int RemoveControl(int id)
		{
			if(Controls.ContainsKey(id)) Controls.Remove(id);
			
			return id;
		}
		
		public int RemoveControl(Control control)
		{
			foreach(int id in Controls.Keys)
			{
				if(Controls[id] == control)
				{
					Controls.Remove(id);
					
					return id;
				}
			}
			
			return -1;
		}
	}
}
