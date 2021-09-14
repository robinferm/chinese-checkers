using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Board
    {
        public List<Location> Locations { get; private set; }
        public List<Piece> Pieces { get; private set; }

        public Board(int[,] locations, List<Player> players)
        {
            this.Locations = new List<Location>();
            //PopulateLocations(locations);
        }

        //public void PopulateLocations(int[,] locations)
        //{

        //    for (int i = 0; i < locations.GetLength(0); i++)
        //    {
        //        Locations.Add(new Location(locations));
        //    }
        //}
    }
}
