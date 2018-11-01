﻿/**
* 9/27/2018
* CSC 253
* Group 1
* Gabriela Canjura & Mallory Milstead
* Dungeon Crawl Game displays a menu to user that
* allows for displaying lists and arrays including moving through a 2D array of locations
*/

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

            StandardMessages.IntroMessage();
            Tuple<bool, Player> loginPlayer;

            do
            {
                loginPlayer = Login.LoginMenu();
            }
            while (loginPlayer.Item1 == false);

            StandardMessages.Menu();
            string option = Console.ReadLine();

            while (option != "exit")
            {
                //Create Singleton instance of player object here
                //NEED TO RETURN PLAYER OBJECT HERE FROM Login.createPlayer()
                menu(option, loginPlayer.Item2);
                option = Console.ReadLine().ToLower();
               
            }   
        }

        public static void menu(string option, Player newPlayer)
        {
            /* menu that allows user to make choice in room it calls a method to let them move through rooms*/



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
                        break;
                    case "potions":
                        World.printList(World.getList(World.potions));
                        break;
                    case "treasures":
                        World.printList(World.getList(World.treasures));
                        break;
                    case "rooms":
                        World.printList(World.getList(World.rooms));

                       
                        string direction = "";

                        Console.WriteLine($"\nYou are currently in {newPlayer.currentLocation.Name}");
                        Console.WriteLine($"\nYour hitpoints are {newPlayer.HitPoints}");
                        Console.WriteLine("\nType the direction where you would like to move." +
                            "\nType menu to return to the menu.\n");
                        direction = Console.ReadLine().ToLower();

                        while (direction != "menu") 

                        {
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
                            }

                            
                            Console.WriteLine($"\nType the direction where you would like to move." +
                                $"\nType menu to return to the menu.\n");
                            direction = Console.ReadLine().ToLower();
                        }
                        Console.WriteLine($"\nPlayer's current location is {newPlayer.currentLocation.Name}\n");//This was a test to make sure the currentLocation is being saved as a property of the 
                                                                                                                //Player class outside of the while loop, it is.
                       
                        Console.WriteLine($"\n\nTo view your weapons type weapons. \nTo view potions type potions. " +
                            $"\nTo view treasures type treasures. \nTo view rooms type rooms. \nTo exit type exit.\n");

                        break;
                    case "items":
                        World.printList(World.getList(World.items));
                        break;
                    case "enemies":
                        World.printList(World.getList(World.enemies));
                        break;
                    case "save":
                        Player.sendToPlayerFile(newPlayer); //If the option is save, send the new player properties to the file (could ve combined with exit or a save and exit)
                        break; //It will not rewrite this information to the text file because it says the race/character properties are empty. I am not sure why
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("\nInvalid Option.\n");
                        break;
                }
            }
        }
    }
}