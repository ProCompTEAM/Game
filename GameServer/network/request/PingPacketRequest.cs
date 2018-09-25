using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class PingPacketRequest : network.Packet
	{
		public PingPacketRequest(string raw) : base(raw)
		{
			Id = Network.PING_PACKET;
		}
		
		public override string GetName()
		{
			return "Ping Packet Request";
		}
	}
}
