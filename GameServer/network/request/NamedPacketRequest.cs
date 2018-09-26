using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class NamedPacketRequest : network.Packet
	{
		public NamedPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.NAMED_PACKET;
		}
		
		public override string GetName()
		{
			return "Named Packet Request";
		}
	}
}
