using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Square
    {
        //Attributes
        char path;
        char roomName;
        //Constructor
        public Square(char path, char room)
        {
            this.path = path;
            this.roomName = room;
        }
        //Properties
        public char Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        public char RoomName
        {
            get { return this.roomName; }
        }
    }
}
