using System;

namespace GameServer.events
{
	public class LevelLoadedEvent : Event
	{
		level.Level LoadedLevel;
		level.generator.Generator Generator;
		
		public LevelLoadedEvent(level.Level level)
		{
			LoadedLevel = level;
			Generator = level.Generator;
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
