using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class RegisterPacketRequest : network.Packet
	{
		public RegisterPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REGISTER_PACKET;
		}
		
		public override string GetName()
		{
			return "Register Packet Request";
		}
	}
}
