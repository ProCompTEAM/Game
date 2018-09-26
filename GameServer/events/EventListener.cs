using System;
using System.Collections.Generic;

namespace GameServer.events
{
	public class EventListener
	{
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
