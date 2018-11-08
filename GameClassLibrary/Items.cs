using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Items : IItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public Items(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
        }


    }
}
