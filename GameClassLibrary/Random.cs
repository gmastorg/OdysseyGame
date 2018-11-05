using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Random
    {
        public static System.Random random = new System.Random();
        public int randomValue;

        public int GetRandom(int min, int max)
        {
            randomValue = random.Next(min, max+1);
            
            //Return the random value
            return randomValue;
        }
    }
}
