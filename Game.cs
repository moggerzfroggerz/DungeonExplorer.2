using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
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
            // Player name and Health:
            player = new Player("Player", 100);

            // Bread is a healing item that every player begins with:
            Item Bread = new Item("Bread");
            player.AddItemToInventory(Bread);

            // The rooms are created below:
            Room room1 = new Room("A dark room, covered in slime, with a foul aroma overpowering your nose.\nA huge slug-like monster is watching you, creeping closer...");
            Room room2 = new Room("A dimly lit room. Remnants of bones crunch as you take each step forwards.\nA fierce wolf drops down from its perch, snarling at you...");
            Room room3 = new Room("A damp room, the walls covered with moss.\nYou hear the croaking of a frog nearby.\nYou turn around, and are greeted by its huge, bulging eyes staring deep within your soul...");
            Room room4 = new Room("A fusty room, with wooden planks nailed to the wall. \nUpon inspection, a caterpillar gnaws its narly teeth into the planks. \nIt stills, lifts its head to fix its 6 crazed eyes upon you...");
            Room room5 = new Room("A compact space with nowhere to move your feet. \nBlocking the doorway, a large, venomous snake wiggles its slimy, forked tongue at you in delight...");
            Room room6 = new Room("At first this room appears empty.\nTapping you on the shoulder, a green gremlin glares at you, its teeth glinting...");
            Room room7 = new Room("The room is pitch black, but you can hear the scurrying of millions of bugs beneath your feet.\nLighting a lantern, a Sneaky Silverfish the size of a small child appears before you...");
            
            // Monsters are assigned to their rooms below:
            Monster ScarySlug = new ScarySlug();
            room1.MonsterInRoom = ScarySlug;

            Monster WarWolf = new WarWolf();
            room2.MonsterInRoom = WarWolf;

            Monster FreakyFrog = new FreakyFrog();
            room3.MonsterInRoom = FreakyFrog;

            Monster CreepyCaterpillar = new CreepyCaterpillar();
            room4.MonsterInRoom = CreepyCaterpillar;

            Monster SassySnake = new SassySnake();
            room5.MonsterInRoom = SassySnake;

            Monster GreedyGremlin = new GreedyGremlin();
            room6.MonsterInRoom = GreedyGremlin;

            Monster SneakySilverfish = new SneakySilverfish();
            room7.MonsterInRoom = SneakySilverfish;

            // The rooms are connected together below:
            room1.ConnectRoom("North", room2); 
            room2.ConnectRoom("South", room1); 

            room2.ConnectRoom("North", room3); 
            room3.ConnectRoom("South", room2); 
           
            room4.ConnectRoom("East", room3); 
            room3.ConnectRoom("West", room4); 

            room4.ConnectRoom("South", room5); 
            room5.ConnectRoom("North", room4); 

            room5.ConnectRoom("South", room6); 
            room6.ConnectRoom("North", room5); 

            room6.ConnectRoom("East", room7); 
            room7.ConnectRoom("West", room6); 

            // The game starts the player in the room shown below:
            currentRoom = room1;
        }

        public void Start()
        {
            bool playing = true;
            // Displays a description of the current room:
            Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");

            // Displays items found within the current room:
            Console.WriteLine($"\n- Items in this room: {currentRoom.RoomItems()}.");

            while (playing)
            {
                // Current player and monster Health are displayed:
                Console.WriteLine($"\nYour current Health is:");
                // Player Health value is shown in green:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.ShowHealth());
                Console.ResetColor();
                Console.WriteLine();

                if (currentRoom.MonsterInRoom != null)
                {
                    Console.WriteLine($"\n{currentRoom.MonsterInRoom.GetName()} current Health is:");
                    // Monster Health values are shown in Dark Magenta:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(currentRoom.MonsterInRoom.GetHealth());
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\nYou cannot attack as there is no monster in this room.");
                }
                string input = ExplorerInput();

                // Error-handling is used here to make sure the player cannot deal damage when no monster exists:
                if (input == "d" && currentRoom.MonsterInRoom != null)
                {
                    var bestWeapon = player.SortWeaponsDmg().FirstOrDefault();
                    if (bestWeapon != null)
                    {
                        // Console is cleared so the game looks tidier:
                        Console.Clear();

                        int damageDone = bestWeapon.ItemDmg;
                        // Player deals damage to the monster:
                        Console.WriteLine(currentRoom.MonsterInRoom.Damage(damageDone));

                        // If monster still has Health, it will attack the player:
                        if (currentRoom.MonsterInRoom.IsAlive())
                        {
                            currentRoom.MonsterInRoom.AttackPlayer(player);
                        }
                    }

                }
                else if (input == "c")
                {
                    foreach (var item in currentRoom.Inventory.ToList())
                    {
                        player.AddItemToInventory(item);
                        currentRoom.RemoveItem(item);   
                    }
                    Console.WriteLine($"\nYou have picked up all of the items in this room.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");
                    Console.WriteLine($"\n- Items in this room: {currentRoom.RoomItems()}.");
                }
                else if (input == "b" && player.HasItems())
                {
                    Console.Clear();
                    Console.WriteLine($"Your backpack contains: {player.ShowInventory()}");
                    Console.WriteLine("\nEnter 'u' to use an item, or enter 'r' to close the backpack.");
                    string secondInput = Console.ReadLine();

                    if (secondInput == "r")
                    {
                        Console.Clear();
                        Console.WriteLine("You closed your backpack.");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");
                        Console.WriteLine($"\n- Items in this room: {currentRoom.RoomItems()}.");
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
                        //Console.Clear();
                        //Console.WriteLine("You currently have no healing items.");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Your healing items:\n");
                        Console.WriteLine(string.Join(", ", usableItems));
                        Console.WriteLine("\nWhich item would you like to use?");
                        
                        string itemToUse = Console.ReadLine()?.Trim().ToLower();

                        var matchingItem = usableItems.FirstOrDefault(item => item.ToLower().Trim() == itemToUse);

                        if (matchingItem != null)
                        {
                            Console.WriteLine(player.Consume(itemToUse));
                        }
                        else
                        {
                            Console.WriteLine($"\n{itemToUse} is not a healing item in your inventory.");
                        }
                    }
                }
                else
                {
                    // If the user enters an invalid input, this string will be shown:
                    Console.WriteLine("Please enter a valid option.");
                }
                // Checks to see if the monster has been defeated and the player is still alive:
                if (currentRoom.MonsterInRoom != null && !currentRoom.MonsterInRoom.IsAlive() && player.ShowHealth() > 0)
                {
                    Console.Clear();
                    // Shows the player that they have slain the monster:
                    Console.WriteLine("You have defeated the monster in this room!");

                    // String prints when player has killed the monster:
                    currentRoom.SetDescription("Remains of a defeated monster lay flat on the floor.");

                    String itemDropped = "a mysterious key";
                    Console.WriteLine($"\n> The {currentRoom.MonsterInRoom.GetName()} dropped {itemDropped}!\n");

                    Item droppedItem = new Item(itemDropped);
                    
                    player.AddItemToInventory(droppedItem);

                    currentRoom.MonsterInRoom = null;

                    Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");

                    if (currentRoom.MonsterInRoom == null)
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
            Console.WriteLine("\n\nMake your move:");
            Console.WriteLine("Enter d to deal damage.");
            Console.WriteLine("Enter e to eat food and regain health.");
                Console.WriteLine("Enter c to collect items.");
            if (player.HasItems()) Console.WriteLine("Enter b to open your backpack.");
            Console.WriteLine("Enter m to move to another room.");

            string input = Console.ReadLine().Trim().ToLower();
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
            // Return the string without changing it if it is empty or null:
            if (string.IsNullOrEmpty(input))
                return input;  

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

            // The first letter of the user input is capitalised so that it will be a valid input, regardless of what case was used:
            direction = CapitalInitialLetter(direction);

            if (currentRoom.ConnectedRooms.ContainsKey(direction))
            {
                currentRoom = currentRoom.ConnectedRooms[direction];
                Console.Clear();
                Console.WriteLine($"Current Room: {currentRoom.RoomDescription()}");

                // If there is a monster present in the room, displays which monster it is: 
                if (currentRoom.MonsterInRoom != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nYou encounter a {currentRoom.MonsterInRoom.GetName()}!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // Displays which items are in the player's current room: 
                Console.WriteLine($"\nItems in this room: {currentRoom.RoomItems()}");
            }
            else
            {
                // If the user tries to go somewhere where there is no room: 
                Console.WriteLine("\nInvalid direction. Try again.");
                Thread.Sleep(2000);
            }

            // The console is cleared everytime the player moves to another room. This is to prevent confusion for the player, and makes the game tidier:
        }
    }
}