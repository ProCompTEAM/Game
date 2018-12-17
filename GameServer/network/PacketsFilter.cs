using System;
using GameServer.events;

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
				
				if(ev.GetPacket().GetPacketID() == Network.AUTH_PACKET && ev.GetPacket().GetStatus() == Packet.RESPONSE_STATUS_OK)
				{
					response.AuthPacketResponse packet = (response.AuthPacketResponse) ev.GetPacket();
					
					Server.CurrentLevel.JoinPlayer(new player.Player(packet.Token, packet.Login));
				}
			}
		}
	}
}
