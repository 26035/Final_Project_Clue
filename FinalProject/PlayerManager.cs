using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace FinalProject
{
    //fonction : montrer les cartes du joueur qui lui sont encore inconnus
    //fonction : chercher les cartes possibles et choisir quelle carte montrer 
    static class PlayerManager
    {
        public static List<Player> AllPlayers = new List<Player>();
        /// <summary>
        /// Used to initializes the list of player with their handtrail
        /// </summary>
        /// <param name="nbPlayers">integer that represents the number of players</param>
        /// <param name="remainingCards">list of card that represent all the card without the 3 card murderer </param>
        /// <param name="board">represents the board</param>
        /// <returns>list of player with a handtrail initialized </returns>
        public static List<Player> Initialization(int nbPlayers, List<Card> remainingCards, GameBoard board)
        {
            int round = 1;
            List<Player> players = new List<Player>();
            List<Card> piece = new List<Card>(CardsManager.cardsSuspects.FamilyCards);
            //int piece;
            do
            {
                bool available = false;
                int choice = 0;
                int indice;
                do
                {
                    indice = 10;
                    choice = Program.VerificationInputConsole("What's your piece player "+round+" ? " + Cards.PrintList(piece),10,15);
                    for (int i =0;i<piece.Count;i++)
                    {
                        if (piece[i].ID == choice) { available = true;
                            indice = i;
                            break; }
                    }
                    if(available == false) { Console.WriteLine("Warning ! The player that you want has already been selected"); }
                } while (available == false);
                piece.RemoveAt(indice);
                Player p = new Player(CardsManager.cardsSuspects.FamilyCards[choice - 10].Name, round, nbPlayers, board);
                remainingCards = CardsManager.CardsDistribution(p, remainingCards);
                players.Add(p);
                round++;
                /*do
                {
                    Console.WriteLine("What's your piece player {0} (0 : Col Mustard, 1 : Mr Green, 2 : Prof Plum, 3 : Mrs Blue, 4 : Miss Scarlet, 5 : Mrs White)?", round);
                    piece = Convert.ToInt32(Console.ReadLine());
                } while (piece<0||piece>5);
                Player p = new Player(CardsManager.cardsSuspects.FamilyCards[piece].Name, round, nbPlayers, board);
                remainingCards = CardsManager.CardsDistribution(p, remainingCards);
                players.Add(p);
                round++;*/
            } while (round <= nbPlayers);
            return players;
        }
        /// <summary>
        /// Used to allow a player to choose the piece of their choice if the dice result was double 6 or double
        /// </summary>
        /// <param name="p">represents the player</param>
        /// <param name="board">represents the board</param>
        public static void ChooseRoom(Player p, GameBoard board)
        {
            Position currentPos = p.Pos;
            Position nextPosition = new Position(); 
            int nbOfTheRoom;
            nbOfTheRoom = Program.VerificationInputConsole("In which room do you want to go? \n 1: Kitchen" +
                    "\n2: Lounge" +
                    "\n3: Ball Room" +
                    "\n4: Dinning Room" +
                    "\n5: Hall" +
                    "\n6: Billard Room" +
                    "\n7: Library" +
                    "\n8: Study" +
                    "\n9: Greenhouse", 1,9);
            /*do
            {
                Console.WriteLine("In which room do you want to go? \n 1: Billard Room " +
                    "\n2: Kitchen" +
                    "\n3: Greenhouse" +
                    "\n4: Lounge" +
                    "\n5: Hall" +
                    "\n6: Library" +
                    "\n7: Study" +
                    "\n8: Dinning Room" +
                    "\n9: Ball Room");
                nbOfTheRoom = Convert.ToInt32(Console.ReadLine());
            } while (nbOfTheRoom < 1 || nbOfTheRoom >= 10);*/
            int position;
            switch (nbOfTheRoom)
            {
                case 1:
                    nextPosition = board.PositionRooms[0][0];
                    break;
                case 2:
                    nextPosition = board.PositionRooms[1][0];
                    break;
                case 3:
                    nextPosition = board.PositionRooms[2][0];
                    break;
                case 4:
                    nextPosition = board.PositionRooms[3][0];
                    break;
                case 5:
                    position = Program.VerificationInputConsole("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,12)" +
                            "\n2: (6,13)", 0, 2);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,13)" +
                            "\n2: (7,14)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 3);*/
                    nextPosition = board.PositionRooms[4][position];
                    break;
                case 6:
                    position = Program.VerificationInputConsole("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)", 0, 1);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[5][position];
                    break;
                case 7:
                    position = Program.VerificationInputConsole("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)",0,1);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[6][position];
                    break;
                case 8:
                    position = Program.VerificationInputConsole("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)", 0, 1);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[7][position];
                    break;
                case 9:
                    position = Program.VerificationInputConsole("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)", 0, 3);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 4);*/
                    nextPosition = board.PositionRooms[8][position];
                    break;

            }
            if(board.IsOccupied(nextPosition))
            {
                ChooseRoom(p, board);
            }
            else
            {
                board.MarkMove(currentPos, nextPosition);
                p.Pos = nextPosition;
            }
            board.PrintBoard();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static (List<Player> runningOrder, List<Player> players, int round) ResumptionRoundPlayers()
        {
            string[] line = File.ReadAllLines("Players_RunningOrder_round.csv");
            string[] values = line[0].Split(';');
            int round = Convert.ToInt32( values[0]);
            values = line[1].Split(';');
            List<Player> players = new List<Player>();
            for(int i =0;i<values.Length; i ++)
            {
                for(int j = 10;j<15;j++)
                {
                    //Console.WriteLine((values[i])+ " " + PlayerManager.AllPlayers[j - 10].Id);
                    if(Convert.ToInt32(values[i]) == PlayerManager.AllPlayers[j-10].Id)
                    {
                        players.Add(PlayerManager.AllPlayers[j - 10]);
                        break;
                    }
                }
            }
            values = line[2].Split(';');
            List<Player> runningOrder = new List<Player>();
            for(int i =0;i<values.Length;i++)
            {
                for(int j =10;j<15;j++)
                {
                    if(Convert.ToInt32(values[i])==PlayerManager.AllPlayers[j-10].Id)
                    {
                        runningOrder.Add(PlayerManager.AllPlayers[j - 10]);
                    }
                }
            }
            return (runningOrder, players, round);
        }

        public static void ChooseRoomSocket(Player p, GameBoard board)
        {
            Position currentPos = p.Pos;
            Position nextPosition = new Position();
            int nbOfTheRoom;
            nbOfTheRoom = GameMultiPlayer.VerificationInputConsoleSocket("In which room do you want to go? \n 1: Kitchen" +
                    "\n2: Lounge" +
                    "\n3: Ball Room" +
                    "\n4: Dinning Room" +
                    "\n5: Hall" +
                    "\n6: Billard Room" +
                    "\n7: Library" +
                    "\n8: Study" +
                    "\n9: Greenhouse", 1, 9, p.PlayerSocket);
            /*= Program.VerificationInputConsole("In which room do you want to go? \n 1: Billard Room " +
                "\n2: Kitchen" +
                "\n3: Greenhouse" +
                "\n4: Lounge" +
                "\n5: Hall" +
                "\n6: Library" +
                "\n7: Study" +
                "\n8: Dinning Room" +
                "\n9: Ball Room", 1, 9);*/
            /*do
            {
                Console.WriteLine("In which room do you want to go? \n 1: Billard Room " +
                    "\n2: Kitchen" +
                    "\n3: Greenhouse" +
                    "\n4: Lounge" +
                    "\n5: Hall" +
                    "\n6: Library" +
                    "\n7: Study" +
                    "\n8: Dinning Room" +
                    "\n9: Ball Room");
                nbOfTheRoom = Convert.ToInt32(Console.ReadLine());
            } while (nbOfTheRoom < 1 || nbOfTheRoom >= 10);*/
            int position;
            switch (nbOfTheRoom)
            {
                case 1:
                    nextPosition = board.PositionRooms[0][0];
                    break;
                case 2:
                    nextPosition = board.PositionRooms[1][0];
                    break;
                case 3:
                    nextPosition = board.PositionRooms[2][0];
                    break;
                case 4:
                    nextPosition = board.PositionRooms[3][0];
                    break;
                case 5:
                    position = GameMultiPlayer.VerificationInputConsoleSocket("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,13)" +
                            "\n2: (7,14)", 0, 2, p.PlayerSocket);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,13)" +
                            "\n2: (7,14)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 3);*/
                    nextPosition = board.PositionRooms[4][position];
                    break;
                case 6:
                    position = GameMultiPlayer.VerificationInputConsoleSocket("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)", 0, 1, p.PlayerSocket);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[5][position];
                    break;
                case 7:
                    position = GameMultiPlayer.VerificationInputConsoleSocket("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)", 0, 1, p.PlayerSocket);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[6][position];
                    break;
                case 8:
                    position = GameMultiPlayer.VerificationInputConsoleSocket("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)", 0, 1, p.PlayerSocket);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);*/
                    nextPosition = board.PositionRooms[7][position];
                    break;
                case 9:
                    position = GameMultiPlayer.VerificationInputConsoleSocket("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)", 0, 3, p.PlayerSocket);
                    /*do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 4);*/
                    nextPosition = board.PositionRooms[8][position];
                    break;

            }

            board.MarkMove(currentPos, nextPosition);
            p.Pos = nextPosition;
            Server.SendBoardToClients(board);
        }
        public static List<Player> InitializationSocket(int nbPlayers, List<Card> remainingCards, GameBoard board)
        {
            int round = 1;
            List<Player> players = new List<Player>();
            List<Card> piece = new List<Card>(CardsManager.cardsSuspects.FamilyCards);
            //int piece;
            do
            {
                bool available = false;
                int choice = 0;
                int indice;

                do
                {
                    indice = 10;
                    //Server.SendToClient("1What's your piece player "+round+" ? "+Cards.PrintList(piece),Server.allClients[round-1]);
                    //Console.WriteLine("What's your piece player {0}" + Cards.PrintList(piece), round);
                    choice = GameMultiPlayer.VerificationInputConsoleSocket("Enter your pawn's ID player " + round + " : " + Cards.PrintList(piece), 10, 15, Server.allClients[round - 1]);
                    Thread.Sleep(1000);
                    for (int i = 0; i < piece.Count; i++)
                    {
                        if (piece[i].ID == choice)
                        {
                            available = true;
                            indice = i;
                            break;
                        }
                    }
                    if (available == false) { Server.SendToClient(2, "Warning ! The player that you want has already been selected", Server.allClients[round - 1]); }
                } while (available == false);
                piece.RemoveAt(indice);
                Player p = new Player(CardsManager.cardsSuspects.FamilyCards[choice - 10].Name, round, nbPlayers, board, Server.allClients[round - 1]);
                remainingCards = CardsManager.CardsDistribution(p, remainingCards);
                players.Add(p);
                round++;
            } while (round <= nbPlayers);
            Console.WriteLine(players[0].ToString());
            Console.WriteLine(players[1].ToString());
            return players;
        }
    }
}
