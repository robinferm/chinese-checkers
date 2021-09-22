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

        void UseAbility();
    }
}
