using System;

namespace GameServer.events
{
	public class AddonLoadedEvent : Event
	{
		public addon.IModule Addon;
		
		public AddonLoadedEvent(addon.IModule IModule) 
		{
			Addon = IModule;
		}
		
		public override string GetName()
		{
			return "Addon Loaded Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_AddonLoadedEvent;
		}
	}
}
