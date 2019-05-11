using System;

using GameServer.ui.form;

namespace GameServer.events
{
	public class ActivatedFormEvent : Event
	{
		public player.Player Player;
		
		public ActivatedForm Form;
		
		public string Metadata;
		
		public ActivatedFormEvent(player.Player player, ActivatedForm form)
		{
			Player = player;
			Form = form;
			Metadata = "";
		}
		
		public override string GetName()
		{
			return "Activated Form Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_ActivatedFormEvent;
		}
	}
}
