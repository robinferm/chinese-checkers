using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Text;

namespace chinese_checkers.Core.Models.Characters
{
    public class Druid : ICharacter
    {
        public string Name { get; set; } = "Druid";
        public CanvasBitmap Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UseAbility()
        {
            throw new NotImplementedException();
        }
    }
}
