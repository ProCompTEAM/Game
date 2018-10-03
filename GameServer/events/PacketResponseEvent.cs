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
		
		public override string GetName()
		{
			return "Packet Response Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_PacketResponseEvent;
		}
	}
}
