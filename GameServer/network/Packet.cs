using System;
using System.Collections.Generic;

namespace GameServer.network
{
	public class Packet
	{
		public List<string> RawDataContanier;
		
		protected int Id;
		
		public string Address;
		
		public const string RESPONSE_STATUS_OK = "ok";
		public const string RESPONSE_STATUS_NO = "no";
		
		public Packet(string RawData, string BackAddress)
		{
			RawDataContanier = new List<string>(RawData.Split('+'));
			
			Address = BackAddress;
			
			try{ Id = Convert.ToInt32(GetData("p")); }
			catch { Id = Network.EMPTY_PACKET; }
		}
		
		public int GetPacketID()
		{
			return Id;
		}
		
		public string TransformToRawData()
		{
			string raw = "";
			
			foreach (string Item in RawDataContanier)
				raw += Item + "+";
			
			return raw.Substring(0, raw.Length - 1);
		}
		
		public string GetData(string Option)
		{
			foreach (string Item in RawDataContanier)
			{
			 	string[] block = Item.Split('=');
			 	if(Option == block[0]) return block[1];
			 }
			
			return null;
		}
		
		public void SetData(string Option, string Value)
		{
			foreach(string line in RawDataContanier)
			{
				string[] block = line.Split('=');
			 	
			 	if(Option == block[0])
			 	{
			 		RawDataContanier.Remove(line);
			 		RawDataContanier.Add(Option + "=" + Value);
			 		
			 		return;
			 	}
			}
			
			RawDataContanier.Add(Option + "=" + Value);
		}
		
		public virtual string GetName()
		{
			return "Empty Packet";
		}
		
		public void InitializeAsResponse()
		{
			SetStatus(RESPONSE_STATUS_OK);
		}
		
		public string GetStatus()
		{
			return GetData("status");
		}
		
		public void SetStatus(string Status)
		{
			SetData("status", Status);
		}
	}
}
