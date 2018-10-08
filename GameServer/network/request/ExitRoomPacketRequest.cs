using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class ExitRoomPacketRequest : network.Packet
	{	
		public ExitRoomPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.EXITROOM_PACKET;
			
		}
		
		public override string GetName()
		{
			return "ExitRoom Packet Request";
		}
	}
}
