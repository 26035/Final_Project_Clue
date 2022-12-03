using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject
{
    static class Server
    {
        static int port = 13000;
        //static string IpAddress = "10.0.76.255";
        static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        static IPAddress ip = ipHost.AddressList[1];
        static Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static IPEndPoint ep = new IPEndPoint(ip, port);
        public static List<Socket> allClients = new List<Socket>();
        static Socket client = default(Socket);
        public static void SetUpServer(int nb)
        {
            ServerListener.Bind(ep);
            ServerListener.Listen(6);
            Console.WriteLine("Server listening...");
            allClients = AcceptConnection(nb);
        }
        public static List<Socket> AcceptConnection(int nb)
        {
            int counter = 0;
            Console.Clear();
            Console.WriteLine("Waiting {0} players to connect", nb);
            while (counter != nb)
            {
                counter++;
                client = ServerListener.Accept();
                allClients.Add(client);
                Console.WriteLine("Client {0} connected", counter);
            }
            Thread.Sleep(1000);
            Console.Clear();
            return allClients;

        }
        public static void SendToOtherClients(int type, string text, Socket client)
        {
            for (int i = 0; i < allClients.Count; i++)
            {
                if (allClients[i] != client)
                {
                    Server.SendToClient(type, text, allClients[i]);
                }
            }
        }
        public static void SendToAll(int type, string text)
        {
            foreach (Socket client in allClients)
            {
                Server.SendToClient(type, text, client);
            }
        }
        public static void SendBoardToClients(GameBoard board)
        {
            foreach (Socket client in allClients)
            {
                Server.SendToClient(0, board.ToString(), client);
            }
        }
        public static void SendToClient(int type, string text, Socket client)
        {

            byte[] buffer = Encoding.ASCII.GetBytes(type + text);
            client.Send(buffer);
            Console.WriteLine("Sent text");

        }
        public static string ReceiveFromClient(Socket client)
        {
            string textServer = "";
            while (client.Connected && textServer == "")
            {
                byte[] buffer = new byte[1024];
                client.Receive(buffer);
                if (buffer.Length != 0)
                {
                    Console.WriteLine("Text received");
                    textServer = Encoding.ASCII.GetString(buffer);
                }
            }
            return textServer;
        }
    }
}
