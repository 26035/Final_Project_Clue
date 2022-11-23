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
        public override List<Card> AllCards
        {
            get { return new List<Card> { new Card("Dagger"), new Card("Chandelier"), new Card("Revolver"), new Card("Rope"), new Card("Baton"), new Card("Knife") }; }
        }
        #endregion


    }
}
