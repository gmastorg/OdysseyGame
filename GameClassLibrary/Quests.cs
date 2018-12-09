using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace GameClassLibrary
{
    public class Quests
    {
        public static void Ogygia(Player newPlayer)
        {
            List<IItems> selected = new List<IItems> { World.GetItemByName("canvas"),
                                      World.GetItemByName("wooden planks"),
                                      World.GetItemByName("rope")
                                     };

            IItems item1;
            IItems item2;
            IItems item3;

            Console.WriteLine("Calyposo has fallen in love with you and has trapped you on her island." +
                "You must select the currect three items to escape and continue your journey home.");

            Console.WriteLine("Select wisely\n");
            Console.WriteLine("Select Item 1:");
            item1 = getInventoryItem(newPlayer);
            Console.WriteLine("Select Item 2:");
            item2 = getInventoryItem(newPlayer);
            Console.WriteLine("Select Item 3:");
            item3 = getInventoryItem(newPlayer);

            while (!selected.Contains(item1) && !selected.Contains(item2) && !selected.Contains(item3));
            {
                Console.WriteLine("You selected the wrong items. Try again.\n");
                Console.WriteLine("Select Item 1:");
                item1 = getInventoryItem(newPlayer);
                Console.WriteLine("Select Item 2:");
                item2 = getInventoryItem(newPlayer);
                Console.WriteLine("Select Item 3:");
                item3 = getInventoryItem(newPlayer);
            }

            Console.WriteLine("You selected the correct items to build your raft and escape from Calypso.\n");
            newPlayer.Inventory.Remove(item1);
            newPlayer.Inventory.Remove(item2);
            newPlayer.Inventory.Remove(item3);
            item1.beenUsed = true;
            item2.beenUsed = true;
            item3.beenUsed = true;
            newPlayer.currentLocation.QuestCompleted = true;
        }

        public static Player Sirens(Player newPlayer)
        {
            IItems selectedItem;

            Console.WriteLine("You approach the Isle of Sirens when a beautiful melody begins to play. It lures you towards the Sirens and potentially " +
                "your doom. You reach into you bag to select the item that will help you avoid this parilous death.");

            Console.WriteLine("Enter the item you would like to select:");
            selectedItem = getInventoryItem(newPlayer);

            while (selectedItem != World.GetItemByName("beeswax") && newPlayer.HP > 0)
            {
                Console.WriteLine("Enter the item you would like to select:");
                selectedItem = getInventoryItem(newPlayer);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe wrong item was selected.\nThe sirens attacked.\n");
        
                int damageFromSirens = Random.GetRandom(1, World.GetEnemyByName("Sirens").MaxDamage);
                newPlayer.HP -= damageFromSirens;
                if (newPlayer.HP > 0)
                {
                    Console.WriteLine($"The player's HP is: {newPlayer.HP}");
                    Console.WriteLine("\n");
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
            if(selectedItem != World.GetItemByName("beeswax") && newPlayer.HP<0)
            {
                newPlayer.IsAlive = false;

            }
            if (selectedItem == World.GetItemByName("beeswax") && newPlayer.HP>0)
            {
                Console.WriteLine("You put the beeswax in an resisted the alluring sounds of the sirens." +
                    "\n You have suceeded and escaped.");
                newPlayer.IsAlive = true;
                newPlayer.currentLocation.QuestCompleted = true;
            }



        }

        public static IItems getInventoryItem(Player newPlayer)
        {
            string getInventoryItem = Console.ReadLine().ToLower();

            while (newPlayer.Inventory.Contains(World.GetItemByName(getInventoryItem)) == false)
            {
                Console.WriteLine("\nThat item is not in your inventory.\n\n");
            }

            IItems getItem = World.GetItemByName(getInventoryItem);

            return getItem;
        }
    }
}
