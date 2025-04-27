using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
    {
        // Initialises the name and health attribute for the player: 
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) : base(name, health)
        {
            Name = name;
            Tests.TestForPositiveInteger(health);
            Health = health;
        }
        public void AddItemToInventory(string item)
        {
            inventory.Add(item);
        }
        public bool HasItems()
        {
            return inventory.Count > 0;
        }

        public string ShowInventory()
        {
            return inventory.Count > 0 ? string.Join(", ", inventory) : "Your backpack is empty";
        }
        // The variable below returns the user's health value:  
        public int ShowHealth()
        {
            return this.Health;
        }

        // Allows other classes to access this method without making any changes:
        public List<string> GetInventory()
        {
            return new List<string>(inventory);
        }

        private static readonly Dictionary<string, int> healingItems = new Dictionary<string, int>
        // Below are the healing items that can be used within the game:
        {
            {"Bread", 10},
            {"Minor Health Potion", 20},
            {"Major Health Potion", 30}
        };

        public static bool IsHealingItem(string item)
        {
            string normalisedItem = item.ToLower().Trim();
            return healingItems.Keys.Any(k => k.ToLower().Trim() == normalisedItem);
            //var healingItems = new List<string> { "bread,", "minor health potion", "major health potion" };
            //return healingItems.Contains(item.ToLower().Trim());
        }

        // The code below boosts the player's health value when they choose to eat: 
        public string Consume(string item)
        {
            item = item.ToLower().Trim();

            if (!inventory.Contains(item))
            {
                Console.Clear();
                return $"\nYou don't have any {item} to consume.";
            }

            // Does healing, as long as the item is a healing item:
            if (healingItems.ContainsKey(item))
            {
                int healValue = healingItems[item];
                Health = Math.Min(Health + healValue, 300);
                inventory.Remove(item);
                Console.Clear();
                return $"You used {item} and healed {healValue} Health. Your current Health is: {Health}.";
            }
            else
            {
                return $"{item} is not a healing item.";
            }
        }
        // The method below alters the players health value when a monster has dealt damage: 
        public void DamagePlayer(int damage)
        {
            Health = Health - damage;
            Console.WriteLine($"You lost {damage} Health!");
        }
        public bool Escaped()
        {
            return Health > 0;
        }
    }
}