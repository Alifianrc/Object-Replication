using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary;

namespace Object_Replication_Client
{
    class Client
    {
        TcpClient client;
        NetworkStream networkStream;
        int ipAd = 1313;

        public Client()
        {
            client = new TcpClient();

            while (!client.Connected)
            {
                client.Connect(IPAddress.Loopback, ipAd);
                networkStream = client.GetStream();
            }

            Console.WriteLine("Client Connected!");
            BeginSendData();
        }

        private void BeginSendData()
        {
            string inputClient;
            BinaryFormatter formatter = new BinaryFormatter();

            while (client.Connected)
            {
                Console.WriteLine("Input Command -> login ?  play ? exit");

                Console.Write("Your Command : "); 
                inputClient = Console.ReadLine();
                Statement state = new Statement();

                switch (inputClient)
                {
                    case "login":
                        Login login = new Login();

                        Console.Write("Input Name     : ");
                        login.username = Console.ReadLine();

                        Console.Write("Input Password : ");
                        login.password = Console.ReadLine();
                     
                        formatter.Serialize(networkStream, login);
                        formatter.Serialize(networkStream, login);

                        break;
                    case "play":
                        Play play = new Play();

                        Console.Write("Input Room Name : ");
                        play.roomName = Console.ReadLine();

                        Console.Write("Input Level     : ");
                        play.level = int.Parse(Console.ReadLine());

                        formatter.Serialize(networkStream, play);
                        formatter.Serialize(networkStream, play);

                        break;
                    case "exit":
                        client.Close();
                        break;
                    default:
                        Console.WriteLine("Command Not Detected");
                        break;
                }

                Console.WriteLine("\n");
            }
        }
    }
}
