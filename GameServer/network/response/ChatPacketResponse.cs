using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class ChatPacketResponse : Packet
	{		
		public player.Player Player = null;
		
		public ChatPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.CHAT_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			if(GetChat() != null)
				SetData("raw", string.Join(";", GetChat().GetMessages()));
			else SetError(Errors.InvalidToken);
		}
			
		public override string GetName()
		{
			return "Chat Packet Response";
		}
		
		public player.Chat GetChat()
		{
			if(Player != null) return Player.CurrentChat;
			else return null;
		}
	}
}
