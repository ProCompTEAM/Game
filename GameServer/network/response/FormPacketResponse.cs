using System;
using GameServer.events;
using GameServer.network;
using GameServer.ui.form;

namespace GameServer.network.response
{
	public class FormPacketResponse : Packet
	{
		public player.Player Player = null;
		
		public FormPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.FORM_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			if(Player != null && GetData("af") != null) 
				Events.CallEvent(new ActivatedFormEvent(Player, ActivatedForm.Decode(GetData("af"))));
			
			if(Player != null && Player.Forms.Count > 0) SetData("raw", Player.Forms.Dequeue().ToString());
		}
			
		public override string GetName()
		{
			return "Form Packet Response";
		}
	}
}
