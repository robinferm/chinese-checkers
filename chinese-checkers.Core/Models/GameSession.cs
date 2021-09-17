using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

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
        public Player CurrentlyPlaying { get; set; }


        public GameSession(List<Location> locations, int numberOfAI, ICharacter playerCharacter)
        {
            this.Players = new List<Player>();
            this.Players.Add(new Player(0, playerCharacter, NestColor.Green));
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
                    PlayerScore[player]++;
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
        }
    }
}
