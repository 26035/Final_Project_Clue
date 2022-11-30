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
        /// Used to randomly define the 3 murder cards and create a deck without the 3 murder cards
        /// </summary>
        /// <returns>list of card without the 3 murder cards</returns>
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
        /// Used to create a player’s handtrail by distributing cards randomly
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="remainingCards">list of card that represents the deck</param>
        /// <returns>list of card that represents the player's handtrail</returns>
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
        /// <summary>
        /// Used to remove a specific card from a list of card 
        /// </summary>
        /// <param name="ListCards">list of car to remove the card from</param>
        /// <param name="card">card to remove</param>
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
        /// <summary>
        /// used to know if the player’s accusation is correct
        /// </summary>
        /// <param name="Accusation">list of the 3 accused card </param>
        /// <returns>bool that represent the status of the accusation (correct or not)</returns>
        public static bool ComparisonAccusationAndMurder(List<Card> Accusation)
        {
            bool rightAccusation = true;
            for(int i =0;i<3;i++)
            {
                if(Accusation[0].ID!=CardsManager.cardsRooms.CardMurderer.ID || 
                    Accusation[1].ID!= CardsManager.cardsSuspects.CardMurderer.ID|| 
                    Accusation[2].ID!= CardsManager.cardsWeapons.CardMurderer.ID)
                {
                    rightAccusation = false;
                }
            }
            return rightAccusation;
        }
        
        
    }
}
