using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Combat
    {

        public static Player InitiateCombat(Player player, Enemies enemy) //Add storm to list of enemies //Add weapon to player Login(write to file)


        {
            Console.WriteLine($"The player's HP is: {player.HP}");
            Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}");


            if (enemy.Name != "storm")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"YOU ARE BEING ATTACKED BY {enemy.Name}!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nYou may try to \"run away\" or \"attack with {weapon}\"\n");
               

                do
                {
                    
                        string attack = Console.ReadLine();

                    if (attack == $"attack with {player.CurrentWeapon.Name}")
                    {

                        int damageFromWeapon = Random.GetRandom(player.CurrentWeapon.Damage / 2, player.CurrentWeapon.Damage); //Returns a random value from 0 up to the max damage the weapon can do
                        if (damageFromWeapon == 0 && player.HP > 0 && enemy.HP > 0)//If damage is zero and player and monster are still alive
                        {
                            StandardMessages.hitMissed(player.CurrentWeapon); //Show that hit missed
                        }
                        if (damageFromWeapon != 0 && player.HP > 0 && enemy.HP > 0)//If damage is not zero and player and monster are still alive
                        {
                            enemy.HP -= damageFromWeapon; //Damage done to monster
                            Console.ForegroundColor = ConsoleColor.Green;
                            StandardMessages.hitSuccessful(enemy, player.CurrentWeapon, damageFromWeapon); //Show that hit was successful
                            if (enemy.HP > 0)
                            {
                                Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}\n"); //Show enemies HP
                            }
                            else
                            {
                                Console.WriteLine($"This hit killed {enemy.Name}");
                            }

                        }
                        Console.ForegroundColor = ConsoleColor.White;


                    } //End of if-else for "attack with {weapon}

                    string[] split = attack.Split(' ');
                    if (split[0] == "change" && split[1] == "weapon" && split[2] == "to")
                    {
                        
                            if(player.Inventory.Contains(World.GetWeaponByName(split[3])))
                            {
                                player.CurrentWeapon = World.GetWeaponByName(split[3]);
                                Console.WriteLine($"You have changed your weapon to {player.CurrentWeapon.Name}");

                            }
                        else
                        {
                            Console.WriteLine($"You do not have the weapon {split[3]} in your inventory.");
                        }
                        

                    }






                    int damageFromEnemy = Random.GetRandom(0, enemy.MaxDamage);
                        if (damageFromEnemy == 0 && enemy.HP > 0 && player.HP > 0) //If damage is zero and player and monster are still alive
                        {
                            Console.WriteLine($"{enemy.Name}'s attack missed.\n"); //Show that hit missed
                        }
                        if (damageFromEnemy != 0 && enemy.HP > 0 && player.HP > 0) //If damage is not zero and player and monster are still alive
                        {
                            player.HP -= damageFromEnemy; //Damage done to player
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{enemy.Name} attacked you, doing {damageFromEnemy} damage.\n"); //Show that hit was successful
                            if (player.HP > 0)
                            {
                                Console.WriteLine($"The player's HP is: {player.HP}\n"); //Show Player's HP
                            }
                            else
                            {
                                Console.WriteLine("This hit killed you.");
                            }

                        }
                        Console.ForegroundColor = ConsoleColor.White;

                    
                    
                    
                }
                while (enemy.HP > 0 && player.HP > 0);
                
                if (enemy.HP <= 0)
                {
                    //Tell the player the enemy was defeated
                    StandardMessages.enemyDefeated(enemy);
                    enemy.IsAlive = false;
                    player.Gold_reward += enemy.Gold_reward;
                    Console.WriteLine($"You recieved {enemy.Gold_reward} gold for defeating {enemy.Name}.");
                    
                }

                if (player.HP <= 0)
                {
                    Console.WriteLine($"You have been defeated by {enemy.Name}, you have died...\n");
                    player.HP = 0;
                    player.IsAlive = false; //killed player in program will reload from last save
                   
                }

                return player;
           
            }
            else //if the enemy is a storm 
            {
                if (player.HP > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nA storm is approaching...\n");
                    DateTime time = DateTime.Now;
                    time = time.AddSeconds(5);
                    int timeToHit = Random.GetRandom(1, 4);
                    int i;
                    for (i = 0; i < timeToHit; i++)
                    {
                        int damageFromStorm = Random.GetRandom(1, enemy.MaxDamage);
                        player.HP -= damageFromStorm;
                        Console.WriteLine("The storm rages...\n");
                        Console.WriteLine($"The player's HP is: {player.HP}");
                        Console.WriteLine("\n");
                    }

                    return player;
                }
                Console.ForegroundColor = ConsoleColor.White;

                return player;
                
            }
        }

    }
}
