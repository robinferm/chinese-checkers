﻿using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models {
    public class Board {
        public List<Location> Locations { get; private set; }
        public List<Piece> Pieces { get; private set; }
        public List<Item> Items { get; set; }
        public Board(List<Location> locations, List<Player> players)
        {
            this.Locations = locations;
            this.Pieces = new List<Piece>();
            PopulateLocations(players);
            Buff_Debuff();
        }
        private void PopulateLocations(List<Player> players)
        {
            //Set amount and location of pieces based of length of Players

            foreach (var L in Locations)
            {
                if (L.NestColor != null && players.Contains(players.Find(x => x.NestColor == L.NestColor)))
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
            return availableMoves;
        }

        public void MovePiece(Location L, Piece selectedPiece)
        {
            this.Locations.Find(Loc => selectedPiece.Id == Loc.PieceId).PieceId = null;
            selectedPiece.Point = L.Point;
            L.PieceId = selectedPiece.Id;
        }

        public Point GetRandomNeutralPosition()
        {
            List<Point> neutralPoints = new List<Point>();
            foreach (var L in Locations)
            {
                if (L.NestColor == null)
                {
                    neutralPoints.Add(L.Point);
                }
            }
            Random rndNeutralPoint = new Random();
            int rndPoint = rndNeutralPoint.Next(neutralPoints.Count + 1);
            return neutralPoints[rndPoint]; // Randomized position of neutral positions
        }
        public Point GetRandomFreeNeutralPosition()
        {
            var rndPosition = GetRandomNeutralPosition();
            while (!Locations.Find(x=>x.Point==rndPosition).IsFree())
            {
                rndPosition = GetRandomNeutralPosition();
            }
            return rndPosition;
        }

        public void Buff_Debuff() 
        {
            List<Point> randomNeutralPoints = new List<Point>();

            for (int i = 0; i < 6; i++)
            {
                randomNeutralPoints.Add(GetRandomNeutralPosition());

                // Random Item 
                Random randomItem = new Random();
                Type type = typeof(Item);
                Array values = type.GetEnumValues();
                int index = randomItem.Next(values.Length);
                Item ramdomItemId = (Item)values.GetValue(index);

                var randomPosition = GetRandomFreeNeutralPosition();
                Locations.Find(x => x.Point == randomPosition).ItemId=ramdomItemId;
            }
        }
    }
}
 