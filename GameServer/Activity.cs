using System;
using System.Text.RegularExpressions;
using GameServer.events;
using GameServer.network;
using GameServer.network.request;
using GameServer.network.response;
using GameServer.player;
using GameServer.utils;

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
					
					if(ev.GetPacket().GetInt("ox") != -1 && player != null)
					{
						ushort ox = ev.GetPacket().GetUShort("ox");
						ushort oy = ev.GetPacket().GetUShort("oy");
						ushort x = ev.GetPacket().GetUShort("x");
						ushort y = ev.GetPacket().GetUShort("y");
						int id = ev.GetPacket().GetInt("id");
						
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
							
							Regex r = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_]{1,20}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
							if(!r.Match(packet.Login).Success)
							{
								packet.SetError(Errors.InvalidName);
								
								Data.SendToLog("Name invalid! Closed: " + packet.Login, Data.Log_Warning);
								
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
					
					case Network.SETTINGS_PACKET:
						if(ev.GetPacket().GetStatus() == Packet.RESPONSE_STATUS_OK)
						{
							SettingsPacketResponse packet = (SettingsPacketResponse) ev.GetPacket();
							
							Events.CallEvent(new SettingsActivatedEvent(packet.Player, packet.ActivatedElement));
						}
					break;
				}
			}
		}
	}
}
