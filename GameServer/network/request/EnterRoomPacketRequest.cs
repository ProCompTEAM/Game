using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class EnterRoomPacketRequest : network.Packet
	{	
		public EnterRoomPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.ENTERROOM_PACKET;
			
		}
		
		public override string GetName()
		{
			return "EnterRoom Packet Request";
		}
	}
}
