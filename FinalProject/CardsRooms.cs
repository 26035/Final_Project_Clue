using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CardsRooms : Cards
    {
        
        #region properties
        public override List<Card> AllCards
        {
            get { return new List<Card> { new Card("Kitchen"), new Card("Lounge"), new Card("Ballroom"), new Card("Dinning Room"), new Card("Hall"), new Card("Billard Room"), new Card("Library"), new Card("Study"), new Card("Greehouse") }; }
        }
        #endregion


    }
}
