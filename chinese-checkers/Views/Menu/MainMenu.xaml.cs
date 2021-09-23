using chinese_checkers.Views.Menu.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsDialog dialog = new OptionsDialog();
            await dialog.ShowAsync();
        }

        private async void helpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpDialog dialog = new HelpDialog();
            await dialog.ShowAsync();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
