using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameClassLibrary
{
    public class Login

    {
        public static Tuple<bool,Player> LoginMenu()
        {
            Player newPlayer;
            bool tryAgain = true;
            Tuple<bool, Player> loginPlayer;

            //gets menu display from standard messages class
            StandardMessages.Login();
            String choice = Console.ReadLine();

            //validates input
            while (choice == "" || char.IsDigit(choice, 0) == false)
            {
                //gives message that input was invalid
                StandardMessages.invalidInput();
                choice = Console.ReadLine();
            }
            int decision = int.Parse(choice);

            //checks if number is in menu
            while (decision < 1 || decision > 2)
            {
                StandardMessages.invalidInput();
                choice = Console.ReadLine();
                decision = int.Parse(choice);
            }

            if (decision == 1)
            {
                loginPlayer = getUsernameAndPassword();
                Console.WriteLine("\n");
                return loginPlayer;
            }

            else if (decision == 2)
            {
                newPlayer = createPlayer();
                Console.WriteLine("\n");
                tryAgain = true;

                loginPlayer = new Tuple<bool, Player>(tryAgain, newPlayer);
                return loginPlayer;
            }

            return null;
        }

        public static Tuple<bool, Player> getUsernameAndPassword()
        {
            Tuple<bool, Player> userInfo;
            bool valid;
            StandardMessages.getUsername();
            string username = Console.ReadLine();

            StandardMessages.getPassword();
            string password = Console.ReadLine();

            //ADD GET CURRENT ROOM AS PROPERTY OF PLAYER HERE

            bool verify = InputValidation.VerifyUsername(username) && InputValidation.VerifyPassword(password);

            if (verify == true)
            {
                Console.WriteLine("You are logged in.");
                //eventually print character info and stats here//CAN ADD ROOM PROPERTY HERE
                Player newPlayer = getPlayer(username, password);
                valid = true;
                userInfo = new Tuple<bool, Player>(valid, newPlayer);
                return userInfo;
            }
            else
            {
                Console.WriteLine("Incorrect Information. Try again.");
                valid = false;
                Player newPlayer = null;
                userInfo = new Tuple<bool, Player>(valid, newPlayer);
                return userInfo;
            }
        }

        public static Player createPlayer()
        {
            Player newPlayer;
            StandardMessages.getUsername();
            string username = Console.ReadLine();

            if (InputValidation.VerifyUsername(username) == true)
            {
                //if username is already used prompts user to try to login or select a new username
                StandardMessages.usernameTaken();
                Console.WriteLine("Would you like to try to login? (Y/N)");
                string login = Console.ReadLine().ToLower();

                char[] letter = login.ToCharArray();

                switch (letter[0])
                {
                    case 'y':
                        LoginMenu();
                        break;
                    case 'n':
                        createPlayer();
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;

                }

                return null;
            }

            else
            {
                StandardMessages.createPassword();
                string password = Console.ReadLine();
                while (!InputValidation.ValidatePassword(password))
                {
                    //prompts user to keep trying password until reqs met
                    StandardMessages.createPassword();
                    password = Console.ReadLine();
                }
                StandardMessages.selectClass();
                string characterClass = Console.ReadLine();
                Tuple<string, int> characterClassTuple;

                while (!InputValidation.CharacterClassValidation(characterClass))
                {
                    //prompts user to enter a class choice that is given
                    StandardMessages.selectClass();
                    characterClass = Console.ReadLine();
                }

                characterClassTuple = Enums.getCharacterInfo(characterClass);

                StandardMessages.selectRace();
                string race = Console.ReadLine();
                Tuple<string, int> raceTuple;

                while (!InputValidation.characterRaceValidation(race))
                {
                    //prompts user to enter a race choice that is given
                    StandardMessages.selectRace();
                    race = Console.ReadLine();
                }

                raceTuple = Enums.getRaceInfo(race);

                Rooms currentLocation = World.GetRoomByName("Troy");//This defaults the player to Troy
                bool isalive = true; //defaults to alive
                //Create player object
                newPlayer = new Player(username, password, characterClassTuple.Item1, raceTuple.Item1, currentLocation, characterClassTuple.Item2, raceTuple.Item2, isalive);
                Player.sendToLoginFile(newPlayer);
                //Send the properties to the text file
                Player.sendToPlayerFile(newPlayer);
                return newPlayer;
            }
        }

        public static Player getPlayer(string username, string password)
        {
            using (StreamReader reader = new StreamReader(@"../../../GameClassLibrary/TextFiles/" + username+".txt"))
            {
                if (new FileInfo(@"../../../GameClassLibrary/TextFiles/" + username +".txt").Length != 0)

                    while (!reader.EndOfStream)
                    {
                        string classOfCharacter = reader.ReadLine();
                        int HP = int.Parse(reader.ReadLine());
                        string race = reader.ReadLine();
                        int AC = int.Parse(reader.ReadLine());
                        Rooms location = World.GetRoomByName(reader.ReadLine());
                        bool isalive = true;

                        Player player = new Player(username, password, classOfCharacter, race, location, HP, AC, isalive);

                        return player;
                    }

                }
                return null;
            }
        }
    }
