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
                do
                {

                     int damageFromWeapon = Random.GetRandom(player.CurrentWeapon.Damage/2, player.CurrentWeapon.Damage); //Returns a random value from 0 up to the max damage the weapon can do
                     if (damageFromWeapon == 0)
                        {
                            StandardMessages.hitMissed(player.CurrentWeapon);
                        }
                        else
                        {
                            enemy.HP -= damageFromWeapon;
                            StandardMessages.hitSuccessful(enemy, player.CurrentWeapon, damageFromWeapon);
                            Console.WriteLine($"The {enemy.Name}'s HP is {enemy.HP}\n");
                    }
                        

                    int damageFromEnemy = Random.GetRandom(0, enemy.MaxDamage);
                     if (damageFromEnemy == 0)
                        {
                            Console.WriteLine($"{enemy.Name}'s attack missed.\n");
                        }
                        else
                        {
                            //TODO need to use AC points first then use the player's HP
                            player.HP -= damageFromEnemy;
                        Console.WriteLine($"{enemy.Name} attacked you, doing {damageFromEnemy} damage.\n");
                        Console.WriteLine($"The player's HP is: {player.HP}\n");
                    }
                    
                }
                while (enemy.HP > 0 && player.HP > 0);
                
                if (enemy.HP <= 0)
                {
                    //Tell the player the enemy was defeated
                    StandardMessages.enemyDefeated(enemy);
                    enemy.IsAlive = false;
                    player.Gold_reward += enemy.Gold_reward;
                    Console.WriteLine($"You recieved {enemy.Gold_reward} for defeating {enemy.Name}.");
                    
                }

                if (player.HP <= 0)
                {
                    Console.WriteLine($"You have been defeated by {enemy.Name}, you have died...");
                    player.HP = 0;
                    player.IsAlive = false; //killed player in program will reload from last save
                   
                }
                return player;
           
            }
            else //if the enemy is a storm 
            {
                Console.WriteLine("A storm is approaching...");
                DateTime time = DateTime.Now;
                time = time.AddSeconds(5);
                Console.WriteLine("The storm rages...");
                int timeToHit = Random.GetRandom(1, 4);
                int i;
                for (i = 0; i < timeToHit; i++)
                {
                    int damageFromStorm = Random.GetRandom(1, enemy.MaxDamage);
                    player.HP -= damageFromStorm;
                    Console.WriteLine($"The player's HP is: {player.HP}");
                }

                return player;
               
            }
        }

    }
}
