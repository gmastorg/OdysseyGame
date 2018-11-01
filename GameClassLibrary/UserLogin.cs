using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameClassLibrary
{
    public class UserLogin
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Filename { get; }

        public UserLogin(string name, string password)
        {
            Name = name;
            Password = password;
            Filename = name + ".txt";
        }

        public static void sendToFile(UserLogin newLogin)
        {
            StreamWriter outputFile;

            outputFile = File.AppendText(@"../../../GameClassLibrary/TextFiles/login.txt");

            outputFile.WriteLine(newLogin.Name);
            outputFile.WriteLine(newLogin.Password);
            outputFile.WriteLine(newLogin.Filename);
            outputFile.Close();
        }
    }

    
}
