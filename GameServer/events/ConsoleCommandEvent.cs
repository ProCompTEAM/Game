﻿using System;

namespace GameServer.events
{
	public class ConsoleCommandEvent : Event
	{
		public string[] Command;
		
		public ConsoleCommandEvent(string[] CommandArgs)
		{
			Command = CommandArgs;
		}
		
		public override int GetCode()
		{
			return Events.Code_ConsoleCommandEvent;
		}
		
		public override string GetName()
		{
			return "Console Command Event";
		}
	}
}