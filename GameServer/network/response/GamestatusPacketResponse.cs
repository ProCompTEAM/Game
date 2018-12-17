using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class GamestatusPacketResponse : Packet
	{		
		player.Player Player;
		
		public GamestatusPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.GAMESTATUS_PACKET;
			
			InitializeAsResponse();
			
			SetData("mass", Server.CurrentLevel.LevelGenerator.CalculateMass().ToString());
			SetData("online", Server.CurrentLevel.GetOnlinePlayers().Length.ToString());
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			if(Player != null)
				foreach(string option in Player.GameOptions.Keys)
					SetData(option, Player.GameOptions[option]);
		}
			
		public override string GetName()
		{
			return "Game Status Packet Response";
		}
	}
}
