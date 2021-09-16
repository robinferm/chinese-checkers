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
        public NestColor? NestColorId { get; set; }
        public Item? ItemId { get; set; }

        public Location(int x, int y, NestColor? nestColorId = null, int? pieceId = null, Item? itemId = null)
        {
            this.Point = new Point(x, y);
            this.NestColorId = nestColorId;
            this.PieceId = pieceId;
            this.ItemId = itemId;
        }

        public bool IsFree()
        {
            return PieceId is null && ItemId is null;
        }
    }
}
