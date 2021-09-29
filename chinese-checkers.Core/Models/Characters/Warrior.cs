using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;

namespace chinese_checkers.Core.Models.Characters
{
    public class Warrior : ICharacter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Location> UsableLocations(Board board, Player currentlyPlaying)
        {
            throw new NotImplementedException();
        }

        public void UseAbility(Board board, Location location = null)
        {
            throw new NotImplementedException();
        }
    }
}
