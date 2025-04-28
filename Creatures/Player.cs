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
        private List<Item> inventory = new List<Item>();

        public Player(string name, int health) : base(name, health)
        {
            Name = name;
            Tests.TestForPositiveInteger(health);
            Health = health;
        }
        public List<Item> SortWeaponsDmg()
        {
            return inventory
                .Where(item => item.ItemDmg > 0)
                .OrderByDescending(item => item.ItemDmg)
                .ToList();
        }
        public void AddItemToInventory(Item item)
        {
            inventory.Add(item);
        }
        public bool HasItems()
        {
            return inventory.Count > 0;
        }

        public string ShowInventory()
        {
            var sortedWeapons = SortWeaponsDmg();
            var healingItems = inventory.Where(item => item.Health > 0).OrderByDescending(item => item.Health).ToList();

            var inventoryDisplay = new List<string>();

            if (healingItems.Any())
            {
                inventoryDisplay.Add("\nHealing items: ");
                foreach (var healingItem in healingItems)
                {
                    inventoryDisplay.Add($"{healingItem.Name} \n(Healing: {healingItem.Health})");
                }
            }

            
            if (sortedWeapons.Any())
            {
                inventoryDisplay.Add("\nWeapons: ");
                foreach (var weapon in sortedWeapons)
                {
                    inventoryDisplay.Add($"{weapon.Name} \n(Damage: {weapon.ItemDmg})");
                }
            }


            return inventoryDisplay.Count > 0 ? string.Join("\n", inventoryDisplay) : "Your backpack is empty.";
            //return inventory.Count > 0 ? string.Join(", ", inventoryDisplay) : "Your backpack is empty";
        }
        // The variable below returns the user's health value:  
        public int ShowHealth()
        {
            return this.Health;
        }

        // Allows other classes to access this method without making any changes:
        public List<string> GetInventory()
        {
            return inventory.Select(item => item.Name).ToList();
        }

        private static readonly Dictionary<string, int> healingItems = new Dictionary<string, int>
        // Below are the healing items that can be used within the game:
        {
            {"bread", 10},
            {"minor health potion", 20},
            {"major health potion", 30}
        };

        public static bool IsHealingItem(string item)
        {
            string normalisedItem = item.ToLower().Trim();
            return healingItems.Keys.Any(k => k.ToLower().Trim() == normalisedItem);
            //var healingItems = new List<string> { "bread,", "minor health potion", "major health potion" };
            //return healingItems.Contains(item.ToLower().Trim());
        }

        // The code below boosts the player's health value when they choose to eat: 
        public string Consume(string itemName)
        {
            itemName = itemName.ToLower().Trim();
            var item = inventory.FirstOrDefault(i => i.Name.ToLower().Trim() == itemName);

            if (item == null)
            {
                Console.Clear();
                return $"\nYou don't have any {itemName} to consume.";
            }

            // Does healing, as long as the item is a healing item:
            if (IsHealingItem(itemName))
            {
                if (healingItems.ContainsKey(itemName))
                {
                    int healValue = healingItems[itemName];
                    Health = Math.Min(Health + healValue, 300);
                    inventory.Remove(item);
                    Console.Clear();
                    return $"You used {itemName} and healed {healValue} Health. Your current Health is: {Health}.";
                }
                else
                {
                    return $"{itemName} is not a healing item.";
                }
            }
            return $"You can't consume {itemName} because it isn't a healing item...";
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