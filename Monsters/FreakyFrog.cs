using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the frog's attributes and other methods:  
    internal class FreakyFrog : Monster
    {
        // Defines the Monster name, Main Health, Minimum Health, Maximum Health, Minimum Damage, Maximum Damage:
        public FreakyFrog() : base("Freaky Frog", 90, 70, 100, 15, 25)
        {
        }

        public override string Damage()
        {
            // This allows the monster to deal random damage within the minimum and maximum damage values:
            Random random = new Random();
            int damageDone = random.Next(MinimumDamage, MaximumDamage + 1);

            this.Health -= damageDone;

            if (this.Health < 0)
            {
                this.Health = 0;
            }

            return $"You hit the {Name} as hard as you could, dealing {damageDone} damage.\nCroaking, it leaps backwards, and now has {Health} health.";
        }
    }
}