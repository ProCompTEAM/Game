using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class SettingsPacketRequest : network.Packet
	{
		player.Player Player;
		
		public SettingsPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.SETTINGS_PACKET;
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
		}
		
		public override string GetName()
		{
			return "Settings Packet Request";
		}
	}
}
