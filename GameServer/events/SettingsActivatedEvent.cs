using System;

namespace GameServer.events
{
	public class SettingsActivatedEvent : Event
	{
		public player.Player Player;
		
		public enum Settings
		{
			Home,
			Music,
			Transport,
			Time,
			Whether,
			Context
		}
		
		public int Activated;
		
		public string Metadata;
		
		public SettingsActivatedEvent(player.Player player, int settingsAct)
		{
			Player = player;
			Activated = settingsAct;
			Metadata = "";
		}
		
		public override string GetName()
		{
			return "Settings Activated Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_SettingsActivatedEvent;
		}
	}
}
