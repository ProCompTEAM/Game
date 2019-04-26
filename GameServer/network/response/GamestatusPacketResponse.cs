using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class GamestatusPacketResponse : Packet
	{		
		public player.Player Player = null;
		
		public GamestatusPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.GAMESTATUS_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			if(Player != null)
				SetData("mass", Player.Level.Mass.ToString());
			SetData("online", Server.GetOnlinePlayers().Length.ToString());
			
			if(Player != null)
			{
				foreach(string option in Player.GameOptions.Keys)
					SetData(option, Player.GameOptions[option]);
				SetData("inv", Player.Inventory.CalculateMass().ToString());
				
				Player.GameOptions.Clear();
			}
			else SetError(Errors.InvalidToken);
		}
			
		public override string GetName()
		{
			return "Game Status Packet Response";
		}
	}
}
