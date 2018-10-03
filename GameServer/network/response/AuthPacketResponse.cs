using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class AuthPacketResponse : network.Packet
	{		
		public static string Token;
		public static string Login;
		public static string Password;
		
		public AuthPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
			
			InitializeAsResponse();
			
			Token = GetData("token");
			Login = GetData("uid");
			Password = GetData("pass");
			if(Token == null || Login == null || Password == null)
				SetError("lang.error.packet.baddata");
		}
			
		public override string GetName()
		{
			return "Auth Packet Response";
		}
	}
}
