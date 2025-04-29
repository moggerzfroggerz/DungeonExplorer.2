using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the Scary Slug's methods and attributes: 
    internal class ScarySlug : Monster
    {
        // Defines the Monster name, Main Health, Minimum Health, Maximum Health, Minimum Damage, Maximum Damage:
        public ScarySlug() : base("Scary Slug", 75, 60, 80, 5, 15)
        {
        }

        public override string Damage(int damageDone)
        {
            // This allows the monster to deal random damage within the minimum and maximum damage values:
            Random random = new Random();
            int damageGiven = random.Next(MinimumDamage, MaximumDamage + 1);

            this.Health -= damageDone;

            if (this.Health < 0)
            {
                this.Health = 0;
            }

            return $"You hit the {Name} as hard as you could, dealing {damageDone} damage.\nIt slithers back, squelching, and now has {Health} health.";
        }
    }
}