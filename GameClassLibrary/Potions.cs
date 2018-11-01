using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Potions : IItems
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public int HealthIncrease { get; set; }

        public Potions(string name, string description, int healthIncrease)
        {
            Name = name;
            Description = description;
            HealthIncrease = healthIncrease;
        }

    }
}