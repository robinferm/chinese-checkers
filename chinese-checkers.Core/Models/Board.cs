using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models
{
    class Board
    {
        public CanvasBitmap Image { get; private set; }
        public List<Location> Locations { get; private set; }
        public List<Piece> Pieces { get; private set; }

        public Board(CanvasBitmap image, List<int[]> locations, int numberOfPlayers)
        {
            this.Image = image;
            this.Locations = new List<Location>();
            PopulateLocations(locations);
        }

        public void PopulateLocations(List<int[]> locations)
        {
            locations.ForEach(x => Locations.Add(new Location(x)));
        }
    }
}
