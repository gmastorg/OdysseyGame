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
            List<IItems> selected = new List<IItems>();

            IItems item1;
            IItems item2;
            IItems item3;

            Console.WriteLine("Calyposo has fallen in love with you and has trapped you on her island." +
                "You must select the currect three items to escape and continue your journey home.");

            Console.WriteLine("Select wisely\n");
            Console.WriteLine("Select Item 1:");
            item1 = getInventoryItem(newPlayer);
            selected.Add(item1);
            Console.WriteLine("Select Item 2:");
            item2 = getInventoryItem(newPlayer);
            selected.Add(item2);
            Console.WriteLine("Select Item 3:");
            item3 = getInventoryItem(newPlayer);
            selected.Add(item3);

            while (!selected.Contains(World.GetItemByName("canvas"))|| !selected.Contains(World.GetItemByName("rope"))
                || !selected.Contains(World.GetItemByName("wood")))
            {
                Console.WriteLine("You selected the wrong items. Try again.\n");
                Console.WriteLine("To refresh your memory you may view your inventory by typing inventory.\nTo try again hit the enter key.");
                string inventory = Console.ReadLine();
                if (inventory == "inventory")
                {
                    seeInventory(newPlayer);
                }
                selected.Clear();
                Console.WriteLine("Select Item 1:");
                item1 = getInventoryItem(newPlayer);
                selected.Add(item1);
                Console.WriteLine("Select Item 2:");
                item2 = getInventoryItem(newPlayer);
                selected.Add(item2);
                Console.WriteLine("Select Item 3:");
                item3 = getInventoryItem(newPlayer);
                selected.Add(item3);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe wrong item was selected.\nThe sirens attacked.\n");

                int damageFromSirens = Random.GetRandom(1, World.GetEnemyByName("sirens").MaxDamage);
                newPlayer.HP -= damageFromSirens;
                if (newPlayer.HP > 0)
                {
                    Console.WriteLine($"The player's HP is: {newPlayer.HP}");
                    Console.WriteLine("\n");
                }

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Enter the item you would like to select:");
                selectedItem = getInventoryItem(newPlayer);
            }
            if (selectedItem != World.GetItemByName("beeswax") && newPlayer.HP < 0)
            {
                newPlayer.IsAlive = false;
                Console.WriteLine("You've succumed to the Sirens.");
            }
            else if (selectedItem == World.GetItemByName("beeswax") && newPlayer.HP > 0)
            {
                Console.WriteLine("You put the beeswax in an resisted the alluring sounds of the sirens." +
                    "\n You have suceeded and escaped.");
                newPlayer.IsAlive = true;
                newPlayer.currentLocation.QuestCompleted = true;
            }

            return newPlayer;
        }

        public static void Ithaca(Player newPlayer)
        {
            Console.WriteLine("You have completed your long journey and returned to Ithaca. However, your parils are not over! You must use your" +
                "trusty bow and arrow and have your aim be true.");

            Console.WriteLine("Be sure to have your bow+arrow in hand and use any additional items" +
                "from your inventory before preparing for this battle.");

            Console.WriteLine("To arm yourself type inventory");
            string inventory = Console.ReadLine();

            string leave = "";

            if (inventory == "inventory")
            {
                seeInventory(newPlayer);
                do
                {
                    leave = keepUsingItems(newPlayer);
                }
                while (leave != "back");
            }

            while (newPlayer.CurrentWeapon != World.GetWeaponByName("bow+arrow"))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("You do not have the bow+arrow. Arm yourself with this weapon.");
                useItems(newPlayer);
                Console.ForegroundColor = ConsoleColor.White;
            }

            while(Random.GetRandom(0,10)!=3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your shot missed. Take aim and try again.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            newPlayer.currentLocation.QuestCompleted = true;
        }

        public static IItems getInventoryItem(Player newPlayer)
        {
            string getInventoryItem = Console.ReadLine().ToLower();

            while (newPlayer.Inventory.Contains(World.GetItemByName(getInventoryItem)) == false)
            {
                Console.WriteLine("\nThat item is not in your inventory.\n\n");
                Console.WriteLine("Select an item in your inventory.");
                getInventoryItem = Console.ReadLine().ToLower();
            }

            IItems getItem = World.GetItemByName(getInventoryItem);

            return getItem;
        }

        public static void seeInventory(Player newPlayer)
        {
            if (newPlayer.Inventory.Count == 0)
            {
                Console.WriteLine("\n\nYour inventory is empty\n\n");
            }

            else
            {

                foreach (IItems InventoryItem in newPlayer.Inventory)
                {
                    if (InventoryItem.beenUsed == false)
                    {
                        Console.WriteLine("");
                        Console.Write(InventoryItem.Name);
                    }
                }

                Console.WriteLine("\n");
            }
        }

        public static void useItems(Player newPlayer)
        {
            //Let the player know the options for using items
            Console.WriteLine("To heal yourself with a potion, type \"heal {item}\"");
            Console.WriteLine("To equip yourself with a weapon, type \"equip {weapon}\"");
            Console.WriteLine("To utilize defensive gear, type \"defend {item}\"");
            Console.WriteLine("To go back to the adventure, type \"back\"");
            string getInventoryItem = Console.ReadLine().ToLower();

            string[] splitgetInventoryItem = getInventoryItem.Split(' ');

            switch (splitgetInventoryItem[0])
            {
                case "heal"://This allows the player to use a potion
                    if (splitgetInventoryItem.Count() == 3)
                    {
                        string thisitem = splitgetInventoryItem[1] + " " + splitgetInventoryItem[2];

                        if (newPlayer.Inventory.Contains(World.GetItemByName(thisitem)))
                        {
                            newPlayer = World.useItem(World.GetPotionByName(thisitem), newPlayer);
                            Console.WriteLine($"Your new HP is: {newPlayer.HP}\n"); //Show Player's HP
                            newPlayer.Inventory.Remove(World.GetItemByName(thisitem));
                            World.GetPotionByName(thisitem).beenUsed = true; //Bool to say the potion has been used
                        }
                        else
                        {
                            Console.WriteLine("\nThat item is not in your inventory.\n\n");
                        }

                    }
                    break;



                case "equip"://This changes the current weapon - need more for bow and arrow (anything weapon than one word)


                    if (newPlayer.Inventory.Contains(World.GetItemByName(splitgetInventoryItem[1])))
                    {
                        newPlayer = World.useItem(World.GetWeaponByName(splitgetInventoryItem[1]), newPlayer);
                    }

                    else
                    {
                        Console.WriteLine("\nThat item is not in your inventory.\n");
                    }
                    break;



                case "defend"://This changes the current armor -- need more for bow and arrow (anything treasure more than one word)

                    if (newPlayer.Inventory.Contains(World.GetItemByName(splitgetInventoryItem[1])))
                    {
                        newPlayer = World.useItem(World.GetTreasureByName(splitgetInventoryItem[1]), newPlayer);
                    }

                    else
                    {
                        Console.WriteLine("\nThat item is not in your inventory.\n");
                    }
                    break;


                case "back"://Breaks out of the switch/case
                    {
                        break;
                    }

            }
        }
        public static string keepUsingItems(Player newPlayer)
        {
            //Let the player know the options for using items
            Console.WriteLine("To heal yourself with a potion, type \"heal {item}\"");
            Console.WriteLine("To equip yourself with a weapon, type \"equip {weapon}\"");
            Console.WriteLine("To utilize defensive gear, type \"defend {item}\"");
            Console.WriteLine("To go back to the adventure, type \"back\"");
            string getInventoryItem = Console.ReadLine().ToLower();

            string[] splitgetInventoryItem = getInventoryItem.Split(' ');

            switch (splitgetInventoryItem[0])
            {
                case "heal"://This allows the player to use a potion
                    if (splitgetInventoryItem.Count() == 3)
                    {
                        string thisitem = splitgetInventoryItem[1] + " " + splitgetInventoryItem[2];

                        if (newPlayer.Inventory.Contains(World.GetItemByName(thisitem)))
                        {
                            newPlayer = World.useItem(World.GetPotionByName(thisitem), newPlayer);
                            Console.WriteLine($"Your new HP is: {newPlayer.HP}\n"); //Show Player's HP
                            newPlayer.Inventory.Remove(World.GetItemByName(thisitem));
                            World.GetPotionByName(thisitem).beenUsed = true; //Bool to say the potion has been used
                        }
                        else
                        {
                            Console.WriteLine("\nThat item is not in your inventory.\n\n");
                        }

                    }
                    return splitgetInventoryItem[0];



                case "equip"://This changes the current weapon - need more for bow and arrow (anything weapon than one word)


                    if (newPlayer.Inventory.Contains(World.GetItemByName(splitgetInventoryItem[1])))
                    {
                        newPlayer = World.useItem(World.GetWeaponByName(splitgetInventoryItem[1]), newPlayer);
                    }

                    else
                    {
                        Console.WriteLine("\nThat item is not in your inventory.\n");
                    }
                    return splitgetInventoryItem[0]; 



                case "defend"://This changes the current armor -- need more for bow and arrow (anything treasure more than one word)

                    if (newPlayer.Inventory.Contains(World.GetItemByName(splitgetInventoryItem[1])))
                    {
                        newPlayer = World.useItem(World.GetTreasureByName(splitgetInventoryItem[1]), newPlayer);
                    }

                    else
                    {
                        Console.WriteLine("\nThat item is not in your inventory.\n");
                    }
                    return splitgetInventoryItem[0]; 


                case "back"://Breaks out of the switch/case
                    {
                        return splitgetInventoryItem[0]; 
                    }
            }
            return splitgetInventoryItem[0];
        }
    }
}
