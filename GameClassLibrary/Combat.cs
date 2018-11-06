using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Combat
    {
        public static void InitiateCombat(Player player, Enemies enemy) //Add storm to list of enemies
        {
            Console.WriteLine($"The player's HP is: {player.HP}");
            Console.WriteLine($"The {enemy.Name}'s HP is {player.HP}");

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
                        }
                        

                    int damageFromEnemy = Random.GetRandom(0, enemy.MaxDamage);
                     if (damageFromEnemy == 0)
                        {
                            Console.WriteLine($"{enemy.Name}'s attack missed.");
                        }
                        else
                        {
                            player.HP -= damageFromEnemy;
                        Console.WriteLine($"{enemy.Name} attacked you, doing {damageFromEnemy} damage.");
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
                }
           
            }
            else //if the enemy is a storm
            {
                //Code here
            }
        }
    }
}
