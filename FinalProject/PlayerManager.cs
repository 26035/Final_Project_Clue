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
        public static List<Player> Initialization(int nbPlayers, List<Card> remainingCards, GameBoard board)
        {
            int round = 1;
            List<Player> players = new List<Player>();
            int piece;
            do
            {
                do
                {
                    Console.WriteLine("What's your piece player {0} (0 : Col Mustard, 1 : Mr Green, 2 : Prof Plum, 3 : Mrs Blue, 4 : Miss Scarlet, 5 : Mrs White)?", round);
                    piece = Convert.ToInt32(Console.ReadLine());
                } while (piece<0||piece>5);
                Player p = new Player(CardsManager.cardsSuspects.FamilyCards[piece].Name, round, nbPlayers, board);
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
            } while (nbOfTheRoom < 1 || nbOfTheRoom >= 10);
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
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (5,10)" +
                            "\n1: (6,13)" +
                            "\n2: (7,14)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 3);
                    nextPosition = board.PositionRooms[4][position];
                    break;
                case 6:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,7)" +
                            "\n1: (11,3)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.PositionRooms[5][position];
                    break;
                case 7:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (13,3)" +
                            "\n1: (16,6)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.PositionRooms[6][position];
                    break;
                case 8:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (9,18)" +
                            "\n1: (13,17)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 2);
                    nextPosition = board.PositionRooms[7][position];
                    break;
                case 9:
                    do
                    {
                        Console.WriteLine("By which door do you want to enter in the room ?" +
                            "\n0: (18,11)" +
                            "\n1: (18,14)" +
                            "\n2: (20,9)" +
                            "\n3: (20,16)");
                        position = Convert.ToInt32(Console.ReadLine());
                    } while (position < 0 || position >= 4);
                    nextPosition = board.PositionRooms[8][position];
                    break;

            }
            board.MarkMove(currentPos, nextPosition);
        }

    }
}
