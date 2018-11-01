using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameClassLibrary
{
   
    public class Player:UserLogin
    {
        public string ClassOfCharacter { get; set; }
        public string Race { get; set; }
        public Rooms currentLocation { get; set; }
        public int HitPoints { get; set; }//these are attack points
        public int Defense { get; set; } //should these be defense points?
        

        //The constructor takes no parameters, we will set them in the code after the object is first referenced (in the Login class)
        public Player(string username,  string password, string classOfcharacter, string race, Rooms currentlocation, int hitpoints, int defense)
            :base(username,password)
        {
            ClassOfCharacter = classOfcharacter;
            Race = race;
            currentLocation = currentlocation;
            HitPoints = hitpoints;
            Defense = defense;
        }

        public static void sendToFile(Player user)
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