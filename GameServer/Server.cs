using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;

using GameServer.utils;

namespace GameServer
{
	public static class Server
	{
		public static Config Properties;
		
		public static Thread ServerThread;
		
		public static HttpListener Listener;
		
		public static bool Working;
		
		public static Level DefaultLevel, CurrentLevel;
		
		public static player.PlayersProvider PlayersProvider;
		
		public static void Main()
		{
			try
			{
				Properties = new Config("server.properties");
				initProperties();
				
				if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON && 
				   !File.Exists(Data.LOG_FILE)) 
					File.WriteAllText(Data.LOG_FILE, "Server log file of " + DateTime.Now.ToString());
				
				Data.SendToLog("Server of " + Data.GetGameName() + " v." + Data.GetGameVersion(), Data.Log_Info, ConsoleColor.Cyan);
				
				Working = false;
				
				DefaultLevel = new Level("DEFAULT");
				DefaultLevel.Generate();
				
				ServerStart(Properties.GetProperty("server-address"), Convert.ToInt32(Properties.GetProperty("server-port")));
				
				CurrentLevel = DefaultLevel;
				
				events.Events.CallEvent(new events.ServerLoadedEvent("first start"));
				
				ConsoleReader.InitializeDafaultLines();
				
				addon.Addons.LoadAll();
				
				PlayersProvider = new player.PlayersProvider();
				
				player.control.Ban.InitializeAll();
				
				Data.SendToLog("Done! For help, type 'help' or '?'");
				
				ConsoleReader.Read();
			}
			catch(Exception ex)
			{
				Data.Crash(ex);
				Console.ReadKey();
			}
		}
		
		public static void ServerStart(string Address, int Port = Data.DEFAULT_SERVER_PORT)
		{
			Data.SendToLog("Server starting on " + Address + ":" + Port, Data.Log_Info, ConsoleColor.Green);
			
			Data.SetTitle("Waiting for requests...");
			
			ServerThread = new Thread( (ThreadStart) delegate
			{
			    Listener = new HttpListener();
			    if(!HttpListener.IsSupported)
			    	ServerCritical("Http server not supported!!!");
			    
			    Listener.Prefixes.Add(@"http://" + Address + ":" + Port + "/");
			    
			    Listener.Start();
			    
			    while(Listener.IsListening)
			    {
					HttpListenerContext context = Listener.GetContext();
					HttpListenerRequest request = context.Request;
					HttpListenerResponse response = context.Response;
						
					//Answer
					string data = request.Url.AbsolutePath.Substring(1);
					
					Data.Debug("Accepted new request from " + request.UserHostAddress);
					
					if(player.control.Ban.IsIPBanned(request.UserHostAddress.Split(':')[0]))
					{
						Data.SendToLog("Closed, because IP Banned! Source: " + request.UserHostAddress, Data.Log_Warning);
						
						return;
					}
						
					byte[] buffer = Encoding.UTF8.GetBytes(Handler.SendRequest(Uri.UnescapeDataString(data), request.UserHostAddress));
					
					response.AppendHeader("Access-Control-Allow-Origin", "*");
						
					response.ContentLength64 = buffer.Length;
					Stream output = response.OutputStream;
					try { output.Write(buffer, 0, buffer.Length); }
					catch { Data.SendToLog("[network] Incorrect reply!", Data.Log_Warning); }
					output.Close();
			    }
			});
			
			Working = true;
			
			ServerThread.Start();
			ServerThread.IsBackground = true;
		}
		
		public static void ServerStop()
		{
			if(Working)
			{
				Working = false;
				
				addon.Addons.UnloadAll();
				
				Data.SendToLog("Server was stopped...");
				events.Events.CallEvent(new events.ServerStoppedEvent("stopped"));
				
				Listener.Close();
				
				ServerThread.Abort();
			}
		}
		
		public static void Exit()
		{
			ServerStop();
			
			Thread.Sleep(2000);
			Environment.Exit(0);
		}
		
		public static void ServerResume()
		{
			if(!Working)
			{
				ServerStart(Properties.GetProperty("server-address"), Convert.ToInt32(Properties.GetProperty("server-port")));
				events.Events.CallEvent(new events.ServerLoadedEvent("resume"));
			}
		}
		
		public static void ServerCritical(string Message)
		{
			Data.SendToLog(Message, Data.Log_Critical, ConsoleColor.Red);
			ServerStop();
		}
		
		public static string GetFullAddress()
		{
			return Server.Properties.GetProperty("server-address") + ":" + Server.Properties.GetProperty("server-port");
		}
		
		public static void initProperties()
		{
			if(!Properties.ExistsProperty("server-address"))
				Properties.SetProperty("server-address", "127.0.0.1");
			if(!Properties.ExistsProperty("server-port"))
				Properties.SetProperty("server-port", Data.DEFAULT_SERVER_PORT.ToString());
			if(!Properties.ExistsProperty("server-name"))
				Properties.SetProperty("server-name", Data.GetGameName() + " v." + Data.GetGameVersion() + " server");
			if(!Properties.ExistsProperty("logging"))
				Properties.SetProperty("logging", Config.SWITCH_ON);
			if(!Properties.ExistsProperty("use-addons"))
				Properties.SetProperty("use-addons", Config.SWITCH_OFF);
			if(!Properties.ExistsProperty("save-crashes"))
				Properties.SetProperty("save-crashes", Config.SWITCH_ON);
			if(!Properties.ExistsProperty("debug-logging"))
				Properties.SetProperty("debug-logging", Config.SWITCH_OFF);
			if(!Properties.ExistsProperty("console-colors"))
				Properties.SetProperty("console-colors", Config.SWITCH_ON);
			
			Properties.Save();
		}
	}
}
