using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Media;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Player name and HP:
            player = new Player("Player", 100);

            // Bread is a healing item that every player begins with:
            player.AddItem("Bread");

            // The rooms are created below:
            Room room1 = new Room("A dark room, covered in slime, with a foul aroma overpowering your nose.\nA huge slug-like monster is watching you, creeping closer...");
            Room room2 = new Room("A dimly lit room. Remnants of bones crunch as you take each step forwards.\nA fierce wolf drops down from its perch, snarling at you...");
            Room room3 = new Room("A damp room, the walls covered with moss.\nYou hear the croaking of a frog nearby.\nYou turn around, and are greeted by its huge, bulging eyes staring deep within your soul...");
            
            // Monsters are assigned to their rooms below:
            Monster ScarySlug = new ScarySlug();
            room1.RoomMonster = ScarySlug;

            Monster Wolf = new Wolf();
            room2.RoomMonster = Wolf;

            Monster FreakyFrog = new FreakyFrog();
            room3.RoomMonster = FreakyFrog;

            // The rooms are connected together below:
            room1.ConnectRoom("North", room2); // Connects Room1 to Room2 via the North
            room2.ConnectRoom("South", room1); // Connects Room2 to Room1 via the South
            room2.ConnectRoom("North", room3); // Connects Room2 to Room3 via the North
            room3.ConnectRoom("South", room2); // Connects Room3 to Room2 via the South

            // The game starts the player in the room shown below:
            currentRoom = room1;
        }

        public void Start()
        {
            bool playing = true;
            // Displays a description of the current room:
            Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");

            // Displays items found within the current room:
            Console.WriteLine($"\nItems in this room: {currentRoom.RoomItems()}.");

            while (playing)
            {
                // Current player and monster health are displayed:
                Console.WriteLine($"\nYour current HP is:");
                // Player HP value is shown in green:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.ShowHealth());
                Console.ResetColor();
                Console.WriteLine();

                if (currentRoom.RoomMonster != null)
                {
                    Console.WriteLine($"\n{currentRoom.RoomMonster.GetName()} current HP is:");
                    // Monster HP values are shown in Dark Magenta:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(currentRoom.RoomMonster.GetHealth());
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\nYou cannot attack as there is no monster in this room.");
                }
                string input = ExplorerInput();

                // Error-handling is used here to make sure the player cannot deal damage when no monster exists:
                if (input == "d" && currentRoom.RoomMonster != null)
                {
                    // Console is cleared so the game looks tidier:
                    Console.Clear();

                    // Player deals damage to the monster:
                    Console.WriteLine(currentRoom.RoomMonster.Damage());

                    // If monster still has HP, it will attack the player:
                    if (currentRoom.RoomMonster.IsAlive())
                    {
                        currentRoom.RoomMonster.AttackPlayer(player);
                    }

                }
                else if (input == "c" && !player.HasItems())
                {
                    string item = currentRoom.RoomItems();
                    player.AddItem(item);
                    Console.Clear();
                    Console.WriteLine($"You have picked up {item}.");
                }
                else if (input == "b" && player.HasItems())
                {
                    Console.WriteLine($"\nYour backpack contains: {player.ShowInventory()}");
                    Console.WriteLine("\nEnter 'u' to use an item, or enter 'r' to return the item to the backpack.");
                    string secondInput = Console.ReadLine();

                    if (secondInput == "r")
                    {
                        Console.Clear();
                        Console.WriteLine("You returned the item to the backpack.");
                    }
                    else if (secondInput == "u")
                    {
                        // Asks the player what item they want to use:
                        Console.WriteLine("Enter the item that you want to use:");
                        string itemToUse = Console.ReadLine();

                        string result = player.Consume(itemToUse);
                        Console.WriteLine(result);
                    }
                }
                else if (input == "m")
                {
                    // Moves the player to another room:
                    MoveToAnotherRoom();
                }
                else if (input == "e")
                {
                    var inventoryItems = player.GetInventory();
                    var usableItems = new List<string>();

                    foreach (var item in inventoryItems)
                    {
                        if (Player.IsHealingItem(item))
                        {
                            usableItems.Add(item);
                        }
                    }

                    if (usableItems.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\nYou currently have no healing items.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nYour healing items:");
                        Console.WriteLine(string.Join(", ", usableItems));
                        Console.WriteLine("\nWhich item would you like to use?");
                        string itemToUse = Console.ReadLine()?.Trim(); // Trim user input too

                        if (usableItems.Contains(itemToUse))
                        {
                            Console.WriteLine(player.Consume(itemToUse));
                        }
                        else
                        {
                            Console.WriteLine($"{itemToUse} is not a healing item in your inventory.");
                        }
                    }
                }
                else
                {
                    // If the user enters an invalid input, this string will be shown:
                    Console.WriteLine("Please enter a valid option.");
                }
                // Checks to see if the monster has been defeated and the player is still alive:
                if (currentRoom.RoomMonster != null && !currentRoom.RoomMonster.IsAlive() && player.ShowHealth() > 0)
                {
                    Console.Clear();
                    // Shows the player that they have slain the monster:
                    Console.WriteLine("You have defeated the monster in this room!");

                    // String prints when player has killed the monster:
                    currentRoom.SetDescription("Remains of a defeated monster lay flat on the floor.");

                    string itemDropped = "a mysterious key";
                    Console.WriteLine($"\n> The {currentRoom.RoomMonster.GetName()} dropped {itemDropped}!\n");

                    player.AddItem(itemDropped);

                    currentRoom.RoomMonster = null;

                    Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");

                    if (currentRoom.RoomMonster == null)
                    {
                        Console.WriteLine("Everything feels still. You feel as though you must push forwards...");
                        // Displays items found within the current room:
                        Console.WriteLine($"\nItems in this room: {currentRoom.RoomItems()}");
                    }
                    
                }
            }
        }

        private string ExplorerInput()
        {
            Console.WriteLine("");
            Console.WriteLine("\nMake your move:");
            Console.WriteLine("Enter d to deal damage.");
            Console.WriteLine("Enter e to eat food and regain health.");
            if (!player.HasItems()) Console.WriteLine("Enter c to collect items.");
            if (player.HasItems()) Console.WriteLine("Enter b to open your backpack.");
            Console.WriteLine("Enter m to move to another room.");

            string input = Console.ReadLine();
            Debug.Assert(input == "d" || input == "e" || input == "c" || input == "b" || input == "m", "Invalid input.");
            return input;
        }

        private void UnlockDoors()
        {
            // Prompt to see if the player wants to unlock the door:
            Console.WriteLine("You come across a door. Do you want to use an item to unlock it?");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                Console.WriteLine("The door is unlocked! You move forward.");
            }
            else
            {
                Console.WriteLine("You decide not to use the item.");
            }
        }

        // Method to convert the user's input to begin with a capital letter. This is for error-handling, and means that the user can go in a direction, if they use lowercase or capital:
        private string CapitalInitialLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;  // Return the string as-is if it's empty or null.

            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }
        private void MoveToAnotherRoom()
        {
            // Show the rooms that are possible to be entered from the player's current room:
            Console.WriteLine("\nAvailable directions: \n");
            Console.WriteLine(currentRoom.GetAvailableDirections());

            // Console asks the player which direction they want to go:
            Console.WriteLine("\nWhere would you like to go?");
            // The Trim() method is used so that the user can still go in a direction, even if they accidentally type a blank character:
            string direction = Console.ReadLine().Trim();

            // The console is cleared everytime the player moves to another room. This is to prevent confusion for the player, and makes the game tidier:
            Console.Clear();

            // The first letter of the user input is capitalised so that it will be a valid input, regardless of what case was used:
            direction = CapitalInitialLetter(direction);

            if (currentRoom.ConnectedRooms.ContainsKey(direction))
            {
                currentRoom = currentRoom.ConnectedRooms[direction];
                Console.WriteLine($"\nCurrent Room: {currentRoom.RoomDescription()}");

                // Check if there's a monster:
                if (currentRoom.RoomMonster != null)
                {
                    Console.WriteLine($"\nYou encounter a {currentRoom.RoomMonster.GetName()}!");
                }

                // Show room items:
                Console.WriteLine($"\nItems in this room: {currentRoom.RoomItems()}");
            }
            else
            {
                Console.WriteLine("Invalid direction. Try again.");
            }
        }
    }
}