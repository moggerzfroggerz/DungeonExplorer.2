using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; set; }
        public List<Item> Inventory { get; set; }
        public Dictionary<string, Room> ConnectedRooms { get; set; }
        public Monster MonsterInRoom { get; set; }

        public bool IsDoorLocked { get; set; }
        public string DoorLockMessage { get; set; }


        private static List<string> allAvailableItems = new List<string>
        {
            "Bread",
            "Minor Health Potion",
            "Major Health Potion",
            "Shortsword",
            "Mace",
            "Boomerang",
            "Nunchucks",
            "Staff of Power",
            "Spear",
            "Mysterious Key"
        };


        public Room(string description)
        {
            Description = description;
            Inventory = RandomlyChooseItems();
            ConnectedRooms = new Dictionary<string, Room>();
            if (description.Contains("appears empty"))
            {
                Item mysteriousKey = new Item("Mysterious Key");
                Inventory.Add(mysteriousKey);
            }
            if (description.Contains("pitch black"))
            {
                IsDoorLocked = true;
                DoorLockMessage = "This door is locked...\nYou need a key to open it.";
            }
            else
            {
                IsDoorLocked = false;
                DoorLockMessage = "";
            }

        }

        // This method connects the rooms:
        public void ConnectRoom(string direction, Room Room)
        {
            ConnectedRooms[direction] = Room;
        }

        public string UnlockDoor(Player player)
        {
            var hasKey = player.GetInventory().Contains("Mysterious key");

            if (hasKey)
            {
                IsDoorLocked = false;
                DoorLockMessage = "You have unlocked the door!";
                return "You used the mysterious key to unlock the door.";
            }
            else
            {
                return "You don't have the key to unlock the door.";
            }
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
        private List<Item> RandomlyChooseItems()
        {
            int itemCount = rand.Next(1, 3);
            List<Item> theseWeapons = new List<Item>();

            for (int i = 0; i < itemCount; i++)
            {
                string thisWeapon = allAvailableItems[rand.Next(allAvailableItems.Count)];
                int weaponDmg = 0;
                int healingAmount = 0;


                if (thisWeapon == "Shortsword") weaponDmg = 8;
                else if (thisWeapon == "Mace") weaponDmg = 16;
                else if (thisWeapon == "Boomerang") weaponDmg = 12;
                else if (thisWeapon == "Nunchucks") weaponDmg = 14;
                else if (thisWeapon == "Staff of Power") weaponDmg = 20;
                else if (thisWeapon == "Spear") weaponDmg = 15;
                else if (thisWeapon == "Bread") healingAmount = 10;
                else if (thisWeapon == "Minor Health Potion") healingAmount = 20;
                else if (thisWeapon == "Major Health Potion") healingAmount = 30;
                else if (thisWeapon == "Mysterious Key") weaponDmg = 0;

                theseWeapons.Add(new Item(thisWeapon, weaponDmg, healingAmount));
            }
            return theseWeapons;
        }

        // The method below shows which items are in whichever room is called: 
        public string RoomItems()
        {
            if (Inventory.Count == 0)
            {
                return "None";
            }
            return string.Join(", ", Inventory.Select(item => item.Name));
        }

        // This method adds an item to the room: 
        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        // This method removes an item from the room, used when a player has picked up an item: 
        public void RemoveItem(Item item)
        {
            Inventory.Remove(item);
        }
    }
}
