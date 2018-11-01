using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClassLibrary
{
    public static class Move
    {


        public static Rooms MoveTo(Rooms newRoom, Player newPlayer)
        {
            if (newRoom != null)
            {
                newPlayer.currentLocation = newRoom;
                Console.WriteLine("You are in " + newRoom.Name);
                Console.WriteLine("");
                Console.WriteLine(newRoom.Description);
                return newRoom;
            }
            else
            {
                Console.WriteLine("Nothing exists in that direction...");
                return null;
            }
        }

        public static void moveNorth(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToNorth, newPlayer);
        }

        public static void moveEast(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToEast, newPlayer);
        }

        public static void moveSouth(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToSouth, newPlayer);
        }

        public static void moveWest(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToWest, newPlayer);
        }

        public static void moveNortheast(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToNortheast, newPlayer);
        }

        public static void moveNorthwest(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToNorthwest, newPlayer);
        }

        public static void moveSoutheast(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToSoutheast, newPlayer);
        }

        public static void moveSouthwest(Player newPlayer)
        {
            MoveTo(newPlayer.currentLocation.roomToSouthwest, newPlayer);
        }
    }
}
