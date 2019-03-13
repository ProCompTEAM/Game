using System;
using GameServer.events;
using GameServer.player;

namespace GameServer.network
{
	public class PacketsFilter : IEventListener
	{
		public PacketsFilter()
		{
			Events.AddListener(this);
		}
		
		public void Handler(Event CurrentEvent)
		{
			if(CurrentEvent.GetCode() == Events.Code_PacketRequestEvent)
			{
				PacketRequestEvent ev = (PacketRequestEvent) CurrentEvent;
				
				if(ev.GetPacket().GetPacketID() == Network.INVENTORY_PACKET && ev.GetPacket().GetData("set") != null)
				{
					string[] data = ev.GetPacket().GetData("set").Split(',');
					
					Tile t = new Tile(Convert.ToInt32(data[2]), Convert.ToInt32(data[1]), Convert.ToInt32(data[0]));
					
					Player player = ((request.InventoryPacketRequest)  ev.GetPacket()).Player;
					
					if(player != null)
					{
						if(player.Action(PlayerActionEvent.Actions.Tileset, t))
						{
							player.Level.SetTile(t);
						
							player.Inventory.TakeItem(Convert.ToInt32(data[2]), 1);
							
							Server.Log("Level {0} updated...", player.Level.ToString());
						}
					}
				}
				
				if(ev.GetPacket().GetPacketID() == Network.CHAT_PACKET && !ev.Cancelled)
				{
					request.ChatPacketRequest packet = (request.ChatPacketRequest) ev.GetPacket();
					
					if(packet.Message.Length > 0)
						packet.Player.Chat(packet.Message);
				}
			}
			
			if(CurrentEvent.GetCode() == Events.Code_PacketResponseEvent)
			{
				PacketResponseEvent ev = (PacketResponseEvent) CurrentEvent;
				
				switch(ev.GetPacket().GetPacketID())
				{
					case Network.AUTH_PACKET:
						if(ev.GetPacket().GetStatus() == Packet.RESPONSE_STATUS_OK)
						{
							response.AuthPacketResponse packet = (response.AuthPacketResponse) ev.GetPacket();
							
							if(player.control.Ban.IsBanned(packet.Login))
							{
								packet.SetError(Errors.PlayerBanned);
								
								Data.SendToLog("Name banned! Closed: " + packet.Login, Data.Log_Warning);
								
								return;
							}
							
							if(Array.IndexOf(Server.GetOnlinePlayersStr(), packet.Login) < 0)
								Server.JoinPlayer(new Player(packet.Token, packet.Login, packet.Address));
							else packet.SetError(Errors.PlayerAlreadyExists);
						}
					break;
					
					case Network.GAMESTATUS_PACKET:
						if(ev.GetPacket().GetStatus() == Packet.RESPONSE_STATUS_OK)
						{
							response.GamestatusPacketResponse packet = (response.GamestatusPacketResponse) ev.GetPacket();
							if(packet.Player != null) packet.Player.Connection.NewStamp(DateTime.Now);
						}
					break;
				}
			}
		}
	}
}
