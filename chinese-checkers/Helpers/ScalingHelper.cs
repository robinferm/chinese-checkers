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
    static class ScalingHelper
    {
        public static float ScalingValue { get; private set; } = 64;
        public static float ScaleWidth { get; private set; } = 1;
        public static float ScaleHeight { get; private set; } = 1;

        public static int DesginWidth = 1920;
        public static int DesginHeight = 1080;

        public static void SetScale()
        {
            var scaleWidth = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Width / 1920);
            var scaleHeight = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Height / 1080);
            scaleWidth *= 1.4f;
            scaleHeight -= .1f;
            if (scaleWidth < scaleHeight)
            {
               scaleHeight = scaleWidth;
            }
            else
            {
                scaleWidth = scaleHeight;
            }
            ScaleWidth = scaleWidth;
            ScaleHeight = scaleHeight;
            ScalingValue = 64 * scaleWidth;
        }

        public static Transform2DEffect Img(CanvasBitmap source)
        {
            Transform2DEffect image;
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(ScaleWidth, ScaleHeight);
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
        public static float CalculateX(float x, float y)
        {
            return (x + 4) * ScalingValue + ((y - 8) * (ScalingValue / 2)) + (x+4) * 3 + 100;
        }

        public static float CalculateY(float y)
        {
            return (y + 4) * ScalingHelper.ScalingValue;
        }

    }
}
