using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class AuthPacketRequest : network.Packet
	{
		public string Login;
		
		public AuthPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
			
			Login = GetData("uid");
		}
		
		public override string GetName()
		{
			return "Auth Packet Request";
		}
	}
}
