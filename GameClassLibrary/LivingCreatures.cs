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

        public LivingCreatures(Rooms location, int hp, int ac, bool isalive)
        {
            currentLocation = location;
            HP = hp;
            AC = ac;
            IsAlive = isalive;
        }
    }
}
