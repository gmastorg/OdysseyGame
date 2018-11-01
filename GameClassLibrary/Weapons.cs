using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Weapons : IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }

        //Constructor
        public Weapons(string name, string description, int damage)
        {
            Name = name;
            Description = description;
            Damage = damage;


        }
    }
}
