using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class LevelPacketResponse : Packet
	{
		public player.Player Player = null;
		
		public LevelPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.LEVEL_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			if(Player != null)
				SetData("raw", utils.LevelCompressor.Compress(Player.Level.Generator.ToString()));
		}
			
		public override string GetName()
		{
			return "Level Packet Response";
		}
	}
}
