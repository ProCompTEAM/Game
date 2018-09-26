using System;

namespace GameServer.network
{
	public static class Network
	{
		//ID's of all packets : constants : list-table
		
		public const int EMPTY_PACKET = 0x00;
		public const int PING_PACKET  = 0x01;
		
		//Network functions
		
		public static Packet HandleRequest(string RawRequestData, string Address)
		{
			Packet currentPacket = new Packet(RawRequestData, Address);
			
			int pid = currentPacket.GetPacketID();
			
			switch(pid)
			{
				case PING_PACKET: return new request.PingPacketRequest(RawRequestData, Address);
					
				default: return new Packet(RawRequestData, Address);
			}
		}
		
		public static Packet ConvertToResponse(Packet packet)
		{
			string address = Server.Properties.GetProperty("address");
			
			switch(packet.GetPacketID())
			{
				case PING_PACKET: return new response.PingPacketResponse(packet.TransformToRawData(), address);
					
				default: return new Packet(packet.TransformToRawData(), address);
			}
		}
	}
}
