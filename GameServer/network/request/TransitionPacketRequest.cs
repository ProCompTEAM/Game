using System;
using GameServer.network;

namespace GameServer.network.request
{
	public class TransitionPacketRequest : network.Packet
	{	
		public TransitionPacketRequest(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.TRANSITION_PACKET;
			
		}
		
		public override string GetName()
		{
			return "Transition Packet Request";
		}
	}
}
