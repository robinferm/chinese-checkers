using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models {
    public class Board {
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
            foreach (var L in Locations)
            {
                if (L.NestColor != null)
                {
                    var P = new Piece(Pieces.Count, L.Point, L.NestColor.Value);
                    Pieces.Add(P);
                    L.PieceId = P.Id;
                }
            }
        }

        public List<Location> GetAvailableMoves(Piece piece)
        {
            var availableMoves = CalculateAvailableMoves(piece.Point);
            availableMoves.RemoveAt(0);
            return availableMoves;
        }
        private List<Location> CalculateAvailableMoves(Point point, List<Location> availableMoves = null, bool hasJumped = false)
        {

            if (availableMoves is null)
            {
                availableMoves = new List<Location>();
                availableMoves.Add(Locations.Find(L => L.Point == point));
            }
            Location targetLocation;
            List<(int, int)> moves = new List<(int, int)>()
            {
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1),
                (1, -1),
                (-1, 1)
            };

            foreach (var m in moves)
            {
                targetLocation = Locations.Find(L => L.Point == new Point(point.X + m.Item1, point.Y + m.Item2));
                // If location does not have a piece (can have buff)
                if (targetLocation != null && !availableMoves.Contains(targetLocation)) // If targetlocation is on the board and location is not already in availableMoves
                {
                    if (targetLocation.PieceId is null)
                    {
                        if (!hasJumped)
                        {
                            availableMoves.Add(targetLocation);
                        }
                    }
                    // If location have a piece
                    else
                    {
                        targetLocation = Locations.Find(L => L.Point == new Point(point.X + (m.Item1 * 2), point.Y + (m.Item2 * 2)));
                        if (targetLocation != null && !availableMoves.Contains(targetLocation)) // If targetlocation is on the board and location is not already in availableMoves
                        {
                            if (targetLocation.PieceId is null)
                            {
                                availableMoves.Add(targetLocation);
                                availableMoves = CalculateAvailableMoves(targetLocation.Point, availableMoves, true);
                            }
                        }

                    }
                }

            }
            // x + 1, y



            // x - 1, y

            // x, y + 1

            // x, y - 1

            // x + 1, y - 1

            // x - 1, y + 1


            //availableMove = CalculateAvailableMoves(point);
            //if (Locations.Find(x => x.Point.X == point.X + 1 && x.Point.Y == point.Y))
            //{

            //    AvailableMoves(piece, )
            //}
            return availableMoves;
        }
    }
}
