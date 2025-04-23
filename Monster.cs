using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        public Room CurrentRoom { get; set; }
        public int Damage { get; set; }
        public static Random random = new Random();
        public Monster(string name, int minimumHP, int maximumHP, int minimumDamage, int maximumDamage) : base(name, random.Next(minimumHP, maximumHP))
        {
            Debug.Assert(name != null, "Test failure: The player has not chosen a name.");
            Damage = random.Next(minimumDamage, maximumDamage);
        }
        public bool FirstToGo { get; set; } = false;
        
    }

    public class ScarySlug : Monster
    {
        // Min. HP, Max. HP, Min. Damage, Max. Damage
        public ScarySlug() : base("Scary Slug", 40, 60, 4, 10)
        {
            FirstToGo = random.Next(2) == 1;
        }
    }
}












//{
//    // This class contains the get and set methods for the monster's attributes and other methods. 
//    public class Monster : Creature
//    {
//        // Private get and set methods so that there is no accidental overlap or re-defining of the health and name variables. 
//        protected int Health { get; set; }
//        protected string Name { get; set; }
//        public Monster(string Name, int Health)
//        {
//            this.Name = Name;
//            this.Health = Health;
//        }
//        public virtual int GetHealth()
//        {
//            return this.Health;
//        }
//        public virtual string GetName()
//        {
//            return Name;
//        }
//        public virtual string Damage()
//        {
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
//        public bool IsAlive()
//        {
//            return this.Health > 0;
//        }
//    }
//}
