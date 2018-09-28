using System;

namespace GameServer.player
{
	public class Player
	{
		protected string Id;
		protected string Token;
		
		public Player(string CurrentToken, string ProfileId)
		{
			Token = CurrentToken;
			Id = ProfileId;
		}
		
		public string GetName()
		{
			return Id;
		}
		
		public string GetToken()
		{
			return Token;
		}
		
		
	}
}
