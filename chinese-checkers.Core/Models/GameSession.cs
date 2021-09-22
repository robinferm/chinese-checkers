﻿using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace chinese_checkers.Core.Models
{
    /// <summary>
    /// This is used when a new game is created
    /// </summary>
    public class GameSession
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; }
        public Dictionary<Player, int> PlayerScore { get; set; }
        public Dictionary<NestColor, NestColor> GoalColor { get; set; }
        public Dictionary<NestColor, Point> GoalLocation { get; set; }
        public Player CurrentlyPlaying { get; set; }


        public GameSession(List<Location> locations, int numberOfAI, ICharacter playerCharacter)
        {
            this.Players = new List<Player>();
            this.Players.Add(new Player(0, playerCharacter, NestColor.Green));
            //this.Players.Add(new Player(0, NestColor.Green));
            this.PlayerScore = new Dictionary<Player, int>();
            this.GoalColor = new Dictionary<NestColor, NestColor>()
            {
                { NestColor.Green, NestColor.Red },
                { NestColor.White, NestColor.Black },
                { NestColor.Blue, NestColor.Yellow },
                { NestColor.Red, NestColor.Green },
                { NestColor.Black, NestColor.White },
                { NestColor.Yellow, NestColor.Blue }
            };

            this.GoalLocation = new Dictionary<NestColor, Point>()
            {
                { NestColor.Green, new Point(8, -4) },
                { NestColor.White, new Point(12, 0) },
                { NestColor.Blue, new Point(0, 0) },
                { NestColor.Red, new Point(0, 12) },
                { NestColor.Black, new Point(-4, 8) },
                { NestColor.Yellow, new Point(8, 8) }
            };

            switch (numberOfAI)
            {
                case 1:
                    this.Players.Add(new Player(1, NestColor.Red));
                    break;

                case 2:
                    this.Players.Add(new Player(1, NestColor.Yellow));
                    this.Players.Add(new Player(2, NestColor.Black));
                    break;

                case 3:
                    this.Players.Add(new Player(1, NestColor.White));
                    this.Players.Add(new Player(2, NestColor.Red));
                    this.Players.Add(new Player(3, NestColor.Black));
                    break;
                case 4:
                    this.Players.Add(new Player(1, NestColor.White));
                    this.Players.Add(new Player(2, NestColor.Yellow));
                    this.Players.Add(new Player(3, NestColor.Red));
                    this.Players.Add(new Player(4, NestColor.Black));
                    break;

                case 5:
                    this.Players.Add(new Player(1, NestColor.White));
                    this.Players.Add(new Player(2, NestColor.Yellow));
                    this.Players.Add(new Player(3, NestColor.Red));
                    this.Players.Add(new Player(4, NestColor.Black));
                    this.Players.Add(new Player(5, NestColor.Blue));
                    break;
            }

            this.Players.ForEach(x => this.PlayerScore.Add(x, 0));
            this.Board = new Board(locations, this.Players);
            this.CurrentlyPlaying = Players.First();
        }

        public void CheckForWin()
        {
            // Clears score in dictionary
            this.Players.ForEach(x => this.PlayerScore[x] = 0);
            foreach (var P in Board.Pieces)
            {
                var pieceLocation = Board.Locations.Find(x => x.Point == P.Point);
                var goal = GoalColor[P.NestColor];
                if (pieceLocation.NestColor == goal)
                {
                    var player = Players.Find(x => x.NestColor == P.NestColor);
                    this.PlayerScore[player]++;
                }

            }
            foreach (KeyValuePair<Player, int> kvp in this.PlayerScore)
            {
                if (kvp.Value == 10)
                {
                    kvp.Key.Placement = this.Players.Where(x => x.Placement != null).Count() + 1;
                }
            }
            if (this.Players.Where(x => x.Placement != null).Count() == Players.Count - 1)
            {
                // TODO end game, last player | show results
                
                Debug.WriteLine("Game Ended");
            }
        }

        public void ChangeTurn()
        {
            var nextPlayer = this.Players.FirstOrDefault(x => x.Id == CurrentlyPlaying.Id + 1);
            if (nextPlayer == null)
            {
                nextPlayer = this.Players.First();
            }
            this.CurrentlyPlaying = nextPlayer;

            if (this.CurrentlyPlaying.Placement == null)
            {

                // Debug.WriteLine(nextPlayer.Placement.ToString());
                CheckForWin();
            
                if (this.CurrentlyPlaying.IsAI)
                {
                    MovePieceAI();
                }
            }
            else
            {

                ChangeTurn();
            }
        }

        private void MovePieceAI()
        {
            Thread.Sleep(100);

            // Make move automatically
            var pieces = Board.Pieces.Where(x => x.NestColor == this.CurrentlyPlaying.NestColor);

            Dictionary<Piece, List<Location>> availableMoves = new Dictionary<Piece, List<Location>>();

            // Add all available moves from all pieces to a list
            foreach (var P in pieces)
            {
                var moves = Board.GetAvailableMoves(P);
                if (moves.Count > 0)
                {
                    availableMoves.Add(P, moves);
                }
            }

            //Random rnd = new Random();
            //var randomPieceWithAvailableMove = availableMoves.ElementAt(rnd.Next(0, availableMoves.Count));
            //var piece = randomPieceWithAvailableMove.Key;
            //var targetLocation = randomPieceWithAvailableMove.Value.ElementAt(rnd.Next(0, randomPieceWithAvailableMove.Value.Count));

            var longestMove = GetLongestMove(availableMoves);
            Board.MovePiece(longestMove.Value, longestMove.Key);
            ChangeTurn();
        }

        private KeyValuePair<Piece, Location> GetLongestMove(Dictionary<Piece, List<Location>> availableMoves)
        {
            Random rnd = new Random();
            var rndPiece = availableMoves.Keys.ElementAt(rnd.Next(availableMoves.Count - 1));

            var rndMove = availableMoves[rndPiece].ElementAt(rnd.Next(availableMoves[rndPiece].Count - 1));
            KeyValuePair<Piece, Location> longestMove = new KeyValuePair<Piece, Location>(rndPiece, rndMove);

            double longestDistance = 0;
            double shortestDistanceLeft = 1000;

            foreach (var piece in availableMoves.Keys)
            {
                // Distance between current location and goal location
                var currentDistance = GetDistance(piece.Point, GoalLocation[piece.NestColor]);

                foreach (var am in availableMoves[piece])
                {
                    // Distance between target location and goal location
                    var targetDistance = GetDistance(am.Point, GoalLocation[piece.NestColor]);

                    //If remaining pieces are 7 or more do (this) instead
                    if (this.PlayerScore[this.CurrentlyPlaying] > 6)
                    {
                        //List of all empty locations in opposite nest
                        var emptyGoalLocations = this.Board.Locations.Where(x => x.NestColor == GoalColor[piece.NestColor] && x.PieceId == null);

                        //List of own pieces not in opposite nest
                        var notFinnishedPieces = this.Board.Pieces.Where(x => GoalColor[x.NestColor] != this.Board.Locations.Find(z => z.Point == x.Point).NestColor);

                        //If current piece is in "notFinnishedPieces"
                        if (notFinnishedPieces.Any(x => x == piece))
                        {
                            foreach (var emptyLocation in emptyGoalLocations)
                            {
                                targetDistance = GetDistance(emptyLocation.Point, am.Point);
                                if (targetDistance < shortestDistanceLeft)
                                {
                                    shortestDistanceLeft = targetDistance;
                                    longestMove = new KeyValuePair<Piece, Location>(piece, am);
                                }
                            }
                        }
                    }
                    // If remaning pieces are less than seven do this
                    else
                    {
                        if (currentDistance - targetDistance > longestDistance && (piece.NestColor == am.NestColor || GoalColor[piece.NestColor] == am.NestColor || null == am.NestColor))
                        {
                            longestDistance = currentDistance - targetDistance;
                            longestMove = new KeyValuePair<Piece, Location>(piece, am);
                        }
                    }
                }
            }
            return longestMove;
        }

        private double GetDistance(Point P1, Point P2)
        {
            double xDistance = Math.Abs(P2.X - P1.X);
            double yDistance = Math.Abs(P2.Y - P1.Y);

            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }
    }
}
