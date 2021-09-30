using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public Point Point { get; set; }
        public NestColor NestColor { get; set; }
        public List<Item> Items { get; set; }
        public CanvasBitmap Image { get; set; }

        public bool Hidden { get; private set; }

        public Piece(int id, Point point, NestColor nestColor)
        {
            this.Id = id;
            this.Point = point;
            this.NestColor = nestColor;
            this.Health = 20;
            this.Damage = 1;
            this.Hidden = false;
        }

        public void Move(Piece piece)
        {

        }

        public void ToggleHidden()
        {
            this.Hidden = !this.Hidden;
        }

    }
}
