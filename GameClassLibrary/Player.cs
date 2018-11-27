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
    public class Player : LivingCreatures
    {
        //Method to connect to DB
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            //the return statement is returning the connection details that we
            //established in the project's app.Config)
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Filename { get; set; }
        public string ClassOfCharacter { get; set; }
        public string Race { get; set; }
        public Weapons CurrentWeapon { get; set; }
        public List<IItems> Inventory = new List<IItems>();

        //The constructor takes no parameters, we will set them in the code after the object is first referenced (in the Login class)
        public Player(string username, string password, string classOfcharacter, string race, Rooms currentlocation, int hp, int ac, bool isalive, Weapons currentweapon, int gold_reward, List<IItems> inventory) :
            base(currentlocation, hp, ac, isalive, gold_reward)
        {
            Name = username;
            Password = password;
            Filename = username+".txt";
            ClassOfCharacter = classOfcharacter;
            Race = race;
            CurrentWeapon = currentweapon;
            Inventory = inventory;
        }

        public static void CreatePlayerTable(Player user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                string sql = "CREATE TABLE IF NOT EXISTS canjuras (Class varchar(20), HP int, Race varchar(20), AC int, Location varchar(20),"
                    + "Gold int, Weapon varchar(20), Enemy varchar(20), Alive varchar(20), Inventory varchar(20) )";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);
               //command.Parameters.Add(new SQLiteParameter("@param1", user.Name));
                command.ExecuteNonQuery();

                cnn.Close();
            }

            sendToPlayerFile(user);
        }

        public static void sendToLoginFile(Player user)
        {
            //StreamWriter outputFile;

            //outputFile = File.AppendText(@"../../../GameClassLibrary/TextFiles/login.txt");

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                string sql = "insert into Logins (Username, Password, Filename) values (@param1, @param2, @param3)";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                command.Parameters.Add(new SQLiteParameter("@param1", user.Name));
                command.Parameters.Add(new SQLiteParameter("@param2", user.Password));
                command.Parameters.Add(new SQLiteParameter("@param3", user.Filename));
                command.ExecuteNonQuery();

                cnn.Close();
            }
        }

        public static void sendToPlayerFile(Player user)
        {
            //StreamWriter outputFile;

            //outputFile = File.CreateText(@"../../../GameClassLibrary/TextFiles/" + user.Filename);

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                int enemiesSize = World.enemies.Count();
                int inventorySize = user.Inventory.Count();
                int rows = 0;

                if (enemiesSize > inventorySize)
                {
                    rows = enemiesSize;
                }
                else
                {
                    rows = inventorySize;
                }


                string sql = "replace into canjuras (Class, HP, Race, AC, Location, Gold, Weapon, Enemy, Alive, Inventory) values (@param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11)";

                for (int i = 0; i < rows; i++)
                {
                    SQLiteCommand command = new SQLiteCommand(sql, cnn);


                    command.Parameters.Add(new SQLiteParameter("@param2", user.ClassOfCharacter));
                    command.Parameters.Add(new SQLiteParameter("@param3", user.HP));
                    command.Parameters.Add(new SQLiteParameter("@param4", user.Race));
                    command.Parameters.Add(new SQLiteParameter("@param5", user.AC));
                    command.Parameters.Add(new SQLiteParameter("@param6", user.currentLocation.Name));
                    command.Parameters.Add(new SQLiteParameter("@param7", user.Gold_reward));
                    if (user.CurrentWeapon != null)
                    {
                        command.Parameters.Add(new SQLiteParameter("@param8", user.CurrentWeapon.Name));
                    }
                    else
                    {
                        command.Parameters.Add(new SQLiteParameter("@param8", "None"));
                    }
                    if (i < enemiesSize)
                    {
                        command.Parameters.Add(new SQLiteParameter("@param9", World.enemies[i].Name));
                        command.Parameters.Add(new SQLiteParameter("@param10", World.enemies[i].IsAlive.ToString()));
                    }
                    else
                    {
                        command.Parameters.Add(new SQLiteParameter("@param9", "None"));
                        command.Parameters.Add(new SQLiteParameter("@param10", "None"));
                    }
                    if (i < inventorySize)
                    {
                        command.Parameters.Add(new SQLiteParameter("@param11", user.Inventory[i].Name));

                    }
                    else
                    {
                        command.Parameters.Add(new SQLiteParameter("@param11", "None"));

                    }

                    command.ExecuteNonQuery();   
                }

                cnn.Close();


                //outputFile.WriteLine(user.ClassOfCharacter);
                //outputFile.WriteLine(user.HP);
                //outputFile.WriteLine(user.Race);
                //outputFile.WriteLine(user.AC);
                //outputFile.WriteLine(user.currentLocation.Name);
                //outputFile.WriteLine(user.Gold_reward);
                //if (user.CurrentWeapon != null)
                //{
                //    outputFile.WriteLine(user.CurrentWeapon.Name);
                //}
                //foreach (Enemies item in World.enemies)
                //{
                //    outputFile.WriteLine(item.Name);
                //    outputFile.WriteLine(item.IsAlive.ToString());
                //}       
                //foreach (IItems item in user.Inventory)
                //{
                //    outputFile.WriteLine(item.Name);
                //}
                //outputFile.Close();
            }
        }
    }
}