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
        public readonly static List<Card> AllCards = cardsSuspects.FamilyCards.Concat(cardsWeapons.FamilyCards.Concat(cardsRooms.FamilyCards.ToList())).ToList();

        // initialisation of cards 
        /// <summary>
        /// initialization of cardsSuspects, cardsWeapons, cardsrooms
        /// draw of the murder's 3 cards 
        /// </summary>
        /// <returns></returns>
        public static List<Card> Initialization()
        {
            cardsSuspects.CardMurderer = cardsSuspects.FamilyCards[Program.random.Next(cardsSuspects.FamilyCards.Count)];
            cardsWeapons.CardMurderer = cardsWeapons.FamilyCards[Program.random.Next(cardsWeapons.FamilyCards.Count)];
            cardsRooms.CardMurderer = cardsRooms.FamilyCards[Program.random.Next(cardsRooms.FamilyCards.Count)];
            List<Card> remainingCards = cardsWeapons.FamilyCards.Concat(cardsSuspects.FamilyCards.Concat(cardsRooms.FamilyCards.ToList())).ToList();
            RemoveCardAt(remainingCards, cardsSuspects.CardMurderer);
            RemoveCardAt(remainingCards, cardsWeapons.CardMurderer);
            RemoveCardAt(remainingCards, cardsRooms.CardMurderer);
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
                RemoveCardAt(remainingCards, card);
                RemoveCardAt(p.StillSuspected, card);
            }
            return remainingCards;
        }

        public static void RemoveCardAt(List<Card> ListCards, Card card)
        {
            for(int i = 0; i < ListCards.Count(); i++)
            {
                if(card.ID==ListCards[i].ID)
                {
                    ListCards.RemoveAt(i);
                    break;
                }
            }
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
        public static string ToStringMurderer()
        {
            return cardsSuspects.CardMurderer.Name + " killed the victim with a " + cardsWeapons.CardMurderer.Name + " in the " + cardsRooms.CardMurderer.Name;
        }


        
        
    }
}
