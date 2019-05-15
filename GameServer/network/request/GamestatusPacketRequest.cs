using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class GamestatusPacketRequest : network.Packet
	{
		player.Player Player;
		
		public GamestatusPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.GAMESTATUS_PACKET;
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
		}
		
		public override string GetName()
		{
			return "Game Status Packet Request";
		}
	}
}
