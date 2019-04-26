using System;
using GameServer.task;

namespace GameServer.security
{
	public class ServerTranslator : AsyncTask
	{
		public ServerTranslator(string address, int port) : base("HttpsListener")
		{
			Open(address, port);
		}
		
		protected override void Run(params object[] args)
		{
			string address = (string) args[0];
			int port = (int) args[1];
			
			//SendToLog("Loading https Protocol Translator on " + address + ":" + port + "...");
			SendToLog(locale.Strings.From("translator.off"));
		}
	}
}
