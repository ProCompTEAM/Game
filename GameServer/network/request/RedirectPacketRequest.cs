using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class RedirectPacketRequest : network.Packet
	{
		public RedirectPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REDIRECT_PACKET;
		}
		
		public override string GetName()
		{
			return "Redirect Packet Request";
		}
	}
}
