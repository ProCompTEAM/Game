using System;
using GameServer.events;
using GameServer.network;
using GameServer.network.request;
using GameServer.network.response;
using GameServer.player;

namespace GameServer
{
	public class Activity : IEventListener
	{
		public Activity()
		{
			Events.AddListener(this);
		}
		
		public void Handler(Event CurrentEvent)
		{
			if(CurrentEvent.GetCode() == Events.Code_PacketRequestEvent)
			{
				PacketRequestEvent ev = (PacketRequestEvent) CurrentEvent;
				
				if(ev.GetPacket().GetPacketID() == Network.INVENTORY_PACKET)
				{
					Player player = ((InventoryPacketRequest)  ev.GetPacket()).Player;
					
					if(ev.GetPacket().GetData("ox") != null && player != null)
					{
						ushort ox = Convert.ToUInt16(ev.GetPacket().GetData("ox"));
						ushort oy = Convert.ToUInt16(ev.GetPacket().GetData("oy"));
						ushort x = Convert.ToUInt16(ev.GetPacket().GetData("x"));
						ushort y = Convert.ToUInt16(ev.GetPacket().GetData("y"));
						int id = Convert.ToInt32(ev.GetPacket().GetData("id"));
						
						player.Click(ox, oy, x, y, id);
					}
				}
				
				if(ev.GetPacket().GetPacketID() == Network.CHAT_PACKET && !ev.Cancelled)
				{
					ChatPacketRequest packet = (ChatPacketRequest) ev.GetPacket();
					
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
							AuthPacketResponse packet = (AuthPacketResponse) ev.GetPacket();
							
							if(player.control.Ban.IsBanned(packet.Login))
							{
								packet.SetError(Errors.PlayerBanned);
								
								Data.SendToLog("Name banned! Closed: " + packet.Login, Data.Log_Warning);
								
								return;
							}
							
							if(Array.IndexOf(Server.GetOnlinePlayersStr(), packet.Login) < 0)
								Server.JoinPlayer(new Player(packet.Token, packet.Login, 
								                             Server.GetLevel(Server.Properties.GetProperty("default-level-name")), packet.Address));
							else packet.SetError(Errors.PlayerAlreadyExists);
						}
					break;
					
					case Network.GAMESTATUS_PACKET:
						if(ev.GetPacket().GetStatus() == Packet.RESPONSE_STATUS_OK)
						{
							GamestatusPacketResponse packet = (GamestatusPacketResponse) ev.GetPacket();
							if(packet.Player != null) packet.Player.Connection.NewStamp(DateTime.Now);
						}
					break;
				}
			}
		}
	}
}
