using chinese_checkers.Core.Enums;
using chinese_checkers.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chinese_checkers.Core.Helpers
{
    /// <summary>
    /// Used to perform tasks related to Locations, e.g. to create logical locations on the board.
    /// </summary>
    public static class LocationHelper
    {

        public static List<Location> CreateLocations()
        {
            List<Location> LocationList = new List<Location>()
            {
                { new Location( 0, 0, NestColor.Yellow )},
                { new Location( 0, 1, NestColor.Yellow )}, { new Location( 1, 0, NestColor.Yellow )},
                { new Location( 0, 2, NestColor.Yellow )}, { new Location( 1, 1, NestColor.Yellow )}, { new Location( 2, 0, NestColor.Yellow )},
                { new Location( 0, 3, NestColor.Yellow )}, { new Location( 1, 2, NestColor.Yellow )}, { new Location( 2, 1, NestColor.Yellow )}, { new Location( 3, 0, NestColor.Yellow )},
                { new Location( -4, 8, NestColor.White )}, { new Location( -3, 7, NestColor.White )}, { new Location( -2, 6, NestColor.White )}, { new Location( -1, 5, NestColor.White )}, { new Location( 0, 4 )}, { new Location( 1, 3 )}, { new Location( 2, 2 )}, { new Location( 3, 1 )}, { new Location( 4, 0 )}, { new Location( 5, -1, NestColor.Red )}, { new Location( 6, -2, NestColor.Red )}, { new Location( 7, -3, NestColor.Red )}, { new Location( 8, -4, NestColor.Red)},
                { new Location( -3, 8, NestColor.White )}, { new Location( -2, 7, NestColor.White )}, { new Location( -1, 6, NestColor.White )}, { new Location( 0, 5 )}, { new Location( 1, 4 )}, { new Location( 2, 3 )}, { new Location( 3, 2 )}, { new Location( 4, 1 )}, { new Location( 5, 0 )}, { new Location( 6, -1, NestColor.Red )}, { new Location( 7, -2, NestColor.Red)}, { new Location( 8, -3, NestColor.Red )},
                { new Location( -2, 8, NestColor.White )}, { new Location( -1, 7, NestColor.White )}, { new Location( 0, 6 )}, { new Location( 1, 5 )}, { new Location( 2, 4 )}, { new Location( 3, 3 )}, { new Location( 4, 2 )}, { new Location( 5, 1 )}, { new Location( 6, 0 )}, { new Location( 7, -1, NestColor.Red )}, { new Location( 8, -2, NestColor.Red )},
                { new Location( -1, 8, NestColor.White )}, { new Location( 0, 7 )}, { new Location( 1, 6 )}, { new Location( 2, 5 )}, { new Location( 3, 4 )}, { new Location( 4, 3 )}, { new Location( 5, 2 )}, { new Location( 6, 1 )}, { new Location( 7, 0 )}, { new Location( 8, -1, NestColor.Red )},
                { new Location( 0, 8 )}, { new Location( 1, 7 )}, { new Location( 2, 6 )}, { new Location( 3, 5 )}, { new Location( 4, 4 )}, { new Location( 5, 3 )}, { new Location( 6, 2 )}, { new Location( 7, 1 )}, { new Location( 8, 0 )},
                { new Location( 0, 9, NestColor.Green )}, { new Location( 1, 8 )}, { new Location( 2, 7 )}, { new Location( 3, 6 )}, { new Location( 4, 5 )}, { new Location( 5, 4 )}, { new Location( 6, 3 )}, { new Location( 7, 2 )}, { new Location( 8, 1 )}, { new Location( 9, 0, NestColor.Black )},
                { new Location( 0, 10, NestColor.Green )}, { new Location( 1, 9, NestColor.Green )}, { new Location( 2, 8 )}, { new Location( 3, 7 )}, { new Location( 4, 6 )}, { new Location( 5, 5 )}, { new Location( 6, 4 )}, { new Location( 7, 3 )}, { new Location( 8, 2 )}, { new Location( 9, 1, NestColor.Black )}, { new Location( 10, 0, NestColor.Black )},
                { new Location( 0, 11, NestColor.Green )}, { new Location( 1, 10, NestColor.Green )}, { new Location( 2, 9, NestColor.Green )}, { new Location( 3, 8 )}, { new Location( 4, 7 )}, { new Location( 5, 6 )}, { new Location( 6, 5 )}, { new Location( 7, 4 )}, { new Location( 8, 3 )}, { new Location( 9, 2, NestColor.Black )}, { new Location( 10, 1, NestColor.Black )}, { new Location( 11, 0, NestColor.Black )},
                { new Location( 0, 12, NestColor.Green )}, { new Location( 1, 11, NestColor.Green )}, { new Location( 2, 10, NestColor.Green )}, { new Location( 3, 9, NestColor.Green )}, { new Location( 4, 8 )}, { new Location( 5, 7 )}, { new Location( 6, 6 )}, { new Location( 7, 5 )}, { new Location( 8, 4 )}, { new Location( 9, 3, NestColor.Black )}, { new Location( 10, 2, NestColor.Black )}, { new Location( 11, 1, NestColor.Black )}, { new Location( 12, 0, NestColor.Black )},
                { new Location( 5, 8, NestColor.Blue )}, { new Location( 6, 7, NestColor.Blue )}, { new Location( 7, 6, NestColor.Blue )}, { new Location( 8, 5, NestColor.Blue )},
                { new Location( 6, 8, NestColor.Blue )}, { new Location( 7, 7, NestColor.Blue )}, { new Location( 8, 6, NestColor.Blue )},
                { new Location( 7, 8, NestColor.Blue )}, { new Location( 8, 7, NestColor.Blue )},
                { new Location( 8, 8, NestColor.Blue )}
            };

            return LocationList;
        }
    }
}
