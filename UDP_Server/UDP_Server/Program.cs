using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Server
{
	class MainClass
    {
        public static void Main(string[] args)
        {
            Server s1 = new Server();
        }
    }

	class Server
    {
		public Server()
		{
			Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 9000));

			Console.WriteLine("UDP-server started...\n");

			byte[] receiveBuf = new byte[1];

			EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

			while (true)
			{    
				ServerSocket.ReceiveFrom(receiveBuf, ref remoteEP);

                string str = System.Text.Encoding.Default.GetString(receiveBuf);
            
				if(str == "u" || str == "U")
				{
					byte[] sendBuf = new byte[1000];

					FileStream fs = File.OpenRead("/proc/uptime");
					fs.Read(sendBuf, 0, sendBuf.Length);
					fs.Close();

					Console.WriteLine($"Received uptime data request.\n" +
					                  $"Sending current data: {System.Text.Encoding.Default.GetString(sendBuf)}");

					ServerSocket.SendTo(sendBuf, remoteEP);               
				}
				else if(str == "l" || str == "L")
				{
					byte[] sendBuf = new byte[1000];

                    FileStream fs = File.OpenRead("/proc/loadavg");
                    fs.Read(sendBuf, 0, sendBuf.Length);
                    fs.Close();

					Console.WriteLine($"Received CPU-load data request.\n" +
                                      $"Sending current data: {System.Text.Encoding.Default.GetString(sendBuf)}");

                    ServerSocket.SendTo(sendBuf, remoteEP);
				}
			}
		}
    }   
}
