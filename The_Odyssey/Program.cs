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

            DateTime time = DateTime.Now;

            DateTime answer = InitializeStorm(time);

            DateTime scheduled = time.AddSeconds(5);

            while (option != "exit")
            {
               
                //NEED TO RETURN PLAYER OBJECT HERE FROM Login.createPlayer()
                menu(option, loginPlayer.Item2);
                option = Console.ReadLine().ToLower();

                //moves storm to random locations
                //shows if object is moving rooms
                //Console.WriteLine("storm");
                //Console.WriteLine(time.ToString());
                //Console.WriteLine(answer.ToString());
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
                    scheduled = time.AddMinutes(10);
                }

                time = DateTime.Now;

                
                
            }
        }

        private static DateTime InitializeStorm(DateTime time)
        {
            System.Random random = new System.Random();
            int randomMinutes = random.Next(0,20);
            DateTime answer = time.AddMinutes(randomMinutes);
            return answer;
        }

        public static void menu(string option, Player newPlayer)
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
                            Console.WriteLine($"\nYour hitpoints are {newPlayer.HP}");
                            Console.WriteLine("\nType the direction where you would like to move." +
                                "\nType menu to return to the menu.\n");
                            direction = Console.ReadLine().ToLower();

                            //Give the player a dagger
                            newPlayer.CurrentWeapon = World.GetWeaponByName("dagger");

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



                                Console.WriteLine($"\nType the direction where you would like to move." +
                                    $"\nType menu to return to the menu.\n");
                                direction = Console.ReadLine().ToLower();
                            }
                            Console.WriteLine($"\nPlayer's current location is {newPlayer.currentLocation.Name}\n");






                            //should recreate player from last saved point need to test
                            if (newPlayer.IsAlive == false)
                            {
                                newPlayer = Login.getPlayer(newPlayer.Name, newPlayer.Password);
                            }

                            Console.WriteLine($"\n\nTo view your weapons type weapons. \nTo view potions type potions. " +
                                    $"\nTo view treasures type treasures. \nTo view rooms type rooms. \nTo save type save. \nTo exit type exit.\n");

                            break;
                        case "items":
                            World.printList(World.getList(World.items));
                            break;
                        case "enemies":
                            World.printList(World.getList(World.enemies));
                            break;
                        case "save":
                            Player.sendToPlayerFile(newPlayer);
                            break;
                        case "exit":
                            break;
                        default:
                            Console.WriteLine("\nInvalid Option.\n");
                            break;
                    }
                }
            
        }

        public static void MoveRandomly()
        {
            System.Random random = new System.Random();

            int randomRoom = random.Next(0, 8);

            World.GetEnemyByName("storm").currentLocation = World.rooms[randomRoom];
            Console.WriteLine(World.GetEnemyByName("storm").currentLocation.Name.ToString());

        }

        public static void MoveScheduled()
        {
            System.Random random = new System.Random();

            int randomSpot= random.Next(0, 8);

            World.GetEnemyByName("poseidon").currentLocation = World.rooms[randomSpot];
            Console.WriteLine("poseidon");
            Console.WriteLine(World.GetEnemyByName("poseidon").currentLocation.Name.ToString());

        }
    }
}
    