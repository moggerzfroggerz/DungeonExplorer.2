using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Item
    {
        // Initialises the name and health attribute for the item: 
        public string Name { get; private set; }
        public int ItemDmg { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Item(string name, int itemDmg = 0, int health = 0)
        {
            Name = name;
            // This will check that the user's health is 0 or above:
            Tests.TestForZeroOrAbove(itemDmg);
            ItemDmg = itemDmg;
            Tests.TestForZeroOrAbove(health);
            Health = health;
        }
    }
}
