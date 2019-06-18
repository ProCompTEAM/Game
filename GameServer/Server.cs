using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using GameServer.network;
using GameServer.player;
using GameServer.utils;
using GameServer.locale;
using GameServer.level;

namespace GameServer
{
	public static class Server
	{
		public const string SERVER_BUILD_CODE = "as 0.4 beta builds";
		
		public static readonly Config Properties = new Config("server.properties");
		
		public static Thread ServerThread;
		
		public static Listener Listener = new Listener();
		
		public static bool Working = false;
		
		public static readonly Activity DefaultActivity = new Activity();
		
		public static readonly List<Player> Players = new List<Player>();
		
		public static readonly LevelsProvider LevelsProvider = new LevelsProvider();
		
		public static readonly PlayersProvider PlayersProvider = new PlayersProvider();
		
		public static readonly List<Level> Levels = new List<Level>();
		
		public static void Main()
		{
			try
			{
				InitProperties();
				
				if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON && 
				   !File.Exists(Data.LOG_FILE)) 
					File.WriteAllText(Data.LOG_FILE, "Server log file of " + DateTime.Now.ToString());
				
				Strings.ExecuteLang(Properties.GetProperty("server-language"));
				
				Data.SendToLog(Strings.From("server.init") + Data.GetGameName() + " v." + Data.GetGameVersion() + " " + SERVER_BUILD_CODE
				               , Data.Log_Info, ConsoleColor.Cyan);
				
				Level defaultLevel = LevelsProvider.Load(Server.Properties.GetProperty("default-level-name"));
				if(!LevelsProvider.Available || defaultLevel == null)
				{
					defaultLevel = new Level(Server.Properties.GetProperty("default-level-name"));
					
					Creator.CreateMesh(defaultLevel, 10, 10);
				}
				Levels.Add(defaultLevel);
				
				ServerStart(Properties.GetProperty("server-address"), Convert.ToInt32(Properties.GetProperty("server-port")));
				
				events.Events.CallEvent(new events.ServerLoadedEvent("first start"));
				
				ConsoleReader.InitializeDafaultLines();
				
				addon.Addons.LoadAll();
				
				player.control.Ban.InitializeAll();
				
				Data.SendToLog(Strings.From("server.done"));
				
				ConsoleReader.Read();
			}
			catch(Exception ex)
			{
				Data.Crash(ex);
				Console.ReadKey();
			}
		}
		
		public static void ServerStart(string address, int port = Data.DEFAULT_SERVER_PORT)
		{
			Data.SendToLog(Strings.From("server.start") + address + ":" + port, Data.Log_Info, ConsoleColor.Green);
			
			Data.SetTitle(Strings.From("server.wait"));
			
			ServerThread = new Thread( (ThreadStart) delegate
			{
			      Listener.Listen(address, port);
			});
			
			Working = true;
			
			ServerThread.Start();
			ServerThread.IsBackground = true;
			
			if(Server.Properties.GetProperty("https-translator") == Config.SWITCH_ON)
				new security.ServerTranslator(address, port);
		}
		
		public static void ServerStop()
		{
			if(Working)
			{
				Working = false;
				
				addon.Addons.UnloadAll();
				
				Data.SendToLog(Strings.From("server.stopped"));
				events.Events.CallEvent(new events.ServerStoppedEvent("stopped"));
				
				Listener.Stop();
				
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
		
		public static void InitProperties()
		{
			if(!Properties.ExistsProperty("server-address"))
				Properties.SetProperty("server-address", "127.0.0.1");
			if(!Properties.ExistsProperty("server-port"))
				Properties.SetProperty("server-port", Data.DEFAULT_SERVER_PORT.ToString());
			if(!Properties.ExistsProperty("server-name"))
				Properties.SetProperty("server-name", Data.GetGameName() + " v." + Data.GetGameVersion() + " server");
			if(!Properties.ExistsProperty("server-language"))
				Properties.SetProperty("server-language", Strings.LangCode);
			if(!Properties.ExistsProperty("default-level-name"))
				Properties.SetProperty("default-level-name", "main");
			if(!Properties.ExistsProperty("https-translator"))
				Properties.SetProperty("https-translator", Config.SWITCH_OFF);
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
			if(!Properties.ExistsProperty("save-levels"))
				Properties.SetProperty("save-levels", Config.SWITCH_OFF);
			if(!Properties.ExistsProperty("inventory-kit"))
				Properties.SetProperty("inventory-kit", Config.SWITCH_ON);
			
			Properties.Save();
		}
		
		public static void JoinPlayer(Player p)
		{
			Players.Add(p);
				
			Data.SendToLog(Strings.From("player") + p.Name + Strings.From("level.join") + "'" + p.Level.Name + "'", Data.Log_Info, ConsoleColor.Yellow);
			
			BroadcastMessage(Strings.From("player") + p.Name + Strings.From("player.joined"), Colors.Yellow);
		}
		
		public static void LeavePlayer(Player p)
		{
			Data.SendToLog(Strings.From("player") + p.Name + Strings.From("player.left"), Data.Log_Info, ConsoleColor.DarkYellow);
			
			Players.Remove(p);
			
			BroadcastMessage(Strings.From("player") + p.Name + Strings.From("player.left"), Colors.Yellow);
		}
		
		public static Player[] GetOnlinePlayers()
		{
			return Players.ToArray();
		}
		
		public static string[] GetOnlinePlayersStr()
		{
			string cache = "";
			
			foreach(Player p in GetOnlinePlayers()) cache += "+" + p.Name;
			
			return cache.Split('+');
		}
		
		public static bool IsOnline(string playerName)
		{
			return (Array.IndexOf(GetOnlinePlayersStr(), playerName) != -1);
		}
		
		public static Player GetPlayer(string playerName)
		{
			foreach(Player p in GetOnlinePlayers()) 
				if(p.Name == playerName) return p;
			return null;
		}
		
		public static void BroadcastMessage(string messageText)
		{
			foreach(Player p in GetOnlinePlayers()) p.CurrentChat.SendMessage(messageText);
			
			Data.SendToLog(messageText, Data.Log_Chat, ConsoleColor.Magenta);
		}
		
		public static void BroadcastMessage(string messageText, Color color)
		{
			foreach(Player p in GetOnlinePlayers()) p.CurrentChat.SendMessage(messageText, color);
			
			Data.SendToLog(messageText, Data.Log_Chat, ConsoleColor.Magenta);
		}
		
		public static void BroadcastBar(string messageText)
		{
			foreach(Player p in GetOnlinePlayers()) p.Bar(messageText);
		}
		
		public static void BroadcastBar(string messageText, Color color)
		{
			foreach(Player p in GetOnlinePlayers()) p.Bar(messageText, color);
		}
		
		public static Level GetLevel(string Name)
		{
			foreach(Level level in Levels)
			{
				if(level.ToString().ToLower() == Name.ToLower()) return level;
			}
			
			return null;
		}
		
		public static Level DefaultLevel
		{
			get { return GetLevel(Server.Properties.GetProperty("default-level-name")); }
		}
		
		public static void Log(string Message, params object[] args)
		{
			Data.SendToLog(string.Format(Message, args), Data.Log_Info, ConsoleColor.Gray);
		}
		
		public static string Directory
		{
			get { return Environment.CurrentDirectory + @"\"; }
		}
	}
}
