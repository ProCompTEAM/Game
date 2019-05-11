﻿using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class SettingsPacketResponse : Packet
	{
		public player.Player Player = null;
		public readonly int ActivatedElement;
		
		public SettingsPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.SETTINGS_PACKET;
			
			InitializeAsResponse();
			
			Player = player.Tokenizer.GetFromToken(GetData("token"));
			
			ActivatedElement = Convert.ToInt32(GetData("act"));
		}
			
		public override string GetName()
		{
			return "Settings Packet Response";
		}
	}
}
