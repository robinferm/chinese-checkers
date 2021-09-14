using System;

using chinese_checkers.ViewModels;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace chinese_checkers.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public static int[,] test;
        public MainPage()
        {
            InitializeComponent();
            test = new int[,]
            {
            { 0, 0 },
            { 0, 1 }, { 1, 0 },
            { 0, 2 }, { 1, 1 }, { 2, 0 },
            { 0, 3 }, { 1, 2 }, { 2, 1 }, { 3, 0 },
            { -4, 8 }, { -3, 7 }, { -2, 6 }, { -1, 5 }, { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 1 }, { 4, 0 }, { 5, -1 }, { 6, -2 }, { 7, -3 }, { 8, -4 },
            { -3, 8 }, { -2, 7 }, { -1, 6 }, { 0, 5 }, { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }, { 5, 0 }, { 6, -1 }, { 7, -2 }, { 8, -3 },
            { -2, 8 }, { -1, 7 }, { 0, 6 }, { 1, 5 }, { 2, 4 }, { 3, 3 }, { 4, 2 }, { 5, 1 }, { 6, 0 }, { 7, -1 }, { 8, -2 },
            { -1, 8 }, { 0, 7 }, { 1, 6 }, { 2, 5 }, { 3, 4 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 0 }, { 8, -1 },
            { 0, 8 }, { 1, 7 }, { 2, 6 }, { 3, 5 }, { 4, 4 }, { 5, 3 }, { 6, 2 }, { 7, 1 }, { 8, 0 },
            { 0, 9 }, { 1, 8 }, { 2, 7 }, { 3, 6 }, { 4, 5 }, { 5, 4 }, { 6, 3 }, { 7, 2 }, { 8, 1 }, { 9, 0 },
            { 0, 10 }, { 1, 9 }, { 2, 8 }, { 3, 7 }, { 4, 6 }, { 5, 5 }, { 6, 4 }, { 7, 3 }, { 8, 2 }, { 9, 1 }, { 10, 0 },
            { 0, 11 }, { 1, 10 }, { 2, 9 }, { 3, 8 }, { 4, 7 }, { 5, 6 }, { 6, 5 }, { 7, 4 }, { 8, 3 }, { 9, 2 }, { 10, 1 }, { 11, 0 },
            { 0, 12 }, { 1, 11 }, { 2, 10 }, { 3, 9 }, { 4, 8 }, { 5, 7 }, { 6, 6 }, { 7, 5 }, { 8, 4 }, { 9, 3 }, { 10, 2 }, { 11, 1 }, { 12, 0 },
            { 5, 8 }, { 6, 7 }, { 7, 6 }, { 8, 5 },
            { 6, 8 }, { 7, 7 }, { 8, 6 },
            { 7, 8 }, { 8, 7 },
            { 8, 8 }
            };
        }
        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {

            for (int i = 0; i < test.GetLength(0); i++)
            {

                //args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 50, (test[i, 1] + 4) * 50), 10, Colors.Red);
                args.DrawingSession.DrawCircle(new System.Numerics.Vector2((test[i, 0] + 4) * 40 +(test[i,1]*20), (test[i, 1] + 4) * 40),15 , Colors.Red);
                args.DrawingSession.DrawLine(new System.Numerics.Vector2(200,0), new System.Numerics.Vector2(200,500), Colors.Red);
                //args.DrawingSession.DrawText(test[i, 0].ToString() + ',' + test[i, 1].ToString(), new System.Numerics.Vector2((test[i, 0] + 4) * 40 + (test[i, 1] * 17), (test[i, 1] + 4) * 40), Colors.Red);

            }
        }
    }
}
