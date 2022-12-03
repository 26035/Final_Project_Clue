using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Die
    {
        int dieOne;
        int dieTwo;

        public int DieOne => this.dieOne;
        public int DieTwo => this.dieTwo;

        public Die()
        {

           dieOne = Program.random.Next(1,7);
           dieTwo = Program.random.Next(1,7);
           

        }
        /// <summary>
        /// Used to add up the results of the dice
        /// </summary>
        /// <returns>integer that represents the sum of the dtwo dice</returns>
        public int ResultDices()
        {
            return dieOne + dieTwo;
        }
    }
}
