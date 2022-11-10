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
        char[,] board;
        //Constructor
        public GameBoard(string filename)
        {
            this.board = new char[24, 24];
            StreamReader lecture = new StreamReader(filename);
            int i = 0;
            while (!lecture.EndOfStream)
            {
                string line = lecture.ReadLine();
                string[] list = line.Split(';');
                for(int j=0;j<24;j++)
                {
                    this.board[i, j] = Convert.ToChar(list[j]);
                }
                i++;
            }
            
        }
        //Properties
        public char[,] Board
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

        //Methods
        public void PrintBoard(GameBoard board)
        {
            GameBoard room = new GameBoard("RoomNames.csv");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string column = "1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 ";
            Console.WriteLine(column);
            int row = 1;
            for (int i = 1; i < board.ToString().Length + 1; i++)
            {
                char color = board.ToString()[i - 1];

                Console.BackgroundColor = ConsoleColor.Black;
                if (color == 'R')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("   ");
                }
                if (color == 'G')
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("   ");
                }
                if (color == 'B')
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("   ");
                }
                if (color == 'Y')
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("   ");
                }
                if (color == 'W')
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("   ");
                }
                if (color == 'P')
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("   ");
                }
                if (color == '.')
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(room.ToString()[i - 1] + "  ");
                }
                if (color == 'X')
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(room.ToString()[i - 1] + "  ");
                }
                if (color == '_')
                {
                    if (room.ToString()[i - 1] == '!')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(color + "  ");
                }
                if (i % 24 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    if(row<10)
                    {
                        Console.WriteLine(" "+row+" ");
                    }
                    else
                    {
                        Console.WriteLine(row+" ");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    row++;
                }
            }
        }
       
        public bool IsSecretPassage()
        {
            bool res = false;

            return res;
        }//A terminer 
        public bool IsOccupied(Position pos)
        {
            bool occupied = false;
            if (board[pos.Row,pos.Column]=='R'|| board[pos.Row, pos.Column] == 'G' || board[pos.Row, pos.Column] == 'B' 
                || board[pos.Row, pos.Column] == 'W' || board[pos.Row, pos.Column] == 'P' || board[pos.Row, pos.Column] == 'Y' )
            {
                occupied = true;
            }
            return occupied;
        }
        
        public bool IsWallOrStairs(Position pos)
        {
            bool blocked = false;
            if(board[pos.Row, pos.Column] == '.' || board[pos.Row, pos.Column] == 'X')
            {
                blocked = true;
            }
            return blocked;
        }
        public bool InsideRoom(Position pos)
        {
            GameBoard room = new GameBoard("RoomNames.csv");
            bool inside = false;
            if (room.Board[pos.Row, pos.Column] == '!')
            {
                inside = true;
            }
            return inside;
        }

        public Func<Position, bool> ValidPos = (pos) => (pos.Row<24)&&(pos.Row>=0)&& (pos.Column < 24)&&(pos.Column>=0);
        
        public void MarkMove(Position currentPos, Position futurePos)
        {
            if (board[currentPos.Row, currentPos.Column] == 'Y') this.board[futurePos.Row, futurePos.Column] = 'Y';
            if (board[currentPos.Row, currentPos.Column] == 'G') this.board[futurePos.Row, futurePos.Column] = 'G';
            if (board[currentPos.Row, currentPos.Column] == 'P') this.board[futurePos.Row, futurePos.Column] = 'P';
            if (board[currentPos.Row, currentPos.Column] == 'B') this.board[futurePos.Row, futurePos.Column] = 'B';
            if (board[currentPos.Row, currentPos.Column] == 'R') this.board[futurePos.Row, futurePos.Column] = 'R';
            if (board[currentPos.Row, currentPos.Column] == 'W') this.board[futurePos.Row, futurePos.Column] = 'W';
            this.board[currentPos.Row, currentPos.Column] = '_';
            
        }
        public override string ToString()
        {
            string test = "";
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    test = test + board[i, j];
                }
                
            }
            return test;
        }
    }
}
