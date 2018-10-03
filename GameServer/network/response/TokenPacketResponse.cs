using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class TokenPacketResponse : network.Packet
	{	
		public string Token;
		
		public TokenPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.TOKEN_PACKET;
			
			InitializeAsResponse();
			
			Token = utils.TextUtil.GenerateToken();
			
			SetData("token", Token);
		}
			
		public override string GetName()
		{
			return "Token Packet Response";
		}
		
		
	}
}
