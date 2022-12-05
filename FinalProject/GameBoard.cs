using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace FinalProject
{
    class GameBoard
    {
        //Attribut
        Square[,] board; 
        List<List<Position>> positionRooms;
        //Constructors
        public GameBoard()
        {
            char[,] game = ReadFile("Game.csv");
            char[,] room = ReadFile("RoomNames.csv");
            this.board = new Square[24, 24];
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    this.board[i, j] = new Square(game[i, j], room[i, j]);
                }
            }
            positionRooms = new List<List<Position>>();
            InitializationRooms();
           
        }
        public GameBoard(string fileName)
        {
            char[,] game = ReadFile(fileName);
            char[,] room = ReadFile("RoomNames.csv");
            this.board = new Square[24, 24];
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    this.board[i, j] = new Square(game[i, j], room[i, j]);
                }
            }
            positionRooms = new List<List<Position>>();
            InitializationRooms();
        }
        //Properties
        public Square[,] Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
            }
        }
        public List<List<Position>> PositionRooms => this.positionRooms;

        //Methods
        /// <summary>
        /// Used to create the board game by reading a predefined file
        /// </summary>
        /// <param name="fileName">string that represents the file's name</param>
        /// <returns>matrix of caractere created by reading the file</returns>
        public char[,] ReadFile(string fileName)
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
        /// Used to initializes the list of each door's postion for every room
        /// </summary>
        public void InitializationRooms()
        {
            positionRooms.Add(new List<Position> { new Position(4, 18), new Position(0, 17) });//kitchen
            positionRooms.Add(new List<Position> { new Position(18, 20), new Position(23, 18) });//lounge
            positionRooms.Add(new List<Position> { new Position(17, 10), new Position(17, 13), new Position(19, 8), new Position(19, 15) });//ball room
            positionRooms.Add(new List<Position> { new Position(8, 17), new Position(12, 16) });//dinning room
            positionRooms.Add(new List<Position> { new Position(4, 9), new Position(5, 11), new Position(5, 12) });//hall
            positionRooms.Add(new List<Position> { new Position(3, 5), new Position(0, 6) });//billard
            positionRooms.Add(new List<Position> { new Position(8, 6), new Position(10, 2) });//library
            positionRooms.Add(new List<Position> { new Position(12, 1), new Position(15, 5) });//study
            positionRooms.Add(new List<Position> { new Position(20, 5), new Position(23, 6) });//greenhouse
        }
        /// <summary>
        /// Used to print the game board 
        /// </summary>
        public void PrintBoard()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string column = "1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 ";
            Console.WriteLine(column);
            int row = 1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    char path = board[i, j].Path;
                    char roomName = board[i, j].RoomName;
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
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Used to know if there is a secret passage in the room
        /// </summary>
        /// <param name="pos">position of the player's pawn</param>
        /// <returns>bool that represents the status of the room (with secret passage or mot) </returns>
        public bool IsSecretPassage(Position pos)
        {
            bool res = false;
            if (pos.IsEquals(new Position(3, 5)) == true || pos.IsEquals(new Position(20, 5)) == true || pos.IsEquals(new Position(4, 18)) == true || pos.IsEquals(new Position(18, 20)) == true)
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// Used to know if the square is occupied by another player
        /// </summary>
        /// <param name="pos">position of the player</param>
        /// <returns>bool that represents the status of the square (occupied or not)</returns>
        public bool IsOccupied(Position pos)
        {
            bool occupied = false;
            if (board[pos.Row,pos.Column].Path=='R'|| board[pos.Row, pos.Column].Path == 'G' || board[pos.Row, pos.Column].Path == 'B' 
                || board[pos.Row, pos.Column].Path == 'W' || board[pos.Row, pos.Column].Path == 'P' || board[pos.Row, pos.Column].Path == 'Y' )
            {
                occupied = true;
            }
            return occupied;
        }
        /// <summary>
        /// Used to know if the square is a wall or stairs
        /// </summary>
        /// <param name="pos">position of the player</param>
        /// <returns>bool that represents the status of the square (path or not)</returns>
        public bool IsWallOrStairs(Position pos)
        {
            bool blocked = false;
            if(board[pos.Row, pos.Column].Path == '.' || board[pos.Row, pos.Column].Path == 'X')
            {
                blocked = true;
            }
            return blocked;
        }
        /// <summary>
        /// Used to know if the square is a door of a room
        /// </summary>
        /// <param name="pos">position of the player</param>
        /// <returns>bool that represents the status of the square</returns>
        public bool InsideRoom(Position pos)
        {
            bool inside = false;
            if (board[pos.Row, pos.Column].RoomName == '!' || board[pos.Row, pos.Column].RoomName == '?')
            {
                inside = true;
            }
            return inside;
        }
        /// <summary>
        /// Function with lambda expression that permits to know if the position if outside the game board
        /// </summary>

        public Func<Position, bool> ValidPos = (pos) => (pos.Row<24)&&(pos.Row>=0)&& (pos.Column < 24)&&(pos.Column>=0);
        /// <summary>
        /// Used to mark the player's pawn by changing the matrix of the game board
        /// </summary>
        /// <param name="currentPos">current position of the player </param>
        /// <param name="futurePos">future position of the player </param>
        public void MarkMove(Position currentPos, Position futurePos)
        {
            this.board[futurePos.Row, futurePos.Column].Path = this.board[currentPos.Row, currentPos.Column].Path;
            
            if ((board[currentPos.Row, currentPos.Column].Path == 'Y'||
                board[currentPos.Row, currentPos.Column].Path == 'G'||
                board[currentPos.Row, currentPos.Column].Path == 'P'||
                board[currentPos.Row, currentPos.Column].Path == 'B'||
                board[currentPos.Row, currentPos.Column].Path == 'R'||
                board[currentPos.Row, currentPos.Column].Path == 'W')&& board[currentPos.Row, currentPos.Column].RoomName != '?')
            {
                this.board[currentPos.Row, currentPos.Column].Path = '_';
            }
           if((board[currentPos.Row, currentPos.Column].Path == 'Y' ||
                board[currentPos.Row, currentPos.Column].Path == 'G' ||
                board[currentPos.Row, currentPos.Column].Path == 'P' ||
                board[currentPos.Row, currentPos.Column].Path == 'B' ||
                board[currentPos.Row, currentPos.Column].Path == 'R' ||
                board[currentPos.Row, currentPos.Column].Path == 'W')&& board[currentPos.Row, currentPos.Column].RoomName == '?')
            {
                this.board[currentPos.Row, currentPos.Column].Path = '.';
            }
            
            
        }
        /// <summary>
        /// Used by player to move from a room to another by using secret passage
        /// </summary>
        /// <param name="current">
        /// <returns>position of the player</returns>
        public Position MoveSecretPassage(Position current)
        {
            Position newRoom = new Position();
            int choice;
            do
            {
                
                do
                {
                    Console.WriteLine("Where do you want to go? (6: Billard Room, 1: Kitchen,  9: Greenhouse, 2: Lounge)");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice != 6 && choice != 1 && choice != 9 && choice != 2);
                switch (choice)
                    {
                        case 6:
                            newRoom = new Position(0, 6);
                            break;
                        case 1:
                            newRoom = new Position(0, 17);
                            break;
                        case 9:
                            newRoom = new Position(23, 5);
                            break;
                        case 2:
                            newRoom = new Position(23, 18);
                            break;
                    }
                
                if (IsOccupied(newRoom) == true)
                {
                    Console.WriteLine("You can't go inside the room : it's occupied");
                }
                else if (AlreadyInTheRoom(current,choice))
                {
                    Console.WriteLine("Choose another room : you're already inside this room");
                }
            } while (AlreadyInTheRoom(current, choice) == true || IsOccupied(newRoom) == true);
            return newRoom;
           
        }
        public bool AlreadyInTheRoom(Position current, int choice)
        {
            bool response = false;
            for(int i=0;i<positionRooms[choice-1].Count;i++)
            {
                if(positionRooms[choice-1][i].IsEquals(current))
                {
                    response = true;
                }
            }
            return response;
        }

        /// <summary>
        /// Used to delete the mark of the pawn's player once he has made a false accusation
        /// </summary>
        /// <param name="pos">last position of the player</param></param>
        public void DeleteMark(Position pos)
        {
            if ((board[pos.Row, pos.Column].Path == 'Y' ||
                board[pos.Row, pos.Column].Path == 'G' ||
                board[pos.Row, pos.Column].Path == 'P' ||
                board[pos.Row, pos.Column].Path == 'B' ||
                board[pos.Row, pos.Column].Path == 'R' ||
                board[pos.Row, pos.Column].Path == 'W') && board[pos.Row, pos.Column].RoomName=='?'){
                this.board[pos.Row, pos.Column].Path = '.';
            }
            if ((board[pos.Row, pos.Column].Path == 'Y' ||
                board[pos.Row, pos.Column].Path == 'G' ||
                board[pos.Row, pos.Column].Path == 'P' ||
                board[pos.Row, pos.Column].Path == 'B' ||
                board[pos.Row, pos.Column].Path == 'R' ||
                board[pos.Row, pos.Column].Path == 'W') && board[pos.Row, pos.Column].RoomName != '?')
            {
                this.board[pos.Row, pos.Column].Path = '_';
            }
        }
        public Position MoveSecretPassageSocket(Position current, Socket client)
        {
            Position newRoom = new Position();
            int choice;
            do
            {
                
                do
                {
                    Server.SendToClient(1, "Where do you want to go ? (6: Billard Room, 1: Kitchen,  9: Greenhouse, 2: Lounge)", client);
                    choice = Convert.ToInt32(Server.ReceiveFromClient(client));
                } while (choice != 6 && choice != 1 && choice != 9 && choice != 2);
                switch (choice)
                {
                    case 6:
                        newRoom = new Position(0, 6);
                        break;
                    case 1:
                        newRoom = new Position(0, 17);
                        break;
                    case 9:
                        newRoom = new Position(23, 5);
                        break;
                    case 2:
                        newRoom = new Position(23, 18);
                        break;
                }

                if (IsOccupied(newRoom) == true)
                {
                    Console.WriteLine("You can't go inside the room : it's occupied");
                }
                else if (AlreadyInTheRoom(current, choice))
                {
                    Console.WriteLine("Choose another room : you're already inside this room");
                }
            } while (AlreadyInTheRoom(current, choice) == true || IsOccupied(newRoom) == true);
            return newRoom;

        }
        public override string ToString()
        {
            string test = "";
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    test = test + board[i, j].Path;
                }
                
            }
            return test;
        }
    }
}
