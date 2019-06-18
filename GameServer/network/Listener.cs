using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using GameServer.locale;

namespace GameServer.network
{
	public class Listener
	{
		public const int REQUEST_SIZE = 2048;
		public const int REQUSTS_COUNT = 100;
		
		static bool Listening = false;
		
		static Socket CurrentListener;
		
		public void Listen(string address, int port)
		{
			IPEndPoint iep = new IPEndPoint(IPAddress.Parse(address), port);
			
			CurrentListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			CurrentListener.Bind(iep);
			
			CurrentListener.Listen(REQUSTS_COUNT);
			
			Listening = true;
			
			while(true)
			{
				if(!Listening) break;
				
				try {
					Socket s = CurrentListener.Accept();
					
					if(s.Available <= REQUEST_SIZE)
					{
						byte[] buffer = new byte[REQUEST_SIZE];
						
						s.Receive(buffer);
						
						string getr = Encoding.UTF8.GetString(buffer).Split('\n')[0].Split(' ')[1].Substring(1);
						
						Data.Debug(Strings.From("server.request") + s.RemoteEndPoint.ToString());
						
						if(player.control.Ban.IsIPBanned(s.RemoteEndPoint.ToString().Split(':')[0]))
	                    {
	                        Data.SendToLog(Strings.From("player.bannedip") + s.RemoteEndPoint.ToString(), Data.Log_Warning);
	                       
	                        return;
	                    }
						
						string data = PacketsHandler.SendRequest(Uri.UnescapeDataString(getr), s.RemoteEndPoint.ToString());
						
						byte[] reply = Encoding.UTF8.GetBytes(GetHeaders(data.Length) + data);
						
						try { s.Send(reply); }
	                    catch { Data.SendToLog("[network] Incorrect reply!", Data.Log_Warning); }
	                    
	                    s.Shutdown(SocketShutdown.Both);
	                    s.Close();
					}
				}
				catch { Data.SendToLog("[network] Incorrect reply!", Data.Log_Warning); }
			}
			
			CurrentListener.Close();
		}
		
		public void Stop()
		{
			CurrentListener.Close();
			Listening = false;
		}
		
		private string GetHeaders(int contentLength)
		{
			const string ln = "\r\n";
			string headers = "";
			
			headers += "HTTP/1.0 200 OK" + ln;
			//headers += "Content-Type: text/html" + ln;
			headers += "Content-Length: " + contentLength + ln;
			headers += "Access-Control-Allow-Origin: *" + ln;
			return headers + ln;
		}
	}
}
