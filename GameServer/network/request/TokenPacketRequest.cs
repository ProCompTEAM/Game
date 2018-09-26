using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class TokenPacketRequest : network.Packet
	{
		public TokenPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.TOKEN_PACKET;
		}
		
		public override string GetName()
		{
			return "Token Packet Request";
		}
	}
}
