using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Item
    {
        // Initialises the name and health attribute for the player. 
        public string Name { get; private set; }
        public int Harm { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Item(string name, int harm, int health)
        {
            Name = name;
            // This will check that the user's health is a positive integer and above 0. It could be useful in the future once the monster can attack
            Tests.TestForPositiveInteger(harm);
            Harm = harm;
            Tests.TestForPositiveInteger(health);
            Health = health;
        }
    }
}
