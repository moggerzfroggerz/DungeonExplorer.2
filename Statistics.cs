using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Statistics
    {
        // List for the number of rooms the player has explored during the game's duration:
        private static int RoomsEntered;

        // List for the damage the player has done to monsters during the game's duration:
        private static List<int> DamageDone = new List<int>();

        // List for the damage the player has received during the game's duration:
        private static List<int> DamageTaken = new List<int>();
        public Statistics()

        {
            // Rooms explored by the player begins at 0 and will increase for every room they enter:
            RoomsEntered = 0;
        }

        public static void RoomsExplored()
        {
            // This will add one to the RoomsExplored list for every room the player enters:
            RoomsEntered += 1;
            return;
        }
        // The damage dealt by the player is stored in a list:
        public static void DoneDamage(int damage)
        {
            // This will store the damage the player has done to monsters:
            DamageDone.Add(damage);
            return;
        }
        public static void TakenDamage(int damage)
        {
            // This will store the damage the player has taken from monsters:
            DamageTaken.Add(damage);
            return;
        }
        private static int ListSum(List<int> list)
        {
            return list.Sum();
        }
        private static int ListNum(List<int> list)
        {
            return list.Count();
        }
        private static float AverageDmg(List<int> list)
        {
            int listsummary = ListSum(list);
            int listnum = ListNum(list);
            return listsummary / listnum;
        }
        // This is the logic for all of the statistics stored and later displayed at the end of the game:
        public static string EndGameStats()
        {
            string statistics = "";
                Console.Clear();
                statistics = ($"Statistics:\n\nYou dealt {ListSum(DamageDone)} damage.\nYou attacked {ListNum(DamageDone)} times, and your average damage per attack was: {AverageDmg(DamageDone)}. \n\n") +
                ($"You took {ListSum(DamageTaken)} damage from monsters.\nYou were damaged {ListNum(DamageTaken)} times. The average damage you took from attacks was: {AverageDmg(DamageTaken)}. \n\n") +
                ($"You entered {RoomsEntered} rooms.\n\n");
            statistics += ("Game over.\nThanks for playing!\n\n\n");

            return statistics;
        }
    }
}