using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class PingPacketRequest : network.Packet
	{
		public PingPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.PING_PACKET;
		}
		
		public override string GetName()
		{
			return "Ping Packet Request";
		}
	}
}
