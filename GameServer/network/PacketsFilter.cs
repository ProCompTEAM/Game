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
				
				if(ev.GetPacket().GetPacketID() == Network.GAMESTATUS_PACKET && ev.GetPacket().GetData("tile-set") != null)
				{
					string[] data = ev.GetPacket().GetData("tile-set").Split(';');
					
					Server.CurrentLevel.SetTile(
						new Tile(
							Convert.ToInt32(data[2]), 
							Convert.ToInt32(data[0]),
							Convert.ToInt32(data[1]))
					);
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
							
							if(Array.IndexOf(Server.CurrentLevel.GetOnlinePlayersStr(), packet.Login) < 0)
								Server.CurrentLevel.JoinPlayer(new Player(packet.Token, packet.Login, packet.Address));
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
