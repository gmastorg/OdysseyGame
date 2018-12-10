using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public static class World
    {
        //Declare static lists to hold the objects //Took readonly out of here
        public static readonly List<Weapons> weapons = new List<Weapons>();
        public static  readonly List<Potions> potions = new List<Potions>();
        public static readonly List<Treasures> treasures = new List<Treasures>(); //Not currently using
        public static readonly List<Items> items = new List<Items>();
        public static readonly List<Enemies> enemies = new List<Enemies>();
        public static readonly List<UserLogin> logins = new List<UserLogin>();
        public static readonly List<Rooms> rooms = new List<Rooms>();

        //Declare a list of all items in the IItems Interface class
        public static List<IItems> allItems = new List<IItems>();

     




        //Static methods to return lists of strings for the object's name properties
        public static List<string> getList(List<Weapons> weapons)
        {
            List<string> weaponList = new List<string>();

            foreach (Weapons val in weapons)
            {
                string weapon = (val.Name);
                weaponList.Add(weapon);
            }

            return weaponList;
        }

        public static List<string> getList(List<Potions> potions)
        {
            List<string> potionsList = new List<string>();

            foreach (Potions val in potions)
            {
                string potion = (val.Name);
                potionsList.Add(potion);
            }

            return potionsList;
        }

        public static List<string> getList(List<Treasures> treasures)
        {
            List<string> treasuresList = new List<string>();

            foreach (Treasures val in treasures)
            {
                string treasure = (val.Name);
                treasuresList.Add(treasure);
            }

            return treasuresList;
        }

        public static List<string> getList(List<Rooms> rooms)
        {
            List<string> roomsList = new List<string>();

            foreach (Rooms val in rooms)
            {
                string room = (val.Name);
                roomsList.Add(room);
            }

            return roomsList;
        }

        public static List<string> getList(List<Items> items)
        {
            List<string> itemsList = new List<string>();

            foreach (Items val in items)
            {
                string item = (val.Name);
                itemsList.Add(item);
            }

            return itemsList;
        }

        public static List<string> getList(List<Enemies> enemies)
        {
            List<string> enemiesList = new List<string>();

            foreach (Enemies val in enemies)
            {
                string enemy = (val.Name);
                enemiesList.Add(enemy);
            }

            return enemiesList;
        }

        //Method to return the object if called by name
        public static IItems GetItemByName(string Name)
        {
            foreach (IItems item in allItems)
            {
                if (item.Name == Name)
                {

                    return item;
                }
            }
            return null;
        }

        //Method to return the room object if called by name
        public static Rooms GetRoomByName(string Name)
        {
            foreach (Rooms room in rooms)
            {
                if (room.Name == Name)
                {
                    return room;
                }
            }
            return null;
        }

        public static void printList(List<string> output)
        // prints list items to screen
        {
            foreach (string val in output)
            {
                Console.Write($"{val}\n");
            }
            Console.WriteLine("");
        }

        public static string Look(IItems item)
        {
            return ($"\n{item.Name}\nDescription: {item.Description}\n");
        }

        public static string Look(Rooms currentLocation)
        {
            return ($"You are in {currentLocation.Name} {currentLocation.Description}");
        }

        //To return an enemy object using its Name
        public static Enemies GetEnemyByName(string Name)
        {
            foreach (Enemies enemy in enemies)
            {
                if (enemy.Name == Name)
                {
                    return enemy;
                }
            }
            return null;
        }

        //To return a weapon object using its Name
        public static Weapons GetWeaponByName(string Name)
        {
            foreach (Weapons weapon in weapons)
            {
                if (weapon.Name == Name)
                {
                    return weapon;
                }
            }
            return null;
        }

        public static Potions GetPotionByName(string Name)
        {
            foreach (Potions potion in potions)
            {
                if (potion.Name == Name)
                {
                    return potion;
                }
            }
            return null;
        }

        public static Treasures GetTreasureByName(string Name)
        {
            foreach (Treasures treasure in treasures)
            {
                if (treasure.Name == Name)
                {
                    return treasure;
                }
            }
            return null;
        }

        public static Player useItem(Weapons item, Player player)
        {
            if (World.weapons.Contains(item))
            {
                player.CurrentWeapon = item;
                Console.WriteLine($"Your current weapon is {player.CurrentWeapon.Name}\n");
            }

            else
            {
                Console.WriteLine($"{item} is not a valid item.\n");
            }

            return player;
        }

        public static Player useItem(Potions item, Player player)
        {

            if (World.potions.Contains(item))
            {
                player.HP += item.HealthIncrease;
                Console.WriteLine($"The {item.Name} increased your HP by {item.HealthIncrease}\n");
            }

            else
            {
                Console.WriteLine($"{item} is not a valid item.\n");
            }

            return player;
        }

        public static Player useItem(Treasures item, Player player)
        {

            if (World.treasures.Contains(item))
            {
                player.AC += item.Value;
                Console.WriteLine($"You are currently using the {item.Name} to defend yourself.");
                player.CurrentDefense = World.GetTreasureByName(item.Name);
            }

            else
            {
                Console.WriteLine($"{item} is not a valid item.\n");
            }

            return player;

        }

    }

}