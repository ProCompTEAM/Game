using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class ChatPacketRequest : network.Packet
	{
		public player.Player Player;
		
		public string Message = "";
		
		public ChatPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.CHAT_PACKET;
			
			Player = player.Tokenizer.GetFromToken(GetString("token"));
			
			if(GetString("msg") != null)
				Message = GetString("msg");
		}
		
		public override string GetName()
		{
			return "Chat Packet Request";
		}
		
		public player.Chat GetChat()
		{
			if(Player != null) return Player.CurrentChat;
			else return null;
		}
	}
}
