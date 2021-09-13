using chinese_checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Models
{
    class Location
    {
        public Vector2 Vector2 { get; set; }
        public int? PieceId { get; set; }
        public NestColor? ColorId { get; set; }
        public Item? ItemId { get; set; }

        public bool IsFree()
        {
            return PieceId is null && ItemId is null;
        }
    }
}
