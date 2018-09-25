using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class PingPacketResponse : network.Packet
	{
		public PingPacketResponse(string raw) : base(raw)
		{
			Id = Network.PING_PACKET;
			
			set("status", "ok");
		}
		
		public string GetStatus()
		{
			return get("status");
		}
		
		public override string GetName()
		{
			return "Ping Packet Response";
		}
	}
}
