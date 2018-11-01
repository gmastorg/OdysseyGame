using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class UserLogin
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Filename { get; }


        public UserLogin(string username, string password)
        {
            Name = username;
            Password = password;
            Filename = username + ".txt";
        }
    }
}
