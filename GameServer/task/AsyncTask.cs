using System;
using System.Threading;

namespace GameServer.task
{
	public abstract class AsyncTask
	{
		string Name;
		bool Started;
		
		Thread CurrentThread;
		
		protected AsyncTask(string TaskName = "AsyncTask", bool AutoStart = false)
		{
			Name = TaskName;
			
			Started = false;
			
			if(AutoStart) Open();
		}
		
		public override string ToString()
		{
			return Name;
		}

		
		public void Open(params object[] args)
		{
			if(!Started)
			{
				CurrentThread = new Thread( (ThreadStart) delegate
				{
				           	Run(args);
				});
				CurrentThread.Start();
				
				Started = true;
				
				Data.Debug("Loaded Task #" + ToString());
			}
		}
		
		public void Kill()
		{
			if(Started)
			{
				CurrentThread.Abort();
				
				Data.Debug("Finished Task #" + ToString());	
			}
		}
		
		protected void SendToLog(string Message, string Type = "Log/Task", ConsoleColor cl = ConsoleColor.White)
		{
			string line = string.Format("[{0}][{1}] {2}", DateTime.Now.ToLongTimeString(), Type, Message);
			
			if(cl == ConsoleColor.White || 
			   Server.Properties.GetProperty("console-colors") != utils.Config.SWITCH_ON) Console.WriteLine(line);
			else
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("[{0}][{1}] ", DateTime.Now.ToLongTimeString(), Type);
				Console.ForegroundColor = cl;
				Console.WriteLine(Message);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		
		protected void Debug(string Message)
		{
			if(Server.Properties.GetProperty("debug-logging") == utils.Config.SWITCH_ON)
				SendToLog(Message, "Log/Task/Debug", ConsoleColor.DarkGray);
		}
		
		protected abstract void Run(params object[] args);
	}
}
