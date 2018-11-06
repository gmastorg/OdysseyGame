using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Enemies : LivingCreatures, IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gold_reward { get; set; } //To hold the amount of gold received for killing the enemy
        public int MaxDamage { get; set; } //To hold the amount of damage the enemy does
   


        public Enemies(string name, string description, int gold_reward, int maxdamage, Rooms currentlocation, int hp, int ac, bool isalive): base(currentlocation, hp, ac, isalive)
        {
            Name = name;
            Description = description;
            Gold_reward = gold_reward;
            MaxDamage = maxdamage;
            currentLocation = currentlocation;
            HP = hp;
            AC = ac;
            IsAlive = isalive;
        }

        






    }
}
