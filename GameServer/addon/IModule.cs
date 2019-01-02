using System;

namespace GameServer.addon
{
	public interface IModule
	{
    	void OnLoaded();
    	void OnDisabled();
    	
    	string GetMetadata();
    	string GetDescription();
	}
}
