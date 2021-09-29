using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Helpers {
    public static class AnimationHelper {

        public static double FrameTime { get; set; } = 60 / 2;
        public static Vector2 MovePiece(Point speed, Vector2 current, Point target)
        {
            double xSpeed = (double)(target.X - speed.X) / FrameTime;
            double ySpeed = (double)(target.Y - speed.Y) / FrameTime;

            if (current.X != target.X || current.Y != target.Y)
            {
                current.X += (float)xSpeed;
                current.Y += (float)ySpeed;
            }
            return current;
        }
    }
}
