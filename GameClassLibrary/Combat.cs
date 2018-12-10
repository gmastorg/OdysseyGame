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
            if (enemy.IsAlive == true && enemy.HP > 0 && player.IsAlive == true && player.HP > 0)
            {

                int chanceToRun = 1;
                string run;
                string attack;


                Console.WriteLine($"The player's HP is: {player.HP}");
                if (enemy.Name != "storm")
                {
                    Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}");
                }


                if (enemy.Name != "storm")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"YOU ARE BEING ATTACKED BY {enemy.Name}!");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (enemy.Name != "mob of suitors")
                    {
                        Console.WriteLine("\nYou may try to \"run away\" or \"attack\"\n");

                        run = Console.ReadLine();
                        Console.WriteLine(run);
                        run = run.ToLower();
                        Console.WriteLine(run);
                        run = run.Trim();
                        Console.WriteLine(run);

                        //run = Console.ReadLine().ToLower().Trim();


                    }

                    else
                    {
                        run = "attack";
                    }


                    if (run.Equals("run away"))
                    {
                        chanceToRun = Random.GetRandom(0, 3);


                        if (chanceToRun == 0)
                        {
                            Console.WriteLine($"\nYou have escaped from {enemy.Name}\n");
                            return player;
                        }

                        else
                        {
                            Console.WriteLine($"\nYou could not escape, you must battle {enemy.Name}\n");
                        }

                    }

                    else
                    {

                        do
                        {

                            if (chanceToRun != 0)
                            {



                                Console.WriteLine("\nYou may \"attack with {CURRENT weapon}\" or \"change weapon to {weapon}\"");
                                attack = Console.ReadLine().ToLower().Trim();
                                while (attack == "")
                                {
                                    attack = Console.ReadLine().ToLower().Trim();
                                }
                                string[] attackSplit = attack.Split(' ');

                                while (attackSplit.Count() < 3)
                                {
                                    attack = Console.ReadLine().ToLower().Trim();
                                }
                                attackSplit = attack.Split(' ');

                                if (player.CurrentWeapon != World.GetWeaponByName(attackSplit[2]) && attackSplit[0] != "change")
                                {
                                    Console.WriteLine($"\n{attackSplit[2]} IS NOT YOUR CURRENT WEAPON.\n");
                                    Console.WriteLine("\nTo change to that weapon, type \"change weapon to {weapon}\"\n");
                                    attack = Console.ReadLine().ToLower();
                                }

                                if (attack == $"attack with {player.CurrentWeapon.Name}")
                                {



                                    int damageFromWeapon = Random.GetRandom(0, player.CurrentWeapon.Damage); //Returns a random value from 0 up to the max damage the weapon can do
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
                                            Console.WriteLine($"This hit killed {enemy.Name}\n");
                                            if (player.CurrentDefense != null)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine($"\nYour {player.CurrentDefense.Name} was damaged in battle and cannot be used again");
                                                player.CurrentDefense = null;
                                                Console.ForegroundColor = ConsoleColor.White;
                                            }


                                        }

                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                }



                                string[] split = attack.Split(' ');
                                if (split[0] == "change" && split[1] == "weapon" && split[2] == "to")
                                {

                                    if (split.Length == 4)
                                    {
                                        if (player.Inventory.Contains(World.GetItemByName(split[3])))
                                        {
                                            player.CurrentWeapon = World.GetWeaponByName(split[3]);
                                            Console.WriteLine($"\nYou have changed your weapon to {player.CurrentWeapon.Name}\n");

                                        }


                                        else
                                        {
                                            Console.WriteLine($"\nYou do not have the weapon {split[3]} in your inventory.\n");
                                        }

                                    }


                                }

                                //The monster hits the player

                                int damageFromEnemy = Random.GetRandom(0, enemy.MaxDamage);
                                if (damageFromEnemy == 0 && enemy.HP > 0 && player.HP > 0) //If damage is zero and player and monster are still alive
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{enemy.Name}'s attack missed.\n"); //Show that hit missed
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                if (damageFromEnemy != 0 && enemy.HP > 0 && player.HP > 0) //If damage is not zero and player and monster are still alive
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{enemy.Name} attacked you!");
                                    if (player.CurrentDefense != null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine($"\nYour {player.CurrentDefense.Name} helped defend against the attack! \n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        int defenseAmount = Random.GetRandom(0, player.CurrentDefense.Value);
                                        if (damageFromEnemy > defenseAmount)
                                        {
                                            damageFromEnemy -= defenseAmount;
                                        }
                                        
                                    }
                                    player.HP -= damageFromEnemy; //Damage done to player
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"The attack did { damageFromEnemy} damage to you.\n"); //Show that hit was successful
                                    if (player.HP > 0)
                                    {
                                        Console.WriteLine($"The player's HP is: {player.HP}\n"); //Show Player's HP
                                    }
                                    else
                                    {
                                        Console.WriteLine("This hit killed you.");
                                        player.CurrentDefense = null;
                                    }

                                }




                                Console.ForegroundColor = ConsoleColor.White;

                            }
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
                        if (player.HP > 0)
                        {
                            int damageFromStorm = Random.GetRandom(1, enemy.MaxDamage);
                            player.HP -= damageFromStorm;
                            Console.WriteLine("The storm rages...\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            if (player.HP > 0)
                            {
                                Console.WriteLine($"The player's HP is: {player.HP}");
                                Console.WriteLine("\n");
                            }
                        }

                    }

                    return player;
                }

                if (player.HP <= 0)
                {
                    player.IsAlive = false;
                }

                Console.ForegroundColor = ConsoleColor.White;

                return player;

            }
        }

    }
}
