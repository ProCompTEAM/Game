using System;

namespace GameServer.events
{
	public class ServerStoppedEvent : Event
	{
		public string Description;
		
		public ServerStoppedEvent(string DescriptionOfEvent)
		{
			Description = DescriptionOfEvent;
		}
		
		public override string GetName()
		{
			return "Server Stopped Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_ServerStoppedEvent;
		}
	}
}
