using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public class Look
    {
        public static void lookAround(Rooms currentRoom)
        {


            if (currentRoom.roomToNorth != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the North...");
            }
            if (currentRoom.roomToEast != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the East...");
            }
            if (currentRoom.roomToSouth != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the South...");
            }
            if (currentRoom.roomToWest != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the West...");
            }
            if (currentRoom.roomToNortheast != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the Northeast...");
            }
            if (currentRoom.roomToNorthwest != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the Northwest...");
            }
            if (currentRoom.roomToSoutheast != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the Southeast...");
            }
            if (currentRoom.roomToSouthwest != null)
            {
                Console.WriteLine("\nThere appears to be an exit to the Southwest...");
            }

            Console.WriteLine($" ");

        }
    }
}
