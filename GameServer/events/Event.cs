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
		
		public virtual int GetCode()
		{
			return Events.Code_Event;
		}
		
		public bool IsWorkingNext()
		{
			return !Cancelled;
		}
		
		public void Cancel()
		{
			Cancelled = true;
		}
	}
}
