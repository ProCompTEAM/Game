using System;

namespace GameServer.network
{
	public static class Network
	{
		public static PacketsFilter DefaultPacketsFilter = new PacketsFilter();
		
		//ID's of all packets : constants : list-table
		
		public const int EMPTY_PACKET = 0x00;
		public const int PING_PACKET  = 0x01;
		public const int NAMED_PACKET  = 0x02;
		public const int OS_PACKET = 0x03;
		public const int CUSTOM_PACKET = 0x04;
		public const int TOKEN_PACKET = 0x05;
		public const int AUTH_PACKET = 0x06;
		public const int REGISTER_PACKET = 0x07;
		public const int TRANSITION_PACKET = 0x08;
		
		//Network functions
		
		public static Packet HandleRequest(string RawRequestData, string Address)
		{
			Packet currentPacket = new Packet(RawRequestData, Address);
			
			int pid = currentPacket.GetPacketID();
			
			switch(pid)
			{
				case PING_PACKET: return new request.PingPacketRequest(RawRequestData, Address);
				case NAMED_PACKET: return new request.NamedPacketRequest(RawRequestData, Address);
				case OS_PACKET: return new request.OSPacketRequest(RawRequestData, Address);
				case CUSTOM_PACKET: return new request.CustomPacketRequest(RawRequestData, Address);
				case TOKEN_PACKET: return new request.TokenPacketRequest(RawRequestData, Address);
				case AUTH_PACKET: return new request.AuthPacketRequest(RawRequestData, Address);
				case REGISTER_PACKET: return new request.RegisterPacketRequest(RawRequestData, Address);
				case TRANSITION_PACKET: return new request.TransitionPacketRequest(RawRequestData, Address);
					
				default: return new Packet(RawRequestData, Address);
			}
		}
		
		public static Packet ConvertToResponse(Packet packet)
		{
			string address = Server.Properties.GetProperty("address");
			
			switch(packet.GetPacketID())
			{
				case PING_PACKET: return new response.PingPacketResponse(packet.TransformToRawData(), address);
				case NAMED_PACKET: return new response.NamedPacketResponse(packet.TransformToRawData(), address);
				case OS_PACKET: return new response.OSPacketResponse(packet.TransformToRawData(), address);
				case CUSTOM_PACKET: return new response.CustomPacketResponse(packet.TransformToRawData(), address);
				case TOKEN_PACKET: return new response.TokenPacketResponse(packet.TransformToRawData(), address);
				case AUTH_PACKET: return new response.AuthPacketResponse(packet.TransformToRawData(), address);
				case REGISTER_PACKET: return new response.RegisterPacketResponse(packet.TransformToRawData(), address);
				case TRANSITION_PACKET: return new response.TransitionPacketResponse(packet.TransformToRawData(), address);
					
				default: return new Packet(packet.TransformToRawData(), address);
			}
		}
	}
}
