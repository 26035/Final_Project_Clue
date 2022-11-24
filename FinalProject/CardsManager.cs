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
            int a = 0;
            for(int i =0;i<21-a;i++)
            {
                if(remainingCards[i].ID == cardsSuspects.CardMurderer.ID || remainingCards[i].ID == cardsWeapons.CardMurderer.ID || remainingCards[i].ID == cardsRooms.CardMurderer.ID)
                {
                    remainingCards.RemoveAt(i);
                    a++;
                    
                }
            }
            /*remainingCards.RemoveAt(cardsSuspects.CardMurderer.ID-1);
            remainingCards.RemoveAt(cardsWeapons.CardMurderer.ID-1);
            remainingCards.RemoveAt(cardsRooms.CardMurderer.ID-1);*/
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
                for (int j = 0; j < remainingCards.Count; j++)
                {
                    if (card.ID == remainingCards[j].ID)
                    {
                        //p.StillSuspected.RemoveAt(j);
                        remainingCards.RemoveAt(j);
                    }
                }
                //Console.WriteLine(p.StillSuspected.Count);
                for(int j =0;j<p.StillSuspected.Count;j++)
                {
                    if (card.ID == p.StillSuspected[j].ID)
                    {
                        p.StillSuspected.RemoveAt(j);
                       
                    }
                }
                
                /*p.StillSuspected.RemoveAt(card.ID - 1 - p.NumberOfCards);
                remainingCards.RemoveAt(card.ID - 1 - p.NumberOfCards);*/
            }
            foreach (var i in p.StillSuspected)
            {
                Console.WriteLine(i.Name + "-" + i.ID);
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
