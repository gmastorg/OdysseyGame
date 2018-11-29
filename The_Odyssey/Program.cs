/**
* 9/27/2018
* CSC 253
* Group 1
* Gabriela Canjura & Mallory Milstead
* Dungeon Crawl Game displays a menu to user that
* allows for displaying lists and arrays including moving through a 2D array of locations
*/

//TODO make sure hitpoints no longer go into negative
//TODO get random moving working 
//TODO test that copy works 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameClassLibrary;

//github test
namespace The_Odyssey
{
    class Program
    {
        static void Main(string[] args)
        {


            System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());

            ListBuilder.Build();
            string restart = "";
            StandardMessages.IntroMessage();
            Tuple<bool, Player> loginPlayer;
            Player player;
            
            do
            {
                loginPlayer = Login.LoginMenu();
            }
            while (loginPlayer.Item1 == false);

            while (restart != "exit") 
            {
                player = Login.getPlayer(loginPlayer.Item2.Name, loginPlayer.Item2.Password);

                string option = "";

                DateTime time = DateTime.Now;

                DateTime answer = InitializeStorm(time, rand);
                DateTime scheduled = time.AddSeconds(5);

                while (player.IsAlive == true && option != "exit")
                {

                    //Code to display stats in another console window
                    //using (var displayProcess = new Process())
                    //{
                    //    displayProcess.StartInfo.FileName = (@"..\..\..\DisplayStats\bin\Debug\DisplayStats.exe");
                     
                    //    displayProcess.Start();
                    //    displayProcess.Refresh();
                    //}

                    StandardMessages.Menu();
                    option = Console.ReadLine().ToLower();
                    
                    //moves storm to random locations
                    //shows if object is moving rooms
                    //Console.WriteLine("storm");
                    //Console.WriteLine(time.ToString());
                    //Console.WriteLine(answer.ToString());
                    if (time > answer)
                    {
                        MoveRandomly(rand);
                        answer = InitializeStorm(time, rand);
                    }
                    //shows if object is moving rooms
                    //Console.WriteLine("poseidon");
                    //Console.WriteLine(time.ToString());
                    //Console.WriteLine(scheduled.ToString());
                    if (time > scheduled)
                    {
                        MoveScheduled(rand);
                        scheduled = time.AddSeconds(10);
                    }

                    time = DateTime.Now;

                    player = menu(option, player, time, answer, scheduled, rand);
                }

                if (player.IsAlive == false)
                {
                    Console.WriteLine("You have died. Would you like to start again from last save point or exit?\nTo restart type restart. To exit type exit.");
                    restart = Console.ReadLine();
                }
                else
                {
                    restart = option; 
                }

                Console.WriteLine(World.GetEnemyByName("poseidon").currentLocation.Name.ToString());
                Console.WriteLine(World.GetEnemyByName("storm").currentLocation.Name.ToString());
            }
            
        }
     

        private static DateTime InitializeStorm(DateTime time, System.Random rand)
        {
            int randomMinutes = rand.Next(0,20);
            DateTime answer = time.AddSeconds(randomMinutes);
            return answer;
        }



        public static Player menu(string option, Player newPlayer, DateTime time, DateTime answer, DateTime scheduled, System.Random rand)
        { /* menu that allows user to make choice in room it calls a method to let them move through rooms*/

                string[] split = option.Split(' ');
                string item = "";

                if (split[0] == "look")
                {
                    if (split.Count() == 4)
                    {
                        item = split[1] + " " + split[2] + " " + split[3];
                    }
                    if (split.Count() == 3)
                    {
                        item = split[1] + " " + split[2];
                    }
                    if (split.Count() == 2)
                    {
                        item = split[1];
                    }

                    if (World.allItems.Any(given_item => given_item.Name == item))
                    {
                        Console.WriteLine(World.Look(World.GetItemByName(item)));

                    }
                    else
                    {
                        Console.WriteLine("That item does not exist...");
                    }
                }

                else
                {
                    switch (option)
                    {
                    case "weapons":
                        //calls method that calls a method in a class that returns a list
                        World.printList(World.getList(World.weapons));
                        Console.WriteLine($"Player's Weapon is {newPlayer.CurrentWeapon.Name}");
                        return newPlayer; 
                    case "potions":
                        World.printList(World.getList(World.potions));
                        return newPlayer; 
                    case "treasures":
                        World.printList(World.getList(World.treasures));
                        return newPlayer;
                    case "rooms":
                        World.printList(World.getList(World.rooms));

                        string direction = "";

                        Console.WriteLine($"\nYou are currently in {newPlayer.currentLocation.Name}");
                        Console.WriteLine($"\nYour hitpoints are {newPlayer.HP}");

                        while ((newPlayer.IsAlive == true) && (direction != "menu"))
                        {
                            //Console.WriteLine("storm");
                            //Console.WriteLine(time.ToString());
                            //Console.WriteLine(answer.ToString());
                            if (time > answer)
                            {
                                MoveRandomly(rand);
                                answer = InitializeStorm(time, rand);
                            }
                            //shows if object is moving rooms
                            //Console.WriteLine("poseidon");
                            //Console.WriteLine(time.ToString());
                            //Console.WriteLine(scheduled.ToString());
                            if (time > scheduled)
                            {
                                MoveScheduled(rand);
                                scheduled = time.AddSeconds(10);
                            }

                            

                            time = DateTime.Now;
                            Console.WriteLine($"\nType the direction where you would like to move." +
                                        $"\nType menu to return to the menu.\nType inventory to see the items you have.\n");
                            direction = Console.ReadLine().ToLower();


                            switch (direction)
                            {
                                case "north":
                                    Move.moveNorth(newPlayer);
                                    break;
                                case "south":
                                    Move.moveSouth(newPlayer);
                                    break;
                                case "east":
                                    Move.moveEast(newPlayer);
                                    break;
                                case "west":
                                    Move.moveWest(newPlayer);
                                    break;
                                case "northeast":
                                    Move.moveNortheast(newPlayer);
                                    break;
                                case "northwest":
                                    Move.moveNorthwest(newPlayer);
                                    break;
                                case "southeast":
                                    Move.moveSoutheast(newPlayer);
                                    break;
                                case "southwest":
                                    Move.moveSouthwest(newPlayer);
                                    break;
                                case "save":
                                    Player.saveToDataBase(newPlayer);
                                    break;
                                case "inventory":
                                    if (newPlayer.Inventory.Count == 0)
                                    {
                                        Console.WriteLine("\n\nYour inventory is empty\n\n");
                                    }

                                    else
                                    {

                                        foreach (IItems InventoryItem in newPlayer.Inventory)
                                        {
                                            //if (InventoryItem != null)
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine(InventoryItem.Name);
                                                Console.WriteLine("");
                                            }

                                        }

                                        Console.WriteLine("");
                                    }

                                    //Let the player know the options for using items
                                    Console.WriteLine("To use an item, type \"use {item}\"");
                                    Console.WriteLine("To equip yourself with a weapon, type \"equip {weapon}\"");
                                    Console.WriteLine("To utilize defensive gear, type \"defend {item}\"");
                                    Console.WriteLine("To go back to the adventure, type \"back\"");
                                    string getInventoryItem = Console.ReadLine().ToLower();

                                    string[] splitgetInventoryItem = getInventoryItem.Split(' ');

                                    switch (splitgetInventoryItem[0])
                                    {
                                        case "use"://This allows the player to use a potion
                                            if (splitgetInventoryItem.Count() == 3)
                                            {
                                                string thisitem = splitgetInventoryItem[1] + " " + splitgetInventoryItem[2];

                                                if (newPlayer.Inventory.Contains(World.GetItemByName(thisitem)))
                                                {
                                                    newPlayer = World.useItem(World.GetPotionByName(thisitem), newPlayer);
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

                                    break;

                            }

                                //Determines if there is an item in the room and if so, adds it to the player's inventory
                                foreach (IItems items in World.allItems)
                            {
                               
                                if (newPlayer.currentLocation == items.CurrentLocation)
                                {
                                    if (!newPlayer.Inventory.Contains(items))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine($"\nYou have found {items.Name}!\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        newPlayer.Inventory.Add(items); }
                                }
                            }

                            //Determines if there in an enemy in the room and if it is alive, initiate combat
                            foreach (Enemies enemy in World.enemies)
                                {
                                if (enemy.Name == "poseidon")
                                {
                                    Enemies poseidonCopy = new Enemies(enemy.Name, enemy.Description, enemy.Gold_reward, enemy.MaxDamage, enemy.currentLocation, enemy.HP, enemy.AC, enemy.IsAlive);
                                    if (newPlayer.currentLocation == poseidonCopy.currentLocation)
                                    {
                                        newPlayer = Combat.InitiateCombat(newPlayer, poseidonCopy);
                                    }
                                    
                                }

                               if (enemy.currentLocation == newPlayer.currentLocation && enemy.IsAlive == true && enemy.Name != "poseidon")
                                        {
                                            newPlayer = Combat.InitiateCombat(newPlayer, enemy);
                                        }
                                       
                                }
                           

                        Console.WriteLine($"\nPlayer's current location is {newPlayer.currentLocation.Name}\n");
                        }

                        if (newPlayer.IsAlive == true)
                        {
                            Console.WriteLine("You are back in the main menu.");
                        }

                        return newPlayer;

                    case "inventory":
                        if (newPlayer.Inventory.Count == 0)
                        {
                            Console.WriteLine("\n\nYour inventory is empty\n\n");
                        }
                        foreach (IItems InventoryItem in newPlayer.Inventory)
                        {
                            Console.WriteLine(InventoryItem.Name);
                        }
                        return newPlayer;
                    case "enemies":
                        World.printList(World.getList(World.enemies));
                        return newPlayer;
                    case "save":
                        Player.saveToDataBase(newPlayer);
                        return newPlayer; 
                    case "exit":
                        return newPlayer;
                    default:
                        Console.WriteLine("\nInvalid Option.\n");
                        return newPlayer;
                }
            }
            return newPlayer;
        }

        public static void MoveRandomly(System.Random rand)
        {
            int randomRoom = rand.Next(0, 8);

            World.GetEnemyByName("storm").currentLocation = World.rooms[randomRoom];

            int randomSpot = rand.Next(0, 8);

            World.GetEnemyByName("storm").currentLocation = World.rooms[randomSpot];

            //Console.WriteLine(World.GetEnemyByName("storm").currentLocation.Name.ToString());

        }

        public static void MoveScheduled(System.Random rand)
        {

            int randomSpot= rand.Next(0, 8);

            World.GetEnemyByName("poseidon").currentLocation = World.rooms[randomSpot];

            int randomRoom = rand.Next(0, 8);

            World.GetEnemyByName("poseidon").currentLocation = World.rooms[randomRoom];

            //Console.WriteLine("poseidon");
            //Console.WriteLine(World.GetEnemyByName("poseidon").currentLocation.Name.ToString());

        }

    }
    
}
    