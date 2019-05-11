using System;
using System.Collections.Generic;

using GameServer.events;
using GameServer.locale;
using GameServer.utils;
using GameServer.level;
using GameServer.ui.form;


namespace GameServer.player
{
	public class Player
	{
		public Dictionary<string, string> GameOptions = new Dictionary<string, string>();
			
		public readonly string Token, Name;
		
		public readonly Session Connection;
		
		public readonly Chat CurrentChat;
		
		public inventory.Inventory Inventory;
		
		public level.Level Level;
		
		public int StatMoney = 0, StatPopulation = 0;
		public char MoneySymbol = '$';
		
		public readonly Queue<Form> Forms;
		
		public readonly List<string> Permissions = new List<string>();
		
		public Player(string token, string name, level.Level level, string address = "0.0.0.0")
		{
			Token = token;
			Name = name;
			
			Connection = new Session(address);
			
			CurrentChat = new Chat();
			
			Inventory = new inventory.Inventory(128, Name);
			
			if(Server.Properties.GetProperty("inventory-kit") == Config.SWITCH_ON) 
				Inventory.SetDevelopmentKit();
			
			if(!Action(PlayerActionEvent.Actions.Born)) Close();
			
			CurrentChat.SendMessage(Strings.From("player.joinmsg") + Server.GetFullAddress());
			
			Level = level;
			
			Forms = new Queue<Form>();
		}
		
		public override string ToString()
		{
			return Name;
		}
		
		public void Close(string message = "")
		{
			if(message != "") SendMessage(message);
			
			SendGameData("close", "ok");
				
			Server.LeavePlayer(this);
			
			Action(PlayerActionEvent.Actions.Closed);
		}
		
		public void SendGameData(string option, string value)
		{
			GameOptions[option] = value;
		}
		
		public void SendMessage(string message)
		{
			SendGameData("message", TextUtil.ClearForPacket(message));
		}
		
		public void SendChatMessage(string message)
		{
			CurrentChat.SendMessage(TextUtil.ClearForPacket(message));
		}
		
		public void SendChatMessage(string message, Color color)
		{
			CurrentChat.SendMessage(TextUtil.ClearForPacket(message), color);
		}
		
		public void Error(string ErrorMessage)
		{
			Data.Debug(Strings.From("player") + " #" + Name + " error: " + ErrorMessage);
			
			SendGameData("error", TextUtil.ClearForPacket(ErrorMessage));
		}
		
		public void Click(ushort offsetX, ushort offsetY, ushort x, ushort y, int id)
		{
			if(Action(PlayerActionEvent.Actions.Tileset, offsetX, offsetY, x, y, id))
			{
				if(Level.GetChunk(offsetX, offsetY) != null)
				{
					Inventory.TakeItem(id, 1);
					
					Level.GetChunk(offsetX, offsetY).TileSet(x, y, id);
					
					Level.CompressedData = Compressor.Compress(Level.RawData);
				}
			}
		}
		
		public void Chat(string Message, string Prefix = ": ")
		{
			if(Action(PlayerActionEvent.Actions.Chat, Message, (Name + Prefix + Message)))
				Server.BroadcastMessage(Name + Prefix + Message);
		}
		
		public void Bar(string messageText)
		{
			SendGameData("bar", TextUtil.ClearForPacket(messageText));
		}
		
		public void Bar(string messageText, Color color)
		{
			SendGameData("bar", color.Format + TextUtil.ClearForPacket(messageText));
		}
		
		public bool Action(PlayerActionEvent.Actions action, params object[] args)
		{
			events.Event e = new events.PlayerActionEvent(this, action, args);
			events.Events.CallEvent(e);
			return e.IsWorkingNext();
		}
		
		public void SendForm(Form form)
		{
			Forms.Enqueue(form);
			
			Data.Debug("@sent form for #" + Name);
		}
		
		public void Notice(string message, string title = "")
		{
			Form form = new Form(title);
			
			ContentText msg = new ContentText(message);
			Button btn = new Button("OK");
			
			form.AddControl(msg);
			form.AddControl(btn);
			
			SendForm(form);
		}
		
		public void OpenWebLink(string url)
		{
			SendGameData("wurl", TextUtil.ClearForPacket(url));
		}
		
	}
}
