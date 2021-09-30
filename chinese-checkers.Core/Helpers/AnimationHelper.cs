using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Helpers {
    public static class AnimationHelper {

        public static double FrameTime { get; set; } = 60 / 10;
      
        public static Vector2 MovePiece(Point start, Vector2 current, Point target)
        {
            double xSpeed = (double)(target.X - start.X) / FrameTime;
            double ySpeed = (double)(target.Y - start.Y) / FrameTime;

            if (current.X != target.X || current.Y != target.Y)
            {
                current.X += (float)xSpeed;
                current.Y += (float)ySpeed;
            }
            return current;
        }

        public static float MoveScoreEntry(float current, float target)
        {
            double xSpeed = (double)(target - current)/2;

            if (current != target)
            {
                current += (float)xSpeed;
            }
            return current;
        }
    }
}
