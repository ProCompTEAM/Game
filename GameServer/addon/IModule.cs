using System;

namespace GameServer.addon
{
	public interface IModule
	{
    	void OnLoaded();
    	void OnDisabled();
		
    	void HandleEvent(events.Event CurrentEvent);
    	
    	string GetMetadata();
    	string GetDescription();
	}
}
