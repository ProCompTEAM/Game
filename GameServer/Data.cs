using System;
using System.IO;
using System.Data.SQLite;
using GameServer.utils;

namespace GameServer
{
	public static class Data
	{
		public const int DEFAULT_SERVER_PORT = 48888;
		
		public const string Log_Info = "Log/Info";
		public const string Log_Warning = "Log/Warning";
		public const string Log_Error = "Log/Error";
		public const string Log_Critical = "Log/Critical";

		public static string LOG_FILE = "server-log.txt";
		
		public static string GetGameName()
		{
			return "Building Race";
		}
		
		public static double GetGameVersion()
		{
			return 1.0;
		}
		
		public static void SendToLog(string Message, string Type = Log_Info)
		{
			string line = string.Format("[{0}][{1}] {2}", DateTime.Now.ToLongTimeString(), Type, Message);
			
			Console.WriteLine(line);
			
			if(Server.Properties.GetProperty("logging") == Config.SWITCH_ON)
				File.AppendAllText(LOG_FILE, Environment.NewLine + line);
		}
		
		public static void SetTitle(string Title)
		{
			Console.Title = GetGameName() + " v." + GetGameVersion() + " | " + Title;
		}
		
		public static void Crash(Exception Ex, bool StopServer = false)
		{
			string crash = "<-=================- C R A S H -=================->";
			
			Console.WriteLine(Environment.NewLine + crash);
			
			Data.SendToLog("Detected server crash", Data.Log_Critical);
			
			Console.WriteLine(" * " + Ex.ToString());
			Console.WriteLine(" * Source: " + Ex.Source);
			Console.WriteLine(crash + Environment.NewLine);
			
			if(StopServer) Server.ServerStop();
			
			if(Server.Properties.GetProperty("save-crashes") == Config.SWITCH_ON)
			{
				string dt = DateTime.Now.ToShortDateString().Replace('.', '-') + "_" + DateTime.Now.ToLongTimeString().Replace(':', '-');
				File.AppendAllText("crashdump_" + dt + ".txt", Ex.ToString());
			}
		}
		
		public static void Input(string arr1, string arr2, string arr3)
		{
			SQLiteConnection DB = new SQLiteConnection("Data Source=Server.db; Version = 3");
			DB.Open();
			SQLiteCommand com = DB.CreateCommand();
			
			com.Parameters.Add("@email", System.Data.DbType.String).Value = arr1.ToUpper();
			com.Parameters.Add("@nick", System.Data.DbType.String).Value = arr2.ToUpper();
		    com.Parameters.Add("@password", System.Data.DbType.String).Value = arr3.ToUpper();
		    
		    com.CommandText = "insert into PlayersData(email, nick, password) values (@email, @nick, @password)";
		    com.ExecuteNonQuery();
		    DB.Close();
		}
		
		public static void DeleteData()
		{
			SQLiteConnection DB = new SQLiteConnection("Data Source=Server.db; Version = 3");
			DB.Open();
			SQLiteCommand com = DB.CreateCommand();
			com.CommandText = "delete from PlayersData";
			com.ExecuteNonQuery();
			DB.Close();
		}
		
		public static void Output()
		{
			SQLiteConnection DB = new SQLiteConnection("Data Source=Server.db; Version = 3");
			DB.Open();
			SQLiteCommand com = DB.CreateCommand();
		    com.CommandText = "SELECT * FROM PlayersData";
		    SQLiteDataReader read = com.ExecuteReader();
		    while(read.Read()){
		    	Console.WriteLine("Email: " + read["email"] + " Nick: " + read["nick"] + " Password: " + read["password"]);
		    }
		    DB.Close();
		}
		
		public static int Check(string arr1, string arr2, string arr3)
		{
			SQLiteConnection DB = new SQLiteConnection("Data Source=Server.db; Version = 3");
			DB.Open();
			SQLiteCommand com = DB.CreateCommand();
		    com.CommandText = "SELECT * FROM PlayersData";
		    SQLiteDataReader read = com.ExecuteReader();
		    while(read.Read()){
		    	if(arr1 == read["email"] && arr2 == read["nick"] && arr3 == read["password"]) return 0;
		    }
		    return 1;
		    DB.Close();
		}
	}
}
