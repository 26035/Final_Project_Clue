using System;
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
        //static variable that are used by several classes 
        public readonly static Random random = new Random();
        public static int nbAccusations = 0;
        public static Player winner = null;
        /// <summary>
        /// main that start the game
        /// propose to play with one or several devices
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int response = VerificationInputConsole("How do you want to play ? \n\t1: with only one computer \n\t2: with multiple computers", 1, 2);
            Console.Clear();
            if (response==1)
            {
                Game();
            }
            else
            {
                Console.Title = "Server";
                GameMultiPlayer.Run();
            }
        }
        /// <summary>
        /// game body 
        /// Steps of the game
        /// </summary>
       static void Game()
        {
            int option = VerificationInputConsole("Do you want to \n1: begin a new game \n2: continue the saved part? ", 1, 2);
            
            GameBoard board = new GameBoard();
            List<Player> players = new List<Player>();
            List<Player> runningOrder = new List<Player>();
            int round = 0;
            if (option ==1)
            {
                (board, players, runningOrder, round) = NewGame();
            }
            else { (board, players, runningOrder, round) = ResumptionGame(); }
            //initialization of the timer 
            Console.Clear();
            Timer.Init();
            Timer.PrintTime();
            //loop that stops if it rests just one player, or if one player find the 3 cards murderer
            while (nbAccusations<players.Count-1 && winner==null)
            {
                Console.WriteLine("it's up to {0}", runningOrder[round].Name);
                Console.WriteLine("press enter to continue");
                Console.ReadKey();
                Console.Clear();
                Die dices = new Die();
                int resultDices = dices.ResultDices();
                bool accusation = false;
                Console.WriteLine("you obtain a " + resultDices + " to your thrown");
                //if 66 or 11, player can choose his room 
                if (((dices.DieOne == dices.DieTwo) && (dices.DieOne == 6)) || ((dices.DieOne == dices.DieTwo) && (dices.DieOne == 1)))
                {
                    Console.WriteLine("double 6 or double 1");
                    PlayerManager.ChooseRoom(runningOrder[round], board);
                }
                else { runningOrder[round].NextMove(resultDices, board); }
                bool insideARoom = board.InsideRoom(runningOrder[round].Pos);
                //condition "if" that verify if the player is in a room
                //if it's true, he can do an hypothesis 
                if (insideARoom ==true)
                {
                    Console.WriteLine(runningOrder[round].ToString());
                    int choice = VerificationInputConsole("Do you want to do a \n1: An hypothesis \n2: An accusation", 1, 2) ;
                    List<Card> CardsSuspectedByTheCurrentPlayer = new List<Card>();
                    List<Card> CardsSuspectedPresentInTheOtherPlayerHandrail = new List<Card>();
                    //condition "if" that test if the player want to do an accusation or an hypothesis
                    if(choice ==1)
                    {
                        CardsSuspectedByTheCurrentPlayer = Hypothesis(board, runningOrder, round, CardsSuspectedByTheCurrentPlayer);
                        //search of the cards in handrail of each player to find the suspect card to show at the current player
                        Console.Clear();
                        ShowAllCardsOfARound(players, runningOrder, round, CardsSuspectedPresentInTheOtherPlayerHandrail, CardsSuspectedByTheCurrentPlayer);
                        Console.WriteLine("{0} your list of cards which is suspected is now: \n" + Cards.PrintList(runningOrder[round].StillSuspected)+ "\n press enter to continue", runningOrder[round].Name);
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else
                    {
                        int response = VerificationInputConsole("Are you sure to be in the room of the murder? \n1: yes \n2: no", 1, 2);
                        List<Card> Accusation = new List<Card>();
                        //condition "if" to let the player knows if he commit a mistake by asking for an accusation
                        //if it's the right choice, he can do an accusatio, else, he can do an hypothesis 
                        if (response == 1)
                        {
                            //do a list of the three cards of the accusation
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
                            //print on the console the accusation
                            Console.Clear();
                            Console.WriteLine("everybody can watch the cards selected by the player");
                            Console.WriteLine(" your accusation is: " + Cards.PrintList(CardsSuspectedByTheCurrentPlayer)+ "\n press enter to continue");
                            Console.ReadKey();
                            Console.Clear();
                            bool rightAccusation = CardsManager.ComparisonAccusationAndMurder(CardsSuspectedByTheCurrentPlayer);
                            //condition "if" that show if the accusation is true or if it's false
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
                                //it's not the wrong solution, so the player is removed from the runningOrder list and its pawn is removed from the board game
                                runningOrder[round].Accusation = true;
                                nbAccusations++;
                                SaveGame(board, players, runningOrder, round);
                                board.DeleteMark(runningOrder[round].Pos);
                                runningOrder.RemoveAt(round);
                                accusation = true;

                            }
                        }
                        else
                        {
                            response = VerificationInputConsole("you can't do an accusation if you are not is the right room. Do you want to do an hypothesis instead? \n1: yes \n2: no", 1, 2);
                           
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
                //SaveGame(board, players, runningOrder, round);
                if (accusation != true)
                {
                    SaveGame(board, players, runningOrder, round);
                    if (round < runningOrder.Count - 1)
                    {
                        round++;
                    }
                    else { round = 0;
                        Console.Clear();
                        Timer.PrintTime();
                        Thread.Sleep(3000);
                        Console.Clear();  
                    }
                }
                else { accusation = true; }
                
                
            }
            EndOfTheGame(runningOrder, players);
            

            

        }
        /// <summary>
        /// create a list of the 3 cards that are suspected by the current player
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
                        break;
                    }
                }
            }
            //suspect person of the current player
            Console.WriteLine("which person do you suspected?Tape the ID of the card that you choose");
            int cardSuspectHypothesis = VerificationInputConsole(Cards.PrintList(CardsManager.cardsSuspects.FamilyCards), 10, 15);
            //suspect weapon of the current player
            Console.WriteLine("which weapon do you suspected?");
            int cardWeaponHypothesis = VerificationInputConsole(Cards.PrintList(CardsManager.cardsWeapons.FamilyCards), 16, 21);
            //creation of a list of hypothesis from the selection of the current player
            var r = from suspect in CardsManager.AllCards
                    where suspect.ID == cardSuspectHypothesis || suspect.ID == cardWeaponHypothesis
                    select suspect;
            foreach (var i in r)
            {
                CardsSuspectedByTheCurrentPlayer.Add(i);
            }
            Console.WriteLine(" you ask for this 3 cards : " + Cards.PrintList(CardsSuspectedByTheCurrentPlayer)+"\nPress enter to continue");
            Console.ReadKey();
            runningOrder[round].AllHypothesis.Add(CardsSuspectedByTheCurrentPlayer);
            return CardsSuspectedByTheCurrentPlayer;
        }
        /// <summary>
        /// used to be sure that the player enter a right number
        /// the function is called until the player enter a correct number
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int VerificationInputConsole(string input, int min, int max)
        {
            #region try catch
            int choice;
            Console.WriteLine(input);
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if(choice >= min && choice <= max)
                {
                    return choice;
                }
                else { return VerificationInputConsole(input, min, max); }
            }catch 
            {
                return VerificationInputConsole(input,min,max);
            }
            return choice;
            #endregion
        }
        /// <summary>
        /// used to search for every player cards that are identical in their handtrail and in the list of the current hypothesis.
        /// ask every player to select the card that they want to show
        /// show the card to the current player and removes it of his handtrail
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
                    int nbOfCardsSuspectedInYourPossession = CardsSuspectedPresentInTheOtherPlayerHandrail.Count;
                    bool showOneCard = false;
                    Card cardToShow = new Card();
                    do
                    {
                        for (int i = 0; i < nbOfCardsSuspectedInYourPossession; i++)
                        {
                            int response = VerificationInputConsole(string.Concat("Do you want to show the card :", CardsSuspectedPresentInTheOtherPlayerHandrail[i].Name," ? \n1:yes \n2:no"), 1, 2);
                            
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
        /// used to end the game
        /// print out the winner and the murderer cards
        /// delete the files that are related to this game
        /// </summary>
        /// <param name="runningOrder"></param>
        static void EndOfTheGame(List<Player> runningOrder, List<Player> players)
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
            DeleteGame(players);

        }
        /// <summary>
        /// used to save the board, the list of players/runningOrder/round, each player and his characteristics and the murderer cards
        /// </summary>
        /// <param name="board"></param>
        /// <param name="players"></param>
        /// <param name="runningOrder"></param>
        /// <param name="round"></param>
        static void SaveGame(GameBoard board,List<Player> players,List<Player> runningOrder, int round)
        {
            string fileNameBoard = "savedBoard";
            for(int r =0;r<players.Count;r++)
            {
                if(players[r].Id==runningOrder[round].Id)
                {
                    string filePlayerr = string.Concat("Player_", players[r].Id);
                    string fileRound = "Players_RunningOrder_round";
                    Register.SaveBoard(fileNameBoard, board);
                    Register.SavePlayer(filePlayerr, players[r]);
                    Register.SaveRound(fileRound, round, players, runningOrder);
                    break;
                }
            }
            Register.SaveMurderCards("cardsMurderer");
        }
        /// <summary>
        /// used to initialize a new game
        /// </summary>
        /// <returns></returns>
        static (GameBoard board, List<Player> players, List<Player> runningOrder, int round) NewGame()
        {
            // Début de partie : affichage plateau
            GameBoard board = new GameBoard();
            board.PrintBoard();
            InitializationAllPlayers(board);
            //initialisation list AllPlayers
            List<string> piece = new List<string>() { "Col Mustard", "Mr Green", "Prof Plum", "Mrs Blue", "Miss Scarlet", "Mrs White" };
            for (int i = 0; i < 6; i++)
            {
                Player p = new Player(piece[i], i + 1, 6, board);
                PlayerManager.AllPlayers.Add(p);
            }


            //Initialization of the game...
            List<Card> remainingCards = CardsManager.Initialization();
            int nbPlayers;
            nbPlayers = VerificationInputConsole("How many players are going to play (2,3,4,5,6) ?", 2, 6);
            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            List<Player> runningOrder = players.OrderBy(item => random.Next()).ToList();
            int round = 0;
            //...until the distribution of all cards
            return (board, players, runningOrder, round);
        }
        /// <summary>
        /// used to start the recovery of the saved game
        /// </summary>
        /// <returns></returns>
        static (GameBoard board,List<Player> players, List<Player> runningOrder, int round) ResumptionGame()
        {
            //initialization of the board
            GameBoard board = new GameBoard("savedBoard.csv");
            InitializationAllPlayers(board);
            
            List<Player> runningOrder = new List<Player>();
            List<Player> players = new List<Player>();
            int round;
            //initialization of the list of players/runningOrder/round
            (runningOrder, players, round)  = PlayerManager.ResumptionRoundPlayers();
            //initialization of all players
            for (int i = 0;i<players.Count;i++)
            {
                string[] line = File.ReadAllLines(string.Concat("Player_", Convert.ToString(i+1),".csv"));
                string[] values = line[1].Split(';');
                int ID  = Convert.ToInt32(values[0]);
                Player p = new Player();
                for(int j =0;j<players.Count;j++)
                {
                    if(ID==players[j].Id)
                    {

                        p = new Player(string.Concat("Player_", i+1));
                        players[j] = p;
                    }
                }
                for(int j =0;j<runningOrder.Count;j++)
                {
                    if(ID==runningOrder[j].Id)
                    {
                        runningOrder[j] = p;
                    }
                }

            }
            //initialization of the cardsMurderer
            CardsManager.ResumptionCardsMurderer();
            return (board, players, runningOrder, round);

        
        }
        /// <summary>
        /// used to delete the files related to the game
        /// </summary>
        /// <param name="players"></param>
        static void DeleteGame(List<Player> players)
        {
            for(int i =0;i<players.Count; i ++)
            {
                Register.DeleteFile(string.Concat("Player_", i + 1));
            }
            Register.DeleteFile("savedBoard");
            Register.DeleteFile("Players_RunningOrder_round");
            Register.DeleteFile("cardsMurderer");
        }
        /// <summary>
        /// used to initialize the list of all players
        /// </summary>
        /// <param name="board"></param>
        static void InitializationAllPlayers(GameBoard board)
        {
            List<string> piece = new List<string>() { "Col Mustard", "Mr Green", "Prof Plum", "Mrs Blue", "Miss Scarlet", "Mrs White" };
            for (int i = 0; i < 6; i++)
            {
                Player p = new Player(piece[i], i + 1, 6, board);
                PlayerManager.AllPlayers.Add(p);
            }
        }
    }
}
