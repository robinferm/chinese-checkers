using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace chinese_checkers.Core.Models
{
    public class Mage : ICharacter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Location> UsableLocations(Board board, Player currentlyPlaying)
        {
            List<Piece> enemyPieces = board.Pieces.Where(x => x.NestColor != currentlyPlaying.NestColor).ToList();
            List<Location> enemyPieceLocations = board.Locations.Where(x => enemyPieces.Find(z => z.Point == x.Point) != null && x.NestColor == null).ToList();
            return enemyPieceLocations;
        }

        public void UseAbility(Board board, Location location)
        {
            var targetPiece = board.Pieces.Find(x => x.Point == location.Point);
            targetPiece.Health -= 60;
            if (targetPiece.Health < 1)
            {
                board.RespawnPiece(targetPiece);
            }
        }
    }
}
