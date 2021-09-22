using chinese_checkers.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chinese_checkers.Helpers {
    public static class AnimationHelper {

        public static Vector2 MovePiece(Point speed, Vector2 current, Point target)
        {
            //var x = ((piece.Point.X + 4) * ScalingHelper.ScalingValue + (piece.Point.Y * (ScalingHelper.ScalingValue / 2)));
            //var y = ((piece.Point.Y + 4) * ScalingHelper.ScalingValue);
            double xSpeed = (double)(target.X - speed.X) / (60/2);
            double ySpeed = (double)(target.Y - speed.Y) / (60/2);

            if (current.X != target.X || current.Y != target.Y)
            {
                current.X += (float)xSpeed;
                current.Y += (float)ySpeed;
            }
            return current;
        }
    }
}
