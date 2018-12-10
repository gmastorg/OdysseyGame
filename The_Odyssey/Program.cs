/**
* 9/27/2018
* CSC 253
* Group 1
* Gabriela Canjura & Mallory Milstead
* Dungeon Crawl Game displays a menu to user that
* allows for displaying lists and arrays including moving through a 2D array of locations
*/

//TODO "LOOK" -- for each exit, if the room in not none, print the direction, print room description
//TODO change monster's HP and rooms that items are in
//TODO when you try to change weapon, it says "to" is not your current weapon -- fix immedidately -- test further

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
                    
                    if (time > answer)
                    {
                        MoveRandomly(rand);
                        answer = InitializeStorm(time, rand);
                    }

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
            }
            
        }
     

        private static DateTime InitializeStorm(DateTime time, System.Random rand)
        {
            int randomMinutes = rand.Next(0,5);
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
                    case "inventory":
                        string leave = "";
                        do
                        {
                            Quests.seeInventory(newPlayer);
                            leave = Quests.keepUsingItems(newPlayer);
                        }
                        while (leave != "back");
                        break;
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
                            Console.WriteLine($"\nType menu to return to the menu.\nType inventory to see the items you have.\n" +
                                        $"To look around the current room, type look." );
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
                                case "look":
                                    Look.lookAround(newPlayer.currentLocation);
                                    break;
                                case "inventory":
                                    leave = "";
                                    do
                                    {
                                        Quests.seeInventory(newPlayer);
                                        leave = Quests.keepUsingItems(newPlayer);
                                    }
                                    while (leave != "back");
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

                            if (newPlayer.currentLocation.QuestCompleted==false)
                            {
                                if(newPlayer.currentLocation == World.GetRoomByName("Ogygia"))
                                {
                                    Quests.Ogygia(newPlayer);
                                }
                                if(newPlayer.currentLocation == World.GetRoomByName("Isle of the Sirens"))
                                {
                                    Quests.Sirens(newPlayer);
                                    Console.WriteLine("The Sirens are not pleased that you have escaped their clutches and attack. You " +
                                        "may now use your weapons and items to fight back. May the gods be in your favor!");
                                }
                                if (newPlayer.currentLocation == World.GetRoomByName("Ithaca"))
                                {
                                    Quests.Ithaca(newPlayer);

                                    Console.WriteLine("Your aim was true and you have won the archery contest. At this point," +
                                        "you remove your disguse and reveal yourself to the crowd as Odysseus. The mob of suitors" +
                                        "becomes furious and attacks you. Hope you are ready for battle!");
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

                            if (newPlayer.IsAlive == true)
                            {
                                Console.WriteLine($"\nPlayer's current location is {newPlayer.currentLocation.Name}\n");
                            }

                            if (newPlayer.currentLocation == World.GetRoomByName("Ithaca") && newPlayer.currentLocation.QuestCompleted == true && World.GetEnemyByName("mob of suitors").IsAlive == false) 
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("You have defeated all your enemies and returned home safely you your family." +
                                    "You are a true hero!! We commend your efforts.");
                                Console.WriteLine("Your tale has now ended.");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }

                        if (newPlayer.IsAlive == true)
                        {
                            Console.WriteLine("You are back in the main menu.");
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

            ////--Test to see where the storm is...
            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Console.WriteLine("The storm is " + World.GetEnemyByName("storm").currentLocation.Name.ToString());
            //Console.ForegroundColor = ConsoleColor.Gray;

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
    