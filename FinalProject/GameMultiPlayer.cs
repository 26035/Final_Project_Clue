using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject
{
    static class GameMultiPlayer
    {
        public static Player winner = null;
        public static int nbAccusations = 0;
        /// <summary>
        /// game body to play with multiple computers
        /// </summary>
        public static void Run()
        {
            
                GameBoard board = new GameBoard();

                //Game initialisation...
                List<Card> remainingCards = CardsManager.Initialization();
                int nbPlayers = 0;
                nbPlayers = Program.VerificationInputConsole("How many players are going to play (2,3,4,5,6) ?", 2, 6);
                Server.SetUpServer(nbPlayers);
                Server.SendToAll(2, "Waiting for other player to choose their pawn...");
                List<Player> players = PlayerManager.InitializationSocket(nbPlayers, remainingCards, board);
                List<Player> runningOrder = new List<Player>(players);
                runningOrder.OrderBy(item => Program.random.Next()).ToList();
                //...until cards distribution

            
            int round = 0;
                while (nbAccusations < players.Count - 1 && winner == null)
                {
                    Server.SendBoardToClients(board);
                    Thread.Sleep(1000);
                    Server.SendToOtherClients(2, "It's up to " + runningOrder[round].Name, runningOrder[round].PlayerSocket);
                    Thread.Sleep(1000);
                    Server.SendToClient(2, "It's up to you ! ", runningOrder[round].PlayerSocket);
                    Die dices = new Die();
                    int resultDices = dices.ResultDices();
                    bool accusation = false;
                    Thread.Sleep(1000);
                    Server.SendToClient(2, "You obtain " + resultDices + " by rolling the dice", runningOrder[round].PlayerSocket);//Console.WriteLine("you obtain a " + resultDices + " to your thrown");
                    Thread.Sleep(1000);

                    //if 66 or 11, player can choose his room 
                    if (((dices.DieOne == dices.DieTwo) && (dices.DieOne == 6)) || ((dices.DieOne == dices.DieTwo) && (dices.DieOne == 1)))
                    {

                        Server.SendToClient(2, "Double 6 or double 1", runningOrder[round].PlayerSocket);
                        PlayerManager.ChooseRoomSocket(runningOrder[round], board);
                    }
                    else { runningOrder[round].NextMoveSocket(resultDices, board); }
                    bool insideARoom = board.InsideRoom(runningOrder[round].Pos);
                    if (insideARoom == true)
                    {
                        int choice = VerificationInputConsoleSocket("Do you want to do a \n1: An hypothesis \n2: An accusation", 1, 2, runningOrder[round].PlayerSocket);

                        List<Card> CardsSuspectedByTheCurrentPlayer = new List<Card>();
                        List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail = new List<Card>();
                        if (choice == 1)
                        {
                            CardsSuspectedByTheCurrentPlayer = HypothesisSocket(board, runningOrder, round, CardsSuspectedByTheCurrentPlayer);
                            ShowAllCardsOfARoundSocket(players, runningOrder, round, CardsSuspectedPresentInTheOtherPlayerHandrail, CardsSuspectedByTheCurrentPlayer);
                            Server.SendToClient(2, "List of cards that you're still suspecting: \n" + Cards.PrintList(runningOrder[round].StillSuspected), runningOrder[round].PlayerSocket);//Console.WriteLine("{0} your list of cards which is suspected is now: \n" + Cards.PrintList(runningOrder[round].StillSuspected) + "\n press enter to continue", runningOrder[round].Name);
                        }
                        else
                        {
                            int response = VerificationInputConsoleSocket("Are you sure to be in the room of the murder? \n1: yes \n2: no", 1, 2, runningOrder[round].PlayerSocket);
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
                                int cardSuspectHypothesis = VerificationInputConsoleSocket("Which person do you accuse? \nTape the ID of the card that you choose : " + Cards.PrintList(CardsManager.cardsSuspects.FamilyCards), 10, 15, runningOrder[round].PlayerSocket);

                                //suspect weapon of the current player
                                int cardWeaponHypothesis = VerificationInputConsoleSocket("Which weapon is the murder weapon for you? \nTape the ID of the card that you choose : " + Cards.PrintList(CardsManager.cardsWeapons.FamilyCards), 16, 21, runningOrder[round].PlayerSocket);
                                
                                //creation of a list of hypothesis from the selection of the current player
                                var r = from suspect in CardsManager.AllCards
                                        where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                                        select suspect;
                                foreach (var i in r)
                                {
                                    CardsSuspectedByTheCurrentPlayer.Add(i);
                                }
                                
                                Server.SendToAll(2, runningOrder[round].Name + " made an accusation : \n" + Cards.PrintList(CardsSuspectedByTheCurrentPlayer));//room suspect weapon
                                #endregion
                                bool rightAccusation = CardsManager.ComparisonAccusationAndMurder(CardsSuspectedByTheCurrentPlayer);
                                if (rightAccusation == true)
                                {
                                    Server.SendToAll(2, runningOrder[round].Name + " is the winner ! ");
                                    winner = runningOrder[round];
                                }
                                else
                                {
                                    Server.SendToAll(2, "Unfortunately, the accusation is wrong...");
                                    Thread.Sleep(1000);
                                    Server.SendToClient(2, "You can't play anymore but you still have to show your card, so stay connected !", runningOrder[round].PlayerSocket);
                                    runningOrder[round].Accusation = true;
                                    nbAccusations++;
                                    board.DeleteMark(runningOrder[round].Pos);
                                    runningOrder.RemoveAt(round);
                                    accusation = true;
                                }
                            }
                            else
                            {
                                response = VerificationInputConsoleSocket("You can't do an accusation if you are not is the right room. Do you want to do an hypothesis instead? \n1: yes \n2: no", 1, 2, runningOrder[round].PlayerSocket);
                                if (response == 1)
                                {
                                    Server.SendToClient(2, Cards.PrintList(runningOrder[round].Handtrail), runningOrder[round].PlayerSocket);
                                    HypothesisSocket(board, runningOrder, round, CardsSuspectedByTheCurrentPlayer);
                                    ShowAllCardsOfARoundSocket(players, runningOrder, round, CardsSuspectedPresentInTheOtherPlayerHandrail, CardsSuspectedByTheCurrentPlayer);

                                }
                            }
                            
                        }

                    }

                if (accusation != true)
                {
                    if (round < runningOrder.Count - 1)
                    {
                        round++;
                    }
                    else
                    {
                        round = 0;
                    }
                }
                else
                {
                    accusation = true;
                }
            }
            EndOfTheGameSocket(runningOrder);

        }
        
        static List<Card> HypothesisSocket(GameBoard board, List<Player> runningOrder, int round, List<Card> CardsSuspectedByTheCurrentPlayer)
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
            int cardSuspectHypothesis = VerificationInputConsoleSocket(runningOrder[round].ToString() + "\nwhich person do you suspected? Tape the ID of the card that you choose\n" + Cards.PrintList(CardsManager.cardsSuspects.FamilyCards), 10, 15, runningOrder[round].PlayerSocket);
            
            //suspect weapon of the current player
            int cardWeaponHypothesis = VerificationInputConsoleSocket(runningOrder[round].ToString() + "\n\nwhich weapon do you suspected? \n" + Cards.PrintList(CardsManager.cardsWeapons.FamilyCards), 16, 21, runningOrder[round].PlayerSocket);
            
            //creation of a list of hypothesis from the selection of the current player
            var r = from suspect in CardsManager.AllCards
                    where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                    select suspect;
            foreach (var i in r)
            {
                CardsSuspectedByTheCurrentPlayer.Add(i);
            }
            Server.SendToClient(2, "You ask for this 3 cards : " + Cards.PrintList(CardsSuspectedByTheCurrentPlayer), runningOrder[round].PlayerSocket);
            runningOrder[round].AllHypothesis.Add(CardsSuspectedByTheCurrentPlayer);
            return CardsSuspectedByTheCurrentPlayer;
        }
        public static int VerificationInputConsoleSocket(string input, int min, int max, Socket client)
        {
            int choice;
            Server.SendToClient(1, input, client);
            try
            {

                choice = Convert.ToInt32(Server.ReceiveFromClient(client));
                if (choice >= min && choice <= max)
                {
                    return choice;
                }
                else
                {
                    return VerificationInputConsoleSocket(input, min, max, client);
                }

            }
            catch
            {
                return VerificationInputConsoleSocket(input, min, max, client);
            }
        }
        static void ShowAllCardsOfARoundSocket(List<Player> players, List<Player> runningOrder, int round, List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail, List<Card> CardsSuspectedByTheCurrentPlayer)
        {
            foreach (Player p in players)
            {
                if (p != runningOrder[round])
                {
                    CardsSuspectedPresentInTheOtherPlayerHandrail.Clear();

                    Server.SendToClient(2, p.Name + "You will show a card", p.PlayerSocket);
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
                    int nbOfCardsSuspectedInYourPossession = CardsSuspectedPresentInTheOtherPlayerHandrail.Count;
                    bool showOneCard = false;
                    Card cardToShow = new Card();
                    do
                    {
                        for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                        {
                            int response = VerificationInputConsoleSocket(string.Concat("Do you want to show the card :", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name, " ? \n1:yes \n2:no"), 1, 2, p.PlayerSocket);
                            
                            if (response == 1)
                            {
                                showOneCard = true;
                                cardToShow = CardsSuspectedPresentInTheOtherPlayerHandrail[i];
                                //remove the card seen in the list "still suspected" of the player
                                CardsManager.RemoveCardAt(runningOrder[round].StillSuspected, CardsSuspectedPresentInTheOtherPlayerHandrail[i]);
                                break;
                            }


                        }
                        if (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0)
                        {
                            Thread.Sleep(1000);
                            Server.SendToClient(2, "You have to show one of your card", p.PlayerSocket);
                        }
                    } while (showOneCard == false && CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0);
                    Console.Clear();
                    if (CardsSuspectedPresentInTheOtherPlayerHandrail.Count != 0)
                    {
                        Server.SendToClient(2, "The card of player " + p.Id + " is " + cardToShow.Name, runningOrder[round].PlayerSocket);
                        
                    }
                    else
                    {
                        Server.SendToClient(2, "Player " + p.Id + " have no card to show you", runningOrder[round].PlayerSocket);
                    }
                    Thread.Sleep(2000);
                }
            }
        }
        static void EndOfTheGameSocket(List<Player> runningOrder)
        {
            if (runningOrder.Count == 1)
            {
                winner = runningOrder[0];
            }
            Server.SendToAll(2, "Game is over !\nThe 3 murder cards were : " +
                CardsManager.cardsRooms.CardMurderer.Name + " ; " +
                CardsManager.cardsSuspects.CardMurderer.Name + " ; " +
                CardsManager.cardsWeapons.CardMurderer.Name);
        }
    }
}
