using System;
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
        private Monster monster;

        public Game()
        {
            // Initialize the game with a room, a player, and a monster, and sets their health values. 
            currentRoom = new Room("You enter a dim room and are faced with a huge slug-like monster! You notice a shimmering item on the floor...");
            player = new Player("Player", 100);

        }
        public void Start()
        {
            // This is the main part of the program, which will iterate whilst the playing condition is true and monster health is greater than 0. 
            bool playing = true;
            Monster slug = new ScarySlug();
            Monster wolf = new Wolf();
            Console.WriteLine(currentRoom.RoomDescription());
            while (playing == true)
            {
                // Lines 34 and 35 write the current player and monster health to the console each time the loop iterates. 
                Console.WriteLine($"Your current health is: {player.ShowHealth()}");
                Console.WriteLine($"Scary slug current health is: {slug.GetHealth()}");

                // Line 38 assigns the user input from the ExplorerInput() function so that it can be used in the conditions below. 
                string input = this.ExplorerInput();

                // The following conditional statements call the appropriate methods in relation to what action the user has chosen to take. 
                if (input == ("d"))
                {
                    Console.WriteLine(slug.Damage());
                }
                else if (input == ("s") && player.HasItems() == false)
                {
                    string PlayerInv = currentRoom.RoomItems();
                    player.AddItem(PlayerInv);
                    Console.WriteLine($"On the floor, there was {PlayerInv} and you picked it up.");
                }
                else if (input == ("b") && player.HasItems() == true)
                {
                    Console.WriteLine($"Your backpack contains: {player.ShowInventory()}");
                    Console.WriteLine("Enter u to use the key to unlock a door or r to return it to the backpack.");
                    string secondInput = Console.ReadLine();
                    if (secondInput == ("r"))
                    {
                        Console.WriteLine("Key has been returned to backpack");
                    }
                    else if (secondInput == ("u"))
                    {
                        Console.WriteLine("Door one is made of rotting wood and is covered in rust and moss. Door two is made of shiny metal.");
                        Console.WriteLine("Enter 1 to try the key in door one.");
                        Console.WriteLine("Enter 2 to try the key in door two.");
                        string doorChoiceInput = Console.ReadLine();
                        if (doorChoiceInput == ("1"))
                        {
                            // This option is one of the two path choices that allows the player to exit the dungeon and "win" the game, killing the slug and exiting the game. 
                            Console.WriteLine("The key fits! With a little force, the door opens to reveal a forest bathed in sunlight.");
                            Console.WriteLine($"Standing before you is a {wolf.GetName()}.");
                            wolf.AttackPlayer(player);
                            Console.WriteLine("To escape the dungeon you must kill or tame the wolf!");
                            Console.WriteLine("You search for an item to help you, you see a sword and a bone.");
                            Console.WriteLine("Enter K to pick up the sword and kill the wolf.");
                            Console.WriteLine("Enter T to pick up the bone and tame the wolf.");
                            string killOrTame = Console.ReadLine();
                            if (killOrTame == ("K"))
                            {
                                wolf.Damage();
                            }
                            if (killOrTame == ("T"))
                            {
                                //wolf.
                            }
                            if (!wolf.IsAlive() && player.ShowHealth() > 0)
                            {
                                Console.WriteLine("Congratulations, you have escaped the dungeon and won the game!");
                                playing = false;
                                break;
                            }


                        }
                        if (doorChoiceInput == ("2"))
                        {
                            Console.WriteLine("Regardless of how hard you try, the key does not fit and the door will not open.");
                            Console.WriteLine("In the time it took you to try the door, the scary slug regained 10 health.");
                            slug.Heal();

                        }
                    }
                }
                else if (input == ("e"))
                {
                    Console.WriteLine(player.Eat());
                }

                else
                {
                    // If the user enters a character that is not an option, this statement will tell them to choose a letter above. 
                    Console.WriteLine("Please input a letter from the options above.");
                }
            }
            // Damaging the monster until its health is less than 0 is the second path to win the game.
            Console.WriteLine("You have killed the Scary Slug.");
            Console.WriteLine("Congratulations, you have won the game!");

        }




        private string ExplorerInput()
        {
            // This section presents the user with their options and returns their choice as the variable 'input' that is used above. 
            Console.WriteLine("From here, you can choose from the following options:");
            Console.WriteLine("Enter d to deal damage");
            Console.WriteLine("Enter e to eat food and regain health");

            // The conditional statements below makes sure that whilst the user hasn't searched for items, they can't use them. 
            // It won't give them the option to view the backpack contents, and consequently unlock any doors, until they have searched for items. 
            if (player.HasItems() == false)
            {
                Console.WriteLine("Enter s to search for items");
            }
            if (player.HasItems() == true)
            {
                Console.WriteLine("Enter b to view the backpack contents");
            }

            string input = Console.ReadLine();
            // I used Debug.Assert here to test and make sure that the user input a correct value into the game.
            Debug.Assert(input == "d" || input == "e" || input == "s" || input == "b", "Test failure. Please input one of the letters shown on the screen.");
            return input;
        }
    }
}
