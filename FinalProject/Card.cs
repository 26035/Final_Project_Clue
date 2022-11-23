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
        //properties
        public string Name { get { return this.name; } }

        //Constructor 
        public Card(string name)
        {
            this.name = name;
        }
    }
}
