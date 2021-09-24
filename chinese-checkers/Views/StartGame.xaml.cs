using chinese_checkers.Core.Models;
using chinese_checkers.Core.Models.Characters;
using chinese_checkers.Views.Menu.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace chinese_checkers.Views
{
    public sealed partial class StartGame : Page
    {
        public GameParams Parameters { get; set; }
        public StartGame()
        {
            this.InitializeComponent();
            this.Parameters = new GameParams();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (Parameters.NumberOfAI == 0 || Parameters.PlayerCharacter == null)
            {
                return;
            }
            this.Frame.Navigate(typeof(MainPage), this.Parameters);
        }

        private void characterButton_Click(object sender, RoutedEventArgs e)
        {
            var name = ((RadioButton)e.OriginalSource).Name.ToString().Split("Button")[0];

            switch (name)
            {
                case "mage":
                    Parameters.PlayerCharacter = new Mage();
                    break;
                case "warrior":
                    Parameters.PlayerCharacter = new Warrior();
                    break;
                case "warlock":
                    Parameters.PlayerCharacter = new Warlock();
                    break;
                case "priest":
                    Parameters.PlayerCharacter = new Priest();
                    break;
                case "druid":
                    Parameters.PlayerCharacter = new Druid();
                    break;
                case "hunter":
                    Parameters.PlayerCharacter = new Hunter();
                    break;

            }
        }

        private void aiButton_Click(object sender, RoutedEventArgs e)
        {
            var content = ((RadioButton)e.OriginalSource).Content.ToString();
            Parameters.NumberOfAI = int.Parse(content);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //RadioButton rb = sender as RadioButton;
            //var test = new CompositeTransform();
            //test.ScaleY = 1.1;
            //rb.RenderTransform = test;
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //RadioButton rb = sender as RadioButton;
            //var test = new CompositeTransform();
            //test.ScaleY = 1;
            //rb.RenderTransform = test;
        }

        private async void characterInfoButton_Click(object sender, RoutedEventArgs e)
        {

            MageInfoDialog dialog = new MageInfoDialog();
            await dialog.ShowAsync();
        }
    }

    public class GameParams
    {
        public ICharacter PlayerCharacter { get; set; }
        public int NumberOfAI { get; set; }
    }
}
