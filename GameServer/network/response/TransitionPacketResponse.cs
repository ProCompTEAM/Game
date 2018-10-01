using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class TransitionPacketResponse : network.Packet
	{		
		public TransitionPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.TRANSITION_PACKET;
			
			InitializeAsResponse();
			
			
		}
		
		public override string GetName()
		{
			return "Transition Packet Response";
		}
		
		public void Transit(string TransitionAddress)
		{
			SetData("transit", TransitionAddress);
		}
	}
}
