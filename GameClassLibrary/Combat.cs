﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Combat
    {
        public static void InitiateCombat(Player player, Enemies enemy) //Add storm to list of enemies //Add weapon to player Login(write to file)
=======
        public static void InitiateCombat(Player player, Enemies enemy) 
>>>>>>> master
        {
            Console.WriteLine($"The player's HP is: {player.HP}");
            Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}");


            if (enemy.Name != "storm")
            {
                do
                {

                     int damageFromWeapon = Random.GetRandom(0, player.CurrentWeapon.Damage); //Returns a random value from 0 up to the max damage the weapon can do
                     if (damageFromWeapon == 0)
                        {
                            StandardMessages.hitMissed(player.CurrentWeapon);
                        }
                        else
                        {
                            enemy.HP -= damageFromWeapon;
                            StandardMessages.hitSuccessful(enemy, player.CurrentWeapon, damageFromWeapon);
                            Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}");
                    }
                        

                    int damageFromEnemy = Random.GetRandom(0, enemy.MaxDamage);
                     if (damageFromEnemy == 0)
                        {
                            Console.WriteLine($"{enemy.Name}'s attack missed.");
                        }
                        else
                        {
                            //TODO need to use AC points first then use the player's HP
                            player.HP -= damageFromEnemy;
                        Console.WriteLine($"{enemy.Name} attacked you, doing {damageFromEnemy} damage.");
                        Console.WriteLine($"The player's HP is: {player.HP}");
                    }
                    
                }
                while (enemy.HP > 0 && player.HP > 0);
                
                if (enemy.HP <= 0)
                {
                    //Tell the player the enemy was defeated
                    StandardMessages.enemyDefeated(enemy);
                    enemy.IsAlive = false;
                }

                if (player.HP <= 0)
                {
                    Console.WriteLine($"You have been defeated by {enemy.Name}, you have died...");
                    player.HP = 0;
                    player.IsAlive = false; //killed player in program will reload from last save
                }
           
            }
            else //if the enemy is a storm
            {
                //Code here
            }


            /** TODO add this code below to combat class
             * if (enemy.name = "storm")
             * {
             *      Enemies stormCopy = new Enemies(enemy.name, enemy.description, enemy.gold_reward, enemy.maxdamage, enemy.location, enemy.HP, enemy.AC, enemy.isAlive);
             *      
             *      enemy.location = stormCopy.location;
             */
        }

    }
}
