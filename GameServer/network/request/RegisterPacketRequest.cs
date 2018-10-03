using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class RegisterPacketRequest : network.Packet
	{
		public static string Token;
		public static string Login;
		public static string Password;
		public static string Mail;
		
		public RegisterPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REGISTER_PACKET;
			
			Token = GetData("token");
			Login = GetData("uid");
			Password = GetData("pass");
			Mail = GetData("mail");
		}
		
		public override string GetName()
		{
			return "Register Packet Request";
		}
	}
}
