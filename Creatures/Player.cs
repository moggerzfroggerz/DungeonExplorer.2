using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
    {
        // Initialises the name and health attribute for the player: 
        private List<Item> inventory = new List<Item>();

        public Player(string name, int health) : base(name, health)
        {
            Name = name;
            Tests.TestForPositiveInteger(health);
            Health = health;
        }
        // Orders Weapons by their value of damage caused descending: 
        public List<Item> SortWeaponsDmg()
        {
            return inventory
                .Where(item => item.ItemDmg > 0)
                .OrderByDescending(item => item.ItemDmg)
                .ToList();
        }
        // Adds items to the inventory: 
        public void AddItemToInventory(Item item)
        {
            inventory.Add(item);
        }
        // A boolean value used to confirm that the user has items in their inventory: 
        public bool HasItems()
        {
            return inventory.Count > 0;
        }
        // A string which displays the weapons and their damage value as well as the healing items and their healing value: 
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
                    inventoryDisplay.Add($"{healingItem.Name} - Healing: {healingItem.Health}");
                }
            }

            
            if (sortedWeapons.Any())
            {
                inventoryDisplay.Add("\nWeapons: ");
                foreach (var weapon in sortedWeapons)
                {
                    inventoryDisplay.Add($"{weapon.Name} - Damage: {weapon.ItemDmg}");
                }
            }


            return inventoryDisplay.Count > 0 ? string.Join("\n", inventoryDisplay) : "Your backpack is empty.";
        }

        // Allows other classes to access this method without making any changes:
        public List<string> GetInventory()
        {
            return inventory.Select(item => item.Name).ToList();
        }

        private static readonly Dictionary<string, int> healingItems = new Dictionary<string, int>
        // Below are the healing items that can be used within the game to boost the player's health points:
        {
            {"bread", 10},
            {"minor health potion", 20},
            {"major health potion", 30}
        };
        // Can be used to differentiate between healing and non-healing items: 
        public static bool IsHealingItem(string item)
        {
            string normalisedItem = item.ToLower().Trim();
            return healingItems.Keys.Any(k => k.ToLower().Trim() == normalisedItem);
            
        }

        // The code below boosts the player's health value when they choose to eat: 
        public string Consume(string itemName)
        {
            itemName = itemName.ToLower().Trim();
            var item = inventory.FirstOrDefault(i => i.Name.ToLower().Trim() == itemName);
            // Will inform the player if they try to consume something that they do not have in their posession: 
            if (item == null)
            {
                Console.Clear();
                return $"You don't have any {itemName} to consume.";
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
                    return $"> You used {itemName} and healed {healValue} Health.\n";

                }
                else
                {
                    return $"> {itemName} is not a healing item.\n";
                }
            }

            // Handles error message for if the user tries to consume something that isn't a healing item: 
            Console.Clear();
            return $"You can't consume \"{itemName}\" because it isn't a healing item!";
        }
        // Controls the damage taken by the player: 
        public override void DamageTaken(int damage)
        {
            Health -= (int)(damage * 0.9);
        }
    }
}