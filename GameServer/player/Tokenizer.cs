using System;

namespace GameServer.player
{
	public static class Tokenizer
	{
		public static Player GetFromToken(string token)
		{
			foreach(Player p in Server.CurrentLevel.GetOnlinePlayers())
			{
				if(p.Token == token) return p;
			}
			
			return null;
		}
	}
}
