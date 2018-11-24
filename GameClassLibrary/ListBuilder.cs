using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Dapper;
using System.Data.SQLite;
using System.Configuration;

namespace GameClassLibrary
{
    public static class ListBuilder
    {
        //Method to connect to DB
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            //the return statement is returning the connection details that we
            //established in the project's app.Config)
        }

        public static void Build()
        {
            //Create weapons objects
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Weapons";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);
                    int damage = reader.GetInt16(2);

                    World.weapons.Add(new Weapons(name, description, damage));
                    World.allItems.Add(new Weapons(name, description,damage));
                }
            }

            //Create potions objects
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Potions";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);
                    int healthIncrease = reader.GetInt16(2);

                    World.potions.Add(new Potions(name, description, healthIncrease));
                    World.allItems.Add(new Potions(name, description, healthIncrease));
                }
            }

            //Create treasure objects
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Treasures";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);
                    int value = reader.GetInt16(2);

                    World.treasures.Add(new Treasures(name, description, value));
                    World.allItems.Add(new Treasures(name, description, value));
                }
            }

            //Create rooms objects
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Rooms";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);

                    World.rooms.Add(new Rooms(name, description));
                }
            }

            ////Still need to convert to DB
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
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Items";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);
                    int price = reader.GetInt16(2);

                    World.items.Add(new Items(name, description, price));
                    World.allItems.Add(new Items(name, description, price));
                }
            }


            //Create enemies objects
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand command = cnn.CreateCommand();

                command.CommandText = "select * from Enemies";

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read()) { 

                    string name = reader.GetString(0).ToLower();
                    string description = reader.GetString(1);
                    int gold_reward = reader.GetInt16(2);
                    int maxdamage = reader.GetInt16(3);
                    int HP = reader.GetInt16(4);
                    int AC = reader.GetInt16(5);
                    bool isAlive = getBool(reader.GetString(6));
                    Rooms location = World.GetRoomByName(reader.GetString(7));
                 
                    World.enemies.Add(new Enemies(name, description, gold_reward, maxdamage, location, HP, AC, isAlive));
                    World.allItems.Add(new Enemies(name, description, gold_reward, maxdamage, location, HP, AC, isAlive));
                }
            }

            //Method to return boolean from sqlite3 db
             bool getBool(string dbinput)
            {
                bool dbbool = false;

                if (dbinput == "true")
                {
                    dbbool = true;
                }
                else
                {
                    dbbool = false;
                }

                return dbbool;
            }

            //////Still need to convert this to DB

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

