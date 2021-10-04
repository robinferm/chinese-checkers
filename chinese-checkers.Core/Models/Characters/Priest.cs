using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace chinese_checkers.Core.Models.Characters
{
    public class Priest : ICharacter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Location> UsableLocations(Board board, Player currentlyPlaying)
        {
            List<Piece> friendlyPieces = board.Pieces.Where(x => x.NestColor == currentlyPlaying.NestColor).ToList();
            List<Location> friendlyPieceLocations = board.Locations.Where(x => friendlyPieces.Find(z => z.Point == x.Point) != null).ToList();
            return friendlyPieceLocations;
        }

        public void UseAbility(Board board, Location location = null)
        {
            board.Pieces.Find(x => x.Point == location.Point).Heal(60);
        }
    }
}
