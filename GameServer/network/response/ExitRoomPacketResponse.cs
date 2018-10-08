using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class EnterRoomPacketResponse : network.Packet
	{
		public static string Token;
		public static string RoomID;
		public EnterRoomPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.ENTERROOM_PACKET;
			
			InitializeAsResponse();
			
			Token = GetData("token");
			RoomID = GetData("room");
			if(Token == null || RoomID == null)
				SetError("lang.error.packet.wrongroom");
		}
		
		public override string GetName()
		{
			return "ExitRoom Packet Response";
		}
		

	}
}