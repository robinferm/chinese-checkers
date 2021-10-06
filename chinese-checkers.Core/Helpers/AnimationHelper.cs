using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace chinese_checkers.Core.Helpers {
    public static class AnimationHelper {

        public static double FrameTime { get; set; } = 24;

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
            double xSpeed = (double)(target - current) / 2;

            if (current != target)
            {
                current += (float)xSpeed;
            }
            return current;
        }

        public static Point MoveFireBall(Point start, Point current, Point target)
        {

            double xSpeed = ((target.X - (start.X)) / (FrameTime * 2));
            double ySpeed = ((target.Y - (start.Y)) / (FrameTime * 2));

            Debug.WriteLine($"x: {xSpeed}, y: {ySpeed}");
            if (current.X != target.X && current.Y != target.Y)
            {
                current.X += (int)xSpeed;
                current.Y += (int)ySpeed;
            }

            return current;
        }
    }
}
