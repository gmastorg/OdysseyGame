using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Potions : IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int HealthIncrease { get; set; }
        public Rooms CurrentLocation { get; set; }
        public bool beenUsed { get; set; }

        public Potions(string name, string description, int healthIncrease, Rooms currentLocation)
        {
            Name = name;
            Description = description;
            HealthIncrease = healthIncrease;
            CurrentLocation = currentLocation;
        }

    }
}