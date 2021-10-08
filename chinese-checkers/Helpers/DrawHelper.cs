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

namespace chinese_checkers.Helpers
{
    /// <summary>
    /// Helper function that is used to draw everything on the canvas
    /// </summary>
    public static class DrawHelper
    {
        
        //temp
        public static void DrawBoard(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap locationImage, CanvasBitmap locationImageRed, CanvasBitmap locationImageGreen, CanvasBitmap locationImageBlue, CanvasBitmap locationImageBlack, CanvasBitmap locationImageWhite, CanvasBitmap locationImageYellow, CanvasBitmap mysteriousPosition)
        {
            foreach (var L in board.Locations)
            {
                
                //var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                //var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);

                //if (L.NestColorId != null)
                //{
                //    args.DrawingSession.DrawImage(locationImage, x, y);
                //}

                switch (L.NestColor)
                {
                    //case NestColor.Red:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageRed), x, y);
                    //    break;

                    //case NestColor.Green:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageGreen), x, y);
                    //    break;

                    //case NestColor.Blue:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageBlue), x, y);
                    //    break;

                    //case NestColor.Black:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageBlack), x, y);
                    //    break;

                    //case NestColor.White:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageWhite), x, y);
                    //    break;

                    //case NestColor.Yellow:
                    //    args.DrawingSession.DrawImage(ScalingHelper.Img(locationImageYellow), x, y);
                    //    break;

                    default:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(locationImage), x, y);
                        //args.DrawingSession.DrawText(L.Point.X.ToString() + ", " + L.Point.Y, x, y, Colors.Black);
                        if (L.ItemId != null)
                        {
                            args.DrawingSession.DrawImage(ScalingHelper.Img(mysteriousPosition), x, y);
                        }
                        break;

                }
            }
        }

        public static void DrawPieces(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, CanvasBitmap pieceImageRed, CanvasBitmap pieceImageGreen, CanvasBitmap pieceImageBlack, CanvasBitmap pieceImageWhite, CanvasBitmap pieceImageBlue, CanvasBitmap pieceImageYellow,CanvasBitmap FreezeSelf,CanvasBitmap DoubleDamage,CanvasBitmap HalfDamage,CanvasBitmap Thorns,CanvasBitmap def)
        {
            foreach (var P in board.Pieces)
            {
                if (P.Hidden)
                {
                    continue;
                }
                //var x = ((P.Point.X + 4) * ScalingHelper.ScalingValue + (P.Point.Y * (ScalingHelper.ScalingValue / 2)));
                //var y = ((P.Point.Y + 4) * ScalingHelper.ScalingValue);
                var x = ScalingHelper.CalculateX(P.Point.X, P.Point.Y);
                var y = ScalingHelper.CalculateY(P.Point.Y);
                //args.DrawingSession.DrawImage(P.Image, x+4, y+4);
                //args.DrawingSession.DrawText(P.Id.ToString(), x, y, Colors.Black);
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
                //args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImage), x + 1.5f, y + 1.5f);
                switch(P.Buff)
                {
                    
                    case Item.DoubleDamage:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(DoubleDamage), x, y);
                        break;

                    case Item.HalfDamage:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(HalfDamage), x, y);
                        break;
                    
                    case Item.FreezeSelf:
                        args.DrawingSession.DrawImage(ScalingHelper.Img(FreezeSelf), x, y);

                        break;
                }
                if (P.Thorns)
                    args.DrawingSession.DrawImage(ScalingHelper.Img(Thorns), x, y);

            }
        }

        public static void DrawCharacterAndAbility(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<Player> players, Dictionary<string, CanvasBitmap> frames, Dictionary<string, CanvasBitmap> abilitys,CanvasBitmap highlight)
        {
            foreach (var player in players)
            {
                bool isHighlighted = false;
                
                Vector2 highlightPosition = new Vector2(); 
                Vector2 framePosition = new Vector2();
                Vector2 abilityPosition = new Vector2();
                switch (player.NestColor)
                {
                    case NestColor.Red:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(8, -4) + (90 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-5));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(8, -4) + (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-4));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(8, -4) + (150 * ScalingHelper.ScaleXY) - (40 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-4) + (128 * .4f * ScalingHelper.ScaleXY));
                        break;
                    case NestColor.Black:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(12, 0) + ((120 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(-1) - (ScalingHelper.ScalingValue / 2));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(12, 0) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(12, 0) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue - (40 * ScalingHelper.ScaleXY)), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                            break;
                    case NestColor.Blue:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(8, 8) + ((110 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(7));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(8, 8) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue), ScalingHelper.CalculateY(8));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(8, 8) + ((180 * ScalingHelper.ScaleXY) - ScalingHelper.ScalingValue - (40 * ScalingHelper.ScaleXY)), ScalingHelper.CalculateY(8) + (128 * .4f * ScalingHelper.ScaleXY));
                        break;
                    case NestColor.Green:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(1, 9) - (190 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(11) - (ScalingHelper.ScalingValue / 2));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(0, 12) - (160 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(12) - (ScalingHelper.ScalingValue / 2));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(0, 12) - (85 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(12) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                            break;
                    case NestColor.White:
                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(-5, 8) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(7));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(-4, 8) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(8));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(-4, 8) - (75 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(8) + (128 * .4f * ScalingHelper.ScaleXY));
                            break;
                    case NestColor.Yellow:

                        if (player.Highlight == true)
                        {
                            isHighlighted = true;
                            highlightPosition = new Vector2(ScalingHelper.CalculateX(0, 0) - (210 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(-1) - (ScalingHelper.ScalingValue / 2));
                        }
                            framePosition = new Vector2(ScalingHelper.CalculateX(0, 0) - (150 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2));
                            abilityPosition = new Vector2(ScalingHelper.CalculateX(0, 0) - (75 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(0) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
                            break;
                        default:
                        framePosition = new Vector2();
                        abilityPosition = new Vector2();
                        break;
                }
                if (isHighlighted)
                {
                    args.DrawingSession.DrawImage(ScalingHelper.Img(highlight, .4f), highlightPosition);
                }
                    args.DrawingSession.DrawImage(ScalingHelper.Img(frames[player.Character.GetType().Name], .4f), framePosition);
                    args.DrawingSession.DrawImage(ScalingHelper.Img(abilitys[player.Character.GetType().Name], .5f), abilityPosition);
                
                   
            }
        }

        public static void DrawAvailableMoves(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, List<Location> locations)
        {
            foreach (var L in locations)
            {
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                args.DrawingSession.FillCircle(x + (32 * ScalingHelper.ScaleXY), y + (32 * ScalingHelper.ScaleXY), 32 * ScalingHelper.ScaleXY, Windows.UI.Color.FromArgb((byte)126, (byte)255, (byte)255, (byte)0));
            }
        }

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

        public static void DrawAnimationPiece(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Board board, Vector2 vector, CanvasBitmap pieceImageRed, CanvasBitmap FreezeSelf, CanvasBitmap DoubleDamage, CanvasBitmap HalfDamage, CanvasBitmap Thorns, Piece piece)
        {
            vector.X = ScalingHelper.CalculateX(vector.X, vector.Y);
            vector.Y = ScalingHelper.CalculateY(vector.Y);
            args.DrawingSession.DrawImage(ScalingHelper.Img(pieceImageRed), vector.X, vector.Y);

            switch (piece.Buff)
            {

                case Item.DoubleDamage:
                    args.DrawingSession.DrawImage(ScalingHelper.Img(DoubleDamage), vector.X, vector.Y);
                    break;

                case Item.HalfDamage:
                    args.DrawingSession.DrawImage(ScalingHelper.Img(HalfDamage), vector.X, vector.Y);
                    break;

                case Item.FreezeSelf:
                    args.DrawingSession.DrawImage(ScalingHelper.Img(FreezeSelf), vector.X, vector.Y);

                    break;
            }
            if (piece.Thorns)
                args.DrawingSession.DrawImage(ScalingHelper.Img(Thorns), vector.X, vector.Y);
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
