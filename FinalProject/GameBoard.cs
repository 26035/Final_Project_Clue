using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    class GameBoard
    {
        //Attribut
        Square[,] board;
        List<List<Position>> rooms;
        //Constructor
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
            rooms = new List<List<Position>>();
            Position billardRoomDoor1 = new Position(3, 5);
            Position kitchenDoor1 = new Position(4, 18);
            Position greenhouse1 = new Position(20, 5);
            Position loungeDoor1 = new Position(18, 20);
            Position hall1 = new Position(4, 9);
            Position hall2 = new Position(5, 12);
            Position hall3 = new Position(6, 13);
            Position library1 = new Position(8, 6);
            Position library2 = new Position(10, 2);
            Position study1 = new Position(12, 2);
            Position study2 = new Position(15, 5);
            Position dinningRoom1 = new Position(8, 17);
            Position dinningRoom2 = new Position(12, 16);
            Position ballRoom1 = new Position(17, 10);
            Position ballRoom2 = new Position(17, 13);
            Position ballRoom3 = new Position(19, 8);
            Position ballRoom4 = new Position(19, 15);
            rooms.Add(new List<Position> { billardRoomDoor1 });
            rooms.Add(new List<Position> { kitchenDoor1 });
            rooms.Add(new List<Position> { greenhouse1 });
            rooms.Add(new List<Position> { loungeDoor1 });
            rooms.Add(new List<Position> { hall1, hall2, hall3 });
            rooms.Add(new List<Position> { library1, library2 });
            rooms.Add(new List<Position> { study1, study2 });
            rooms.Add(new List<Position> { dinningRoom1, dinningRoom2 });
            rooms.Add(new List<Position> { ballRoom1, ballRoom2, ballRoom3, ballRoom4 });
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
        public List<List<Position>> Rooms => this.rooms;

        //Methods
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

        public bool IsSecretPassage(Position pos)
        {
            bool res = false;
            if (pos.IsEquals(new Position(3, 5)) == true || pos.IsEquals(new Position(20, 5)) == true || pos.IsEquals(new Position(4, 18)) == true || pos.IsEquals(new Position(18, 20)) == true)
            {
                res = true;
                Console.WriteLine("There is a secret passage in this room");
            }
            return res;
        }
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
        public bool IsWallOrStairs(Position pos)
        {
            bool blocked = false;
            if(board[pos.Row, pos.Column].Path == '.' || board[pos.Row, pos.Column].Path == 'X')
            {
                blocked = true;
            }
            return blocked;
        }
        public bool InsideRoom(Position pos)
        {
            bool inside = false;
            if (board[pos.Row, pos.Column].RoomName == '!')
            {
                inside = true;
            }
            return inside;
        }

        public Func<Position, bool> ValidPos = (pos) => (pos.Row<24)&&(pos.Row>=0)&& (pos.Column < 24)&&(pos.Column>=0);
        
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
        public Position MoveSecretPassage(Position current)
        {
            Position newRoom = new Position();
            do
            {
                Console.WriteLine("Where do you want to go? (1 : Billard Room, 2 : Kitchen, 3 : Greenhouse, 4 : Lounge");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        newRoom = new Position(0, 6);
                        break;
                    case 2:
                        newRoom = new Position(0, 17);
                        break;
                    case 3:
                        newRoom = new Position(23, 6);
                        break;
                    case 4:
                        newRoom = new Position(23, 18);
                        break;
                    default:
                        break;
                }
                if (IsOccupied(newRoom) == true)
                {
                    Console.WriteLine("You can't go inside the room : it's occupied");
                }
                else if (current.IsEquals(newRoom) == true)
                {
                    Console.WriteLine("Choose another room : you're already inside this room");
                }
            } while (current.IsEquals(newRoom) == true || IsOccupied(newRoom) == true);
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
