using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Treasures : IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Treasures(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Treasures()
        {
            Name = "Treasure";
            Description = "Gold, silver and jewels";

        }


    }
}
