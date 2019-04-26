using System;

namespace GameServer.events
{
	public class PacketRequestEvent : Event
	{
		protected network.Packet CurrentPacket;
		
		public PacketRequestEvent(network.Packet Packet)
		{
			CurrentPacket = Packet;
		}
		
		public network.Packet GetPacket()
		{
			return CurrentPacket;
		}
		
		public override string GetName()
		{
			return "Packet Request Event";
		}
		
		public override int GetCode()
		{
			return Events.Code_PacketRequestEvent;
		}
	}
}
