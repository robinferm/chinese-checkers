using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Board
    {
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
        /// <summary>
        /// It sets amount and location of pieces based on number of Players
        /// </summary>
        /// <param name="players"></param>
        private void PopulateLocations(List<Player> players)
        {
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
        /// <summary>
        /// It finds and returns all the available moves for a piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns>availableMoves</returns>
        public List<Location> GetAvailableMoves(Piece piece)
        {
            if (piece.Buffs.Contains(Item.FreezeSelf))
            {
                return new List<Location>();
            }
            var availableMoves = CalculateAvailableMoves(piece.Point);
            availableMoves.RemoveAt(0);
            return availableMoves;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="availableMoves"></param>
        /// <param name="hasJumped"></param>
        /// <returns>availableMoves</returns>
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
        /// <summary>
        /// It moves a piece.
        /// </summary>
        /// <param name="L"></param>
        /// <param name="selectedPiece"></param>
        public void MovePiece(Location L, Piece selectedPiece)
        {
            this.Locations.Find(Loc => selectedPiece.Id == Loc.PieceId).PieceId = null;
            selectedPiece.Point = L.Point;
            L.PieceId = selectedPiece.Id;
        }


        /// <summary>
        ///  /// This method finds all the neutral positions on the board and 
        /// returns a random position of them.
        /// 
        /// <example>
        /// For example:
        /// <code>
        ///     Random rndNeutralPoint = new Random();
        ///     int rndPoint = rndNeutralPoint.Next(neutralPoints.Count);
        ///     return neutralPoints[rndPoint]; 
        /// </code>
        /// results in <c> neutralPoints[rndPoint] </c> is a random position from the neutral positions of the board.
        /// </summary>
        /// <returns>a random position</returns>
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
            int rndPoint = rndNeutralPoint.Next(neutralPoints.Count);
            // Randomized position of neutral positions
            return neutralPoints[rndPoint]; 
        }

        /// <summary>
        /// This method takes a random point from the GetRandomNeutralPosition() method and 
        /// checks if the point is free.
        /// A free point is a location on the board which has not any piece, Buff or Debuff on it.
        /// 
        /// results in <c>rndPosition</c>'s having a free randomized position on the board.
        /// 
        /// </summary>
        public Point GetRandomFreeNeutralPosition()
        {
            var rndPosition = GetRandomNeutralPosition();
            while (!Locations.Find(x=>x.Point==rndPosition).IsFree())
            {
                rndPosition = GetRandomNeutralPosition();
            }
            return rndPosition;
        }

        /// <summary>
        /// This method make a list of six randomized free positions which are found by the help of the GetRandomNeutralPosition()
        /// method. The method sets six randomized item to six randomized free neutral positions of the board.
        /// Items are:  DoubleDamage, HalfDamage, Heal, TakeDamage, FreezeSelf and FreezeOther
        /// 
        /// The result of the method is six randomized positions on the board where each position has a 
        /// random Item
        /// 
        /// 
        /// </summary>
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

        public List<LinkedList<Point>> GetPaths(Point start, List<Location> endLocations)
        {
            List<LinkedList<Point>> paths = new List<LinkedList<Point>>();
            endLocations.ForEach(x => paths.Add(CalculatePath(start, x.Point)));
            return paths;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="path"></param>
        /// <param name="hasJumped"></param>
        /// <returns>path</returns>
        public LinkedList<Point> CalculatePath(Point start, Point end, LinkedList<Point> path = null, bool hasJumped = false)
        {
            if (path == null)
            {
                path = new LinkedList<Point>();
                path.AddLast(start);
            }

            List<(int, int)> directions = new List<(int, int)>()
            {
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1),
                (1, -1),
                (-1, 1)
            };

            foreach (var D in directions)
            {
                var targetLocation = new Point(start.X + D.Item1, start.Y + D.Item2);

                if (Locations.Any(x => x.Point == targetLocation)) // If a location exists in the specified direction
                {

                    bool targetHasPiece = Locations?.Find(x => x.Point == targetLocation).PieceId != null; // If the target location has a piece

                    // One Step without jumping over a piece
                    if (targetLocation == end && !hasJumped)
                    {
                        path.AddLast(targetLocation);
                        return path;
                    }

                    // Will try to jump
                    else if (targetHasPiece)
                    {
                        var nextTargetLocation = new Point(start.X + D.Item1 * 2, start.Y + D.Item2 * 2);

                        if (Locations.Any(x => x.Point == nextTargetLocation) && !path.Contains(nextTargetLocation))
                        {
                            bool nextTargetHasPiece = Locations?.Find(x => x.Point == nextTargetLocation).PieceId != null;
                            if (nextTargetLocation == end)
                            {
                                path.AddLast(targetLocation);
                                path.AddLast(nextTargetLocation);
                                return path;
                            }

                            //else if (!hasPiece)
                            //{
                            if (!nextTargetHasPiece)
                            {
                                path.AddLast(targetLocation);
                                path.AddLast(nextTargetLocation);
                                
                                path = CalculatePath(nextTargetLocation, end, path, true);
                                
                                if (path.Last.Value == end)
                                {
                                    return path;
                                }
                                else
                                {
                                    path.RemoveLast();
                                    path.RemoveLast();
                                }
                            }
                        }
                    }
                }
            }
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece"></param>
        public void RespawnPiece(Piece piece)
        {
            var freeHomeLocations = Locations.Where(x => x.NestColor == piece.NestColor && x.PieceId == null).ToList();
            if (freeHomeLocations.Count > 0)
            {
                Random rnd = new Random();
                MovePiece(freeHomeLocations[rnd.Next(freeHomeLocations.Count)], Pieces[piece.Id]);
                Pieces[piece.Id].Reset();
            }
            else
            {
                // If all home locations are occupied
            }
        }
    }
}
 