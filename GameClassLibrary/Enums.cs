using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    // holds enumerators and the values for characters class and attack 
    // also values for race and defense
    public class Enums
    {   
        public enum CharacterClassHP
        {
            WARRIOR = 140,
            MAGE = 120,
            THIEF = 130,
            CLERIC = 110
        }

        public enum RaceAC
        {
            ELF = 20,
            HUMAN = 40,
            DWARF = 30,
            HOBBIT = 15
        }

        public static Tuple<string, int> getCharacterInfo( string characterClass)
            //**********************DONT NEED TUPLES LOOK AT PAGE 525 for calling int values from tuple
        {
            characterClass = characterClass.ToUpper();
            Tuple<string, int> characterClassTuple;

            switch (characterClass)
            {
                case "WARRIOR":
                    int attack = (int) CharacterClassHP.WARRIOR;
                    characterClassTuple = new Tuple<string, int>(characterClass, attack);
                    return characterClassTuple;
                case "MAGE":
                    attack = (int)CharacterClassHP.MAGE;
                    characterClassTuple = new Tuple<string, int>(characterClass, attack);
                    return characterClassTuple;
                case "THIEF":
                    attack = (int)CharacterClassHP.THIEF;
                    characterClassTuple = new Tuple<string, int>(characterClass, attack);
                    return characterClassTuple;
                case "CLERIC":
                    attack = (int)CharacterClassHP.CLERIC;
                    characterClassTuple = new Tuple<string, int>(characterClass, attack);
                    return characterClassTuple;
                default:
                    return characterClassTuple = new Tuple<string, int>("", 0); 
            }
            
        }

        public static Tuple<string, int> getRaceInfo(string race)
        {
            race = race.ToUpper();
            Tuple<string, int> raceTuple;

            switch (race)
            {
                case "ELF":
                    int attack = (int)RaceAC.ELF;
                    raceTuple = new Tuple<string, int>(race, attack);
                    return raceTuple;
                case "HUMAN":
                    attack = (int)RaceAC.HUMAN;
                    raceTuple = new Tuple<string, int>(race, attack);
                    return raceTuple;
                case "DWARF":
                    attack = (int)RaceAC.DWARF;
                    raceTuple = new Tuple<string, int>(race, attack);
                    return raceTuple;
                case "HOBBIT":
                    attack = (int)RaceAC.HOBBIT;
                    raceTuple = new Tuple<string, int>(race, attack);
                    return raceTuple;
                default:
                    return raceTuple = new Tuple<string, int>("", 0); 
            }

        }

    }
}
