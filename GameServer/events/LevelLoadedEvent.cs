using System;

namespace GameServer.events
{
	public class LevelLoadedEvent : Event
	{
		Level LoadedLevel;
		generator.Generator Generator;
		
		public LevelLoadedEvent(Level level)
		{
			LoadedLevel = level;
			Generator = level.LevelGenerator;
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
