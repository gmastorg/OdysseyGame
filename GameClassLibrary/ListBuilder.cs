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
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Weapons.txt"))
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
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Potions.txt"))
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
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Treasures.txt"))
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
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Rooms.txt"))
            {

                while ((!reader.EndOfStream))
                {

                    string name = reader.ReadLine();
                    string description = reader.ReadLine();
         
                    World.rooms.Add(new Rooms (name, description));
                }
            }

            //Assign exits to rooms
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/RoomsExits.txt"))
            {

                while ((!reader.EndOfStream))
                {
                    Rooms room = World.GetRoomByName(reader.ReadLine());
                    room.roomToNorth = World.GetRoomByName(reader.ReadLine());
                    room.roomToEast = World.GetRoomByName(reader.ReadLine());
                    room.roomToSouth = World.GetRoomByName(reader.ReadLine());
                    room.roomToWest = World.GetRoomByName(reader.ReadLine());
                    room.roomToNortheast = World.GetRoomByName(reader.ReadLine());
                    room.roomToNorthwest = World.GetRoomByName(reader.ReadLine());
                    room.roomToSoutheast = World.GetRoomByName(reader.ReadLine());
                    room.roomToSouthwest = World.GetRoomByName(reader.ReadLine());

                }
            }


            //Create items objects
            using (StreamReader reader = File.OpenText(@"../../../GameClassLibrary/TextFiles/Items.txt"))
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

