using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;
using System.Net.Sockets;

namespace FinalProject
{
    class Player
    {
        string name;
        Socket playerSocket;
        int numberOfCards;
        int id;
        List<Card> handtrail;
        Position pos;
        List<Card> stillSuspected;
        List<List<Card>> allHypothesis;
        bool accusation;
        #region constructors
        //constructor of a new player
        public Player(string name,int player,int nbPlayers,GameBoard game, Socket client=null)
        {
            this.playerSocket = client;
            this.name = name;
            this.id = player;
            if (nbPlayers == 2) this.numberOfCards = 9;
            else if (nbPlayers == 3) this.numberOfCards = 6;
            else if (((nbPlayers == 4 || nbPlayers == 5) && (player == 1 || player == 2)) || (nbPlayers == 5 && player == 3)) this.numberOfCards = 4;
            else this.numberOfCards = 3;
            this.handtrail = new List<Card>(numberOfCards);
            for(int i = 0; i < 24; i++)
            {
                for(int j=0;j<24;j++)
                {
                    if ((name == "Col Mustard" && game.Board[i, j].Path == 'Y')||
                        (name == "Mr Green" && game.Board[i, j].Path == 'G')||
                        (name == "Prof Plum" && game.Board[i, j].Path == 'P')||
                        (name == "Mrs Blue" && game.Board[i, j].Path == 'B')||
                        (name == "Miss Scarlet" && game.Board[i, j].Path == 'R')||
                        (name == "Mrs White" && game.Board[i, j].Path == 'W')) this.pos=new Position(i,j) ;
                    
                }
            }
            this.stillSuspected = CardsManager.cardsSuspects.FamilyCards.Concat(CardsManager.cardsWeapons.FamilyCards.Concat(CardsManager.cardsRooms.FamilyCards.ToList())).ToList();
            this.allHypothesis = new List<List<Card>>();
            this.accusation = false;

        }
        // constructor by default
        public Player() { }
        //constructor with partial informations
        public Player(int id,string name)
        {
            this.id = id;
            this.name = name;
        }
        //constructor from file
        public Player(string fileName)
        {
            string[] line = File.ReadAllLines(fileName+".csv");
            //var file = new StreamReader(File.OpenRead(fileName));
            //List<string> line = new List<string>(7);
            //List < List<string> > values = new List<List<string>>();
            /*for(int i =0;i<(7+allHypothesis.Count); i ++)
            {
                line[i] = file.ReadLine();
            }*/
            this.name = line[0];
            this.id = Convert.ToInt32(line[1]);
            string[] values = line[2].Split(';');
            this.pos = new Position(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
            //Console.WriteLine(values[0] + " " + values[1]);
            //this.pos.Row = Convert.ToInt32(values[0]);
            //this.pos.Column = Convert.ToInt32(values[1]);
            this.numberOfCards = Convert.ToInt32(line[3]);
            values = line[4].Split(';');
            handtrail = new List<Card>();
            for(int i =0; i<values.Length;i++)
            {
                for(int j=0;j<21;j++)
                {
                    if(Convert.ToInt32(values[i])== CardsManager.AllCards[j].ID)
                    {
                        handtrail.Add(CardsManager.AllCards[j]);
                        break;
                    }
                }
            }
            values = line[5].Split(';');
            stillSuspected = new List<Card>();
            for(int i =0;i<values.Length;i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Convert.ToInt32(values[i]) == CardsManager.AllCards[j].ID)
                    {
                        stillSuspected.Add(CardsManager.AllCards[j]);
                        break;
                    }
                }
            }
            allHypothesis = new List<List<Card>>();
            List<Card> cards = new List<Card>();
            for(int i =6;i<line.Length-1;i++)
            {
                values = line[i].Split(';');
                for(int r = 0;r<3;r++)
                {
                    for(int j =0;j<21;j++)
                    {
                        if(Convert.ToInt32(values[r])==CardsManager.AllCards[j].ID)
                        {
                            cards.Add(CardsManager.AllCards[j]);
                            
                            //allHypothesis.Add(CardsManager.AllCards[j]);
                            break;
                        }
                    }
                }
                allHypothesis.Add(cards);
                allHypothesis.Clear();
            }
            this.accusation = Convert.ToBoolean(line[line.Length - 1]);



        }
        #endregion
        //Properties
        public string Name => name;
        public Socket PlayerSocket => playerSocket;
        public int Id => id;
        public List<Card> Handtrail { get { return this.handtrail; } set { this.handtrail = value; } }
        public int NumberOfCards { get { return this.numberOfCards; } }
        public Position Pos { get { return this.pos; }set { this.pos = value; } }
        public List<Card> StillSuspected { get { return this.stillSuspected; } set { this.stillSuspected = value; } }
        public List<List<Card>> AllHypothesis { get { return this.allHypothesis; } set { this.allHypothesis = value; } }
        public bool Accusation { get { return this.accusation; } set { this.accusation = value; } }
        //Methods
        /// <summary>
        /// Used to move the player's pawn using the result of his die rolls
        /// </summary>
        /// <param name="move">integer that represents the result of his rolls die</param>
        /// <param name="board">represents the board</param>
        public void NextMove(int move, GameBoard game)
        {
            char direction;
            string UpperDirection;
            int choice;
            ConsoleKeyInfo key;
            Position next=new Position();//Voir constructeur vide
            if (Stuck(game)==true)
            {
                Console.WriteLine("You're stuck ! You have to wait for the next round to move");
            }
            else
            {
               
                for (int i = 0; i < move; i++)
                {
                    Console.WriteLine(this.name + "\n"+ (move-i) + " move left");
                    game.PrintBoard();
                    do
                    {

                        do
                        {
                            Console.WriteLine("Where do you want to go ? (U = up, D = down, R = right, L = left) \n");
                            key = Console.ReadKey();
                        } while (key.Key != ConsoleKey.U && key.Key != ConsoleKey.D && key.Key != ConsoleKey.R && key.Key != ConsoleKey.L);
                        UpperDirection = Convert.ToString(key.KeyChar).ToUpper();
                        direction = Convert.ToChar(UpperDirection);
                        //direction = Convert.ToChar(Console.ReadLine().ToUpper());
                        if (direction == 'U') next = new Position(pos.Row - 1, pos.Column);
                        if (direction == 'D') next = new Position(pos.Row + 1, pos.Column);
                        if (direction == 'R') next = new Position(pos.Row, pos.Column + 1);
                        if (direction == 'L') next = new Position(pos.Row, pos.Column - 1);
                        if (game.ValidPos(next) == false) { Console.WriteLine("You can't go there : it's outside the board game"); }
                        else if (game.IsOccupied(next) == true) { Console.WriteLine("You can't go there : it's occupied"); }
                        else if (game.IsWallOrStairs(next) == true) { Console.WriteLine("You can't go there : it's a wall or stairs"); }

                    } while ((direction != 'U' && direction != 'D' && direction != 'R' && direction != 'L') || game.ValidPos(next) != true || game.IsOccupied(next) != false || game.IsWallOrStairs(next) != false);
                    game.MarkMove(this.pos, next);
                    this.pos = next;
                    if(game.InsideRoom(this.pos)==true)
                    {
                        Console.Clear();
                        Console.WriteLine("You're in a room");
                        if (game.IsSecretPassage(this.pos) == true)
                        {
                            game.PrintBoard();
                            choice = Program.VerificationInputConsole("There is a secret passage in this room\nDo you want to use it? (1: yes, 2 : no)",1,2);
                            /*do
                            {
                                Console.WriteLine("There is a secret passage in this room");
                                Console.WriteLine("Do you want to use it? (1: yes, 2 : no)");
                                choice = Convert.ToInt32(Console.ReadLine());
                            } while (choice != 1 && choice != 2);*/
                            if (choice == 1)
                            {
                                next = game.MoveSecretPassage(this.pos);
                                game.MarkMove(this.pos, next);
                                this.pos = next;
                            }
                            Console.Clear();
                        }
                        break;
                    }
                    Console.Clear();
                }
            }
            Console.Clear();
            game.PrintBoard();

        }
        /// <summary>
        /// Used to know if the square around the player are avaiblable to him to move
        /// </summary>
        /// <param name="game">represents game board</param>
        /// <returns>bool that represent the status of the player (stuck or free to move)</returns>
        public bool Stuck(GameBoard game)
        {
            bool stuck = false;
            Position up = new Position(this.pos.Row - 1, this.pos.Column);
            Position down = new Position(this.pos.Row + 1, this.pos.Column);
            Position right = new Position(this.pos.Row, this.pos.Column + 1);
            Position left = new Position(this.pos.Row, this.pos.Column - 1);
            if ((game.ValidPos(up)==false ||game.IsOccupied(up)==true||game.IsWallOrStairs(up)==true)&&
                (game.ValidPos(down) == false || game.IsOccupied(down) == true || game.IsWallOrStairs(down) == true) &&
                (game.ValidPos(right) == false || game.IsOccupied(right) == true || game.IsWallOrStairs(right) == true) &&
                (game.ValidPos(left) == false || game.IsOccupied(left) == true || game.IsWallOrStairs(left) == true)) {
                stuck = true;
            }
            return stuck;
        }
        /// <summary>
        /// Used to print a list of Card
        /// </summary>
        /// <param name="list">list of Card</param>
        /// <returns>string that represent used to print the list</returns>
        public void NextMoveSocket(int move, GameBoard game)
        {
            char direction;
            int choice;
            Position next = new Position();//Voir constructeur vide

            if (Stuck(game) == true)
            {
                Thread.Sleep(100);
                Server.SendToClient(2, "You're stuck ! You have to wait for the next round to move", this.playerSocket);//Console.WriteLine("You're stuck ! You have to wait for the next round to move");
            }
            else
            {

                for (int i = 0; i < move; i++)
                {
                    Thread.Sleep(100);
                    Server.SendBoardToClients(game);//board.PrintBoard();
                    Thread.Sleep(100);
                    Server.SendToClient(2, this.name + "\n" + (move - i) + " move left", playerSocket);
                    Thread.Sleep(100);
                    do
                    {
                        Thread.Sleep(200);

                        //Console.WriteLine("Where do you want to go ? (U = up, D = down, R = right, L = left)");
                        Server.SendToClient(3, "Where do you want to go ? (U = up, D = down, R = right, L = left)", this.playerSocket);
                        direction = Server.ReceiveFromClient(this.playerSocket).ToUpper().ToCharArray()[0];
                        if (direction == 'U') next = new Position(pos.Row - 1, pos.Column);
                        else if (direction == 'D') next = new Position(pos.Row + 1, pos.Column);
                        else if (direction == 'R') next = new Position(pos.Row, pos.Column + 1);
                        else if (direction == 'L') next = new Position(pos.Row, pos.Column - 1);
                        if (!game.ValidPos(next)) { Server.SendToClient(2, "You can't go there : it's outside the board game", this.playerSocket); }//Console.WriteLine("You can't go there : it's outside the board game")
                        else if (game.IsOccupied(next)) { Server.SendToClient(2, "You can't go there : it's occupied", this.playerSocket); }//Console.WriteLine("You can't go there : it's occupied")
                        else if (game.IsWallOrStairs(next)) { Server.SendToClient(2, "You can't go there : it's a wall or stairs", this.playerSocket); }//Console.WriteLine("You can't go there : it's a wall or stairs")
                        else { Server.SendToClient(2, "Wrong input", this.playerSocket); }
                    } while ((direction != 'U' && direction != 'D' && direction != 'R' && direction != 'L') || !game.ValidPos(next) || game.IsOccupied(next) || game.IsWallOrStairs(next));
                    game.MarkMove(this.pos, next);
                    this.pos = next;
                    if (game.InsideRoom(this.pos) == true)
                    {
                        //Console.Clear();
                        //Console.WriteLine("You're in a room");
                        if (game.IsSecretPassage(this.pos) == true)
                        {
                            Server.SendBoardToClients(game);//board.PrintBoard();

                            //Console.WriteLine("Do you want to use it? (1: yes, 2 : no)");
                            choice = Convert.ToInt32(GameMultiPlayer.VerificationInputConsoleSocket("There is a secret passage in this room\nDo you want to use it (1: yes, 2: no)", 1, 2, this.playerSocket));
                            if (choice == 1)
                            {
                                next = game.MoveSecretPassageSocket(this.pos, this.playerSocket);
                                game.MarkMove(this.pos, next);
                                this.pos = next;
                            }
                        }
                        break;
                    }
                }
            }
            Server.SendBoardToClients(game);//board.PrintBoard();

        }
        public string PrintList(List<Card> list)
        {
            string res = "";
            foreach (var line in list)
            {
                res = res + line.Name + " / ";
            }
            return res;
        }
        public override string ToString()
        {
            return "\tHandrail: "+ PrintList(this.handtrail)+"\n\nStill suspected : "+PrintList(this.stillSuspected);
        }
    }
}
