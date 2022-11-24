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
        public override List<Card> FamilyCards
        {
            get { return new List<Card> { new Card("Kitchen",1), new Card("Lounge",2), new Card("Ballroom",3), new Card("Dinning Room",4), new Card("Hall",5), new Card("Billard Room",6), new Card("Library",7), new Card("Study",8), new Card("Greehouse",9) }; }
        }
        public override Card CardMurderer { get => this.cardMurderer; set => cardMurderer=value; }
        #endregion


    }
}
