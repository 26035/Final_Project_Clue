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
        /*Avancement du jeu
         * Cartes du meurtier aléatoire + distribution des cartes restantes 
         * Affichage plateau
         * Déplacement fonctionnel avec les conditions (murs/occupé/en dehors du plateau) et nouvel affichage
         */

        /*A faire
         * Lancement des dés avec des thread
         * Double 6 ou 1 = transporter dans une pièce au choix
         * Plus de fonctions lambda expression
         * Passages secrets (Class GameBoard)
         * Vider la liste des suspects des joueurs
         * Remplir les listes hypothèse des joueurs
         * Déroulement du jeu
         * Enregistrement du jeu
         * Reprendre une partie en cours
         */
        /// <summary>
        /// Code distribuer les cartes après avoir enlevé les cartes du meurtier
        /// </summary>
        static Player CardsDistribution(int round, int nbPlayers,GameBoard game)
        {
            Cards cards = new Cards();
            Random rdm = new Random();
            Console.WriteLine("What's your piece player {0} (Col Mustard, Mr Green, Prof Prune, Mme Blue, Miss Scarlet, Mme White)?", round);
            string piece = Console.ReadLine();
            Player p = new Player(piece, round, nbPlayers,game);
                for (int i = 0; i < p.NumberOfCards; i++)
                {
                    int index = rdm.Next(cards.RemainingCards.Count());
                    string card = cards.RemainingCards[index];
                    p.Handtrail.Add(card);
                    cards.RemainingCards.Remove(card);
                }
            return p;
        }
        static void ThreadProc()
        {
            Thread.Sleep(1000);
        }
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
        }
        /// <summary>
        /// Code début du jeu
        /// </summary>
        static void Game()
        {
            // Début de partie : affichage plateau
            GameBoard board = new GameBoard("ColorBoard.csv");
            board.PrintBoard(board);

            
            //Initialisation du jeu...
            Console.WriteLine("How many players are going to play ?");
            int nbPlayers = Convert.ToInt32(Console.ReadLine());
            if (nbPlayers >= 2) {
                Player p1 = CardsDistribution(1, nbPlayers, board);
                Player p2 = CardsDistribution(2, nbPlayers, board);
                Console.WriteLine(p1.ToString() + "\n" + p2.ToString());
            }
            if (nbPlayers >= 3){ Player p3 = CardsDistribution(3, nbPlayers,board); }
            if (nbPlayers >= 4) { Player p4 = CardsDistribution(4, nbPlayers,board); }
            if (nbPlayers >= 5) { Player p5 = CardsDistribution(5, nbPlayers,board); }
            if (nbPlayers == 6) { Player p6 = CardsDistribution(6, nbPlayers,board); }
            //...jusqu'à distribution des cartes

            

        }
        static void Main(string[] args)
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
        }
    }
}
