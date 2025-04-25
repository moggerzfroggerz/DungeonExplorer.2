using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the monster's attributes and other methods. 
    internal class ScarySlug : Monster
    {
        public ScarySlug() : base("ScarySlug", 75)
        {
        }

        public override string Damage()
        {
            this.Health = Health - 5;
            return $"You attacked the {Name}! It now has {Health} health";
        }
    }
}