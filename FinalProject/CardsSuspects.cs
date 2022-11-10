using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class CardsSuspects : Cards
    {
        #region attributs 
        protected List<string> suspects;
        protected string nameMurderer;
        #endregion
        #region properties
        public override string Name => this.name = "cards";
        public List<string> Suspects
        {
            get { return suspects; }
            set { suspects = value; }
        }
        public string NameMurderer
        {
            get { return nameMurderer; }
            set { nameMurderer = value; }
        }

        #endregion
        #region constructor
        public CardsSuspects()
        {
            this.suspects = new List<string> { "Col Mustard", "Mr Green", "Prof Plum", "Mrs Blue", "Miss Scarlet", "Mrs White" };
        }
        #endregion
    }
}
