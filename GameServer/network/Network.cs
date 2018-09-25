using System;

namespace GameServer.network
{
	public static class Network
	{
		//ID's of all packets : constants : list-table
		
		public const int EMPTY_PACKET = 0x00;
		public const int PING_PACKET  = 0x01;
		
		//Network functions
		
		public static Packet HandleRequest(string RawRequestData)
		{
			Packet currentPacket = new Packet(RawRequestData);
			
			int pid = currentPacket.GetPacketID();
			
			switch(pid)
			{
				case PING_PACKET: return new request.PingPacketRequest(RawRequestData);
					
				default: return new Packet(RawRequestData);
			}
		}
		
		public static Packet ConvertToResponse(Packet packet)
		{
			switch(packet.GetPacketID())
			{
				case PING_PACKET: return new response.PingPacketResponse(packet.TransformToRawData());
					
				default: return new Packet(packet.TransformToRawData());
			}
		}
	}
}
