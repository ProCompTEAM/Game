using System;
using GameServer.network;
using System.Collections.Generic;

namespace GameServer.network.response
{
	public class RedirectPacketResponse : network.Packet
	{		
		public RedirectPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REGISTER_PACKET;
			
			if(GetStatus() == null)
			{
				if(Raw.Substring(3) == "")
				{
					SetStatus(RESPONSE_STATUS_NO);
				}
				else
				{
					Redirect(Raw.Substring(8));
					SetStatus(RESPONSE_STATUS_OK);
				}
			}
		}
		
		public override string GetName()
		{
			return "Redirect Packet Response";
		}
		
		public void Redirect(string Url)
		{
			Server.Response.Redirect(Url);
		}
	}
}