using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public GameSession(List<Location> locations, int numberOfAI, ICharacter playerCharacter)
        {
            this.Players = new List<Player>();
            this.Players.Add(new Player(0, playerCharacter, NestColor.Green));
            //for (int i = 1; i < numberOfAI + 1; i++)
            //{
            //    this.Players.Add(new Player(i, (NestColor)i));
            //}

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

            this.Board = new Board(locations, this.Players);
        }

        public void CheckForWin()
        {

        }
    }
}
