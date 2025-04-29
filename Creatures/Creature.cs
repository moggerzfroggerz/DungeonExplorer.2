using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Derived classes of creature implement IDamageable:
    public abstract class Creature : IDamageable
    {
        // Get and set methods for the name and health: 
        public string Name { get; set; }
        protected int Health { get; set; }

        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }
        public int ShowHealth()
        {
            return Health;
        }
        public virtual void DamageTaken(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
        // Boolean value used to check the creature is alive, used mainly for playing logic: 
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}