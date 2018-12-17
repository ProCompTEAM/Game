using System;
using System.Collections.Generic;

namespace GameServer.player
{
	public class Player
	{
		public Dictionary<string, string> GameOptions = new Dictionary<string, string>();
			
		public string Token, Name;
		
		
		public Player(string token, string name)
		{
			Token = token;
			Name = name;
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
		
		public void Click(Tile tile)
		{
			Server.CurrentLevel.SetTile(tile);
		}
	}
}
