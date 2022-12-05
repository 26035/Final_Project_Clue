using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    class Program
    {
        static int port = 13000;
        static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        static IPAddress ip = ipHost.AddressList[0];
        static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly byte[] buffer = new byte[1024];
        static void Main(string[] args)
        {
            Console.Title = "Client";
            ConnectToServer();
            ReceiveFromServer();
            Console.ReadKey();
        }
        /// <summary>
        /// used to connect to the server
        /// </summary>
        static void ConnectToServer()
        {
            Console.Title = "Client";
            string IpAddress=null;
            Console.WriteLine("What is the server IP Address ?\n\t1: 10.0.76.255\n\t2: 10.10.3.114\n\t3: Other");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    IpAddress = "10.0.76.255";
                    break;
                case 2:
                    IpAddress = "10.10.3.114";
                    break;
                case 3:
                    Console.WriteLine("Enter the server IP Address");
                    IpAddress = Console.ReadLine();
                    break;

            }
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);

            byte[] buffer = new byte[1024];
            while (!client.Connected)
            {
                try
                {
                    client.Connect(ep);

                    
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Trying to connect...");
                }
            }
            Console.Clear();
            Console.WriteLine("You're connected!\nWaiting for other player to join...");
        }
        /// <summary>
        /// used to send information to the server
        /// </summary>
        static void SendToServer()
        {
            Console.WriteLine("Send a response :");
            string answer = Console.ReadLine();
            byte[] buffer = Encoding.ASCII.GetBytes(answer);
            client.Send(buffer, 0, buffer.Length, SocketFlags.None);
            Console.Clear();
        }
        /// <summary>
        /// used to send direction move to the server
        /// </summary>
        static void SendDirectionToServer()
        {
            ConsoleKeyInfo answer = Console.ReadKey(true);
            bool valid = false;
            switch (answer.Key)
            {
                case ConsoleKey.U:
                    valid = true;
                    break;

                case ConsoleKey.D:
                    valid = true;
                    break;

                case ConsoleKey.L:
                    valid = true;
                    break;

                case ConsoleKey.R:
                    valid = true;
                    break;
                default:
                    break;

            }
            if (valid)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(answer.KeyChar.ToString());
                client.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            else
            {
                SendDirectionToServer();
            }
        }
        /// <summary>
        /// used to receive informations from the server
        /// </summary>
        static void ReceiveFromServer()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (client.Connected)
                {
                    client.Receive(buffer);
                    if (buffer.Length != 0)
                    {
                        string infoServer = Encoding.Default.GetString(buffer);
                        char typeOfInfo = infoServer[0];
                        string textServer = infoServer.Substring(1);
                        switch (typeOfInfo)
                        {
                            case '0'://print board game on client console
                                Console.Clear();
                                PrintBoard(textServer);
                                break;
                            case '1'://print request and ask for answer
                                Console.WriteLine(textServer);
                                SendToServer();
                                break;
                            case '2'://print infos
                                Console.WriteLine(textServer);
                                break;
                            case '3':
                                Console.WriteLine(textServer);
                                SendDirectionToServer();
                                break;
                        }

                    }
                    Array.Clear(buffer, 0, buffer.Length);
                }

            }
            catch (SocketException)
            {
                Console.WriteLine("Sever disconnected\nPresse enter to exit");
                client.Close();
            }
            

        }
        /// <summary>
        /// used to read a csv file and create a matrix thank to the file
        /// </summary>
        /// <param name="fileName">string that represents the name of the file</param>
        /// <returns>matrix of char that represents the board information (secret passage, door or letter of the room)</returns>
        public static char[,] ReadFile(string fileName)
        {
            char[,] file = new char[24, 24];
            StreamReader lecture = new StreamReader(fileName);
            int i = 0;
            while (!lecture.EndOfStream)
            {
                string line = lecture.ReadLine();
                string[] list = line.Split(';');
                for (int j = 0; j < 24; j++)
                {
                    file[i, j] = Convert.ToChar(list[j]);
                }
                i++;
            }
            return file;
        }
        /// <summary>
        /// used to Create a matrix from a string
        /// </summary>
        /// <param name="text">string that composes the matrix</param>
        /// <returns>matrix of char that represents the board informations (pawn, empty path, wall or stair)</returns>
        static char[,] CreateMatrix(string text)
        {
            int count = 0;
            char[,] board = new char[24, 24];
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    board[i, j] = text[count];
                    count++;
                }
            }
            return board;
        }
        /// <summary>
        /// used to create a colorful print of the board
        /// </summary>
        /// <param name="textBoard">string that represents the board informations (pawn, empty path, wall or stair)</param>
        public static void PrintBoard(string textBoard)
        {

            char[,] room = ReadFile("RoomNames.csv");
            char[,] board = CreateMatrix(textBoard);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string column = "1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 ";
            Console.WriteLine(column);
            int row = 1;
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    char path = board[i, j];
                    char roomName = room[i, j];
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (path == 'R')
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("   ");
                    }
                    if (path == 'G')
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("   ");
                    }
                    if (path == 'B')
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("   ");
                    }
                    if (path == 'Y')
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write("   ");
                    }
                    if (path == 'W')
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("   ");
                    }
                    if (path == 'P')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("   ");
                    }
                    if (path == '.')
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        if (roomName == '?')
                        {

                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write(roomName + "  ");
                        }

                    }
                    if (path == 'X')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(roomName + "  ");
                    }

                    if (path == '_')
                    {
                        if (roomName == '!')
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(path + "  ");

                    }
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (row < 10)
                {
                    Console.Write(" " + row + " ");
                }
                else
                {
                    Console.Write(row + " ");
                }
                row++;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.ResetColor();
        }
       
        
    }
}
