using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class OSPacketRequest : network.Packet
	{
		public OSPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.OS_PACKET;
		}
		
		public override string GetName()
		{
			return "OS Packet Request";
		}
	}
}
