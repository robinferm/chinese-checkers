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
        CanvasBitmap pieceImage;
        Piece selectedPiece;
        Windows.Foundation.Point currentPoint;

        // Temp - Get this from main menu
        List<Location> locations = LocationHelper.CreateLocations();
        ICharacter playerCharacter = new Mage();
        int numberOfAI = 2;

        public MainPage()
        {
            InitializeComponent();
            ScalingHelper.SetScale();
            Window.Current.SizeChanged += Current_SizeChanged;
            gs = new GameSession(locations, numberOfAI, playerCharacter);
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ScalingHelper.SetScale();
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage, locationImageRed, locationImageGreen, locationImageBlue, locationImageBlack, locationImageWhite, locationImageYellow);
            DrawHelper.DrawPieces(sender, args, gs.Board, pieceImage);
            if (selectedPiece != null)
            {
                var availableMoves = gs.Board.GetAvailableMoves(selectedPiece);
                args.DrawingSession.DrawText(selectedPiece.Id.ToString(), 0, 40, Colors.Black);
                DrawHelper.DrawAvailableMoves(sender, args, availableMoves);

            }
            args.DrawingSession.DrawText(((int)currentPoint.X).ToString() + ", " + ((int)currentPoint.Y).ToString(), 0, 0, Colors.Black);

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

            pieceImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/button.png"));
        }

        private void canvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //Debug.WriteLine(e.GetCurrentPoint(canvas).Position);
            var pos = e.GetCurrentPoint(canvas).Position;
            int scalingValue = 40;


            //float x = (0 + 4) * scalingValue + (0 * (scalingValue / 2));
            //float y = (0 + 4) * scalingValue;
            //x = ScalingHelper.Xpos(x);
            //y = ScalingHelper.Ypos(y);
            //Debug.WriteLine(x);

            //if (pos.X >= x && pos.X <= x + 16 && pos.Y >= y && pos.Y <= y + 16)
            //{
            //    Debug.WriteLine("Collision");
            //}

            foreach ( var L in locations)
            {
                var x = (L.Point.X + 4) * scalingValue + (L.Point.Y * (scalingValue / 2));
                var y = (L.Point.Y + 4) * scalingValue;
                if (pos.X >= x && pos.X <= x + 16 && pos.Y >= y && pos.Y <= y + 16)
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
                            locations.Find(Loc => selectedPiece.Id == Loc.PieceId).PieceId = null;
                            selectedPiece.Point = L.Point;
                            L.PieceId = selectedPiece.Id;
                            selectedPiece = null;
                            gs.CheckForWin();
                        }
                        else
                        {
                            selectedPiece = null;
                            canvas_PointerPressed(sender, e);
                        }
                    }
                    // If a piece is not selected
                    else
                    {
                        // If clicked location has a piece
                        if (L.PieceId != null)
                        {
                            selectedPiece = gs.Board.Pieces.Find(piece => piece.Id == L.PieceId.Value);

                            // TODO highlight available moves selectedPiece.avalibleMoves(board)
                            break;
                        }

                    }
                }
            }
        }

        private void canvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            currentPoint = e.GetCurrentPoint(canvas).Position;
        }
    }
}
