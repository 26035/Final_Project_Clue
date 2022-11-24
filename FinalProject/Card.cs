using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Card
    {
        protected string name;
        protected int iD;
        //properties
        public string Name => this.name;
        public int ID => this.iD;
        //Constructor 
        public Card(string name, int ID)
        {
            this.name = name;
            this.iD = ID;
        }
    }
}
