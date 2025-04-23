using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the monster's attributes and other methods. 
    internal class Wolf : Monster
    {
        public Wolf() : base("Wolf", 30)
        {
        }

        public override string Damage()
        {
            this.Health = this.Health - 10;
            return $"You attacked the {Name}! It now has {Health} health";
        }
        public bool Tame()
        {
            return true;
        }
    }
}
