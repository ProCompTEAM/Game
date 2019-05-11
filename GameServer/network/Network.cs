using System;

namespace GameServer.network
{
	public static class Network
	{
		//ID's of all packets : constants : list-table
		
		public const int EMPTY_PACKET = 0x00;
		public const int PING_PACKET  = 0x01;
		public const int NAMED_PACKET  = 0x02;
		public const int OS_PACKET = 0x03;
		public const int CUSTOM_PACKET = 0x04;
		public const int AUTH_PACKET = 0x05;
		public const int LEVEL_PACKET = 0x06;
		public const int GAMESTATUS_PACKET = 0x07;
		public const int CHAT_PACKET = 0x08;
		public const int INVENTORY_PACKET = 0x09;
		public const int FORM_PACKET = 0x0A;
		public const int SETTINGS_PACKET = 0x0B;
		
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
				case AUTH_PACKET: return new request.AuthPacketRequest(RawRequestData, Address);
				case LEVEL_PACKET: return new request.LevelPacketRequest(RawRequestData, Address);
				case GAMESTATUS_PACKET: return new request.GamestatusPacketRequest(RawRequestData, Address);
				case CHAT_PACKET: return new request.ChatPacketRequest(RawRequestData, Address);
				case INVENTORY_PACKET: return new request.InventoryPacketRequest(RawRequestData, Address);
				case FORM_PACKET: return new request.FormPacketRequest(RawRequestData, Address);
				case SETTINGS_PACKET: return new request.SettingsPacketRequest(RawRequestData, Address);
				
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
				case AUTH_PACKET: return new response.AuthPacketResponse(packet.TransformToRawData(), address);
				case LEVEL_PACKET: return new response.LevelPacketResponse(packet.TransformToRawData(), address);
				case GAMESTATUS_PACKET: return new response.GamestatusPacketResponse(packet.TransformToRawData(), address);
				case CHAT_PACKET: return new response.ChatPacketResponse(packet.TransformToRawData(), address);
				case INVENTORY_PACKET: return new response.InventoryPacketResponse(packet.TransformToRawData(), address);
				case FORM_PACKET: return new response.FormPacketResponse(packet.TransformToRawData(), address);
				case SETTINGS_PACKET: return new response.SettingsPacketResponse(packet.TransformToRawData(), address);
				
				default: return new Packet(packet.TransformToRawData(), address);
			}
		}
	}
}
