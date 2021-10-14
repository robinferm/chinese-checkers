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
        public NestColor? NestColor { get; set; }
        public Item? ItemId { get; set; }

        public Location(int x, int y, NestColor? nestColor = null, int? pieceId = null, Item? itemId = null)
        {
            this.Point = new Point(x, y);
            this.NestColor = nestColor;
            this.PieceId = pieceId;
            this.ItemId = itemId;
        }
        /// <summary>
        /// It checks if id for a piece and for an item is null. 
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsFree()
        {
            return PieceId is null && ItemId is null;
        }
    }
}
