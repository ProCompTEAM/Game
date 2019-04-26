using System;
using System.Collections.Generic;

namespace GameServer.player
{
	public class Session
	{
		public string Address;
		List<DateTime> Stamps = new List<DateTime>();
		
		public Session(string addr)
		{
			Address = addr;
			
			Stamps.Add(DateTime.Now);
		}
		
		public void NewStamp(DateTime dt)
		{
			Stamps.Add(dt);
		}
		
		public DateTime GetLastStamp()
		{
			return Stamps[Stamps.Count - 1];
		}
		
		public DateTime[] GetStampHistory()
		{
			return Stamps.ToArray();
		}
	}
}
