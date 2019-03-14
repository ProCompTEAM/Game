using System;
using System.Collections.Generic;

namespace GameServer.ui.form
{
	public class Form
	{
		public const int DEFAULT_WIDTH = 600;
		public const int DEFAULT_HEIGHT = 400;
		
		public int Width, Height;
		
		string NameId;
		
		public string Title;
		
		public object[] MetaData;
		
		public List<Control> Content;
		
		public string ID
		{
			get { return NameId; }
		}
		
		public Form(string nameId, params object[] md)
		{
			NameId = nameId;
			Width = DEFAULT_WIDTH;
			Height = DEFAULT_HEIGHT;
			Title = "";
			Content = new List<Control>();
			MetaData = md;
		}
		
		public Form(string nameId, int width, int height, string title = "", params object[] md)
		{
			NameId = nameId;
			Width = width;
			Height = height;
			Title = title;
			Content = new List<Control>();
			MetaData = md;
		}
		
		public void Add(params Control[] controls)
		{
			foreach(Control c in controls) Content.Add(c);
		}
		
		public void Remove(params Control[] controls)
		{
			foreach(Control c in controls) Content.Remove(c);
		}
		
		/*
		 * FORMAT > <ID>,<width>,<height>,<title>,<strings:controls>
		*/
		
		public override string ToString()
		{
			string result = ID + "," + Width.ToString() + "," + Height.ToString() + "," + Title;
			
			foreach(Control c in Content) result += "," + c.ToString();
			
			return result;
		}
	}
}
