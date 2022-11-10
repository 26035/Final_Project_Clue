using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Cards
    {
        protected string name;
        public abstract string Name { get; }
        
        
        
        
        //Constructor 
        public Cards()
        {
            

        }
        /*
        public abstract List<string> RemainingCards { get; set; }
        public abstract string PrintList();
        public abstract override string ToString();
        public abstract string ToStringMurderer();

        
        public abstract Player CardDistribution();
        */
    }
}
