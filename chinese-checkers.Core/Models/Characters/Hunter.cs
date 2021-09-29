using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace chinese_checkers.Core.Models.Characters
{
    public class Hunter : ICharacter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Location> TargetLocations { get; set; }

        public List<Location> UsableLocations(Board board, Player currentlyPlaying)
        {
            List<Location> usableLocations = new List<Location>();
            Location randomLocation = board.Locations.Find(x => x.Point == board.GetRandomNeutralPosition());
            while (usableLocations.Count < 3)
            {
                if (!usableLocations.Contains(randomLocation) && randomLocation != null)
                {
                    if (randomLocation.PieceId != null)
                    {
                        if (board.Pieces.FirstOrDefault(x => x.Id == randomLocation.PieceId).NestColor != currentlyPlaying.NestColor)
                        {
                            usableLocations.Add(randomLocation);
                        }
                    }
                    else
                    {
                        usableLocations.Add(randomLocation);
                    }
                }
                randomLocation = board.Locations.Find(x => x.Point == board.GetRandomNeutralPosition());
            }
            this.TargetLocations = usableLocations;
            return usableLocations;
        }

        public void UseAbility(Board board, Location location = null)
        {
            foreach (var L in TargetLocations)
            {
                if (L.PieceId != null)
                {
                    board.Pieces.Find(x => x.Point == L.Point).Health -= 1;
                }
            }
            TargetLocations = new List<Location>();
        }
    }
}
