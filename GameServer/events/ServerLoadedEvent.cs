using System;

namespace GameServer.events
{
	public class ServerLoadedEvent : Event
	{
		public string Description;
		
		public ServerLoadedEvent(string DescriptionOfEvent)
		{
			Description = DescriptionOfEvent;
		}
		
		public override string GetName()
		{
			return "Server Loaded Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_ServerLoadedEvent;
		}
	}
}
