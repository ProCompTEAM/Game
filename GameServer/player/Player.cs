using System;
using System.Collections.Generic;
using GameServer.events;
using GameServer.locale;

namespace GameServer.player
{
	public class Player
	{
		public Dictionary<string, string> GameOptions = new Dictionary<string, string>();
			
		public string Token, Name;
		
		public Session Connection;
		
		public Chat CurrentChat;
		
		public inventory.Inventory Inventory;
		
		
		public Player(string token, string name, string address = "0.0.0.0")
		{
			Token = token;
			Name = name;
			
			Connection = new Session(address);
			
			CurrentChat = new Chat();
			
			Inventory = new inventory.Inventory(128, Name);
			Inventory.SetDevelopmentKit();
			
			if(!Action(PlayerActionEvent.Actions.Born)) Close();
			
			CurrentChat.SendMessage(Strings.From("player.joinmsg") + Server.GetFullAddress());
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
			
			Action(PlayerActionEvent.Actions.Closed);
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
			Data.Debug(Strings.From("player") + " #" + Name + " error: " + ErrorMessage);
			
			SendGameData("error", ErrorMessage);
		}
		
		public void Click(Tile tile)
		{
			Server.CurrentLevel.SetTile(tile);
		}
		
		public void Chat(string Message, string Prefix = ": ")
		{
			if(Action(PlayerActionEvent.Actions.Chat, (Name + Prefix + Message)))
				Server.CurrentLevel.BroadcastMessage(Name + Prefix + Message);
		}
		
		public void Bar(string messageText)
		{
			SendGameData("bar", messageText);
		}
		
		public bool Action(PlayerActionEvent.Actions action, params object[] args)
		{
			events.Event e = new events.PlayerActionEvent(this, action, args);
			events.Events.CallEvent(e);
			return e.IsWorkingNext();
		}
	}
}
