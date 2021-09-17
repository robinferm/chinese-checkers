using chinese_checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public bool IsAI { get; set; }
        public ICharacter Character { get; set; }
        public NestColor NestColor { get; set; }
        public int? Placement { get; set; }

        public Player(int id, ICharacter character, NestColor nestColor)
        {
            this.Id = id;
            this.Character = character;
            this.NestColor = nestColor;
        }

        public Player(int id, NestColor nestColor)
        {
            this.Id = id;
            this.IsAI = true;
            this.NestColor = nestColor;
        }
    }
}
