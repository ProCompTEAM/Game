using System;
using GameServer.network;
using System.Collections.Generic;

namespace GameServer.network.response
{
	public class AuthPacketResponse : network.Packet
	{
		public static string Login;
		public static string Mail;
		public static string Password;
		public List<string> RawData;
		
		public AuthPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.AUTH_PACKET;
			
			if(GetStatus() == null)
			{
				ConvertToRawData(Raw);
			}
		}
			
		public override string GetName()
		{
			return "Auth Packet Response";
		}
		
		public void ConvertToRawData(string Raw)
		{
			if(Raw.Substring(3) == "")
			{
				SetStatus(RESPONSE_STATUS_NO);
			}
			else
			{
				int num = 0;
				
				RawData = new List<string>(Raw.Split('+'));
				
				foreach(string Item in RawData)
				{
					if(num == 1 && Item.Substring(0, Item.IndexOf('=')) == "login")
						Login = Item.Substring(Item.IndexOf('=') + 1);
					else if(num == 2 && Item.Substring(0, Item.IndexOf('=')) == "password")
						Password = Item.Substring(Item.IndexOf('=') + 1);
					
					num++;
				}
			}
		}
		
		public string GetLogin()
		{
			return Login;
		}
		
		public string GetPassword()
		{
			return Password;
		}
	}
}