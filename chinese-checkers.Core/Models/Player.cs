using chinese_checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public List<Location> AvailableMoves { get; set; }
        public Piece selectedPiece { get; set; }
        public List<LinkedList<Point>> Paths { get; set; }

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

        public void SelectPiece (Location L , Board board)
        {
            selectedPiece = board.Pieces.Find(piece => piece.Id == L.PieceId.Value);
            Paths = board.GetPaths(selectedPiece.Point, board.GetAvailableMoves(selectedPiece));
        }

        public void DeSelectPiece()
        {
            selectedPiece = null;
            Paths = null;
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
