using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class LevelPacketRequest : network.Packet
	{
		public LevelPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.LEVEL_PACKET;
		}
		
		public override string GetName()
		{
			return "Level Packet Request";
		}
	}
}
