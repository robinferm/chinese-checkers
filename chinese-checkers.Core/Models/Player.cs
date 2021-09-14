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
        public NestColor ColorId { get; set; }

        public Player(int id, ICharacter character)
        {
            this.Id = id;
            this.Character = character;
        }

        public Player(int id)
        {
            this.Id = id;
            this.IsAI = true;
        }
    }
}
