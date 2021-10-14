using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace chinese_checkers.Helpers
{
    /// <summary>
    /// Provides methods to scale objects
    /// </summary>
    static class ScalingHelper
    {
        public static float ScalingValue { get; private set; } = 64;
        public static float ScaleXY { get; set; }
        public static float ScaleWidth { get; private set; } = 1;
        public static float ScaleHeight { get; private set; } = 1;

        public static int DesginWidth = 1920;
        public static int DesginHeight = 1080;

        public static void SetScale()
        {
            var scaleWidth = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Width / 1920);
            var scaleHeight = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Height / 1080);
            ScaleWidth = scaleWidth;
            ScaleHeight = scaleHeight;
            scaleWidth *= 1.6f;
            scaleHeight -= .1f;
            if (scaleWidth < scaleHeight)
            {
                ScaleXY = scaleWidth;
            }
            else
            {
                ScaleXY = scaleHeight;
            }
            ScalingValue = 64 * ScaleXY;
        }

        public static Transform2DEffect Img(CanvasBitmap source, float multiplier = 1)
        {
            Transform2DEffect image;
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(ScaleXY * multiplier, ScaleXY * multiplier);
            return image;
        }

        public static float Xpos(float x)
        {
            return (float)(x * ScaleWidth);
        }

        public static float Ypos(float y)
        {
            return (float)(y * ScaleHeight);
        }

        /// <summary>
        /// Inputs a logical x and y position to return a graphical x position.
        /// </summary>
        /// <returns>Returns a graphical x position</returns>
        public static float CalculateX(float x, float y)
        {
            float positioning = (x + 4) * ScalingValue + (y - 8) * (ScalingValue / 2);
            float centering = ((DesginWidth * ScaleWidth) - ((8 + 4) * ScalingValue + ((8 - 8) * (ScalingValue / 2))) - ScalingValue) / 2;

           // return positioning + 200 * ScaleXY;
           return positioning + centering;
        }

        /// <summary>
        /// Inputs a logical y position to return a graphical y position.
        /// </summary>
        /// <returns>Returns a graphical y position</returns>
        public static float CalculateY(float y)
        {
            return (y + 4) * ScalingHelper.ScalingValue + 40;
        }

        public static Vector2[] CalculateFramePosition(NestColor color)
        {

            Vector2[] characterPosition = new Vector2[2];
            switch (color)
            {
                case NestColor.Red:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(8, -4) + (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-4));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(8, -4) + (150 * ScalingHelper.ScaleXY) - (40 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-4) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                case NestColor.Black:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(12, 0) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(12, 0) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue - (40 * ScalingHelper.ScaleXY)), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                case NestColor.Blue:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(8, 8) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(8));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(8, 8) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue - (40 * ScalingHelper.ScaleXY)), ScalingHelper.CalculateY(8) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                case NestColor.Green:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(0, 12) - (160 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(12) - (ScalingHelper.ScalingValue / 2));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(0, 12) - (85 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(12) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                case NestColor.White:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(-4, 8) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(8));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(-4, 8) - (75 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(8) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                case NestColor.Yellow:
                    characterPosition[0] = new Vector2(ScalingHelper.CalculateX(0, 0) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2));
                    characterPosition[1] = new Vector2(ScalingHelper.CalculateX(0, 0) - (75 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                    break;
                default:
                    characterPosition[0] = new Vector2();
                    characterPosition[1] = new Vector2();
                    break;
            }
            return characterPosition;
        }

    }
}
