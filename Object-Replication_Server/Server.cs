using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Object_Replication_Server
{
    class Server
    {
        TcpListener listener;
        int ipAd = 1313;
        int DataSize = 10140;

        int clientCount = 0;

        public Server()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Any, ipAd));
            listener.Start();

            Console.WriteLine("Server Started");

            BeginListening();
        }

        private void BeginListening()
        {
            while (true)
            {
                // Accecpt new client
                clientCount++;
                TcpClient tcpClient = listener.AcceptTcpClient();
                Console.WriteLine("Client No: " + Convert.ToString(clientCount) + " connected!");

                // Handle client massage
                ClientHandler clientHandler = new ClientHandler(tcpClient, clientCount);
            }
        }
    }
}
