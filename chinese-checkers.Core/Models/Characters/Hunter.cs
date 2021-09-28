using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;

namespace chinese_checkers.Core.Models.Characters
{
    public class Hunter : ICharacter
    {
        public string Name { get; set; } = "Hunter";
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UseAbility()
        {
            throw new NotImplementedException();
        }
    }
}
