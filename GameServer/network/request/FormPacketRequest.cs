using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class FormPacketRequest : network.Packet
	{
		public FormPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.FORM_PACKET;
		}
		
		public override string GetName()
		{
			return "Form Packet Request";
		}
	}
}
