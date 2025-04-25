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
        // Private get and set methods so that there is no accidental overlap or re-defining of the health and name variables. 
        public Monster(string Name, int Health) : base(Name, Health)
        {

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
            // Decreases the monster's health by 5 each time it is called. 
            this.Health = this.Health - 5;
            return $"You hit the {this.Name} as hard as you could... It now has {Health} health.";
        }
        public virtual void Heal()
        {
            // Increases the monster's health by 10 each time it is called. 
            this.Health = this.Health + 10;
        }
        public virtual void AttackPlayer(Player target)
        {
            //The monster will attack the player and decrease their health. 
            int damage = 10;
            Console.WriteLine($"{Name} has attacked you! ");
            target.DamagePlayer(damage);
        }
    }
}
