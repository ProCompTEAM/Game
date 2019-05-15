using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class InventoryPacketRequest : network.Packet
	{
		public player.Player Player = null;
		
		public InventoryPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.INVENTORY_PACKET;
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
		}
		
		public override string GetName()
		{
			return "Inventory Packet Request";
		}
	}
}
