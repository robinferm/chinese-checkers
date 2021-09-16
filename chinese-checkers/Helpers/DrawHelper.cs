using chinese_checkers.Core.Enums;
using chinese_checkers.Core.Models;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace chinese_checkers.Helpers
{
    /// <summary>
    /// Helper function that is used to draw everything on the canvas
    /// </summary>
    public static class DrawHelper
    {
        //temp
        static int scalingValue = 40;
        public static void DrawBoard(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap locationImage, CanvasBitmap locationImageRed, CanvasBitmap locationImageGreen, CanvasBitmap locationImageBlue, CanvasBitmap locationImageBlack, CanvasBitmap locationImageWhite, CanvasBitmap locationImageYellow)
        {
            foreach (var L in board.Locations)
            {
                var x = (L.Point.X + 4) * scalingValue + (L.Point.Y * (scalingValue / 2));
                var y = (L.Point.Y + 4) * scalingValue;
                //if (L.NestColorId != null)
                //{
                //    args.DrawingSession.DrawImage(locationImage, x, y);
                //}

                switch (L.NestColor)
                {
                    case NestColor.Red:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageRed), x, y);
                        break;

                    case NestColor.Green:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageGreen), x, y);
                        break;

                    case NestColor.Blue:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageBlue), x, y);
                        break;

                    case NestColor.Black:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageBlack), x, y);
                        break;

                    case NestColor.White:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageWhite), x, y);
                        break;

                    case NestColor.Yellow:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageYellow), x, y);
                        break;

                    default:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImage), x, y);
                        break;

                }
            }
        }

        public static void DrawPieces(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap pieceImage)
        {
            foreach (var P in board.Pieces)
            {
                var x = (P.Point.X + 4) * scalingValue + (P.Point.Y * (scalingValue / 2));
                var y = (P.Point.Y + 4) * scalingValue;
                //args.DrawingSession.DrawImage(P.Image, x+4, y+4);
                args.DrawingSession.DrawText(P.Id.ToString(), x, y, Colors.Black);
                args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImage), x + 1.5f, y + 1.5f);
            }
        }

        public static void DrawAvailableMoves(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<Location> locations)
        {
            foreach (var L in locations)
            {
                var x = (L.Point.X + 4) * scalingValue + (L.Point.Y * (scalingValue / 2));
                var y = (L.Point.Y + 4) * scalingValue;
                args.DrawingSession.FillRectangle(x, y, 16, 16, Colors.Azure);
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
