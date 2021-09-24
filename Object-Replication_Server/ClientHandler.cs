using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ClassLibrary;
using System.IO;

namespace Object_Replication_Server
{
    class ClientHandler
    {
        TcpClient tcpClient;
        NetworkStream networkStream;
        BinaryFormatter formatter;

        int byteFixedSize = 1024;

        int clietnId;

        public ClientHandler(TcpClient tcpClient, int id)
        {
            this.tcpClient = tcpClient;
            formatter = new BinaryFormatter();

            networkStream = tcpClient.GetStream();

            clietnId = id;

            Thread recieveData = new Thread(ReceiveData);
            recieveData.Start();
        }

        private void ReceiveData()
        {
            while (tcpClient.Connected)
            {
                if (networkStream.DataAvailable)
                {
                    try
                    {
                        // Deserialize 1
                        Statement state = (Statement)formatter.Deserialize(networkStream);

                        // Determine state
                        switch (state.statement)
                        {
                            case Statement.State.LOGIN:
                                // Deserialize 2
                                Login login = (Login)formatter.Deserialize(networkStream);

                                // Print data
                                Console.WriteLine("Client-"+ clietnId + " : Login; Username : " + login.username 
                                    + "; Password : " + login.password);
                                break;
                            case Statement.State.PLAY:
                                // Deserialize 2
                                Play play = (Play)formatter.Deserialize(networkStream);

                                // Print data
                                Console.WriteLine("Client-" + clietnId + " : Play; Roomname : " + play.roomName
                                    + "; Level : " + play.level);
                                break;
                            default:
                                Console.WriteLine("Massage not Detected");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Deserialization : " + e.Message);

                    }
                }
            }
        }
    }
}
