using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public static class PlayerManager
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
    }
}
