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
    public class Player:LivingCreatures
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
        private static List<Weapons> weapons;

        //The constructor takes no parameters, we will set them in the code after the object is first referenced (in the Login class)
        public Player(string username,  string password, string classOfcharacter, string race, Rooms currentlocation, int hp, int ac, bool isalive, Weapons currentweapon, int gold_reward) :
            base(currentlocation,hp,ac,isalive, gold_reward)
        {
            Name = username;
            Password = password;
            Filename = username + ".txt";
            ClassOfCharacter = classOfcharacter;
            Race = race;
            CurrentWeapon = currentweapon;
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
            StreamWriter outputFile;

            outputFile = File.CreateText(@"../../../GameClassLibrary/TextFiles/"+user.Filename);

            outputFile.WriteLine(user.ClassOfCharacter);
            outputFile.WriteLine(user.HP);
            outputFile.WriteLine(user.Race);
            outputFile.WriteLine(user.AC);
            outputFile.WriteLine(user.currentLocation.Name);
            outputFile.WriteLine(user.Gold_reward);
            if (user.CurrentWeapon != null)
            {
                outputFile.WriteLine(user.CurrentWeapon.Name);
            }
            foreach (Enemies item in World.enemies)
            {
                outputFile.WriteLine(item.Name);
                outputFile.WriteLine(item.IsAlive.ToString());
            }       
            /*foreach (IItems item in user.Inventory)
            {
                outputFile.WriteLine(item.Name);
            }*/
            outputFile.Close();
        }
    }
}