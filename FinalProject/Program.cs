using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FinalProject
{
    class Program
    {
        
        public readonly static Random random = new Random();
        public static int nbAccusations = 0;
        static void Main(string[] args)
        {
            Game();
            //test
            /*GameBoard board = new GameBoard();
            List<string> remainingCards = CardsManager.Initialization();
            List<Player> players = PlayerManager.Initialization(2, remainingCards, board);
            players[0].NextMove(8,board);
            Console.WriteLine(players[0].Pos.ToString());
            Console.ReadKey();*/

        }
        /// <summary>
        /// Code distribuer les cartes après avoir enlevé les cartes du meurtier
        /// </summary>
        
        /*
        static void ThreadProc()
        {
            Thread.Sleep(1000);
        }*/
        /*
        /// <summary>
        /// Test dés random pour mouvement non fonctionnel surement les thread
        /// </summary>
        static int Dice()
        {
            Func<Random, int> Die = (x) => x.Next(1, 7);
            int die1 = Die(new Random());

            Thread t = new Thread(ThreadProc);
            t.Start();
            int die2 = Die(new Random());
            Console.WriteLine("Dice results : " + die1 + " + " + die2 + " = " + (die1 + die2) + " moves");
            return die1 + die2;
        }*/
        /// <summary>
        /// Code début du jeu
        /// </summary>
       static void Game()
        {
            // Début de partie : affichage plateau
            GameBoard board = new GameBoard();
            board.PrintBoard();


            //Initialisation du jeu...
            List<string> remainingCards = CardsManager.Initialization();
            int nbPlayers = 0;
            do
            {
                Console.WriteLine("How many players are going to play (2,3,4,5,6) ?");
                nbPlayers = Convert.ToInt32(Console.ReadLine());
            } while (nbPlayers < 2 || nbPlayers >= 7);
            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            //...jusqu'à distribution des cartes

            //break si nbAccusations == nbplayers ou player.accusation == true à chaque round du joueur
            int round = 0;
            while(true)
            {
                Console.WriteLine("it's up to {0}", runningOrder[round].Name);
                Console.WriteLine("press enter to continue");
                Console.ReadKey();
                Die dices = new Die();
                int resultDices = dices.ResultDices();
                Console.WriteLine(resultDices);
                //if 66 or 11, player can choose his room 
                if(((dices.DieOne ==dices.DieTwo)&& (dices.DieOne==6))||((dices.DieOne==dices.DieTwo)&&(dices.DieOne==1)))
                {
                    PlayerManager.ChooseRoom(runningOrder[round], board);
                }
                else { runningOrder[round].NextMove(resultDices, board); }
                bool insideARoom = board.InsideRoom(runningOrder[round].Pos);
                if(insideARoom ==true)
                {
                    Console.WriteLine(runningOrder[round].ToString());
                    int choice=0;
                    do
                    {
                        Console.WriteLine("Do you want to do a \n1: An hypothesis \n2: An accusation");
                        choice = Convert.ToInt32(Console.ReadLine());
                    } while (choice < 1 || choice >= 3);
                    List<string> CardsSuspectedByTheCurrentPlayer = new List<string>();
                    List<string> CardsSuspectedPresentInTheOtherPlayerHandrail = new List<string>();
                    if(choice ==1)
                    {
                        //cards Suspected by the current player
                        //find the room of the player
                        for(int i =0;i<9;i++)
                        {
                            foreach(var j in board.Rooms[i])
                            {
                                if(runningOrder[round].Pos == j)
                                {
                                    string currentRoom = board.NameOfTheRoom[i];
                                    CardsSuspectedByTheCurrentPlayer.Add(currentRoom);
                                    break;
                                }
                            }
                        }
                        //suspect person of the current player
                        Console.WriteLine("which person do you suspected?");
                        string cardSuspected = Console.ReadLine();
                        CardsSuspectedByTheCurrentPlayer.Add(cardSuspected);
                        //suspect weapon of the current player
                        Console.WriteLine("which weapon do you suspected?");
                        cardSuspected = Console.ReadLine();
                        CardsSuspectedByTheCurrentPlayer.Add(cardSuspected);

                        //search of the cards in handrail of each player to find the suspect card to show at the current player
                        foreach(Player p in players)
                        {
                            if(p != runningOrder[round])
                            {
                                foreach(string nameCards in p.Handtrail)
                                {
                                    foreach (string hypothesesCards in CardsSuspectedByTheCurrentPlayer)
                                    {
                                        if (nameCards == hypothesesCards)
                                        {
                                            CardsSuspectedPresentInTheOtherPlayerHandrail.Add(hypothesesCards);
                                        }
                                    }
                                }
                                foreach(string card in CardsSuspectedPresentInTheOtherPlayerHandrail)
                                {
                                    Console.WriteLine(card + " ");
                                }
                                int nbOfCardsSuspectedInYourPossession = CardsSuspectedPresentInTheOtherPlayerHandrail.Count;
                                int response = 0;
                                for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                                {

                                    do
                                    {
                                        Console.WriteLine("Do you want to show the card {0}? \n0:yes \n1:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i]);
                                        response = Convert.ToInt32(Console.ReadLine());
                                    } while (response < 0 || response >= 2);
                                    if (response == 0)
                                    {
                                        Console.WriteLine("the card is {0}", CardsSuspectedPresentInTheOtherPlayerHandrail[i]);
                                        for (int n =0;n<runningOrder[round].StillSuspected.Count;n++)
                                        {
                                            if(runningOrder[round].StillSuspected[n] ==CardsSuspectedPresentInTheOtherPlayerHandrail[i])
                                            {
                                                runningOrder[round].StillSuspected.RemoveAt(n);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    
                }


                if(round<nbPlayers-1)
                {
                    round++;
                }
                else { round = 0; }
            }
            

            

        }
        /*static void Main(string[] args)
        {
            //Game();

            
            //Test mouvement fonctionnel  
            GameBoard board = new GameBoard("ColorBoard.csv");
            
            Player p1 = CardsDistribution(1, 2, board);
            Player p2 = CardsDistribution(2, 2, board);
            Console.WriteLine(p1.ToString() + "\n" + p2.ToString());
            Console.Clear();
            board.PrintBoard(board);
            p2.NextMove(7);
            p1.NextMove(8);
            
            p2.NextMove(1);
            Console.ReadKey();
            Console.Clear();
            board.PrintBoard(board);

            Console.ReadKey();
        }*/
    }
}
