using chinese_checkers.Core.Models;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chinese_checkers.Helpers
{
    /// <summary>
    /// Helper function that is used to draw everything on the canvas
    /// </summary>
    public static class DrawHelper
    {
        public static void DrawBoard(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap locationImage)
        {
            //temp
            var scalingValue = 40;
            foreach (var L in board.Locations)
            {
                var x = (L.Point.X + 4) * scalingValue + (L.Point.Y * (scalingValue / 2));
                var y = (L.Point.Y + 4) * scalingValue;
                args.DrawingSession.DrawImage(locationImage, x, y);

            }
        }

        // Debug stuff
        //for (int i = 0; i < test.GetLength(0); i++)
        //{

        //    //args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 50, (test[i, 1] + 4) * 50), 10, Colors.Red);
        //    //args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 40 + (test[i, 1] * 20), (test[i, 1] + 4) * 40), 15, Colors.Red);
        //    //args.DrawingSession.DrawLine(new System.Numerics.Vector2(200, 0), new System.Numerics.Vector2(200, 500), Colors.Red);
        //    //args.DrawingSession.DrawText(test[i, 0].ToString() + ',' + test[i, 1].ToString(), new System.Numerics.Vector2((test[i, 0] + 4) * 40 + (test[i, 1]), (test[i, 1] + 4) * 40), Colors.Red);

        //}
    }
}
