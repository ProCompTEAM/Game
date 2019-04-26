using System;

namespace GameServer.events
{
	public class LevelLoadedEvent : Event
	{
		level.Level LoadedLevel;
		
		public LevelLoadedEvent(level.Level level)
		{
			LoadedLevel = level;
		}
		
		public override string GetName()
		{
			return "Level Loaded Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_LevelLoadedEvent;
		}
	}
}
