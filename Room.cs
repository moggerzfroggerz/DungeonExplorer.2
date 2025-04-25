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

        public Room(string description)
        {
            Description = description;
            Inventory = new List<string> { "a key" };
            ConnectedRooms = new Dictionary<string, Room>();
        }

        public void ConnectRoom(string direction, Room Room)
        {
            ConnectedRooms[direction] = Room;
        }

        // Room Description:
        public string RoomDescription()
        {
            return Description;
        }

        // Show the items that are in the room as a string:
        public string RoomItems()
        {
            return string.Join(",", Inventory);
        }

        public string GetAvailableDirections()
        {
            return string.Join(", ", ConnectedRooms.Keys);
        }
    }
}


//{
//    public class Room
//    {
//        private string description;
//        private List<string> inventory = new List<string>();
//        // The code below contains the items in the room. 
//        public Room(string description)
//        {
//            this.description = description;
//            this.inventory.Add("a key.");
//        }

//        public string RoomDescription()
//        {
//            return description;
//        }
//        public string RoomItems()
//        {
//            return ("a key");
//        }
//    }
//}