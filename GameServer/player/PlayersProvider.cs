using System;
using System.Threading;

namespace GameServer.player
{
	public class PlayersProvider : task.AsyncTask
	{
		public const int PLAYER_TIMEOUT_S = 5;
		
		public PlayersProvider() : base("Players Provider", true)
		{
			
		}
		
		protected override void Run(params object[] args)
		{
			while(true) 
			{
				foreach(Player p in Server.GetOnlinePlayers())
				{		
					if(DateTime.Now.Subtract(p.Connection.GetLastStamp()).Seconds > PLAYER_TIMEOUT_S)
					{
						SendToLog(p.Name + " " + locale.Strings.From("player.timeout"));
						
						p.Close(locale.Strings.From("player.close"));
					}
				}
				
				Thread.Sleep(PLAYER_TIMEOUT_S * 1000);
			}
		}
	}
}
