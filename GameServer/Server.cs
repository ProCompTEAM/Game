using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;

namespace GameServer
{
	public static class Server
	{
		public static Thread ServerThread;
		
		public static HttpListener Listener;
		
		public static bool Working;
		
		public static void Main()
		{
			Data.SendToLog("Server working for " + Data.GetGameName() + " v." + Data.GetGameVersion());
			
			Working = false;
			
			ServerStart("127.0.0.1");
			
			ConsoleReader.Read();
		}
		
		public static void ServerStart(string Address, int Port = 48888)
		{
			Data.SendToLog("Server starting on " + Address + ":" + Port);
			
			ServerThread = new Thread( (ParameterizedThreadStart) delegate
			{
			    Listener = new HttpListener();
			    if(!HttpListener.IsSupported)
			    {
			    	ServerCritical("Http server not supported!!!");
			    }
			    
			    Listener.Prefixes.Add(@"http://" + Address + ":" + Port + "/");
			    
			    Listener.Start();
				
			    while(Listener.IsListening)
			    {
					HttpListenerContext context = Listener.GetContext();
					HttpListenerRequest request = context.Request;
					HttpListenerResponse response = context.Response;
					
					//Answer
					string data = request.Url.AbsolutePath.Substring(1);
					
					Data.SendToLog("Accepted new request from " + request.UserHostAddress);
					byte[] buffer = Encoding.UTF8.GetBytes(Handler.SendRequest(data.Split('+')));
					response.ContentLength64 = buffer.Length;
					Stream output = response.OutputStream;
					output.Write(buffer, 0, buffer.Length);
					output.Close();
			    }
			});
			
			Working = true;
			
			ServerThread.Start();
			ServerThread.IsBackground = true;
		}
		
		public static void ServerStop()
		{
			Working = false;
			
			Data.SendToLog("Server was stopped...");
			Listener.Close();
			ServerThread.Abort();
		}
		
		public static void Exit()
		{
			if(Working)
				ServerStop();
			
			Thread.Sleep(2000);
			
			Environment.Exit(0);
		}
		
		public static void ServerCritical(string Message)
		{
			Data.SendToLog(Message, Data.Log_Critical);
			ServerStop();
		}
	}
}
