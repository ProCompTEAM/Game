using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class OSPacketResponse : network.Packet
	{
		public OSPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.OS_PACKET;
			
			SetData("os", GetOS());
			InitializeAsResponse();
		}
		
		public override string GetName()
		{
			return "OS Packet Response";
		}
		
		public static string GetOS()
		{
			return Environment.OSVersion.ToString();
		}
	}
}
