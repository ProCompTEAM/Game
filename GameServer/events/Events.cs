using System;
using System.Collections.Generic;

namespace GameServer.events
{
	public static class Events
	{
		public const int Code_Event = 0x00;
		public const int Code_PingEvent = 0x01;
		
		public static List<IEventListener> Listeners = new List<IEventListener>();
		
		public static void CallEvent(Event CalledEvent)
		{
			foreach(IEventListener listener in Listeners)
			{
				listener.Handler(CalledEvent);
			}
		}
		
		public static void AddListener(IEventListener Listener)
		{
			Listeners.Add(Listener);
		}
		
		public static void RemoveListener(IEventListener Listener)
		{
			Listeners.Remove(Listener);
		}
	}
}
