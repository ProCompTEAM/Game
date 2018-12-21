﻿using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class AuthPacketResponse : network.Packet
	{		
		public string Token;
		public string Login;
		
		public AuthPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
			
			InitializeAsResponse();
			
			Token = utils.TextUtil.GenerateToken();
			Login = GetData("uid");
			
			if(Login == null) SetError(Errors.InvalidData);
			else SetData("token", Token);
		}
			
		public override string GetName()
		{
			return "Auth Packet Response";
		}
	}
}
