using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class NamedPacketResponse : network.Packet
	{
		public NamedPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.NAMED_PACKET;
			
			InitializeAsResponse();
			
			SetData("name", GetServerName());
		}
			
		public override string GetName()
		{
			return "Named Packet Response";
		}
		
		public string GetServerName()
		{
			return Server.Properties.GetProperty("server-name");
		}
	}
}
