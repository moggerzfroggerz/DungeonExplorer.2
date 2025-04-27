using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the Wolf's methods and attributes:  
    internal class WarWolf : Monster
    {
        // Defines the Monster name, Main Health, Minimum Health, Maximum Health, Minimum Damage, Maximum Damage:
        public WarWolf() : base("War Wolf", 30, 20, 40, 10, 20)
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

            return $"You hit the {Name} as hard as you could, dealing {damageDone} damage.\nWhimpering, it cowers backwards, growling, and now has {Health} health.";
        }
    }
}