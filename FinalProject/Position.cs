using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Position
    {
        int row;
        int column;
        //Constructor
        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
        public Position() { }
        //Properties
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }
        public int Column
        {
            get { return this.column; }
            set { this.column = value; }
        }
        //Methods
        public override string ToString()
        {
            return "You are at :  row = " + (this.row+1)+ " column = "+(this.column+1);
        }
        public bool IsEquals(Position pos)
        {
            bool same = false;
            if (pos.Row == this.row && pos.Column == this.column)
            {
                same = true;
            }
            return same;
        }
        
    }
}
