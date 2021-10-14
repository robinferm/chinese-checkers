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

        /// <summary>
        /// This return what index from a "gif array" to display, displays each frame in the gif for 5 frames
        /// <example>
        ///   <code>
        ///     GifHelper.RunGif(characterAbilityAnimations[gs.CurrentlyPlaying.Character.GetType().Name].Count());
        ///   </code>
        ///   <c>characterAbilityAnimations[gs.CurrentlyPlaying.Character.GetType().Name].Count()</c> is the amount of frames in that gif.
        /// </example>
        /// </summary>
        public static void RunGif(int time)
        {
            GifCounter++;
            if (GifCounter >= time * 5)
            {
                GifCounter -= time * 5;
            }
        }

        /// <summary>
        /// Uses local variable <c>GifCounter</c> to determine which frame from the collection inputed to use
        /// </summary>
        /// <returns>Returns <c>CanvasBitmap</c> from an array of <c>CanvasBitmap</c></returns>
        public static CanvasBitmap Ability(Dictionary<string, CanvasBitmap[]> abilityAnimations, Player player)
        {
            Debug.WriteLine(GifCounter);
            return abilityAnimations[player.Character.GetType().Name][GifCounter / 5];
        }

    }
}
