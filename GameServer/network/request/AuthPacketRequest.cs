using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class AuthPacketRequest : network.Packet
	{
		public static string Token;
		public static string Login;
		public static string Password;
		
		public AuthPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
			
			Token = GetData("token");
			Login = GetData("uid");
			Password = GetData("pass");
		}
		
		public override string GetName()
		{
			return "Auth Packet Request";
		}
	}
}
