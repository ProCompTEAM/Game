using System;
using GameServer.events;

namespace GameServer.network
{
	public class PacketsFilter : IEventListener
	{
		public PacketsFilter()
		{
			Events.AddListener(this);
		}
		
		public void Handler(Event CurrentEvent)
		{
			if(CurrentEvent.GetCode() == Events.Code_PacketRequestEvent)
			{
				PacketRequestEvent ev = (PacketRequestEvent) CurrentEvent;
				//TODO ...
			}
			
			if(CurrentEvent.GetCode() == Events.Code_PacketResponseEvent)
			{
				PacketResponseEvent ev = (PacketResponseEvent) CurrentEvent;
				//TODO ...
			}
		}
	}
}
