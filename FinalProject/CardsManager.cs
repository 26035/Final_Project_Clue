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
    //montrer une carte 
    public static class CardsManager
    {
        // initialisation of cards 
        /// <summary>
        /// initialization of cardsSuspects, cardsWeapons, cardsrooms
        /// draw of the murder's 3 cards 
        /// </summary>
        /// <returns></returns>
        public static List<string> Initialization()
        {
            
            CardsSuspects cardsSuspects = new CardsSuspects();
            CardsWeapons cardsWeapons = new CardsWeapons();
            CardsRooms cardsRooms = new CardsRooms();

            cardsSuspects.NameMurderer = cardsSuspects.Suspects[Program.random.Next(cardsSuspects.Suspects.Count)];
            cardsWeapons.MurderWeapon = cardsWeapons.Weapons[Program.random.Next(cardsWeapons.Weapons.Count)];
            cardsRooms.CrimeScene = cardsRooms.Rooms[Program.random.Next(cardsRooms.Rooms.Count)];
            List<string> remainingCards;
            remainingCards = cardsSuspects.Suspects.Concat(cardsWeapons.Weapons.Concat(cardsRooms.Rooms.ToList())).ToList();
            remainingCards.Remove(cardsSuspects.NameMurderer);
            remainingCards.Remove(cardsWeapons.MurderWeapon);
            remainingCards.Remove(cardsRooms.CrimeScene);
            return remainingCards;
        }

        //distributions of cards 

        /// <summary>
        /// for each player in param, distribution of the cards for his handtrail
        /// </summary>
        /// <param name="p"></param>
        /// <param name="remainingCards"></param>
        /// <returns></returns>
        public static List <string> CardsDistribution(Player p, List<string> remainingCards)
        {
            for (int i = 0; i < p.NumberOfCards; i++)
            {
                int index = Program.random.Next(remainingCards.Count());
                string card = remainingCards[index];
                p.Handtrail.Add(card);
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
