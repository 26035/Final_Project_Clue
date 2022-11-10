using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class CardsRooms : Cards
    {
        #region attributes
        protected List<string> rooms;
        protected string crimeScene;
        #endregion
        #region properties
        public override string Name => this.name = "rooms";
        public string CrimeScene
        {
            get { return crimeScene; }
            set { crimeScene = value; }
        }
        public List<string> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }
        #endregion
        #region constructor 
        public CardsRooms()
        {
            this.rooms = new List<string> { "Kitchen", "Lounge", "Ballroom", "Dinning Room", "Hall", "Billard Room", "Library", "Study", "Greehouse" };
        }
        #endregion

    }
}
