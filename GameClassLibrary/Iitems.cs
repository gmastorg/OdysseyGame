using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public interface IItems
    {
        string Name { get; set; }
        string Description { get; set; }
        Rooms CurrentLocation { get; set; }
    }
}

