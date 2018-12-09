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
        public static bool Ogygia(Player newPlayer)
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

            do
            {
                Console.WriteLine("Select Item 1:");
                item1 = getInventoryItem(newPlayer);
                if(selected.Contains(item1))

            }
        }

        public static IItems getInventoryItem(Player newPlayer)
        {
            string getInventoryItem = Console.ReadLine().ToLower();

            while (newPlayer.Inventory.Contains(World.GetItemByName(getInventoryItem))==false)
            {
                Console.WriteLine("\nThat item is not in your inventory.\n\n");
            }

            IItems getItem = World.GetItemByName(getInventoryItem);

            return getItem;
        }
    }
}
