using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
        public int GetHealth()
        {
            return this.Health;
        }
        public string GetName()
        {
            return Name;
        }
        // Randomly generates a value of damage to the monster: 
        public virtual string Damage(int damageDone)
        {
            Random random = new Random();
            int damageGiven = random.Next(MinimumDamage, MaximumDamage + 1);

            this.Health -= damageDone;

            if (this.Health < 0)
            {
                this.Health = 0;
            }

            return $"You hit the {this.Name} as hard as you could, dealing {damageDone}. It now has {this.Health} health.";

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
        public virtual int AttackPlayer(Player target)
        {
            Random random = new Random();
            int damage = random.Next(MinimumDamage, MaximumDamage + 1);
            target.DamageTaken(damage);
            Console.WriteLine($"\n{Name} attacks {target.Name} for {damage} damage...\n\n");

            return damage;
        }
        // Overrised the DamageTaken method for the Monster's when they are attacked: 
        public override void DamageTaken(int damage)
        {
            Health -= (int)(damage * 1.1);
        }
    }
}