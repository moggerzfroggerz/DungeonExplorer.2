﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the Gremlin's attributes and other methods:  
    internal class GreedyGremlin : Monster
    {
        // Defines the Monster name, Main Health, Minimum Health, Maximum Health, Minimum Damage, Maximum Damage:
        public GreedyGremlin() : base("Greedy Gremlin", 60, 55, 100, 20, 30)
        {
        }

        public override string Damage(int damageDone)
        {
            // This allows the monster to deal random damage within the minimum and maximum damage values:
            Random random = new Random();
            int damageGiven = random.Next(MinimumDamage, MaximumDamage + 1);

            this.Health -= damageGiven;

            if (this.Health < 0)
            {
                this.Health = 0;
            }

            return $"You hit the {Name} as hard as you could, dealing {damageDone} damage.\nScreeching, it leaps backwards, and now has {Health} health.";
        }
    }
}