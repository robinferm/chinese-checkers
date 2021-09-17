using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace chinese_checkers.Helpers
{
    static class ScalingHelper
    {
        public static int ScalingValue { get; } = 40;

        public static float scaleWidth, scaleHeight;

        public static int DesginWidth = 1920;
        public static int DesginHeight = 1080;

        public static void SetScale()
        {

            scaleWidth = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Width / DesginWidth);
            scaleHeight = (float)(ApplicationView.GetForCurrentView().VisibleBounds.Height / DesginHeight);

        }

        public static Transform2DEffect Img(CanvasBitmap source)
        {
            Transform2DEffect image;
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(scaleWidth, scaleHeight);
            return image;
        }

        public static float Xpos(float x)
        {
            return (float)(x * scaleWidth);

        }
        public static float Ypos(float y)
        {
            return (float)(y * scaleHeight);

        }
    }
}
