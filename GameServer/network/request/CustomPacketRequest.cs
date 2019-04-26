using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class CustomPacketRequest : Packet
	{
		public CustomPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.CUSTOM_PACKET;
		}
		
		public override string GetName()
		{
			return "Custom Packet Request";
		}
	}
}
