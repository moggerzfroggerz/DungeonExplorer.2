using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Inventory
    {
        public class InventoryItems
        {
            private List<string> items = new List<string>();

            public void AddItem(string item)
            {
                items.Add(item);
            }
            public string Backpack()
            {
                return string.Join(", ", items);
            }
        }
        // Initialises the name and health attribute for the player. 
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        Inventory inv = new Inventory();

        // The string below returns all the items in the player's backpack (inventory). 
        public string Backpack()
        {
            return string.Join(", ", inventory);
        }
    }
}