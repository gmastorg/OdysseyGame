using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Rooms
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Rooms roomToNorth {get; set;} 
        public Rooms roomToEast {get; set;}
        public Rooms roomToSouth {get; set;}
        public Rooms roomToWest {get; set;}
        public Rooms roomToNortheast {get; set;}
        public Rooms roomToNorthwest {get; set;}
        public Rooms roomToSoutheast {get; set;}
        public Rooms roomToSouthwest {get; set;}


        public Rooms(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
