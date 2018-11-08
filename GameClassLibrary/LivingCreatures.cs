using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class LivingCreatures
    {
        public Rooms currentLocation { get; set; }
        public int HP { get; set; }//these are attack points
        public int AC { get; set; } //should these be defense points?
        public bool IsAlive { get; set; }//Boolean to determine if the monster is dead or alive
        public int Gold_reward { get; set; } // gold value given or attained 

        public LivingCreatures(Rooms location, int hp, int ac, bool isalive, int gold_reward)
        {
            currentLocation = location;
            HP = hp;
            AC = ac;
            IsAlive = isalive;
            Gold_reward = gold_reward;
        }
    }
}
