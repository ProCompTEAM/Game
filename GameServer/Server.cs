using System;

namespace GameServer
{
	public static class Server
	{
		public static void Main()
		{
			Data.SendToLog("Server was started for " + Data.GetGameName() + " v." + Data.GetGameVersion(), Data.Log_Critical);
			Console.ReadKey();
		}
	}
}
