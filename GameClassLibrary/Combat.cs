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
                            enemy.MaxHP -= damageFromWeapon;
                            StandardMessages.hitSuccessful(enemy, player.CurrentWeapon, damageFromWeapon);
                        }
                        

                    int damageFromEnemy = Random.GetRandom(0, enemy.Damage);
                     if (damageFromEnemy == 0)
                        {
                            Console.WriteLine($"{enemy.Name}'s attack missed.");
                        }
                        else
                        {
                            player.HitPoints -= damageFromEnemy;
                        Console.WriteLine($"{enemy.Name} attacked you, doing {damageFromEnemy} damage.");
                        }
                    
                }
                while (enemy.MaxHP > 0 && player.HitPoints > 0);
                
                if (enemy.MaxHP <= 0)
                {
                    //Tell the player the enemy was defeated
                    StandardMessages.enemyDefeated(enemy);
                }

                if (player.HitPoints <= 0)
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
