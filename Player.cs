using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        // Initialises the name and health attribute for the player. 
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health)
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
        // The code below boosts the player's health when they choose to eat. 
        public string Eat()
        {
            Console.WriteLine("You ate some bread you found on the floor and have healed 40 health.");
            this.Health = this.Health + 40;
            return $"Your health is: {this.Health}";
        }
        public string Hurt()
        {
            Console.WriteLine("You have been hurt and lost 10 health!");
            this.Health = this.Health - 10;
            return $"Your health is: {this.Health}";
        }
        public void DamagePlayer(int damage)
        {
            Health = Health - damage;
            Console.WriteLine($"You have been attacked and lost {damage} points! Player Health: {Health}");
        }
        public bool Escaped()
        {
            return Health > 0;
        }
    }
}