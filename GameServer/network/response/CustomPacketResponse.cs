using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class CustomPacketResponse : Packet
	{
		public CustomPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.CUSTOM_PACKET;
			
			InitializeAsResponse();
		}
			
		public override string GetName()
		{
			return "Custom Packet Response";
		}
		
		public void SendMessage(string Message)
		{
			SetData("out-message", Message);
		}
		
		
		public void SendError(string Message)
		{
			SetData("out-error", Message);
		}
		
		
		public void SendNotice(string Message)
		{
			SetData("out-notice", Message);
		}
		
		public void SendTip(string Message)
		{
			SetData("out-tip", Message);
		}
	}
}
