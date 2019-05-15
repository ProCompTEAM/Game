using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class InventoryPacketResponse : network.Packet
	{
		public player.Player Player = null;
		
		public InventoryPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.INVENTORY_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
			
			if(Player != null) SetData("items", Player.Inventory.ToString());
		}
		
		public override string GetName()
		{
			return "Inventory Packet Response";
		}
	}
}
