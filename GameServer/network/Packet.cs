using System;
using System.Collections.Generic;

namespace GameServer.network
{
	public class Packet
	{
		public List<string> RawDataContanier;
		
		protected int Id;
		
		public Packet(string RawData)
		{
			RawDataContanier = new List<string>(RawData.Split('+'));
			
			try{ Id = Convert.ToInt32(get("p")); }
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
		
		public string get(string Option)
		{
			foreach (string Item in RawDataContanier)
			{
			 	string[] block = Item.Split('=');
			 	if(Option == block[0]) return block[1];
			 }
			
			return null;
		}
		
		public void set(string Option, string Value)
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
	}
}
