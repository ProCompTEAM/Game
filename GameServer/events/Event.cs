using System;

namespace GameServer.events
{
	public class Event
	{
		public bool Cancelled;
		
		public Event()
		{
			Cancelled = false;
		}
		
		public virtual string GetName()
		{
			return "Event";
		}
		
		public bool IsWorkingNext()
		{
			return !Cancelled;
		}
	}
}
