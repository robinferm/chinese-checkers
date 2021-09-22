using System;

using chinese_checkers.ViewModels;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using chinese_checkers.Core.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;
using System.Threading.Tasks;
using chinese_checkers.Core.Helpers;
using chinese_checkers.Helpers;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Linq;
using System.Threading;
using chinese_checkers.Core.Enums;

namespace chinese_checkers.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        GameSession gs;
        CanvasBitmap locationImage;
        CanvasBitmap locationImageRed;
        CanvasBitmap locationImageGreen;
        CanvasBitmap locationImageBlue;
        CanvasBitmap locationImageBlack;
        CanvasBitmap locationImageWhite;
        CanvasBitmap locationImageYellow;
        CanvasBitmap pieceImageRed, pieceImageGreen, pieceImageBlack, pieceImageWhite, pieceImageBlue, pieceImageYellow;
        Piece selectedPiece;
        LinkedList<Point> path;
        Windows.Foundation.Point currentPoint;
        Location mouseover = null;

        Vector2 current = new Vector2(-5000, -5000);
        int counter = 0;
        LinkedListNode<Point> selectedNode;


        public List<LinkedList<Point>> Paths { get; set; }

        // Temp - Get this from main menu
        List<Location> locations = LocationHelper.CreateLocations();
        ICharacter playerCharacter = new Mage();
        int numberOfAI = 5;

        public MainPage()
        {
            InitializeComponent();
            canvas.IsFixedTimeStep = true;
            ScalingHelper.SetScale();
            Window.Current.SizeChanged += Current_SizeChanged;
            gs = new GameSession(locations, numberOfAI, playerCharacter);
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ScalingHelper.SetScale();
        }

        private void canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (current != null && path != null)
            {
                if (counter < 30)
                {
                    // Speed, start, end
                    current = AnimationHelper.MovePiece(selectedNode.Value, current, selectedNode.Next.Value);
                    counter++;
                }
                else
                {
                    Thread.Sleep(250);
                    counter = 0;
                    if (selectedNode.Next != path.Last)
                    {
                        selectedNode = selectedNode.Next;
                    }
                    else
                    {
                        current = new Vector2(-5000, -5000);
                        gs.Board.Pieces.Find(x => x.Point == path.Last.Value).ToggleHidden();
                        path = null;
                    }
                }
            }
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage, locationImageRed, locationImageGreen, locationImageBlue, locationImageBlack, locationImageWhite, locationImageYellow);
            DrawHelper.DrawPieces(sender, args, gs.Board, pieceImageRed, pieceImageGreen, pieceImageBlack, pieceImageWhite, pieceImageBlue, pieceImageYellow);
            if (selectedPiece != null)
            {
                var availableMoves = gs.Board.GetAvailableMoves(selectedPiece);
                args.DrawingSession.DrawText(selectedPiece.Id.ToString(), 0, 40, Colors.Black);
                DrawHelper.DrawAvailableMoves(sender, args, availableMoves);

            }
            args.DrawingSession.DrawText(((int)currentPoint.X).ToString() + ", " + ((int)currentPoint.Y).ToString(), 0, 0, Colors.Black);

            if (Paths != null)
            {
                DrawHelper.DrawPaths(sender, args, Paths, mouseover);
            }

            if (current.X != -5000)
            {
                var color = gs.Board.Pieces.Find(x => x.Point == path.Last.Value).NestColor;
                CanvasBitmap img = pieceImageRed;
                switch (color)
                {
                    case NestColor.Red:
                        img = pieceImageRed;
                        break;

                    case NestColor.Blue:
                        img = pieceImageBlue;
                        break;

                    case NestColor.Green:
                        img = pieceImageGreen;
                        break;

                    case NestColor.Yellow:
                        img = pieceImageYellow;
                        break;

                    case NestColor.White:
                        img = pieceImageWhite;
                        break;

                    case NestColor.Black:
                        img = pieceImageBlack;
                        break;




                }
                DrawHelper.DrawAnimationPiece(sender, args, current, img);
            }

        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());

        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            locationImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/default.png"));
            locationImageRed = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/red.png"));
            locationImageGreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/green.png"));
            locationImageBlue = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/blue.png"));
            locationImageBlack = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/black.png"));
            locationImageWhite = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/white.png"));
            locationImageYellow = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/yellow.png"));

            pieceImageRed = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/red.png"));
            pieceImageGreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/green.png"));
            pieceImageBlue = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/blue.png"));
            pieceImageBlack = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/black.png"));
            pieceImageWhite = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/white.png"));
            pieceImageYellow = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/yellow.png"));
        }

        private void canvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //Debug.WriteLine(e.GetCurrentPoint(canvas).Position);
            var pos = e.GetCurrentPoint(canvas).Position;

            foreach ( var L in locations)
            {
                var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                if (pos.X >= x && pos.X <= x + 64 && pos.Y >= y && pos.Y <= y + 64)
                {
                    Debug.WriteLine(L.PieceId);
                    // If a piece is selected
                    if (selectedPiece != null)
                    {
                        var availableMoves = gs.Board.GetAvailableMoves(selectedPiece);
                        // If location is free, then move piece
                        if (availableMoves.Contains(L))
                        {
                            // Removes piece(id) from old location
                            current = new Vector2(selectedPiece.Point.X, selectedPiece.Point.Y);
                            gs.Board.MovePiece(L, selectedPiece);
                            path = Paths.Find(p => p.Last.Value == L.Point);
                            selectedNode = path.First;
                            selectedPiece.ToggleHidden();
                            selectedPiece = null;
                            Paths = null;
                            //gs.CheckForWin();
                            //gs.ChangeTurn();
                        }
                        else
                        {
                            selectedPiece = null;
                            Paths = null;
                            canvas_PointerPressed(sender, e);
                        }
                    }
                    // If a piece is not selected
                    else
                    {
                        // If clicked location has a piece
                        if (L.PieceId != null)
                        {
                            if (gs.Board.Pieces.Find(P => P.Id == L.PieceId).NestColor == gs.CurrentlyPlaying.NestColor)
                            {
                                selectedPiece = gs.Board.Pieces.Find(piece => piece.Id == L.PieceId.Value);
                                Paths = gs.Board.GetPaths(selectedPiece.Point, gs.Board.GetAvailableMoves(selectedPiece));
                                break;

                            }
                        }
                    }
                }
            }
        }

        private void canvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            currentPoint = e.GetCurrentPoint(canvas).Position;

            foreach (var L in locations)
            {
                var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                if (currentPoint.X >= x && currentPoint.X <= x + 64 && currentPoint.Y >= y && currentPoint.Y <= y + 64)
                {
                    mouseover = L;
                    break;
                    //Debug.WriteLine(mouseover.Point);
                }
                else
                {
                    mouseover = null;
                }
            }
        }
    }
}
