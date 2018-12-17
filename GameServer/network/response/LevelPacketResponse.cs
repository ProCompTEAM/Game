using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class LevelPacketResponse : Packet
	{		
		public LevelPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.LEVEL_PACKET;
			
			InitializeAsResponse();
			
			SetData("raw", Server.CurrentLevel.LevelGenerator.ToString());
		}
			
		public override string GetName()
		{
			return "Level Packet Response";
		}
	}
}
