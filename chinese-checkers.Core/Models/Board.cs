using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
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

        public Board(List<Location> locations, List<Player> players)
        {
            this.Locations = locations;
            //PopulateLocations(locations);
        }

        public void PopulateLocations(Piece piece)
        {

        }

        public void PopulateLocations(Item item)
        {

        }

    
    }
}
