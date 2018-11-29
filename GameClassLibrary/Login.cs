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
    public class Login
    {
        //Method to connect to DB
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            //the return statement is returning the connection details that we
            //established in the project's app.Config)
        }

        public static Tuple<bool, Player> LoginMenu()
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
                int gold_reward = 0;
                Weapons weapon = World.GetWeaponByName("dagger");//defaults to null
                List<IItems> inventory = new List<IItems>();
                inventory.Add(World.GetWeaponByName("dagger"));
                //Create player object
                newPlayer = new Player(username, password, characterClassTuple.Item1, raceTuple.Item1, currentLocation, characterClassTuple.Item2, raceTuple.Item2, isalive, weapon, gold_reward, inventory);
                Player.sendToLoginFile(newPlayer);
                //Send the properties to the text file
                Player.sendToPlayerFile(newPlayer);
                return newPlayer;
            }
        }

        public static Player getPlayer(string username, string password)
        {
            List<IItems> inventory = new List<IItems>();

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "select * from player where Name like @param1";
                cnn.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, cnn))
                {
                    command.Parameters.Add(new SQLiteParameter("@param1", username));

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string classOfCharacter = reader.GetString(1).ToLower();
                        int HP = reader.GetInt16(2);
                        string race = reader.GetString(3);
                        int AC = reader.GetInt16(4);
                        Rooms location = World.GetRoomByName(reader.GetString(5));
                        int gold_reward = reader.GetInt16(6);
                        Weapons weapon = World.GetWeaponByName(reader.GetString(7));
                        bool isAlive = true;
                        string enemy = reader.GetString(8).TrimEnd(',');
                        string enemyAlive = reader.GetString(9).ToLower().TrimEnd(',');
                        string invent = reader.GetString(10).TrimEnd(',');

                        string[] enemies = enemy.Split(',');
                        string[] lives = enemyAlive.Split(',');
                        string[] invents = invent.Split(',');

                        for (int i = 0; i < enemies.Count(); i++)
                        {
                            World.GetEnemyByName(enemies[i]).IsAlive = bool.Parse(lives[i]);
                        }
                        if (invents != null)
                        {

                            for (int i = 0; i < invents.Count(); i++)
                            {
                                inventory.Add(World.GetItemByName(invents[i]));
                            }
                        }


                        Player player = new Player(username, password, classOfCharacter, race, location, HP, AC, isAlive, weapon, gold_reward, inventory);
                        return player;
                    }

                    reader.Close();
                    command.ExecuteNonQuery();
                }
                return null;
            }
        }

        //using (StreamReader reader = new StreamReader(@"../../../GameClassLibrary/TextFiles/" + username + ".txt"))
        //{
        //    if (new FileInfo(@"../../../GameClassLibrary/TextFiles/" + username + ".txt").Length != 0)

        //        while (!reader.EndOfStream)
        //        {
        //            string classOfCharacter = reader.ReadLine();
        //            int HP = int.Parse(reader.ReadLine());
        //            string race = reader.ReadLine();
        //            int AC = int.Parse(reader.ReadLine());
        //            Rooms location = World.GetRoomByName(reader.ReadLine());
        //            bool isalive = true;
        //            int gold_reward = int.Parse(reader.ReadLine());
        //            Weapons weapon = World.GetWeaponByName(reader.ReadLine());
        //            for (int i = 1; i <= 7; i++)
        //            {
        //                string name = reader.ReadLine();
        //                bool alive = bool.Parse(reader.ReadLine());

        //                World.GetEnemyByName(name).IsAlive = alive;
        //            }
        //            while(!reader.EndOfStream)
        //            {
        //                string item = reader.ReadLine();
        //                inventory.Add(World.GetItemByName(item));
        //            }
        //            Player player = new Player(username, password, classOfCharacter, race, location, HP, AC, isalive, weapon, gold_reward, inventory);

        //            return player;
        //        }
        //return null;
    }
} 
