using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public interface ICharacter
    {
        string Name { get; set; }
        CanvasBitmap Image { get; set; }

        List<Location> UsableLocations(Board board, Player currentlyPlaying);

        void UseAbility(Board board, Location location = null);
    }
}
