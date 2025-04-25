using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // This class contains the get and set methods for the monster's attributes and other methods. 
    public class Monster : Creature
    {
        public int MinHP { get; protected set; }
        public int MaxHP { get; protected set; }
        public int MinDmg { get; protected set; }
        public int MaxDmg { get; protected set; }

        // Private get and set methods so that there is no accidental overlap or re-defining of the health and name variables. 
        public Monster(string Name, int Health, int MinHP, int MaxHP, int MinDmg, int MaxDmg) : base(Name, Health)
        {
            this.MinHP = MinHP;
            this.MaxHP = MaxHP;
            this.MinDmg = MinDmg;
            this.MaxDmg = MaxDmg;

            this.Health = Math.Min(Math.Max(Health, MinHP), MaxHP);
        }
        public virtual int GetHealth()
        {
            return this.Health;
        }
        public virtual string GetName()
        {
            return Name;
        }
        public virtual string Damage()
        {
            Random random = new Random();
            int damageDealt = random.Next(MinDmg, MaxDmg + 1);

            this.Health -= damageDealt;
            if (this.Health < MinHP)
            {
                this.Health = MinHP;
            }

            return $"You hit the {this.Name} as hard as you could, dealing {damageDealt}. It now has {Health} health.";
        }

        public virtual void Heal()
        {
            this.Health += 10;
            if (this.Health > MaxHP)
            {
                this.Health = MaxHP;
            }
        }

        public virtual void AttackPlayer(Player target)
        {
            Random random = new Random();
            int damage = random.Next(MinDmg, MaxDmg + 1);
            Console.WriteLine($"{Name} has attacked you with {damage} damage!");
            target.DamagePlayer(damage);
        }
    }
}












//            // Decreases the monster's health by 5 each time it is called. 
//            this.Health = this.Health - 5;
//            return $"You hit the {this.Name} as hard as you could... It now has {Health} health.";
//        }
//        public virtual void Heal()
//        {
//            // Increases the monster's health by 10 each time it is called. 
//            this.Health = this.Health + 10;
//        }
//        public virtual void AttackPlayer(Player target)
//        {
//            //The monster will attack the player and decrease their health. 
//            int damage = 10;
//            Console.WriteLine($"{Name} has attacked you! ");
//            target.DamagePlayer(damage);
//        }
//    }
//}
