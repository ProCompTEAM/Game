using System;
using GameServer.network;
using System.Collections.Generic;

namespace GameServer.network.response
{
	public class RegisterPacketResponse : network.Packet
	{
		public static string Login;
		public static string Mail;
		public static string Password;
		public List<string> RawData;
		
		public RegisterPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.REGISTER_PACKET;
			
			if(GetStatus() == null)
			{
				ConvertRawToData(Raw);
			}
		}
			
		public override string GetName()
		{
			return "Register Packet Response";
		}
		
		public void ConvertRawToData(string Raw)
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
					else if(num == 3 && Item.Substring(0, Item.IndexOf('=')) == "mail")
						Mail = Item.Substring(Item.IndexOf('=') + 1);
					
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
		
		public string GetMail()
		{
			return Mail;
		}
	}
}