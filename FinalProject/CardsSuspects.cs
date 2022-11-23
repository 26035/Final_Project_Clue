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
        public override List<Card> AllCards
        {
            get { return new List<Card> { new Card("Col Mustard"), new Card("Mr Green"), new Card("Prof Plum"), new Card("Mrs Blue"), new Card("Miss Scarlet"), new Card("Mrs White") }; }
        }

        #endregion
    }
}
