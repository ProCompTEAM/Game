using System;
using System.Collections.Generic;

namespace GameServer.ui.form
{
	public class Form
	{
		public readonly string Title;
		public readonly string Token;
		protected readonly Dictionary<int, Control> Controls = new Dictionary<int, Control>();
		
		public readonly string HtmlTextColor, HtmlBGColor;
		
		public Form(string title = "", string htmlTextColor = "", string htmlBGColor = "")
		{
			Title = title;
			
			HtmlTextColor = htmlTextColor;
			HtmlBGColor = htmlBGColor;
			
			Token = utils.TextUtil.GenerateToken(6);
		}
		
		public Control GetControl(int id)
		{
			if(Controls.ContainsKey(id)) return Controls[id];
			
			return null;
		}
		
		public Control[] GetControls()
		{
			List<Control> cs = new List<Control>();
			
			foreach(int id in Controls.Keys) cs.Add(Controls[id]);
			
			return cs.ToArray();
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
		
		/*
		 * STRING [DATA] : <HashToken:6>;<Title>;<TextColor>;<BGColor>;<ContentControls : <id>,<type>,<items>>
		 */
		
		public override string ToString()
		{
			string data = "", controls = "";
			
			data += Token + ";";
			data += Title + ";";
			data += HtmlTextColor + ";";
			data += HtmlBGColor;
			
			foreach(int id in Controls.Keys) controls += ";" + id.ToString() + "," + Controls[id].ToString();
			
			return (data + controls);
		}

	}
}
