using System;

namespace GameServer.events
{
	public class PlayerActionEvent : Event
	{
		public player.Player Player;
		
		public enum Actions
		{
			Born,
			Closed,
			Chat,
			Custom,
			Tileset,
			Selection
		}
		
		public Actions Action;
		
		public object[] Data;
		
		public string Metadata;
		
		public PlayerActionEvent(player.Player player, Actions act, params object[] args)
		{
			Player = player;
			Action = act;
			Data = args;
			Metadata = "";
		}
		
		public override string GetName()
		{
			return "Player Action Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_PlayerActionEvent;
		}
	}
}
