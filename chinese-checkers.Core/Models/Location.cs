using chinese_checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Location
    {
        public Point Point { get; set; }
        public int? PieceId { get; set; }
        public NestColor? ColorId { get; set; }
        public Item? ItemId { get; set; }

        public Location(int[] location)
        {
            this.Point = new Point(location[0], location[1]);
        }

        public bool IsFree()
        {
            return PieceId is null && ItemId is null;
        }
    }
}
