using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the monster's attributes and other methods which can then be inherited and overriden to child classes: 
    public class Monster : Creature, IDamageable
    {
        public int MinimumHealth { get; protected set; }
        public int MaximumHealth { get; protected set; }
        public int MinimumDamage { get; protected set; }
        public int MaximumDamage { get; protected set; }

        // Public get and set methods so that they can be accessed from anywhere and prevents overlap:  
        public Monster(string Name, int Health, int MinimumHealth, int MaximumHealth, int MinimumDamage, int MaximumDamage) : base(Name, Health)
        {
            this.MinimumHealth = MinimumHealth;
            this.MaximumHealth = MaximumHealth;
            this.MinimumDamage = MinimumDamage;
            this.MaximumDamage = MaximumDamage;

            this.Health = Math.Min(Math.Max(Health, MinimumHealth), MaximumHealth);
        }
        public virtual int GetHealth()
        {
            return this.Health;
        }
        public virtual string GetName()
        {
            return Name;
        }
        // Randomly generates a value of damage to the monster: 
        public virtual string Damage()
        {
            Random random = new Random();
            int damageDoled = random.Next(MinimumDamage, MaximumDamage + 1);

            this.Health -= damageDoled;
            if (this.Health < MinimumHealth)
            {
                this.Health = MinimumHealth;
            }

            return $"You hit the {this.Name} as hard as you could, dealing {damageDoled}. It now has {Health} health.";
        }

        public virtual void Heal()
        {
            this.Health += 10;
            if (this.Health > MaximumHealth)
            {
                this.Health = MaximumHealth;
            }
        }
        // When called, it will randomly generate a value of damage that will be took from the player's health: 
        public virtual void AttackPlayer(Player target)
        {
            Random random = new Random();
            int damage = random.Next(MinimumDamage, MaximumDamage + 1);
            Console.WriteLine($"\n{Name} attacked you...");
            target.DamagePlayer(damage);
        }
    }
}