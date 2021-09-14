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

namespace chinese_checkers.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        //public GameSession GameSession { get; } = new GameSession();

        //public static List<int[]> test = new List<int[]>()
        //    {
        //    { 0, 0 },
        //    { 0, 1 }, { 1, 0 },
        //    { 0, 2 }, { 1, 1 }, { 2, 0 },
        //    { 0, 3 }, { 1, 2 }, { 2, 1 }, { 3, 0 },
        //    { -4, 8 }, { -3, 7 }, { -2, 6 }, { -1, 5 }, { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 1 }, { 4, 0 }, { 5, -1 }, { 6, -2 }, { 7, -3 }, { 8, -4 },
        //    { -3, 8 }, { -2, 7 }, { -1, 6 }, { 0, 5 }, { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }, { 5, 0 }, { 6, -1 }, { 7, -2 }, { 8, -3 },
        //    { -2, 8 }, { -1, 7 }, { 0, 6 }, { 1, 5 }, { 2, 4 }, { 3, 3 }, { 4, 2 }, { 5, 1 }, { 6, 0 }, { 7, -1 }, { 8, -2 },
        //    { -1, 8 }, { 0, 7 }, { 1, 6 }, { 2, 5 }, { 3, 4 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 0 }, { 8, -1 },
        //    { 0, 8 }, { 1, 7 }, { 2, 6 }, { 3, 5 }, { 4, 4 }, { 5, 3 }, { 6, 2 }, { 7, 1 }, { 8, 0 },
        //    { 0, 9 }, { 1, 8 }, { 2, 7 }, { 3, 6 }, { 4, 5 }, { 5, 4 }, { 6, 3 }, { 7, 2 }, { 8, 1 }, { 9, 0 },
        //    { 0, 10 }, { 1, 9 }, { 2, 8 }, { 3, 7 }, { 4, 6 }, { 5, 5 }, { 6, 4 }, { 7, 3 }, { 8, 2 }, { 9, 1 }, { 10, 0 },
        //    { 0, 11 }, { 1, 10 }, { 2, 9 }, { 3, 8 }, { 4, 7 }, { 5, 6 }, { 6, 5 }, { 7, 4 }, { 8, 3 }, { 9, 2 }, { 10, 1 }, { 11, 0 },
        //    { 0, 12 }, { 1, 11 }, { 2, 10 }, { 3, 9 }, { 4, 8 }, { 5, 7 }, { 6, 6 }, { 7, 5 }, { 8, 4 }, { 9, 3 }, { 10, 2 }, { 11, 1 }, { 12, 0 },
        //    { 5, 8 }, { 6, 7 }, { 7, 6 }, { 8, 5 },
        //    { 6, 8 }, { 7, 7 }, { 8, 6 },
        //    { 7, 8 }, { 8, 7 },
        //    { 8, 8 }
        //    };
        public static List<Point> test2 = new List<Point>()
        {
            { new Point( 0, 0 )},
            { new Point( 0, 1 )}, { new Point( 1, 0 )},
            { new Point( 0, 2 )}, { new Point( 1, 1 )}, { new Point( 2, 0 )},
            { new Point( 0, 3 )}, { new Point( 1, 2 )}, { new Point( 2, 1 )}, { new Point( 3, 0 )},
            { new Point( -4, 8 )}, { new Point( -3, 7 )}, { new Point( -2, 6 )}, { new Point( -1, 5 )}, { new Point( 0, 4 )}, { new Point( 1, 3 )}, { new Point( 2, 2 )}, { new Point( 3, 1 )}, { new Point( 4, 0 )}, { new Point( 5, -1 )}, { new Point( 6, -2 )}, { new Point( 7, -3 )}, { new Point( 8, -4 )},
            { new Point( -3, 8 )}, { new Point( -2, 7 )}, { new Point( -1, 6 )}, { new Point( 0, 5 )}, { new Point( 1, 4 )}, { new Point( 2, 3 )}, { new Point( 3, 2 )}, { new Point( 4, 1 )}, { new Point( 5, 0 )}, { new Point( 6, -1 )}, { new Point( 7, -2 )}, { new Point( 8, -3 )},
            { new Point( -2, 8 )}, { new Point( -1, 7 )}, { new Point( 0, 6 )}, { new Point( 1, 5 )}, { new Point( 2, 4 )}, { new Point( 3, 3 )}, { new Point( 4, 2 )}, { new Point( 5, 1 )}, { new Point( 6, 0 )}, { new Point( 7, -1 )}, { new Point( 8, -2 )},
            { new Point( -1, 8 )}, { new Point( 0, 7 )}, { new Point( 1, 6 )}, { new Point( 2, 5 )}, { new Point( 3, 4 )}, { new Point( 4, 3 )}, { new Point( 5, 2 )}, { new Point( 6, 1 )}, { new Point( 7, 0 )}, { new Point( 8, -1 )},
            { new Point( 0, 8 )}, { new Point( 1, 7 )}, { new Point( 2, 6 )}, { new Point( 3, 5 )}, { new Point( 4, 4 )}, { new Point( 5, 3 )}, { new Point( 6, 2 )}, { new Point( 7, 1 )}, { new Point( 8, 0 )},
            { new Point( 0, 9 )}, { new Point( 1, 8 )}, { new Point( 2, 7 )}, { new Point( 3, 6 )}, { new Point( 4, 5 )}, { new Point( 5, 4 )}, { new Point( 6, 3 )}, { new Point( 7, 2 )}, { new Point( 8, 1 )}, { new Point( 9, 0 )},
            { new Point( 0, 10 )}, { new Point( 1, 9 )}, { new Point( 2, 8 )}, { new Point( 3, 7 )}, { new Point( 4, 6 )}, { new Point( 5, 5 )}, { new Point( 6, 4 )}, { new Point( 7, 3 )}, { new Point( 8, 2 )}, { new Point( 9, 1 )}, { new Point( 10, 0 )},
            { new Point( 0, 11 )}, { new Point( 1, 10 )}, { new Point( 2, 9 )}, { new Point( 3, 8 )}, { new Point( 4, 7 )}, { new Point( 5, 6 )}, { new Point( 6, 5 )}, { new Point( 7, 4 )}, { new Point( 8, 3 )}, { new Point( 9, 2 )}, { new Point( 10, 1 )}, { new Point( 11, 0 )},
            { new Point( 0, 12 )}, { new Point( 1, 11 )}, { new Point( 2, 10 )}, { new Point( 3, 9 )}, { new Point( 4, 8 )}, { new Point( 5, 7 )}, { new Point( 6, 6 )}, { new Point( 7, 5 )}, { new Point( 8, 4 )}, { new Point( 9, 3 )}, { new Point( 10, 2 )}, { new Point( 11, 1 )}, { new Point( 12, 0 )},
            { new Point( 5, 8 )}, { new Point( 6, 7 )}, { new Point( 7, 6 )}, { new Point( 8, 5 )},
            { new Point( 6, 8 )}, { new Point( 7, 7 )}, { new Point( 8, 6 )},
            { new Point( 7, 8 )}, { new Point( 8, 7 )},
            { new Point( 8, 8 )}
        };

        CanvasBitmap img;
    public MainPage()
        {
            InitializeComponent();

        }
        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            foreach (var item in test2)
            {
                args.DrawingSession.DrawImage(img, (item.X + 4) * 40 + (item.Y * 20), (item.Y + 4) * 40);

            }

            //for (int i = 0; i < test.GetLength(0); i++)
            //{

            //    //args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 50, (test[i, 1] + 4) * 50), 10, Colors.Red);
            //    args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 40 +(test[i,1]*20), (test[i, 1] + 4) * 40),15 , Colors.Red);
            //    args.DrawingSession.DrawLine(new System.Numerics.Vector2(200,0), new System.Numerics.Vector2(200,500), Colors.Red);
            //    //args.DrawingSession.DrawText(test[i, 0].ToString() + ',' + test[i, 1].ToString(), new System.Numerics.Vector2((test[i, 0] + 4) * 40 + (test[i, 1] * 17), (test[i, 1] + 4) * 40), Colors.Red);

            //}
        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());

        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            img = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/StoreLogo.png"));
        }
    }
}
