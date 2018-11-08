using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class StandardMessages
    {
        public static void IntroMessage()
        {
            Console.WriteLine($"Welcome to the Odyssey.\n\nWe begin our mission with our hero Odysseus " +
                  $"on the Island of Troy weary from his travels and anxious to get home.");
        }

        public static void Login()
        {
            Console.WriteLine($"\nTo continue please select an option below: \n" +
                $"1. Login\n2. Create an account");
        }

        public static void Menu()
        {
            Console.WriteLine($"To view your weapons type weapons. \nTo view potions type potions. " +
                  $"\nTo view treasures type treasures. \nTo view rooms type rooms.\nTo view items type items." +
                  $"\nTo view enemies type enemies. \nTo exit type exit.\n");
        }

        public static void invalidInput()
        {
            Console.WriteLine("Invalid choice.");
            Console.WriteLine("Enter your menu choice: ");
        }

        public static void getUsername()
        {
            Console.WriteLine("Username: ");
        }

        public static void getPassword()
        {
            Console.WriteLine("Password: ");
        }

        public static void createPassword()
        {
            Console.WriteLine($"Enter Password (must include at least 1 uppercase " +
                $"letter, 1 lowercase letter, and a special character(@#$%.,)");
        }

        public static void selectClass()
        {
            Console.WriteLine($"Select a class for your character: \n" +
                $"Warrior\nMage\nThief\nCleric");
        }

        public static void selectRace()
        {
            Console.WriteLine($"Select the species of your charater: \n" +
                $"Elf\nHuman\nDwarf\nHobbit");
        }

        public static void usernameTaken()
        {
            Console.WriteLine("This username has already been taken.");
        }

        public static void enemyDefeated(Enemies enemy)
        {
            Console.WriteLine($"\n{enemy.Name} HAS BEEN DEFEATED!\n");
        }

        public static void hitMissed (Weapons weapon)
        {
            Console.WriteLine($"Your attack with the {weapon.Name} failed to hit the enemy\n");
        }

        public static void hitSuccessful(Enemies enemy, Weapons weapon, int damage)
        {
            Console.WriteLine($"You struck the {enemy.Name} with the {weapon.Name} and inflicted {damage} damage!\n");
        }
    }
}
