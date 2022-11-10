using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Cards
    {
        List<string> suspects;
        List<string> weapons;
        List<string> rooms;
        List<string> remainingCards;
        string nameMurderer;
        string murderWeapon;
        string crimeScene;
        //Constructor 
        public Cards()
        {
            this.suspects = new List<string> { "Col Mustard", "Mr Green", "Prof Plum", "Mrs Blue", "Miss Scarlet", "Mrs White" };
            this.weapons = new List<string> { "Dagger", "Chandelier", "Revolver", "Rope", "Baton", "Knife" };
            this.rooms = new List<string> { "Kitchen", "Lounge", "Ballroom", "Dinning Room", "Hall", "Billard Room", "Library", "Study", "Greehouse" };
            Random rand = new Random();
            this.nameMurderer = suspects[rand.Next(suspects.Count)];
            this.murderWeapon = weapons[rand.Next(weapons.Count)];
            this.crimeScene = rooms[rand.Next(rooms.Count)];

            this.remainingCards = suspects.Concat(weapons.Concat(rooms.ToList())).ToList();
            remainingCards.Remove(nameMurderer);
            remainingCards.Remove(murderWeapon);
            remainingCards.Remove(crimeScene);

        }
        //Properties
        public List<string> RemainingCards
        {
            get{ return this.remainingCards; }
            set { this.remainingCards = value; }
        }
        //Methods
        public string PrintList(List<string> list)
        {
            string res = "";
            foreach (var line in list)
            {
                res = res+ line + " / ";
            }
            return res;
        }
        public override string ToString()
        {
            return "Suspects : " + PrintList(suspects) +
                    "\nWeapons : " + PrintList(weapons) +
                    "\nCrime scenes : " + PrintList(rooms);
        }
        public string ToStringMurderer()
        {
            return this.nameMurderer + " killed the victim with a " + this.murderWeapon + " int the " + this.crimeScene;
        }

    }
}
