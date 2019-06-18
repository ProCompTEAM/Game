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
			
			Id = GetInt("p");
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
		
		public string GetString(string Option)
		{
			foreach (string Item in RawDataContanier)
			{
			 	string[] block = Item.Split('=');
			 	if(Option == block[0]) return block[1];
			 }
			
			return null;
		}
		
		public int GetInt(string Option)
		{
			try
			{
				return Convert.ToInt32(GetString(Option));
			}
			catch
			{
				Data.SendToLog("Invalid option in " + GetName() + " : " + Option + " is not integer!", Data.Log_Error, ConsoleColor.Red);
				return -1;
			}
		}
		
		public ushort GetUShort(string Option)
		{
			try
			{
				return Convert.ToUInt16(GetString(Option));
			}
			catch
			{
				Data.SendToLog("Invalid option in " + GetName() + " : " + Option + " is not unsigned short integer!", Data.Log_Error, ConsoleColor.Red);
				return ushort.MaxValue;
			}
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
			return GetString("status");
		}
		
		public void SetStatus(string Status)
		{
			SetData("status", Status);
		}
		
		public void SetError(string ErrorMessage)
		{
			SetStatus(RESPONSE_STATUS_NO);
			SetData("error", ErrorMessage);
		}
	}
}
