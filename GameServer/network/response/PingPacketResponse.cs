﻿using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class PingPacketResponse : network.Packet
	{
		public PingPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.PING_PACKET;
			
			InitializeAsResponse();
			
			events.Events.CallEvent(new events.PingEvent(this));
		}
		
		public override string GetName()
		{
			return "Ping Packet Response";
		}
	}
}
