using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace GameServer.addon
{
	public static class Addons
	{
		public const string DIRECTORY = "addons";
		
		public static List<IModule> AddonsList = new List<IModule>();
		
		public static void LoadAll()
		{
			if(Server.Properties.GetProperty("use-addons") == utils.Config.SWITCH_ON)
			{
				Data.SendToLog("Loading addons for server....");
				
				if(!Directory.Exists(DIRECTORY))
					Directory.CreateDirectory(DIRECTORY);
				else
				{
			        string[] files = System.IO.Directory.GetFiles(DIRECTORY + "/", "*.dll");
			
			        foreach (string file in files)
			        {
				        Assembly assembly = Assembly.LoadFrom(file);
						
				        foreach(Type t in assembly.GetExportedTypes())
				        {
				        	if(typeof(IModule).IsAssignableFrom(t))
				        	{
				        		IModule addon = (IModule) Activator.CreateInstance(t);
				        		
				        		AddonsList.Add(addon);
				        		
				        		events.Events.CallEvent(new events.AddonLoadedEvent(addon));
				        		
				        		Data.SendToLog("Using " + Path.GetFileName(file) + " [meta] " + addon.GetMetadata());
				        		Data.SendToLog("Description: " + addon.GetDescription());
				        	}
				        }
			        }
				}
				
				//on loaded addons
				foreach(IModule addon in AddonsList) addon.OnLoaded();
			}
		}
		
		public static void UnloadAll()
		{
			foreach(IModule addon in AddonsList)
			{
				addon.OnDisabled();
				events.Events.CallEvent(new events.AddonDisabledEvent(addon));
			}
			
			AddonsList.Clear();
		}
		
		public static IModule GetAddon(string Metadata)
		{
			foreach(IModule addon in AddonsList)
			{
				if(addon.GetMetadata() == Metadata) return addon;
			}
			
			return null;
		}
	}
}
