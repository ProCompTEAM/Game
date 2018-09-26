using System;

namespace GameServer.events
{
	public class PingEvent : Event
	{
		protected network.response.PingPacketResponse PingPacket;
		
		public PingEvent(network.response.PingPacketResponse Packet) 
		{
			PingPacket = Packet;
		}
		
		public network.response.PingPacketResponse GetPingPacket()
		{
			return PingPacket;
		}
		
		public string GetAddress()
		{
			return PingPacket.Address;
		}
		
		public override string GetName()
		{
			return "Ping Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_PingEvent;
		}
	}
}
