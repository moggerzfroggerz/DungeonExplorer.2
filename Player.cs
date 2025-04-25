using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
    {
        // Initialises the name and health attribute for the player. 
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) : base(name, health)
        {
            Name = name;
            Tests.TestForPositiveInteger(health);
            Health = health;
        }
        public void AddItem(string item)
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
        // The code below returns the user's health value. 
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
            {"Minor HP Potion", 20},
            {"Major HP Potion", 30}
        };

        public static bool IsHealingItem(string item)
        {
            return healingItems.ContainsKey(item);
        }

        // The code below boosts the player's health when they choose to eat. 
        public string Consume(string item)
        {
            if (!inventory.Contains(item))
            {
                Console.Clear();
                return $"\nYou don't have any {item} to consume.";
            }

            // Does healing, as long as the item is a healing item:
            if (healingItems.ContainsKey(item))
            {
                int healAmount = healingItems[item];
                Health = Math.Min(Health + healAmount, 300);
                inventory.Remove(item);
                Console.Clear();
                return $"You used {item} and healed {healAmount} HP. Your current HP is: {Health}.";
            }
            else
            {
                return $"{item} is not a healing item.";
            }
        }
        public void DamagePlayer(int damage)
        {
            Health = Health - damage;
            Console.WriteLine($"You lost {damage} HP!");
        }
        public bool Escaped()
        {
            return Health > 0;
        }
    }
}