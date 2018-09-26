using System;

namespace GameServer.events
{
	public class PacketResponseEvent : Event
	{
		protected network.Packet CurrentPacket;
		
		public PacketResponseEvent(network.Packet Packet)
		{
			CurrentPacket = Packet;
		}
		
		public network.Packet GetPacket()
		{
			return CurrentPacket;
		}
	}
}
