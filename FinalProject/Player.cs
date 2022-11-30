using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace FinalProject
{
    class Player
    {
        string name;
        int numberOfCards;
        int id;
        List<Card> handtrail;
        Position pos;
        GameBoard game;
        List<Card> stillSuspected;
        List<List<Card>> allHypothesis;
        bool accusation;
        //Constructor
        public Player(string name,int player,int nbPlayers,GameBoard game)
        {
            this.name = name;
            this.id = player;
            if (nbPlayers == 2) this.numberOfCards = 9;
            else if (nbPlayers == 3) this.numberOfCards = 6;
            else if (((nbPlayers == 4 || nbPlayers == 5) && (player == 1 || player == 2)) || (nbPlayers == 5 && player == 3)) this.numberOfCards = 4;
            else this.numberOfCards = 3;
            this.handtrail = new List<Card>(numberOfCards);
            this.game = game;
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
        public Player() { }
        //Properties
        public string Name => name;
        public int Id => id;
        public List<Card> Handtrail { get { return this.handtrail; } set { this.handtrail = value; } }
        public int NumberOfCards { get { return this.numberOfCards; } }
        public Position Pos { get { return this.pos; }set { this.pos = value; } }
        public List<Card> StillSuspected { get { return this.stillSuspected; } set { this.stillSuspected = value; } }
        public List<List<Card>> AllHypothesis { get { return this.allHypothesis; } set { this.allHypothesis = value; } }
        public bool Accusation { get { return this.accusation; } set { this.accusation = value; } }
        //Methods
        public void NextMove(int move, GameBoard board)
        {
            char direction;
            int choice;
            Position next=new Position();//Voir constructeur vide
            if (Stuck()==true)
            {
                Console.WriteLine("You're stuck ! You have to wait for the next round to move");
            }
            else
            {
               
                for (int i = 0; i < move; i++)
                {
                    board.PrintBoard();
                    do
                    {

                        Console.WriteLine("Where do you want to go ? (U = up, D = down, R = right, L = left)");
                        direction = Convert.ToChar(Console.ReadLine().ToUpper());
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
                            board.PrintBoard();
                            Console.WriteLine("Do you want to use it? (1: yes, 2 : no)");
                            choice = Convert.ToInt32(Console.ReadLine());
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
            board.PrintBoard();

        }
        
        public bool Stuck()
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
            return "\tHandrail: "+ PrintList(this.handtrail)+"\nStill suspected : "+PrintList(this.stillSuspected);
        }
    }
}
