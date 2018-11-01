using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Enemies : IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gold_reward { get; set; } //To hold the amount of gold received for killing the enemy
        public int Damage { get; set; } //To hold the amount of damage the enemy does
        public int CurrentHP { get; set; } //Current hitPoints of the enemy
        public int MaxHP { get; set; } //Maximum hitPoints of the enemy
        public bool IsAlive { get; set; }//Boolean to determine if the monster is dead or alive


        public Enemies(string name, string description, int gold_reward, int damage, int currentHP, int maxHP, bool isAlive)
        {
            Name = name;
            Description = description;
            Gold_reward = gold_reward;
            Damage = damage;
            CurrentHP = currentHP;
            MaxHP = maxHP;
            IsAlive = isAlive;
        }

        






    }
}
