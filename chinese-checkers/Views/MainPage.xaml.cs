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

namespace chinese_checkers.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        GameSession gs;
        CanvasBitmap locationImage;

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
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage);
        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());

        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            locationImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/StoreLogo.png"));
        }
    }
}
