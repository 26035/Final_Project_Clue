using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    abstract class Cards
    {
        //Attributes
        protected List<Card> familyCards;
        protected Card cardMurderer;
        //Properties
        public abstract List<Card> FamilyCards { get; }
        public abstract Card CardMurderer { get; set; }
        //Methods
        public override string ToString()
        {
            return PrintList(familyCards) + "\nmurderer " + CardMurderer;
        }
        public static string PrintList(List<Card> list)
        {
            string res = "";
            foreach (var line in list)
            {
                res = res + line.Name +"-"+line.ID+ " / ";
            }
            return res;
        }
    }
}
