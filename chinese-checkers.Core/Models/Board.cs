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
        public List<Item> Items { get; set; }
        public List<Player> Players { get; set; }
        public Board(List<Location> locations, List<Player> players)
        {
            this.Locations = locations;
            this.Pieces = new List<Piece>();
            this.Players = players;
            PopulateLocations();
        }

        private void PopulateLocations()
        {

            //Set amount and location of pieces based of length of Players
            foreach ( var L in Locations)
            {
                if (L.NestColor != null) 
                {
                    var P = new Piece(Pieces.Count, L.Point, L.NestColor.Value);
                    Pieces.Add(P);
                    L.PieceId = P.Id;
                }
            }
        }
    }
}
