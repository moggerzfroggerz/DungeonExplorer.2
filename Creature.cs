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
        public void DamageTaken(int damage)
        {
            Health -= damage;
        }
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}