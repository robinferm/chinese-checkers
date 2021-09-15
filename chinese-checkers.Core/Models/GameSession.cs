using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class GameSession
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; }

        public GameSession(List<Location> locations, int numberOfAI, ICharacter playerCharacter)
        {
            this.Players = new List<Player>();
            this.Players.Add(new Player(0, playerCharacter));

            for (int i = 1; i < numberOfAI + 1; i++)
            {
                this.Players.Add(new Player(i));
            }

            this.Board = new Board(locations, this.Players);
        }
    }
}
