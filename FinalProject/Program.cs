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
           /* GameBoard board = new GameBoard();
            board.PrintBoard();


            //Initialisation du jeu...
            List<Card> remainingCards = CardsManager.Initialization();
            int nbPlayers = 0;
            do
            {
                Console.WriteLine("How many players are going to play (2,3,4,5,6) ?");
                nbPlayers = Convert.ToInt32(Console.ReadLine());
            } while (nbPlayers < 2 || nbPlayers >= 7);
            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();*/
            //...jusqu'à distribution des cartes
            #region test 2
            /*GameBoard board = new GameBoard();
            board.PrintBoard();
            List<Card> remainingCards = CardsManager.Initialization();
            int nbPlayers = 0;
            do
            {
                Console.WriteLine("How many players are going to play (2,3,4,5,6) ?");
                nbPlayers = Convert.ToInt32(Console.ReadLine());
            } while (nbPlayers < 2 || nbPlayers >= 7);
            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            Console.WriteLine(Cards.PrintList(remainingCards));
            Console.WriteLine("handrail 1: " + Cards.PrintList(runningOrder[0].Handtrail) + "\n handrail 2 : " + Cards.PrintList(runningOrder[1].Handtrail));*/
            #endregion
            /*Console.WriteLine(Cards.PrintList(CardsManager.Initialization()));
            Console.WriteLine(Cards.PrintList(CardsManager.AllCards));*/
            #region test1
            /*List<Card> CardsSuspectedByTheCurrentPlayer = new List<Card>();
            //suspect person of the current player
            Console.WriteLine("which person do you suspected?Tape the ID of the card that you choose");
            Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
            int cardSuspectHypothesis = Convert.ToInt32(Console.ReadLine());
            //suspect weapon of the current player
            Console.WriteLine("which weapon do you suspected?");
            int cardWeaponHypothesis = Convert.ToInt32(Console.ReadLine());
            var r = from suspect in CardsManager.AllCards
                    where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                    select suspect;
            foreach (var i in r)
            {
                CardsSuspectedByTheCurrentPlayer.Add(i);
            }
            foreach (var i in CardsSuspectedByTheCurrentPlayer)
            {
                Console.WriteLine(i.Name);
            }*/
            #endregion
            Console.ReadKey();
            /*Console.WriteLine("which person do you suspected?Tape the ID of the card that you choose");
            Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
            int cardSuspected = Convert.ToInt32(Console.ReadLine());
            var r = from suspect in CardsManager.cardsSuspects.FamilyCards
                    where suspect.ID == cardSuspected
                    select suspect;
            r.Select(0);*/

            //CardsSuspectedByTheCurrentPlayer.Add();
            //test
            /*GameBoard board = new GameBoard();
            List<Card> remainingCards = CardsManager.Initialization();
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
            List<Card> remainingCards = CardsManager.Initialization();
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
                Console.WriteLine("you obtain a" + resultDices + "to your thrown");
                //if 66 or 11, player can choose his room 
                if(((dices.DieOne ==dices.DieTwo)&& (dices.DieOne==6))||((dices.DieOne==dices.DieTwo)&&(dices.DieOne==1)))
                {
                    PlayerManager.ChooseRoom(runningOrder[round], board);
                }
                else { runningOrder[round].NextMove(resultDices, board); }
                bool insideARoom = board.InsideRoom(runningOrder[round].Pos);
                if (insideARoom ==true)
                {
                    Console.WriteLine(runningOrder[round].ToString());
                    int choice=0;
                    do
                    {
                        Console.WriteLine("Do you want to do a \n1: An hypothesis \n2: An accusation");
                        choice = Convert.ToInt32(Console.ReadLine());
                    } while (choice < 1 || choice >= 3);
                    List<Card> CardsSuspectedByTheCurrentPlayer = new List<Card>();
                    List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail = new List<Card>();
                    if(choice ==1)
                    {
                        //cards Suspected by the current player
                        //find the room of the player
                        for(int i =0;i<9;i++)
                        {
                            foreach(var j in board.PositionRooms[i])
                            {
                                if(runningOrder[round].Pos == j)
                                {
                                    Card currentRoom = CardsManager.cardsRooms.FamilyCards[i + 1];
                                    CardsSuspectedByTheCurrentPlayer.Add(currentRoom);
                                    break;
                                }
                            }
                        }
                        //suspect person of the current player
                        Console.WriteLine("which person do you suspected?Tape the ID of the card that you choose");
                        Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
                        int cardSuspectHypothesis = Convert.ToInt32(Console.ReadLine());
                        //suspect weapon of the current player
                        Console.WriteLine("which weapon do you suspected?");
                        Console.WriteLine(Cards.PrintList(CardsManager.cardsWeapons.FamilyCards));
                        int cardWeaponHypothesis = Convert.ToInt32 (Console.ReadLine());
                        //creation of a list of hypothesis from the selection of the current player
                        var r = from suspect in CardsManager.AllCards
                                where suspect.ID == cardSuspectHypothesis || suspect.ID ==cardWeaponHypothesis
                                select suspect;
                        foreach (var i in r)
                        {
                            CardsSuspectedByTheCurrentPlayer.Add(i);
                        }
                        //search of the cards in handrail of each player to find the suspect card to show at the current player
                        foreach(Player p in players)
                        {
                            if(p != runningOrder[round])
                            {
                                foreach(Card card in p.Handtrail)
                                {
                                    foreach (Card hypothesesCards in CardsSuspectedByTheCurrentPlayer)
                                    {
                                        if (card.ID == hypothesesCards.ID)
                                        {
                                            CardsSuspectedPresentInTheOtherPlayerHandrail.Add(hypothesesCards);
                                        }
                                    }
                                }
                                /*foreach(Card card in CardsSuspectedPresentInTheOtherPlayerHandrail)
                                {
                                    Console.WriteLine(card.Name + "-" + card.ID);
                                }*/
                                int nbOfCardsSuspectedInYourPossession = CardsSuspectedPresentInTheOtherPlayerHandrail.Count;
                                int response = 0;
                                for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                                {

                                    do
                                    {
                                        Console.WriteLine("Do you want to show the card {0}? \n0:yes \n1:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);
                                        response = Convert.ToInt32(Console.ReadLine());
                                    } while (response < 0 || response >= 2);
                                    if (response == 0)
                                    {
                                        Console.WriteLine("the card is {0}", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);
                                        for (int n =0;n<runningOrder[round].StillSuspected.Count;n++)
                                        {
                                            if(runningOrder[round].StillSuspected[n].ID ==CardsSuspectedPresentInTheOtherPlayerHandrail[i].ID)
                                            {
                                                runningOrder[round].StillSuspected.RemoveAt(n);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                Console.WriteLine(Cards.PrintList(runningOrder[round].StillSuspected));
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
