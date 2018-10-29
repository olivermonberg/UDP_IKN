using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			Client c1 = new Client(args[0], args[1]);
        }
    }

	class Client
	{
		public Client(string ServerIP, string InfoRequest)
		{
			Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint EP = new IPEndPoint(IPAddress.Parse(ServerIP), 9000);

			byte[] receiveBuf = new byte[1000];
            byte[] sendBuf = new byte[1];         
                
			sendBuf = System.Text.Encoding.ASCII.GetBytes(InfoRequest);
            
			ClientSocket.SendTo(sendBuf, EP);
			ClientSocket.Receive(receiveBuf);

			if (InfoRequest == "u" || InfoRequest == "U")
			{
				Console.Write($"{ServerIP} uptime data:\n" +
				              $"{System.Text.Encoding.Default.GetString(receiveBuf)}");
			}
			else if (InfoRequest == "l" || InfoRequest == "L")
			{
				Console.Write($"{ServerIP} CPU-load data:\n" +
				              $"{System.Text.Encoding.Default.GetString(receiveBuf)}");
			}         
		}
	}
}
