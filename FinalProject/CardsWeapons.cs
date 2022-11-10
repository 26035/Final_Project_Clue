using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class CardsWeapons : Cards
    {
        #region attributes
        protected List<string> weapons;
        protected string murderWeapon;
        #endregion
        #region properties
        public override string Name => this.name = "weapons";
        public string MurderWeapon
        {
            get { return murderWeapon; }
            set { murderWeapon = value; }
        }
        public List<string> Weapons
        {
            get { return weapons; }
            set { weapons = value; }
        }
        #endregion
        #region constructor
        public CardsWeapons()
        {
            this.weapons = new List<string> { "Dagger", "Chandelier", "Revolver", "Rope", "Baton", "Knife" };
        }
        #endregion

    }
}
