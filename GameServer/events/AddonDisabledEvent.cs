using System;

namespace GameServer.events
{
	public class AddonDisabledEvent : Event
	{
		public addon.IModule Addon;
		
		public AddonDisabledEvent(addon.IModule IModule) 
		{
			Addon = IModule;
		}
		
		public override string GetName()
		{
			return "Addon Disabled Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_AddonDisabledEvent;
		}
	}
}
