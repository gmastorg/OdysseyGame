using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameClassLibrary
{
    public static class ListBuilder
    {
        public static void Build()
        {
            //Create weapons objects
            using (StreamReader reader = File.OpenText("Weapons.txt"))
            {
                while ((!reader.EndOfStream))
                {
                    string name = reader.ReadLine().ToLower();
                    string description = reader.ReadLine();
                    int damage = int.Parse(reader.ReadLine());

                    World.weapons.Add(new Weapons(name, description, damage));
                    World.allItems.Add(new Weapons(name, description, damage));
                }
            }

            //Create potions objects
            using (StreamReader reader = File.OpenText("Potions.txt"))
            {

                while ((!reader.EndOfStream))
                {
                    string name = reader.ReadLine().ToLower();
                    string description = reader.ReadLine();
                    int healthIncrease = int.Parse(reader.ReadLine());

                    World.potions.Add(new Potions(name, description, healthIncrease));
                    World.allItems.Add(new Potions(name, description, healthIncrease));
                }
            }

            //Create armor objects
            using (StreamReader reader = File.OpenText("Treasures.txt"))
            {

                while ((!reader.EndOfStream))
                {
                    string name = reader.ReadLine().ToLower();
                    string description = reader.ReadLine();

                    World.treasures.Add(new Treasures(name, description));
                    World.allItems.Add(new Treasures(name, description));
                }
            }

            //Create rooms objects
            using (StreamReader reader = File.OpenText("Rooms.txt"))
            {

                while ((!reader.EndOfStream))
                {

                    string name = reader.ReadLine();
                    string description = reader.ReadLine();
                    /*Rooms roomToNorth = reader.ReadLine();
                    Rooms roomToEast = reader.ReadLine();
                    Rooms roomToSouth = reader.ReadLine();
                    Rooms roomToWest = reader.ReadLine();
                    Rooms roomToNortheast = reader.ReadLine();
                    Rooms roomToNorthwest = reader.ReadLine();
                    Rooms roomToSoutheast = reader.ReadLine();
                    Rooms roomToSouthwest = reader.ReadLine();*/


                    World.rooms.Add(new Rooms (name, description));
                }

                
                World.GetRoomByName("Troy").roomToEast = World.GetRoomByName("Cicones");
                World.GetRoomByName("Troy").roomToSouth = World.GetRoomByName("Island of the Cyclops");
                World.GetRoomByName("Troy").roomToSoutheast = World.GetRoomByName("The Underworld");

                World.GetRoomByName("Cicones").roomToEast = World.GetRoomByName("Land of the Lotus Eaters");
                World.GetRoomByName("Cicones").roomToSouth = World.GetRoomByName("The Underworld");
                World.GetRoomByName("Cicones").roomToWest = World.GetRoomByName("Troy");
                World.GetRoomByName("Cicones").roomToSoutheast = World.GetRoomByName("Isle of the Sirens");
                World.GetRoomByName("Cicones").roomToSouthwest = World.GetRoomByName("Island of the Cyclops");

                World.GetRoomByName("Land of the Lotus Eaters").roomToSouth = World.GetRoomByName("Isle of the Sirens");
                World.GetRoomByName("Land of the Lotus Eaters").roomToWest = World.GetRoomByName("Cicones");
                World.GetRoomByName("Land of the Lotus Eaters").roomToSouthwest = World.GetRoomByName("The Underworld");

                World.GetRoomByName("Island of the Cyclops").roomToNorth = World.GetRoomByName("Troy");
                World.GetRoomByName("Island of the Cyclops").roomToEast = World.GetRoomByName("The Underworld");
                World.GetRoomByName("Island of the Cyclops").roomToSouth = World.GetRoomByName("Scylla and Charybdis");
                World.GetRoomByName("Island of the Cyclops").roomToNortheast = World.GetRoomByName("Cicones");
                World.GetRoomByName("Island of the Cyclops").roomToSoutheast = World.GetRoomByName("Ogygia");

                World.GetRoomByName("The Underworld").roomToNorth = World.GetRoomByName("Cicones");
                World.GetRoomByName("The Underworld").roomToEast = World.GetRoomByName("Isle of Sirens");
                World.GetRoomByName("The Underworld").roomToSouth = World.GetRoomByName("Ogygia");
                World.GetRoomByName("The Underworld").roomToWest = World.GetRoomByName("Island of the Cyclops");
                World.GetRoomByName("The Underworld").roomToNortheast = World.GetRoomByName("Land of the Lotus Eaters");
                World.GetRoomByName("The Underworld").roomToNorthwest = World.GetRoomByName("Troy");
                World.GetRoomByName("The Underworld").roomToSoutheast = World.GetRoomByName("Ithaca");
                World.GetRoomByName("The Underworld").roomToSouthwest = World.GetRoomByName("Scylla and Charybdis");

                World.GetRoomByName("Isle of the Sirens").roomToNorth = World.GetRoomByName("Land of the Lotus Eaters");
                World.GetRoomByName("Isle of the Sirens").roomToSouth = World.GetRoomByName("Ithaca");
                World.GetRoomByName("Isle of the Sirens").roomToWest = World.GetRoomByName("The Underworld");
                World.GetRoomByName("Isle of the Sirens").roomToNorthwest = World.GetRoomByName("Cicones");
                World.GetRoomByName("Isle of the Sirens").roomToSouthwest = World.GetRoomByName("Ogygia");

                World.GetRoomByName("Scylla and Charybdis").roomToNorth = World.GetRoomByName("Island of the Cyclops");
                World.GetRoomByName("Scylla and Charybdis").roomToEast = World.GetRoomByName("Ogygia");
                World.GetRoomByName("Scylla and Charybdis").roomToNortheast = World.GetRoomByName("The Underworld");

                World.GetRoomByName("Ogygia").roomToNorth = World.GetRoomByName("The Underworld");
                World.GetRoomByName("Ogygia").roomToEast = World.GetRoomByName("Ithaca");
                World.GetRoomByName("Ogygia").roomToWest = World.GetRoomByName("Scylla and Charybdis");
                World.GetRoomByName("Ogygia").roomToNortheast = World.GetRoomByName("Isle of the Sirens");
                World.GetRoomByName("Ogygia").roomToNorthwest = World.GetRoomByName("Island of the Cyclops");

                World.GetRoomByName("Ithaca").roomToNorth = World.GetRoomByName("Isle of the Sirens");
                World.GetRoomByName("Ithaca").roomToWest = World.GetRoomByName("Ogygia");
                World.GetRoomByName("Ithaca").roomToNorthwest = World.GetRoomByName("The Underworld");


            }


            //Create items objects
            using (StreamReader reader = File.OpenText("Items.txt"))
            {

                while ((!reader.EndOfStream))
                {
                    string name = reader.ReadLine().ToLower();
                    string description = reader.ReadLine();
                    int price = int.Parse(reader.ReadLine());

                    World.items.Add(new Items(name, description, price));
                    World.allItems.Add(new Items(name, description, price));
                }
            }

            //Create enemies objects
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Enemies.txt"))
            {

                while ((!reader.EndOfStream))
                {
                    string name = reader.ReadLine().ToLower();
                    string description = reader.ReadLine();
                    int gold_reward = int.Parse(reader.ReadLine());
                    int maxdamage = int.Parse(reader.ReadLine());
                    int HP = int.Parse(reader.ReadLine());
                    int AC = int.Parse(reader.ReadLine());
                    bool isAlive = bool.Parse(reader.ReadLine());
                    Rooms location = World.GetRoomByName(reader.ReadLine());

                    World.enemies.Add(new Enemies(name, description, gold_reward, maxdamage, location, HP, AC, isAlive));
                    World.allItems.Add(new Enemies(name, description, gold_reward, maxdamage, location, HP, AC, isAlive));
                }
            }

            using (StreamReader reader = new StreamReader(@"../../../GameClassLibrary/TextFiles/login.txt"))
            {
                if (new FileInfo(@"../../../GameClassLibrary/TextFiles/login.txt").Length != 0)
                {
                    while (!reader.EndOfStream)
                    {
                        string username = reader.ReadLine();
                        string password = reader.ReadLine();
                        string filename = reader.ReadLine();
                        //Changed this from an actual player object (since Singleton means only one instance is created to just a simple            
                        //UserLogin object that holds name and password. All other data (race, location, etc) is stored in text file.
                        World.logins.Add(new UserLogin(username, password));
                    }
                    
                }
            }
        }
    }
}

