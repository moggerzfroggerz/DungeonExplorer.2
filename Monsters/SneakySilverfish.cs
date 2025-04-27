using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the Sneaky Silverfish's methods and attributes: 
    internal class SneakySilverfish : Monster
    {
        // Defines the Monster name, Main Health, Minimum Health, Maximum Health, Minimum Damage, Maximum Damage:
        public SneakySilverfish() : base("SneakySilverfish", 175, 150, 250, 50, 100)
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

            return $"You hit the {Name} as hard as you could, dealing {damageDone} damage.\nIt hurriedly scuttles back, and now has {Health} health.";
        }
    }
}
