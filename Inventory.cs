﻿using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Inventory
    {
        // Manages the player's Inventory: 
        public class InventoryItems
        {
            private List<string> items = new List<string>();

            public void AddItemToInventory(string item)
            {
                items.Add(item);
            }
            public string Backpack()
            {
                return string.Join(", ", items);
            }
        }
        // Initialises the name and health attributes for the player:
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        Inventory inv = new Inventory();

        // The string below joins all the items in the player's backpack (inventory): 
        public string Backpack()
        {
            return string.Join(", ", inventory);
        }
    }
}