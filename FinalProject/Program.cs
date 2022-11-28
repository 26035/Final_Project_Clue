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
        public static Player winner = null;
        static void Main(string[] args)
        {
            Game();
            #region test initialisation joueur
            /*GameBoard board = new GameBoard();
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
            foreach(Player player in players)
            {
                Console.WriteLine(player.ToString());
            }
            Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();*/
            #endregion
            /*GameBoard board = new GameBoard();
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
            //List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            Console.WriteLine(players[0].ToString());
            Console.WriteLine(players[1].ToString());*/
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
            while(nbAccusations<players.Count || winner==null)
            {
                Console.WriteLine("it's up to {0}", runningOrder[round].Name);
                Console.WriteLine("press enter to continue");
                Console.ReadKey();
                Console.Clear();
                Die dices = new Die();
                int resultDices = dices.ResultDices();
                Console.WriteLine("you obtain a " + resultDices + " to your thrown");
                //if 66 or 11, player can choose his room 
                if (((dices.DieOne == dices.DieTwo) && (dices.DieOne == 6)) || ((dices.DieOne == dices.DieTwo) && (dices.DieOne == 1)))
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
                                if (runningOrder[round].Pos.IsEquals(j))
                                {
                                    Card currentRoom = CardsManager.cardsRooms.FamilyCards[i];
                                    CardsSuspectedByTheCurrentPlayer.Add(currentRoom);
                                    //CardsSuspectedByTheCurrentPlayer.Add(j);
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
                        Console.WriteLine(" you ask for this 3 cards : "+Cards.PrintList(CardsSuspectedByTheCurrentPlayer));
                        runningOrder[round].AllHypothesis.Add(CardsSuspectedByTheCurrentPlayer);
                        //search of the cards in handrail of each player to find the suspect card to show at the current player
                        Console.Clear();
                        foreach (Player p in players)
                        {
                            if(p != runningOrder[round])
                            {
                                CardsSuspectedPresentInTheOtherPlayerHandrail.Clear();
                                
                                
                                Console.WriteLine("{0} you will show a card, take the laptop and press enter to continue", p.Name);
                                Console.ReadKey();
                                Console.Clear();
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
                                bool showOneCard = false;
                                Card cardToShow= new Card();
                                do {
                                    for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                                    {

                                        do
                                        {
                                            Console.WriteLine("Do you want to show the card : {0}? \n1:yes \n2:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);
                                            response = Convert.ToInt32(Console.ReadLine());
                                        } while (response < 1 || response >= 3);

                                        if (response == 1 )
                                        {
                                            showOneCard = true;
                                            cardToShow = CardsSuspectedPresentInTheOtherPlayerHandrail[i];
                                            /*Console.Clear();
                                            Console.WriteLine("{0} you will be able to see the card, press enter to continue", runningOrder[round].Name) ;
                                            Console.ReadKey();
                                            Console.Clear();
                                            Console.WriteLine("the card is {0}", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);*/

                                            /*for (int n =0;n<runningOrder[round].StillSuspected.Count;n++)
                                            {
                                                if(runningOrder[round].StillSuspected[n].ID ==CardsSuspectedPresentInTheOtherPlayerHandrail[i].ID)
                                                {
                                                    runningOrder[round].StillSuspected.RemoveAt(n);
                                                    break;
                                                }
                                            }*/
                                            CardsManager.RemoveCardAt(runningOrder[round].StillSuspected, CardsSuspectedPresentInTheOtherPlayerHandrail[i]);//Fonction permettant de retirer une carte à vérifier par Lauryne
                                            break;
                                        }
                                        

                                    }
                                    if (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0) { Console.WriteLine("{0} You have to show one of your card", p.Name); }
                                } while (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0);
                                Console.Clear();
                                if(CardsSuspectedPresentInTheOtherPlayerHandrail.Count!=0)
                                {
                                    Console.WriteLine("{0} you will be able to see the card, press enter to continue", runningOrder[round].Name);
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.WriteLine("the card is {0}", cardToShow.Name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} you will be able to see the card, press enter to continue", runningOrder[round].Name);
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.WriteLine("The player have any card to show you");
                                    
                                    

                                }
                                Console.WriteLine("press enter to continue");
                                Console.ReadKey();
                                Console.Clear();
                                
                                
                            }
                        }
                        Console.WriteLine("{0} your list of cards which is suspected is now: \n" + Cards.PrintList(runningOrder[round].StillSuspected)+ "\n press enter to continue", runningOrder[round].Name);
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else
                    {
                        int response;
                        do
                        {
                            Console.WriteLine("Are you sure to be in the room of the murder? \n1: yes \n2: no");
                            response = Convert.ToInt32(Console.ReadLine());
                        } while (response < 1 || response > 2);

                        List<Card> Accusation = new List<Card>();
                        if (response == 1)
                        {
                            #region à mettre dans une fonction
                            //find the position of the room in which the player is
                            for (int i = 0; i < 9; i++)
                            {
                                foreach (var j in board.PositionRooms[i])
                                {
                                    if (runningOrder[round].Pos.IsEquals(j))
                                    {
                                        Card currentRoom = CardsManager.cardsRooms.FamilyCards[i];
                                        CardsSuspectedByTheCurrentPlayer.Add(currentRoom);
                                        break;
                                    }
                                }
                            }
                            //suspect person of the current player
                            Console.WriteLine("which person do you accuse? \n Tape the ID of the card that you choose");
                            Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
                            int cardSuspectHypothesis = Convert.ToInt32(Console.ReadLine());
                            //suspect weapon of the current player
                            Console.WriteLine("which weapon is the murder weapon for you?");
                            Console.WriteLine(Cards.PrintList(CardsManager.cardsWeapons.FamilyCards));
                            int cardWeaponHypothesis = Convert.ToInt32(Console.ReadLine());
                            //creation of a list of hypothesis from the selection of the current player
                            var r = from suspect in CardsManager.AllCards
                                    where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                                    select suspect;
                            foreach (var i in r)
                            {
                                CardsSuspectedByTheCurrentPlayer.Add(i);
                            }
                            Console.Clear();
                            Console.WriteLine("everybody can watch the cards selected by the player");
                            Console.WriteLine(" your accusation is: " + Cards.PrintList(CardsSuspectedByTheCurrentPlayer)+ "\n press enter to continue");
                            Console.ReadKey();
                            Console.Clear();
                            #endregion
                            bool rightAccusation = CardsManager.ComparisonAccusationAndMurder(CardsSuspectedByTheCurrentPlayer);
                            if(rightAccusation ==true)
                            {
                                
                                Console.WriteLine("{0} You win!!", runningOrder[round].Name);
                                winner = runningOrder[round];

                            }
                            else
                            {
                                Console.WriteLine("{0} Unfortunately, your first intuition wasn't the right one :( ", runningOrder[round].Name);
                                Console.WriteLine("you will be take out of the game");
                                Console.WriteLine("you can't play but you will have to show your card so stay connected :)");
                                runningOrder[round].Accusation = true;
                                nbAccusations++;
                                runningOrder.RemoveAt(round);
                                //enlever le pion du plateau ==> Margaux

                            }
                        }
                        else
                        {
                            do
                            {
                                Console.WriteLine("you can't do an accusation if you are not is the right room. Do you want to do an hypothesis instead? \n1: yes \n2: no");
                                response = Convert.ToInt32(Console.ReadLine());
                            } while (response < 1 || response > 2);
                            if (response == 1)
                            {
                                //appeler fonction hypothesis qui va etre creer
                            }
                        }
                        Console.WriteLine("press enter to continue");
                        Console.ReadKey();
                        Console.Clear();

                        foreach (Player p in players)
                        {
                            Console.Write(p.Name + " ");
                            Console.WriteLine();
                            Console.Write(p.Accusation);
                        }
                        foreach(Player p in runningOrder)
                        {
                            Console.Write(p.Name + " ");
                        }
                        
                    }
                    
                }

                //verifier si ça fonctionne encore
                if(round<runningOrder.Count-1)
                {
                    round++;
                }
                else { round = 0; }
            }
            EndOfTheGame();
            

            

        }
        static void EndOfTheGame()
        {
            Console.WriteLine("Game is over \n {0} won" +
                "the 3 murder cards were : {1} ; {2} ; {3} \n Congratulations ! \n press enter to quit the game", 
                winner.Name, CardsManager.cardsRooms.CardMurderer, CardsManager.cardsSuspects.CardMurderer, CardsManager.cardsWeapons.CardMurderer);
            Console.ReadKey();
            Console.Clear();
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
