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
        /// <summary>
        /// used to set up the server
        /// </summary>
        /// <param name="nb">integer that represent the number of player</param>
        public static void SetUpServer(int nb)
        {
            ServerListener.Bind(ep);
            ServerListener.Listen(6);
            Console.WriteLine("Server listening...");
            allClients = AcceptConnection(nb);
        }
        /// <summary>
        /// used to accept a predefine number of connection
        /// </summary>
        /// <param name="nb">integer that represent the number of player (or connection)</param>
        /// <returns>List of socket used by every player connected</returns>
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
        /// <summary>
        /// used to send informations to all players except one
        /// </summary>
        /// <param name="type">integer that represent the type of information</param>
        /// <param name="text">string that is sent to the player</param>
        /// <param name="client">socket of the player that is not receiving the informations</param>
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
        /// <summary>
        /// used to send informations to every player
        /// </summary>
        /// <param name="type">integer that represent the type of informations</param>
        /// <param name="text">string that is sent to the player</param>
        public static void SendToAll(int type, string text)
        {
            foreach (Socket client in allClients)
            {
                Server.SendToClient(type, text, client);
            }
        }
        /// <summary>
        /// used to send the informationof the board to every player
        /// </summary>
        /// <param name="board">that represent the board</param>
        public static void SendBoardToClients(GameBoard board)
        {
            foreach (Socket client in allClients)
            {
                Server.SendToClient(0, board.ToString(), client);
            }
        }
        /// <summary>
        /// Used to send information to only one player
        /// </summary>
        /// <param name="type">integer that represents the type of information</param>
        /// <param name="text">string that represents the information</param>
        /// <param name="client">socket of the player receiving informations</param>
        public static void SendToClient(int type, string text, Socket client)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(type + text);
                client.Send(buffer);
                Console.WriteLine("Sent text");
            }
            catch (SocketException)
            {
                ServerListener.Close();
                Environment.Exit(0);
            }


        }
        /// <summary>
        /// Used to receive informations from players
        /// </summary>
        /// <param name="client">socket of the player sending information</param>
        /// <returns>string that represents the informations sent by a player</returns>
        public static string ReceiveFromClient(Socket client)
        {
            try
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
            catch (SocketException)
            {
                ServerListener.Close();
                Environment.Exit(0);
                return "";
            }
        }
    }
}
