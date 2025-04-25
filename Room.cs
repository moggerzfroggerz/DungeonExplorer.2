using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; set; }
        public List<string> Inventory { get; set; }
        public Dictionary<string, Room> ConnectedRooms { get; set; }
        public Monster RoomMonster { get; set; }


        private static List<string> allAvailableItems = new List<string>
        {
            "Bread",
            "Minor HP Potion",
            "Major HP Potion",
            "Shortsword",
            "Mace",
            "Boomerang",
            "Nunchucks",
            "Staff of Power",
            "Spear"
        };

        public Room(string description)
        {
            Description = description;
            Inventory = RandomlyChooseItems();
            ConnectedRooms = new Dictionary<string, Room>();
        }

        // This method connects the rooms:
        public void ConnectRoom(string direction, Room Room)
        {
            ConnectedRooms[direction] = Room;
        }

        // This method shows a description of the room:
        public string RoomDescription()
        {
            return Description;
        }
        // This method changes the decription of the room after events have taken place:
        public void SetDescription(string newDescription)
        {
            Description = newDescription;
        }

        // This method shows all of the available directions that the player can move to:
        public string GetAvailableDirections()
        {
            return string.Join(", ", ConnectedRooms.Keys);
        }

        private static Random rand = new Random();

        // Choose a random list of items for rooms:
        private List<string> RandomlyChooseItems()
        {
            int itemCount = rand.Next(1, 3);
            List<string> randomItems = new List<string>();

            for (int i = 0; i < itemCount; i++)
            {
                string randomItem = allAvailableItems[rand.Next(allAvailableItems.Count)];
                if (!randomItems.Contains(randomItem))
                {
                    randomItems.Add(randomItem);
                }
            }

            return randomItems;
        }

        // Method to show items in the room
        public string RoomItems()
        {
            if (Inventory.Count == 0)
            {
                return "No items found in this room.";
            }
            return string.Join(", ", Inventory);
        }

        // Method to add an item to the room
        public void AddItem(string item)
        {
            Inventory.Add(item);
        }

        // Method to remove an item from the room
        public void RemoveItem(string item)
        {
            Inventory.Remove(item);
        }
    }
}
