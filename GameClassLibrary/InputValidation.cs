using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class InputValidation
    {
        public static bool ValidatePassword(string password)
        {
            if(( password.Contains('@')||password.Contains('#')||
                password.Contains('$')||password.Contains('%')||
                password.Contains('.')||password.Contains(',')) &&
                password.Any(char.IsUpper) && password.Any(char.IsLower))
            {
                return true;
            }

            return false;
        }

        public static bool CharacterClassValidation(string characterClass)
        {
            characterClass = characterClass.ToUpper();

            switch (characterClass)
            {
                case "WARRIOR":
                    return true;
                case "MAGE":
                    return true;
                case "THIEF":
                    return true;
                case "CLERIC":
                    return true;
                default:
                    return false;
            }
        }

        public static bool characterRaceValidation(string race)
        {
            race = race.ToUpper();

            switch (race)
            {
                case "ELF":
                    return true;
                case "HUMAN":
                    return true;
                case "DWARF":
                    return true;
                case "HOBBIT":
                    return true;
                default:
                    return false;
            }
        }
        // need to ask why this is giving error
        public static bool VerifyUsername(string username)
        {
            if (World.logins == null) //If the list is not empty
            {
                return false;
            }
            else
            { 
                foreach (UserLogin val in World.logins)
                {
                    if (username == val.Name)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        // need to ask why this is giving error
        public static bool VerifyPassword(string password)
        {
            if (World.logins == null)//If the list is not empty
            {
                return false;
            }
            else
            {
                foreach (UserLogin val in World.logins)
                {
                    if (password == val.Password)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
