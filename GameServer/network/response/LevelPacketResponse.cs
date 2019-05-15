using System;
using GameServer.network;
using GameServer.ui.form;

namespace GameServer.network.response
{
	public class LevelPacketResponse : Packet
	{
		public player.Player Player = null;
		
		public LevelPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.LEVEL_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
			
			if(Player != null){
				SetData("raw", Player.Level.CompressedData);
			}
			
		}
			
		public override string GetName()
		{
			return "Level Packet Response";
		}
	}
}
