﻿/**
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

                DateTime answer = InitializeStorm(time);
                DateTime scheduled = time.AddSeconds(5);

                while (player.IsAlive == true && option != "exit")
                {   
                    StandardMessages.Menu();
                    option = Console.ReadLine().ToLower();
                    
                    //moves storm to random locations
                    //shows if object is moving rooms
                    Console.WriteLine("storm");
                    Console.WriteLine(time.ToString());
                    Console.WriteLine(answer.ToString());
                    if (time > answer)
                    {
                        MoveRandomly();
                        answer = InitializeStorm(time);
                    }
                    //shows if object is moving rooms
                    Console.WriteLine("poseidon");
                    Console.WriteLine(time.ToString());
                    Console.WriteLine(scheduled.ToString());
                    if (time > scheduled)
                    {
                        MoveScheduled();
                        scheduled = time.AddSeconds(10);
                    }

                    time = DateTime.Now;

                    player = menu(option, player, time, answer, scheduled);
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
     

        private static DateTime InitializeStorm(DateTime time)
        {
            System.Random random = new System.Random();
            int randomMinutes = random.Next(0,20);
            DateTime answer = time.AddSeconds(randomMinutes);
            return answer;
        }

        public static Player menu(string option, Player newPlayer, DateTime time, DateTime answer, DateTime scheduled)
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

                        //Give the player a dagger
                        newPlayer.CurrentWeapon = World.GetWeaponByName("dagger");

                        while ((newPlayer.IsAlive==true)&&(direction != "menu"))
                        {
                            //Console.WriteLine("storm");
                            //Console.WriteLine(time.ToString());
                            //Console.WriteLine(answer.ToString());
                            if (time > answer)
                            {
                                MoveRandomly();
                                answer = InitializeStorm(time);
                            }
                            //shows if object is moving rooms
                            //Console.WriteLine("poseidon");
                            //Console.WriteLine(time.ToString());
                            //Console.WriteLine(scheduled.ToString());
                            if (time > scheduled)
                            {
                                MoveScheduled();
                                scheduled = time.AddSeconds(10);
                            }

                            time = DateTime.Now;
                            Console.WriteLine($"\nType the direction where you would like to move." +
                                        $"\nType menu to return to the menu.\n");
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
                                    Player.sendToPlayerFile(newPlayer);
                                    break;
                            }
                            //Give the player a dagger
                            newPlayer.CurrentWeapon = World.GetWeaponByName("dagger");

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

                    case "items":
                        World.printList(World.getList(World.items));
                        return newPlayer;
                    case "enemies":
                        World.printList(World.getList(World.enemies));
                        return newPlayer;
                    case "save":
                        Player.sendToPlayerFile(newPlayer);
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

        public static void MoveRandomly()
        {
            System.Random random = new System.Random();

            int randomRoom = random.Next(0, 8);

            World.GetEnemyByName("storm").currentLocation = World.rooms[randomRoom];

            int randomSpot = random.Next(0, 8);

            World.GetEnemyByName("storm").currentLocation = World.rooms[randomSpot];

            Console.WriteLine(World.GetEnemyByName("storm").currentLocation.Name.ToString());

        }

        public static void MoveScheduled()
        {
            System.Random random = new System.Random();

            int randomSpot= random.Next(0, 8);

            World.GetEnemyByName("poseidon").currentLocation = World.rooms[randomSpot];

            int randomRoom = random.Next(0, 8);

            World.GetEnemyByName("poseidon").currentLocation = World.rooms[randomRoom];

            Console.WriteLine("poseidon");
            Console.WriteLine(World.GetEnemyByName("poseidon").currentLocation.Name.ToString());

        }
    }
}
    