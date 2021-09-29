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
using Windows.UI.Xaml.Navigation;
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
        CanvasBitmap mysteriousPosition;
        Windows.Foundation.Point currentPoint;
        Location mouseover = null;

        // Temp - Get this from main menu
        List<Location> locations = LocationHelper.CreateLocations();
        public ICharacter PlayerCharacter { get; set; }
        public int NumberOfAI { get; set; }

        public MainPage()
        {
            InitializeComponent();
            canvas.IsFixedTimeStep = true;
            ScalingHelper.SetScale();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ScalingHelper.SetScale();
        }

        // This happens when pressing start game from the start game view
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (GameParams)e.Parameter;
            this.NumberOfAI = parameters.NumberOfAI;
            this.PlayerCharacter = parameters.PlayerCharacter;
            gs = new GameSession(locations, NumberOfAI, PlayerCharacter);
        }
        
        private void canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            gs.AnimateMove();
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage, locationImageRed, locationImageGreen, locationImageBlue, locationImageBlack, locationImageWhite, locationImageYellow , mysteriousPosition);
            DrawHelper.DrawPieces(sender, args, gs.Board, pieceImageRed, pieceImageGreen, pieceImageBlack, pieceImageWhite, pieceImageBlue, pieceImageYellow);
            if (gs.CurrentlyPlaying.selectedPiece != null)
            {
                var availableMoves = gs.Board.GetAvailableMoves(gs.CurrentlyPlaying.selectedPiece);
                args.DrawingSession.DrawText(gs.CurrentlyPlaying.selectedPiece.Id.ToString(), 0, 40, Colors.Black);
                DrawHelper.DrawAvailableMoves(sender, args, availableMoves);

            }
            args.DrawingSession.DrawText(((int)currentPoint.X).ToString() + ", " + ((int)currentPoint.Y).ToString(), 0, 0, Colors.Black);

            if (gs.CurrentlyPlaying.Paths != null)
            {
                DrawHelper.DrawPaths(sender, args, gs.CurrentlyPlaying.Paths, mouseover);
            }

            if (gs.AnimatedPiece.X != -5000)
            {
                var color = gs.Board.Pieces.Find(x => x.Point == gs.Path.Last.Value).NestColor;
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
                DrawHelper.DrawAnimationPiece(sender, args, gs.AnimatedPiece, img);
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

           mysteriousPosition = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/mysteriosItem.png"));
        }

        private void canvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var pos = e.GetCurrentPoint(canvas).Position;

            foreach ( var L in locations)
            {
                //var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                //var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                // If click is on a Location
                if (pos.X >= x && pos.X <= x + (64*ScalingHelper.ScaleXY) && pos.Y >= y && pos.Y <= y + (64*ScalingHelper.ScaleXY))
                {
                    // If a piece is selected
                    if (gs.CurrentlyPlaying.selectedPiece != null)
                    {
                        var availableMoves = gs.Board.GetAvailableMoves(gs.CurrentlyPlaying.selectedPiece);
                        // If location is free, then move piece
                        if (availableMoves.Contains(L))
                        {
                            gs.MovePieceWithAnimation(L);
                        }
                        // Otherwise deselect piece
                        else
                        {
                            gs.CurrentlyPlaying.DeSelectPiece();
                            canvas_PointerPressed(sender, e);
                        }
                    }
                    // If a piece is not selected
                    else
                    {
                        // If clicked location has a piece
                        if (L.PieceId != null)
                        {
                            // If piece have same color as the player
                            if (gs.Board.Pieces.Find(P => P.Id == L.PieceId).NestColor == gs.CurrentlyPlaying.NestColor)
                            {
                                if (gs.AnimatedPiece.X == -5000)
                                {
                                    gs.CurrentlyPlaying.SelectPiece(L, gs.Board);
                                    break;
                                }

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
                //var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                //var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                if (currentPoint.X >= x && currentPoint.X <= x + (64*ScalingHelper.ScaleXY) && currentPoint.Y >= y && currentPoint.Y <= y + (64 * ScalingHelper.ScaleXY))
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
