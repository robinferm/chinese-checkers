using chinese_checkers.Core.Models;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chinese_checkers.Helpers {
    static class GifHelper {

        public static int GifCounter { get; set; } = 0;

        private static int frameTime = 6;

        public static void RunGif(int time)
        {
            GifCounter++;
            if (GifCounter >= time * 5)
            {
                GifCounter -= time * 5;
            }
        }

        public static CanvasBitmap Ability(Dictionary<string, CanvasBitmap[]> abilityAnimations, Player player)
        {
            Debug.WriteLine(GifCounter);
            return abilityAnimations[player.Character.GetType().Name][GifCounter / 5];
        }

    }
}
