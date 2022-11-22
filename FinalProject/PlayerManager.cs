using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    //fonction : montrer les cartes du joueur qui lui sont encore inconnus
    //fonction : chercher les cartes possibles et choisir quelle carte montrer 
    static class PlayerManager
    {
        /// <summary>
        /// Creation of a list of Players 
        /// distribution of cards for each player
        /// </summary>
        /// <param name="nbPlayers"></param>
        /// <param name="remainingCards"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<Player> Initialization(int nbPlayers, List<string> remainingCards, GameBoard board)
        {
            int round = 1;
            List<Player> players = new List<Player>();
            string piece;
            do
            {
                do
                {
                    Console.WriteLine("What's your piece player {0} (Col Mustard, Mr Green, Prof Plum, Mrs Blue, Miss Scarlet, Mrs White)?", round);
                    piece = Console.ReadLine();
                } while (CardsManager.cardsSuspects.Suspects.Contains(piece)!=true);
                Player p = new Player(piece, round, nbPlayers, board);
                remainingCards = CardsManager.CardsDistribution(p, remainingCards);
                players.Add(p);
                round++;
            } while (round <= nbPlayers);
            return players;
        }
        public static void ChooseRoom(Player p, GameBoard board)
        {
            Position currentPos = p.Pos;
            Position nextPosition = new Position(); 
            int nbOfTheRoom;
            do
            {
                Console.WriteLine("In which room do you want to go? \n 0: Billard Room " +
                    "\n1: Kitchen" +
                    "\n2: Greenhouse" +
                    "\n3: Lounge" +
                    "\n4: Hall" +
                    "\n5: Library" +
                    "\n6: Study" +
                    "\n7: Dinning Room" +
                    "\n8: Ball Room");
                nbOfTheRoom = Convert.ToInt32(Console.ReadLine());
            } while (nbOfTheRoom < 0 || nbOfTheRoom >= 9);
            int position;
            switch (nbOfTheRoom)
            {
                case 0:
                    nextPosition = board.Rooms[0][0];
                    break;
                case 1:
                    nextPosition = board.Rooms[1][0];
                    break;
                case 2:
                    nextPosition = board.Rooms[2][0];
                    break;
                case 3:
                    nextPosition = board.Rooms[3][0];
                    break;
                case 4:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,13)" +
                            "\n2: (7,14)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 3);
                    nextPosition = board.Rooms[4][position];
                    break;
                case 5:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.Rooms[5][position];
                    break;
                case 6:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.Rooms[6][position];
                    break;
                case 7:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.Rooms[7][position];
                    break;
                case 8:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 4);
                    nextPosition = board.Rooms[8][position];
                    break;

            }
            board.MarkMove(currentPos, nextPosition);
        }

    }
}
