using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    // a faire ici 
    //initialisation cartes 
    // distribution cards
    static class CardsManager
    {
        public static CardsSuspects cardsSuspects = new CardsSuspects();
        public static CardsWeapons cardsWeapons = new CardsWeapons();
        public static CardsRooms cardsRooms = new CardsRooms();

        // initialisation of cards 
        /// <summary>
        /// initialization of cardsSuspects, cardsWeapons, cardsrooms
        /// draw of the murder's 3 cards 
        /// </summary>
        /// <returns></returns>
        public static List<Card> Initialization()
        {
            cardsSuspects.CardMurderer = cardsSuspects.AllCards[Program.random.Next(cardsSuspects.AllCards.Count)];
            cardsWeapons.CardMurderer = cardsWeapons.AllCards[Program.random.Next(cardsWeapons.AllCards.Count)];
            cardsRooms.CardMurderer = cardsRooms.AllCards[Program.random.Next(cardsRooms.AllCards.Count)];
            List<Card> remainingCards = cardsSuspects.AllCards.Concat(cardsWeapons.AllCards.Concat(cardsRooms.AllCards.ToList())).ToList();
            remainingCards.Remove(cardsSuspects.CardMurderer);
            remainingCards.Remove(cardsWeapons.CardMurderer);
            remainingCards.Remove(cardsRooms.CardMurderer);
            return remainingCards;
        }

        //distributions of cards 

        /// <summary>
        /// for each player in param, distribution of the cards for his handtrail
        /// </summary>
        /// <param name="p"></param>
        /// <param name="remainingCards"></param>
        /// <returns></returns>
        public static List <Card> CardsDistribution(Player p, List<Card> remainingCards)
        {
            for (int i = 0; i < p.NumberOfCards; i++)
            {
                int index = Program.random.Next(remainingCards.Count());
                Card card = remainingCards[index];
                p.Handtrail.Add(card);
                p.StillSuspected.Remove(card);
                remainingCards.Remove(card);
            }
            return remainingCards;
            
        }

        
        //methodes tests
        public static string PrintList(List<string> list)
        {
            string res = "";
            foreach (var line in list)
            {
                res = res+ line + " / ";
            }
            return res;
        }
        /*public static override string ToString()
        {
            return "Suspects : " + PrintList(suspects) +
                    "\nWeapons : " + PrintList(weapons) +
                    "\nCrime scenes : " + PrintList(rooms);
        }
        public override string ToStringMurderer()
        {
            return this.nameMurderer + " killed the victim with a " + this.murderWeapon + " int the " + this.crimeScene;
        }*/


        
        
    }
}
