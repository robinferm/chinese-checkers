using chinese_checkers.Core.Helpers;
using chinese_checkers.Core.Enums;
using chinese_checkers.Core.Models;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace chinese_checkers.Helpers {
    /// <summary>
    /// Helper function that is used to draw everything on the canvas
    /// </summary>
    public static class DrawHelper
    {
        public static CanvasBitmap currentAbilityFrame { get; set; }


        /// <summary>
        /// It draws a board for the game and shows six randomized mysterious positions on the board by a proper icon.
        ///
        ///
        ///<example>
        /// For example:
        ///     <code>
        ///     args.DrawingSession.DrawImage(ScalingHelper.Img(mysteriousPosition), x, y);
        ///     </code>
        ///     <c>mysteriousPosition</c> is the name of an icon to show mysterious positions on the board.
        ///</example>
        ///
        /// 
        /// </summary>

        public static void DrawBoard(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap locationImage, CanvasBitmap locationImageRed, CanvasBitmap locationImageGreen, CanvasBitmap locationImageBlue, CanvasBitmap locationImageBlack, CanvasBitmap locationImageWhite, CanvasBitmap locationImageYellow, CanvasBitmap mysteriousPosition)
        {
            foreach (var L in board.Locations)
            {               
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);

                args.DrawingSession.DrawImage(ScalingHelper.Img(locationImage), x, y);
                if (L.ItemId != null)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(mysteriousPosition), x, y);
                }
            }
        }

        /// <summary>
        /// It places all the images for the pieces on the board.
        ///
        ///
        ///<example>
        /// For example:
        ///     <code>
        ///      case NestColor.Red:
        ///      args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageRed), x, y);
        ///     </code>
        ///     <c>NestColor.Red</c> is an id color for a piece which is red 
        ///</example>
        /// 
        /// </summary>
        public static void DrawPieces(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap pieceImageRed, CanvasBitmap pieceImageGreen, CanvasBitmap pieceImageBlack, CanvasBitmap pieceImageWhite, CanvasBitmap pieceImageBlue, CanvasBitmap pieceImageYellow, CanvasBitmap FreezeSelf, CanvasBitmap DoubleDamage, CanvasBitmap HalfDamage, CanvasBitmap Thorns, CanvasBitmap cursed, CanvasBitmap cracks1, CanvasBitmap cracks2)
        {
            foreach (var P in board.Pieces)
            {
                if (P.Hidden)
                {
                    continue;
                }
                var x = ScalingHelper.CalculateX(P.Point.X, P.Point.Y);
                var y = ScalingHelper.CalculateY(P.Point.Y);
                switch (P.NestColor)
                {
                    case NestColor.Red:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageRed), x, y);
                        break;

                    case NestColor.Green:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageGreen), x, y);
                        break;

                    case NestColor.Blue:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageBlue), x, y);
                        break;

                    case NestColor.Black:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageBlack), x, y);
                        break;

                    case NestColor.White:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageWhite), x, y);
                        break;

                    case NestColor.Yellow:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageYellow), x, y);
                        break;

                    default:
                        break;

                }

                if (P.Health < 30)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(cracks2), x, y);
                }
                else if (P.Health < 70)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(cracks1), x, y);
                }

                if (P.Cursed)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(cursed, .5f), x, y);
                }
                if (P.Thorns)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(Thorns, .5f), x, y);
                }


                if (P.Buffs.Contains(Item.FreezeSelf))
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(FreezeSelf), x, y);
                }

                if (P.Buffs.Contains(Item.DoubleDamage))
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(DoubleDamage), x, y);
                }

                if (P.Buffs.Contains(Item.HalfDamage))
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(HalfDamage), x, y);
                }
            }
        }

        /// <summary>
        /// This method place an image and an ability icon for each player and highlights a player's frame to show that it's turn to play.
        /// 
        ///<example>For example:
        /// <code>
        ///    args.DrawingSession.DrawImage(ScalingHelper.Img(frames[player.Character.GetType().Name], .4f), pos[0]);
        ///    args.DrawingSession.DrawImage(ScalingHelper.Img(abilitys[player.Character.GetType().Name], .5f), pos[1]);
        /// </code>
        /// <c>frames[player.Character.GetType().Name</c> is the name of the image for the (selected)player.
        /// <c>abilitys[player.Character.GetType().Name]</c> is the name of the icon for the (selected)player which shows the ability of the player.
        /// </example>
        /// 
        /// </summary>
        public static void DrawCharacterAndAbility(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<Player> players, Dictionary<string, CanvasBitmap> frames, Dictionary<string, CanvasBitmap> abilitys, CanvasBitmap highlight)
        {
            foreach (var player in players)
            {
                var pos = ScalingHelper.CalculateFramePosition(player.NestColor);

                bool isHighlighted = false;

                Vector2 highlightPosition = new Vector2();

                switch (player.NestColor)
                {
                    case NestColor.Red:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(8, -4) + (90 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-5));
                        }

                        break;
                    case NestColor.Black:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(12, 0) + ((120 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(-1) - (ScalingHelper.ScalingValue / 2));
                        }

                        break;
                    case NestColor.Blue:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(8, 8) + ((110 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(7));
                        }

                        break;
                    case NestColor.Green:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(1, 9) - (190 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(11) - (ScalingHelper.ScalingValue / 2));
                        }

                        break;
                    case NestColor.White:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(-5, 8) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(7));
                        }

                        break;
                    case NestColor.Yellow:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(0, 0) - (210 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-1) - (ScalingHelper.ScalingValue / 2));
                        }
                        break;
                }
              
                if (isHighlighted)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(highlight, .4f), highlightPosition);
                }

                args.DrawingSession.DrawImage(ScalingHelper.Img(frames[player.Character.GetType().Name], .4f), pos[0]);
                args.DrawingSession.DrawImage(ScalingHelper.Img(abilitys[player.Character.GetType().Name], .5f), pos[1]);

            }
        }

        /// <summary>
        ///  This method helps the player to show the available positions to meve by drawing some circles
        ///  
        ///  <c>x</c> Calculated scailing point.
        ///  <c>y</c> Calculated scailing point.
        /// </summary>
        public static void DrawAvailableMoves(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<Location> locations)
        {
            foreach (var L in locations)
            {
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                args.DrawingSession.FillCircle(x + (32 * ScalingHelper.ScaleXY), y + (32 * ScalingHelper.ScaleXY), 32 * ScalingHelper.ScaleXY, Windows.UI.Color.FromArgb((byte)126, (byte)255, (byte)255, (byte)0));
            }
        }

        /// <summary>
        /// This function draws a path to help the player see the availble movement's path on the board.
        /// To have this feature, the player should enable Debug mode from options
        ///
        ///  <code>
        ///  args.DrawingSession.DrawLine(p1, p2, Colors.Black);
        /// </code>
        ///  <c>p1</c> Calculated scailing point(x).
        ///  <c>p2</c> Calculated scailing point(y).
        /// </summary>
        public static void DrawPaths(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<LinkedList<Point>> paths, Location mouseover)
        {
            foreach (var P in paths)
            {
                var currentNode = P.First;
                while (currentNode != null && currentNode != P.Last)
                {
                    Vector2 p1 = new Vector2(ScalingHelper.CalculateX(currentNode.Value.X, currentNode.Value.Y) + (ScalingHelper.ScalingValue / 2), ScalingHelper.CalculateY(currentNode.Value.Y) + (ScalingHelper.ScalingValue / 2));
                    Vector2 p2 = new Vector2(ScalingHelper.CalculateX(currentNode.Next.Value.X, currentNode.Next.Value.Y) + (ScalingHelper.ScalingValue / 2), ScalingHelper.CalculateY(currentNode.Next.Value.Y) + (ScalingHelper.ScalingValue / 2));

                    if (mouseover?.Point != null && currentNode?.Next?.Value != null)
                    {
                        if (mouseover.Point == P.Last.Value)
                        {
                            args.DrawingSession.DrawLine(p1, p2, Colors.Black);
                        }
                    }
                    currentNode = currentNode.Next;
                }
            }
        }

        /// <summary>
        /// It makes animation to show a moving piece on the board.
        /// The piece with animation may have an icon on it to show that the piece has Buff or freezed state.
        /// This function makes posibility for the pieces to animate even if they have an icon.
        ///
        /// </summary>
        public static void DrawAnimationPiece(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, Vector2 vector, CanvasBitmap pieceImageRed, CanvasBitmap FreezeSelf, CanvasBitmap DoubleDamage, CanvasBitmap HalfDamage, CanvasBitmap Thorns, Piece piece, CanvasBitmap cracks1, CanvasBitmap cracks2)
        {
            vector.X = ScalingHelper.CalculateX(vector.X, vector.Y);
            vector.Y = ScalingHelper.CalculateY(vector.Y);
            args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageRed), vector.X, vector.Y);

            if (piece.Health < 30)
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(cracks2), vector.X, vector.Y);
            }
            else if (piece.Health < 70)
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(cracks1), vector.X, vector.Y);
            }
            if (piece.Thorns)
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(Thorns, .5f), vector.X, vector.Y);
            }
            if (piece.Buffs.Contains(Item.FreezeSelf))
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(FreezeSelf), vector.X, vector.Y);
            }

            if (piece.Buffs.Contains(Item.DoubleDamage))
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(DoubleDamage), vector.X, vector.Y);
            }

            if (piece.Buffs.Contains(Item.HalfDamage))
            {
                args.DrawingSession.DrawImage(ScalingHelper.Img(HalfDamage), vector.X, vector.Y);
            }

        }


        public static void DrawScoreBoard(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, ScoreBoard scoreBoard)
        {
            args.DrawingSession.FillRectangle(ScalingHelper.DesginWidth * ScalingHelper.ScaleWidth - 350, ((ScalingHelper.DesginHeight * ScalingHelper.ScaleHeight) / 2) - 25, 350, scoreBoard.ScoreBoardEntries.Last().Position * 30 + 70, Windows.UI.Color.FromArgb((byte)50, (byte)255, (byte)255, (byte)255));
            foreach (var item in scoreBoard.ScoreBoardEntries)
            {
                var color = Colors.White;
                args.DrawingSession.DrawText(item.Player.ToString(), ScalingHelper.DesginWidth * ScalingHelper.ScaleWidth - 320, (ScalingHelper.DesginHeight * ScalingHelper.ScaleHeight) / 2 + 30 * item.Position, color, new CanvasTextFormat() { FontSize = 20, FontFamily = "Bookman Old Style" });
            }
        }

        public static void DrawAbility(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Player player, CanvasBitmap image, Vector2 abilityLocation)
        {
            var pos = ScalingHelper.CalculateFramePosition(player.NestColor)[1];
            //args.DrawingSession.DrawImage(ScalingHelper.Img(image, .3f), pos.X - 20, pos.Y - 20);
            switch (player.Character.GetType().Name)
            {
                case "Mage":
                    args.DrawingSession.DrawImage(ScalingHelper.Img(image, .3f), abilityLocation.X - 30, abilityLocation.Y - 30);
                    break;
                case "Priest":
                    args.DrawingSession.DrawImage(ScalingHelper.Img(image), abilityLocation.X - (64 * ScalingHelper.ScaleXY), abilityLocation.Y - (64 * ScalingHelper.ScaleXY));
                    break;
                case "Warrior":
                    args.DrawingSession.DrawImage(ScalingHelper.Img(image, .75f), abilityLocation.X - (128 * .125f * ScalingHelper.ScaleXY), abilityLocation.Y - (128 * .1f * ScalingHelper.ScaleXY));
                    break;
                default:
                    args.DrawingSession.DrawImage(ScalingHelper.Img(image, .5f), abilityLocation.X, abilityLocation.Y);
                    break;
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
