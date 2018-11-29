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

        //public static void CreatePlayerTable(Player user)
        //{
        //    using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        cnn.Open();

        //        string sql = "CREATE TABLE IF NOT EXISTS Player (Name varchar(20), Class varchar(20), HP int, Race varchar(20), AC int, Location varchar(20),"
        //            + "Gold int, Weapon varchar(20), Enemy varchar(20), Alive varchar(20), Inventory varchar(20) )";

        //        SQLiteCommand command = new SQLiteCommand(sql, cnn);
        //       //command.Parameters.Add(new SQLiteParameter("@param1", user.Name));
        //        command.ExecuteNonQuery();

        //        cnn.Close();
        //    }

        //    sendToPlayerFile(user);
        //}

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
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                string enemiesList = "";
                string enemiesAliveList = "";
                string inventoryList = "";

                foreach(Enemies enemy in World.enemies)
                {
                    enemiesList += enemy.Name + ",";
                }

                foreach (Enemies enemy in World.enemies)
                {
                    enemiesAliveList += enemy.IsAlive.ToString() + ",";
                }

                
               foreach (IItems item in user.Inventory)
               {
                    if (item != null)
                    {
                        inventoryList += item.Name + ",";
                    }
               }

                string sql = "insert into player (Name, Class, HP, Race, AC, Location, Gold, Weapon, Enemy, Alive, Inventory) values (@name, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11)";

                SQLiteCommand command = new SQLiteCommand(sql, cnn);

                command.Parameters.Add(new SQLiteParameter("@name", user.Name));
                command.Parameters.Add(new SQLiteParameter("@param2", user.ClassOfCharacter));
                command.Parameters.Add(new SQLiteParameter("@param3", user.HP));
                command.Parameters.Add(new SQLiteParameter("@param4", user.Race));
                command.Parameters.Add(new SQLiteParameter("@param5", user.AC));
                command.Parameters.Add(new SQLiteParameter("@param6", user.currentLocation.Name));
                command.Parameters.Add(new SQLiteParameter("@param7", user.Gold_reward));
                command.Parameters.Add(new SQLiteParameter("@param8", user.CurrentWeapon.Name));
                command.Parameters.Add(new SQLiteParameter("@param9",enemiesList));
                command.Parameters.Add(new SQLiteParameter("@param10",enemiesAliveList));
                command.Parameters.Add(new SQLiteParameter("@param11", inventoryList));

                command.ExecuteNonQuery();   

                cnn.Close();
            }
        }
        //public static void sendToPlayerFile(Player user)
        //{
        //StreamWriter outputFile;

        //outputFile = File.CreateText(@"../../../GameClassLibrary/TextFiles/" + user.Filename);

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
//    }
//}

public static void saveToDataBase(Player user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Close();
                GC.Collect();

                string sql = "replace into player (Name, Class, HP, Race, AC, Location, Gold, Weapon, Enemy, Alive, Inventory) values (@name, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11)";

                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                {
                    string enemiesList = "";
                    string enemiesAliveList = "";
                    string inventoryList = "";

                    foreach (Enemies enemy in World.enemies)
                    {
                        enemiesList += enemy.Name + ",";
                    }

                    foreach (Enemies enemy in World.enemies)
                    {
                        enemiesAliveList += enemy.IsAlive.ToString() + ",";
                    }


                    foreach (IItems item in user.Inventory)
                    {
                        if (item != null)
                        {
                            inventoryList += item.Name + ",";
                        }
                    }
                    cmd.Parameters.Add(new SQLiteParameter("@name", user.Name));
                    cmd.Parameters.Add(new SQLiteParameter("@param2", user.ClassOfCharacter.ToUpper()));
                    cmd.Parameters.Add(new SQLiteParameter("@param3", user.HP));
                    cmd.Parameters.Add(new SQLiteParameter("@param4", user.Race));
                    cmd.Parameters.Add(new SQLiteParameter("@param5", user.AC));
                    cmd.Parameters.Add(new SQLiteParameter("@param6", user.currentLocation.Name));
                    cmd.Parameters.Add(new SQLiteParameter("@param7", user.Gold_reward));
                    if (user.CurrentWeapon != null)
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@param8", user.CurrentWeapon.Name));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@param8", "None"));
                    }
                    cmd.Parameters.Add(new SQLiteParameter("@param9", enemiesList));
                    cmd.Parameters.Add(new SQLiteParameter("@param10", enemiesAliveList));
                    if (user.Inventory != null)
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@param11", inventoryList));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@param11", "None"));
                    }
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
                GC.Collect();
            }
        }

        //public static void saveToDataBase(Player user)
        //{
        //StreamWriter outputFile;

        //outputFile = File.CreateText(@"../../../GameClassLibrary/TextFiles/" + user.Filename);

        //using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //{
        //    cnn.Open();

        //    string enemiesAliveList = "";
        //    string inventoryList = "";

        //    foreach (Enemies enemy in World.enemies)
        //    {
        //        enemiesAliveList += enemy.IsAlive.ToString() + ",";
        //    }

        //    foreach (IItems item in user.Inventory)
        //    {
        //        if (item != null)
        //        {
        //            inventoryList += item.Name + ",";
        //        }
        //    }

        //    string sql = "update player set HP = @param3, AC = @param5, Location = @param6, Gold = @param7, Weapon = @param8, Alive = @param10, Inventory = @param11";

        //    SQLiteCommand command = new SQLiteCommand(sql, cnn);

        //    command.Parameters.Add(new SQLiteParameter("@param3", user.HP));
        //    command.Parameters.Add(new SQLiteParameter("@param5", user.AC));
        //    command.Parameters.Add(new SQLiteParameter("@param6", user.currentLocation.Name));
        //    command.Parameters.Add(new SQLiteParameter("@param7", user.Gold_reward));
        //    if (user.CurrentWeapon != null)
        //    {
        //        command.Parameters.Add(new SQLiteParameter("@param8", user.CurrentWeapon.Name));
        //    }
        //    else
        //    {
        //        command.Parameters.Add(new SQLiteParameter("@param8", "None"));
        //    }
        //    command.Parameters.Add(new SQLiteParameter("@param10", enemiesAliveList));
        //    if (user.Inventory != null)
        //    {
        //        command.Parameters.Add(new SQLiteParameter("@param11", inventoryList));
        //    }
        //    else
        //    {
        //        command.Parameters.Add(new SQLiteParameter("@param11", "None"));
        //    }

        //    command.ExecuteNonQuery();

        //    cnn.Close();


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