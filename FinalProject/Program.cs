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
        static void Main(string[] args)
        {
            Game();
            Console.ReadKey();
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
            GameBoard board = new GameBoard("ColorBoard.csv");
            board.PrintBoard(board);


            //Initialisation du jeu...
            List<string> remainingCards = CardsManager.Initialization();
            int nbPlayers = 0;
            do
            {
                Console.WriteLine("How many players are going to play (2,3,4,5,6) ?");
                nbPlayers = Convert.ToInt32(Console.ReadLine());
            } while (nbPlayers < 2 || nbPlayers >= 7);
            List<Player> players = PlayerManager.Initialization(nbPlayers, remainingCards, board);
            
            //...jusqu'à distribution des cartes

            

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

            







            //Console.ReadKey();
        }*/
    }
}
