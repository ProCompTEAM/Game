using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class RegisterPacketResponse : network.Packet
	{		
		public static string Token;
		public static string Login;
		public static string Password;
		public static string Mail;
		
		public RegisterPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REGISTER_PACKET;
			
			InitializeAsResponse();
			
			Token = GetData("token");
			Login = GetData("uid");
			Password = GetData("pass");
			Mail = GetData("mail");
			if(Token == null || Login == null || Password == null || Mail == null)
				SetError("lang.error.packet.baddata");
		}
			
		public override string GetName()
		{
			return "Register Packet Response";
		}
	}
}
