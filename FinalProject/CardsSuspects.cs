using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CardsSuspects : Cards
    {

        #region properties
        public override List<Card> FamilyCards
        {
            get { return new List<Card> { new Card("Col Mustard",10), new Card("Mr Green",11), new Card("Prof Plum",12), new Card("Mrs Blue",13), new Card("Miss Scarlet",14), new Card("Mrs White",15) }; }
        }
        public override Card CardMurderer { get => this.cardMurderer; set => cardMurderer=value; }


        #endregion
    }
}
