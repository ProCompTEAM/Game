using System;
using System.Collections.Generic;

namespace GameServer.ui.form
{
	public class ActivatedForm
	{
		public readonly string Token;
		
		public readonly ActivatedControl[] Controls;
		public readonly object[] Data;
		
		public readonly bool IsFromBase = false;
		
		public ActivatedForm(string token, ActivatedControl[] controls, params object[] data)
		{
			Token = token;
			
			Controls = controls;
			Data = data;
		}
		
		public ActivatedForm(Form form, bool emulateControls = false)
		{
			Token = form.Token;
			
			if(emulateControls)
			{
				List<ActivatedControl> cs = new List<ActivatedControl>();
				
				foreach(Control c in form.GetControls()) cs.Add(new ActivatedControl(c));
					
				Controls = cs.ToArray();
			}
			else Controls = new ActivatedControl[256];
			
			Data[0] = (string) form.Title;
			Data[1] = (string) form.HtmlTextColor;
			Data[2] = (string) form.HtmlBGColor;
			
			IsFromBase = true;
		}
		
		public static ActivatedForm From(Form form)
		{
			return new ActivatedForm(form);
		}
		
		//data : <FToken>;<control : <iId>,<value>>;...;<control : <iId>,<value>>
		public static ActivatedForm Decode(string data)
		{
			List<ActivatedControl> controls = new List<ActivatedControl>();
			
			string[] els = data.Split(';');
			
			string token = els[0];
			
			for(int i = 1; i < els.Length; i++)
			{
				string[] control = els[i].Split(',');
				
				ActivatedControl c = new ActivatedControl(Convert.ToInt32(control[0]), control[1]);
				
				controls.Add(c);
			}
			
			return new ActivatedForm(token, controls.ToArray());
		}
	}
}
