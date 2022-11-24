using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CardsWeapons : Cards
    {

        #region properties
        public override List<Card> FamilyCards
        {
            get { return new List<Card> { new Card("Dagger",16), new Card("Chandelier",17), new Card("Revolver",18), new Card("Rope",19), new Card("Baton",20), new Card("Knife",21) }; }
        }
        public override Card CardMurderer { get => this.cardMurderer; set => cardMurderer =value; }


        #endregion


    }
}
