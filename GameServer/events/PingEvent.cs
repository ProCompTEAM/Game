using System;

namespace GameServer.events
{
	public class PingEvent : Event
	{
		protected network.request.PingPacketRequest PingPacket;
		
		public PingEvent(network.request.PingPacketRequest Packet) 
		{
			PingPacket = Packet;
		}
		
		public network.request.PingPacketRequest GetPingPacket()
		{
			return PingPacket;
		}
		
		public string GetAddress()
		{
			return PingPacket.Address;
		}
		
		public override int GetCode()
		{
			return Events.Code_PingEvent;
		}
	}
}
