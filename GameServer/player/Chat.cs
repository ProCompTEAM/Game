using System;
using System.Collections.Generic;

using GameServer.utils;

namespace GameServer.player
{
	public class Chat
	{
		List<string> Messages = new List<string>();
		
		public string Prefix;
		
		public Chat()
		{	
			Prefix = "";
			
			Clear();
		}
		
		public void Clear()
		{
			Messages.Clear();
			
			Messages.Add("");
			Messages.Add("");
			Messages.Add("");
			Messages.Add("");
			Messages.Add("");
		}
		
		public void SendMessage(string Message)
		{	
			Messages.Add(Message);
		}
		
		public void SendMessage(string Message, Color color)
		{	
			Messages.Add(color.Format + Message);
		}
		
		public string GetMessage(int offset = 0)
		{
			return Messages[Messages.Count - 1 - offset];
		}
		
		public string[] GetMessages(int offset = 0, int count = 5)
		{
			List<string> r = new List<string>();
			
			for(int i = offset; i < count; i++) 
				r.Add(Messages[Messages.Count - 1 - i]);
			
			r.Reverse();
			
			return r.ToArray();
		}
	}
}
