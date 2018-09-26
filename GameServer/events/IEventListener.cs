using System;

namespace GameServer.events
{
	public interface IEventListener
	{
		void Handler(Event HandledEvent);
	}
}
