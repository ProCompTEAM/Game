using System;
using System.Collections.Generic;

namespace GameServer.player
{
	public class Player
	{
		public Dictionary<string, string> GameOptions = new Dictionary<string, string>();
			
		public string Token, Name;
		
		public Session Connection;
		
		public Chat CurrentChat;
		
		
		public Player(string token, string name, string address = "0.0.0.0")
		{
			Token = token;
			Name = name;
			
			Connection = new Session(address);
			
			CurrentChat = new Chat();
			
			CurrentChat.SendMessage("[server] Connected to server " + Server.GetFullAddress());
		}
		
		public override string ToString()
		{
			return Name;
		}

		
		public void Close(string message = "")
		{
			if(message != "") SendMessage(message);
			
			SendGameData("close", "ok");
				
			Server.CurrentLevel.LeavePlayer(this);
		}
		
		public void SendGameData(string option, string value)
		{
			GameOptions[option] = value;
		}
		
		public void SendMessage(string message)
		{
			SendGameData("message", message);
		}
		
		public void Error(string ErrorMessage)
		{
			Data.Debug("Player #" + Name + " error: " + ErrorMessage);
			
			SendGameData("error", ErrorMessage);
		}
		
		public void Click(Tile tile)
		{
			Server.CurrentLevel.SetTile(tile);
		}
		
		public void Chat(string Message, string Prefix = ": ")
		{
			Server.CurrentLevel.BroadcastMessage(Name + Prefix + Message);
		}
	}
}
