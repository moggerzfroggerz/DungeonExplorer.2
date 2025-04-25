using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the monster's attributes and other methods. 
    internal class FreakyFrog : Monster
    {
        // Monster name, Main HP, Minimum HP, Maximum HP, Minimum Damage, Maximum Damage:
        public FreakyFrog() : base("Freaky Frog", 90, 70, 100, 15, 25)
        {
        }

        public override string Damage()
        {
            // This allows the monster to deal random damage within the minimum and maximum damage values:
            Random random = new Random();
            int damageDealt = random.Next(MinDmg, MaxDmg + 1);

            this.Health -= damageDealt;

            if (this.Health < MinHP)
            {
                this.Health = MinHP;
            }

            return $"You hit the {Name} as hard as you could, dealing {damageDealt} damage. Croaking, it leaps backwards, and now has {Health} health.";
        }
    }
}