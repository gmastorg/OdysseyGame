using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class UserLogin
    //used to created objects of existing logins in the login file 
    //only used to verify the login info entered by the player
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Filename { get; }


        public UserLogin(string username, string password)
        {
            Name = username;
            Password = password;
            //grabs 
            Filename = username + ".txt";
        }
    }
}
