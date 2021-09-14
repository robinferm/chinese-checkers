using chinese_checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public Point Point { get; set; }
        public NestColor ColorId { get; set; }
        public List<Item> Items { get; set; } 

        public void Move()
        {

        }
    }
}
