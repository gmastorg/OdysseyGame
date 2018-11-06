using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameClassLibrary
{
   
    public class Player
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Filename { get; set; }
        public string ClassOfCharacter { get; set; }
        public string Race { get; set; }
        public Rooms currentLocation { get; set; }
        public int HitPoints { get; set; }
        public int Defense { get; set; } 
        public Weapons CurrentWeapon { get; set; }

        //The constructor takes no parameters, we will set them in the code after the object is first referenced (in the Login class)
        public Player(string username,  string password, string classOfcharacter, string race, Rooms currentlocation, int hitpoints, int defense)
        {
            Name = username;
            Password = password;
            Filename = username + ".txt";
            ClassOfCharacter = classOfcharacter;
            Race = race;
            currentLocation = currentlocation;
            HitPoints = hitpoints;
            Defense = defense;

        }

        public static void sendToLoginFile(Player user)
        {
            StreamWriter outputFile;

            outputFile = File.AppendText(@"../../../GameClassLibrary/TextFiles/login.txt");

            outputFile.WriteLine(user.Name);
            outputFile.WriteLine(user.Password);
            outputFile.WriteLine(user.Filename);
            outputFile.Close();
        }

        public static void sendToPlayerFile(Player user)
        {
            StreamWriter outputFile;

            outputFile = File.CreateText(@"../../../GameClassLibrary/TextFiles/"+user.Filename);

            outputFile.WriteLine(user.ClassOfCharacter);
            outputFile.WriteLine(user.HitPoints);
            outputFile.WriteLine(user.Race);
            outputFile.WriteLine(user.Defense);
            outputFile.WriteLine(user.currentLocation.Name);

            outputFile.Close();
        }

    }
}