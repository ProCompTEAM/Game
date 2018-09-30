using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class AuthPacketRequest : network.Packet
	{
		public AuthPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
		}
		
		public override string GetName()
		{
			return "Auth Packet Request";
		}
	}
}
