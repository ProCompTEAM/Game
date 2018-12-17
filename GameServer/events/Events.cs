using System;
using System.Collections.Generic;

namespace GameServer.events
{
	public static class Events
	{
		public const int Code_Event = 0x00;
		public const int Code_PingEvent = 0x01;
		public const int Code_PacketRequestEvent = 0x03;
		public const int Code_PacketResponseEvent = 0x04;
		public const int Code_ServerLoadedEvent = 0x05;
		public const int Code_ServerStoppedEvent = 0x06;
		public const int Code_ConsoleCommandEvent = 0x07;
		public const int Code_AddonLoadedEvent = 0x08;
		public const int Code_AddonDisabledEvent = 0x09;
		public const int Code_LevelLoadedEvent = 0x0A;
		
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
