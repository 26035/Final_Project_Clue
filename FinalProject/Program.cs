﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace FinalProject
{
    class Program
    {
        
        public readonly static Random random = new Random();
        public static int nbAccusations = 0;
        public static Player winner = null;
        static void Main(string[] args)
        {
            //Game();
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
            Register.SaveMurderCards("MurderCards");*/
            //List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            //Console.WriteLine(players[0].ToString());
            //Console.WriteLine(players[1].ToString());
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
        
        
        
        /// <summary>
        /// Code début du jeu
        /// </summary>
       static void Game()
        {
            // Début de partie : affichage plateau
            GameBoard board = new GameBoard();
            board.PrintBoard();
            //initialisation list AllPlayers
            List<string> piece = new List<string>() { "Col Mustard", "Mr Green", "Prof Plum", "Mrs Blue", "Miss Scarlet", "Mrs White" };
            for(int i =0;i<6;i++)
            {
                Player p = new Player(piece[i], i + 1, 6, board);
                PlayerManager.AllPlayers.Add(p);
            }
            

            //Initialisation du jeu...
            List<Card> remainingCards = CardsManager.Initialization();
            int nbPlayers = 0;
            nbPlayers = VerificationInputConsole("How many players are going to play (2,3,4,5,6) ?", 2, 6);

            /*do
            {
                Console.WriteLine("How many players are going to play (2,3,4,5,6) ?");
                nbPlayers = Convert.ToInt32(Console.ReadLine());
            } while (nbPlayers < 2 || nbPlayers >= 7);*/

            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            
            //...jusqu'à distribution des cartes

            //break si nbAccusations == nbplayers ou player.accusation == true à chaque round du joueur
            int round = 0;
            while (nbAccusations<players.Count-1 && winner==null)
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
                    int choice = VerificationInputConsole("Do you want to do a \n1: An hypothesis \n2: An accusation", 1, 2) ;
                   
                    /*do
                    {
                        Console.WriteLine("Do you want to do a \n1: An hypothesis \n2: An accusation");
                        choice = Convert.ToInt32(Console.ReadLine());
                    } while (choice < 1 || choice >= 3);*/
                    List<Card> CardsSuspectedByTheCurrentPlayer = new List<Card>();
                    List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail = new List<Card>();
                    if(choice ==1)
                    {
                        CardsSuspectedByTheCurrentPlayer = Hypothesis(board, runningOrder, round, CardsSuspectedByTheCurrentPlayer);
                        //search of the cards in handrail of each player to find the suspect card to show at the current player
                        Console.Clear();
                        ShowAllCardsOfARound(players, runningOrder, round, CardsSuspectedPresentInTheOtherPlayerHandrail, CardsSuspectedByTheCurrentPlayer);
                        /*foreach (Player p in players)
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
                                }
                                int nbOfCardsSuspectedInYourPossession = CardsSuspectedPresentInTheOtherPlayerHandrail.Count;
                                //int response = 0;
                                bool showOneCard = false;
                                Card cardToShow= new Card();
                                do {
                                    for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                                    {
                                        int response = VerificationInputConsole(string.Concat("Do you want to show the card : {0}? \n1:yes \n2:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name), 1, 2);
                                        /*do
                                        {
                                            Console.WriteLine("Do you want to show the card : {0}? \n1:yes \n2:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);
                                            response = Convert.ToInt32(Console.ReadLine());
                                        } while (response < 1 || response >= 3);

                                        if (response == 1 )
                                        {
                                            showOneCard = true;
                                            cardToShow = CardsSuspectedPresentInTheOtherPlayerHandrail[i];
                                            //remove the card seen in the list "still suspected" of the player
                                            CardsManager.RemoveCardAt(runningOrder[round].StillSuspected, CardsSuspectedPresentInTheOtherPlayerHandrail[i]);
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
                        }*/
                        Console.WriteLine("{0} your list of cards which is suspected is now: \n" + Cards.PrintList(runningOrder[round].StillSuspected)+ "\n press enter to continue", runningOrder[round].Name);
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else
                    {
                        int response = VerificationInputConsole("Are you sure to be in the room of the murder? \n1: yes \n2: no", 1, 2);
                        /*do
                        {
                            Console.WriteLine("Are you sure to be in the room of the murder? \n1: yes \n2: no");
                            response = Convert.ToInt32(Console.ReadLine());
                        } while (response < 1 || response > 2);*/

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
                                board.DeleteMark(runningOrder[round].Pos);
                                runningOrder.RemoveAt(round);

                            }
                        }
                        else
                        {
                            response = VerificationInputConsole("you can't do an accusation if you are not is the right room. Do you want to do an hypothesis instead? \n1: yes \n2: no", 1, 2);
                           /* do
                            {
                                Console.WriteLine("you can't do an accusation if you are not is the right room. Do you want to do an hypothesis instead? \n1: yes \n2: no");
                                response = Convert.ToInt32(Console.ReadLine());
                            } while (response < 1 || response > 2);*/
                            if (response == 1)
                            {
                                Console.WriteLine(Cards.PrintList(runningOrder[round].Handtrail));
                                Hypothesis(board, runningOrder, round, CardsSuspectedByTheCurrentPlayer);
                                ShowAllCardsOfARound(players, runningOrder, round, CardsSuspectedPresentInTheOtherPlayerHandrail, CardsSuspectedByTheCurrentPlayer);

                            }
                        }
                        Console.WriteLine("press enter to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    
                }
                SaveGame(board, players, runningOrder, round);

                if (round<runningOrder.Count-1)
                {
                    round++;
                }
                else { round = 0; }
                
            }
            EndOfTheGame(runningOrder);
            

            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="runningOrder"></param>
        /// <param name="round"></param>
        /// <param name="CardsSuspectedByTheCurrentPlayer"></param>
        /// <returns></returns>
        static List<Card> Hypothesis(GameBoard board, List<Player> runningOrder, int round, List<Card> CardsSuspectedByTheCurrentPlayer)
        {
            //cards Suspected by the current player
            //find the room of the player
            for (int i = 0; i < 9; i++)
            {
                foreach (var j in board.PositionRooms[i])
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
            int cardSuspectHypothesis = VerificationInputConsole(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards), 10, 15);
            //Console.WriteLine(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards));
            //int cardSuspectHypothesis = Convert.ToInt32(Console.ReadLine());
            //suspect weapon of the current player
            Console.WriteLine("which weapon do you suspected?");
            int cardWeaponHypothesis = VerificationInputConsole(Cards.PrintList(CardsManager.cardsWeapons.FamilyCards), 16, 21);
            //Console.WriteLine(Cards.PrintList(CardsManager.cardsWeapons.FamilyCards));
            //int cardWeaponHypothesis = Convert.ToInt32(Console.ReadLine());

            //creation of a list of hypothesis from the selection of the current player
            var r = from suspect in CardsManager.AllCards
                    where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                    select suspect;
            foreach (var i in r)
            {
                CardsSuspectedByTheCurrentPlayer.Add(i);
            }
            Console.WriteLine(" you ask for this 3 cards : " + Cards.PrintList(CardsSuspectedByTheCurrentPlayer));
            runningOrder[round].AllHypothesis.Add(CardsSuspectedByTheCurrentPlayer);
            return CardsSuspectedByTheCurrentPlayer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int VerificationInputConsole(string input, int min, int max)
        {
            int choice;
            do
            {
                Console.WriteLine(input);
                choice = Convert.ToInt32(Console.ReadLine());
                //Console.Clear();
            } while (choice < min || choice > max);
            return choice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="players"></param>
        /// <param name="runningOrder"></param>
        /// <param name="round"></param>
        /// <param name="CardsSuspectedPresentInTheOtherPlayerHandrail"></param>
        /// <param name="CardsSuspectedByTheCurrentPlayer"></param>
        static void ShowAllCardsOfARound(List<Player> players, List<Player> runningOrder,int round,List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail, List<Card> CardsSuspectedByTheCurrentPlayer)
        {
            foreach (Player p in players)
            {
                if (p != runningOrder[round])
                {
                    CardsSuspectedPresentInTheOtherPlayerHandrail.Clear();


                    Console.WriteLine("{0} you will show a card, take the laptop and press enter to continue", p.Name);
                    Console.ReadKey();
                    Console.Clear();
                    foreach (Card card in p.Handtrail)
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
                    //int response = 0;
                    bool showOneCard = false;
                    Card cardToShow = new Card();
                    do
                    {
                        for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                        {
                            int response = VerificationInputConsole(string.Concat("Do you want to show the card :", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name," ? \n1:yes \n2:no"), 1, 2);
                            /*do
                            {
                                Console.WriteLine("Do you want to show the card : {0}? \n1:yes \n2:no", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name);
                                response = Convert.ToInt32(Console.ReadLine());
                            } while (response < 1 || response >= 3);*/

                            if (response == 1)
                            {
                                showOneCard = true;
                                cardToShow = CardsSuspectedPresentInTheOtherPlayerHandrail[i];
                                //remove the card seen in the list "still suspected" of the player
                                CardsManager.RemoveCardAt(runningOrder[round].StillSuspected, CardsSuspectedPresentInTheOtherPlayerHandrail[i]);
                                break;
                            }


                        }
                        if (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0) { Console.WriteLine("{0} You have to show one of your card", p.Name); }
                    } while (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0);
                    Console.Clear();
                    if (CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0)
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runningOrder"></param>
        static void EndOfTheGame(List<Player> runningOrder)
        {
            if(runningOrder.Count==1)
            {
                winner = runningOrder[0];
                

            }
            Console.WriteLine("Game is over \n {0} won" +
                "\nThe 3 murder cards were : {1} ; {2} ; {3} \n Congratulations ! \n press enter to quit the game", 
                winner.Name, CardsManager.cardsRooms.CardMurderer.Name, CardsManager.cardsSuspects.CardMurderer.Name, CardsManager.cardsWeapons.CardMurderer.Name);
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="players"></param>
        /// <param name="runningOrder"></param>
        /// <param name="round"></param>
        static void SaveGame(GameBoard board,List<Player> players,List<Player> runningOrder, int round)
        {
            string fileNameBoard = "savedBoard";
            string filePlayeri = string.Concat("Player_", runningOrder[round].Id);
            string fileRound = "Players_RunningOrder_round";
            Register.SaveBoard(fileNameBoard, board);
            Register.SavePlayer(filePlayeri,runningOrder[round]);
            Register.SaveRound(fileRound,round,players,runningOrder);
        }
        /*static (GameBoard,List<Player> players, List<Player> runningOrder, int round) ResumptionGame()
        {
            GameBoard board = new GameBoard("savedBoard.csv");
            //recuperer nb de joueurs pour faire un for et les réinitialiser un par un
            List<Player> runningOrder = new List<Player>();
            List<Player> players = new List<Player>();
            int round;
            (runningOrder, players, round)  = PlayerManager.ResumptionRoundPlayers();
        
        }*/
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
