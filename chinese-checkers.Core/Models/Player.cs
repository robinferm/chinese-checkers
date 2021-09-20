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

        // Player
        public Player(int id, ICharacter character, NestColor nestColor)
        {
            this.Id = id;
            this.Character = character;
            this.NestColor = nestColor;
        }

        // AI
        public Player(int id, NestColor nestColor)
        {
            this.Id = id;
            this.IsAI = true;
            this.NestColor = nestColor;
            this.Character = GetRandomCharacter();
        }

        private ICharacter GetRandomCharacter()
        {
            Random rnd = new Random();
            switch (rnd.Next(5))
            {
                case 0:
                    return new Mage();
                case 1:
                    return new Mage();
                case 2:
                    return new Mage();
                case 3:
                    return new Mage();
                case 4:
                    return new Mage();
                case 5:
                    return new Mage();
                default:
                    return null;
            }
        }
    }
}
