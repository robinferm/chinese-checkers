using chinese_checkers.Core.Models;
using chinese_checkers.Core.Models.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            //this.DataContext = Parameters;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (Parameters.NumberOfAI == 0 || Parameters.PlayerCharacter == null)
            {
                return;
            }
            this.Frame.Navigate(typeof(MainPage), this.Parameters);
        }

        private void mageButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Mage();
        }

        private void druidButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Druid();
        }

        private void warriorButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Warrior();
        }

        private void hunterButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Hunter();
        }

        private void priestButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Priest();
        }

        private void warlockButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.PlayerCharacter = new Warlock();
        }

        private void aiButton1_Click(object sender, RoutedEventArgs e)
        {
            Parameters.NumberOfAI = 1;
        }

        private void aiButton2_Click(object sender, RoutedEventArgs e)
        {
            Parameters.NumberOfAI = 2;
        }

        private void aiButton3_Click(object sender, RoutedEventArgs e)
        {
            Parameters.NumberOfAI = 3;
        }

        private void aiButton4_Click(object sender, RoutedEventArgs e)
        {
            Parameters.NumberOfAI = 4;
        }

        private void aiButton5_Click(object sender, RoutedEventArgs e)
        {
            Parameters.NumberOfAI = 5;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }


        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var container = sender as ComboBox;
        //    var selected = container.SelectedItem as ComboBoxItem;

        //    if (selected != null) {
        //        var data = selected.Content;

        //        if (data != null)
        //        {
        //            Parameters.NumberOfAI = int.Parse(data.ToString());
        //            Debug.WriteLine(data);
        //        }
        //    }
        //}
    }

    public class GameParams
    {
        public ICharacter PlayerCharacter { get; set; }
        public int NumberOfAI { get; set; }
    }
}
