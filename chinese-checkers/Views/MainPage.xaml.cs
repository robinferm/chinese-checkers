﻿using System;

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

        // Temp - Get this from main menu
        List<Location> locations = LocationHelper.CreateLocations();
        ICharacter playerCharacter = new Mage();
        int numberOfAI = 1;

        public MainPage()
        {
            InitializeComponent();
            gs = new GameSession(locations, numberOfAI, playerCharacter);
        }
        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage, locationImageRed, locationImageGreen, locationImageBlue, locationImageBlack, locationImageWhite, locationImageYellow);
            DrawHelper.DrawPieces(sender, args, gs.Board);
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
        }
    }
}
