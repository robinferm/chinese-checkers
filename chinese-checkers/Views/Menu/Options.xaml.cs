using chinese_checkers.Core.Helpers;
using chinese_checkers.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace chinese_checkers.Views.Menu
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Options : Page, INotifyPropertyChanged
    {
        private bool _debugEnabled;
        private double _speed;
        private double _volume;
        private bool _isMuted;

        public bool DebugEnabled
        {
            get { return _debugEnabled; }
            set
            {
                _debugEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                _isMuted = value;
                OnPropertyChanged();
            }
        }

        public double Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged();
            }
        }
        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged();
            }
        }
        public Options()
        {
            Speed = AnimationHelper.FrameTime;
            Volume = SoundHelper.Volume * 100;
            IsMuted = SoundHelper.mediaPlayer.IsMuted;
            DebugEnabled = DebugHelper.DebugEnabled;
            InitializeComponent();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                string test = e.Parameter.ToString();
                if (test == "mainmenu")
                {
                    backButton.Content = "Back";
                    menuButton.Visibility = Visibility.Collapsed;
                    exitButton.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void speedSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = (Slider)sender;
            AnimationHelper.FrameTime = slider.Value;
        }

        private void defaultSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            Speed = 24;
        }

        private void muteButton_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.mediaPlayer.IsMuted = !SoundHelper.mediaPlayer.IsMuted;

            if (!IsMuted)
            {
                volumeIcon.Glyph = "\ue74f";
            }
            else
            {
                switch (soundSlider.Value)
                {
                    case double val when val == 0:
                        volumeIcon.Glyph = "\ue74f";
                        break;
                    case double val when val > 0 && val <= 50:
                        volumeIcon.Glyph = "\ue993";
                        break;
                    case double val when val <= 99:
                        volumeIcon.Glyph = "\ue994";
                        break;
                    case double val when val == 100:
                        volumeIcon.Glyph = "\ue995";
                        break;
                }
            }
        }

        private void soundSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = (Slider)sender;
            SoundHelper.Volume = slider.Value / 100;

            if (!IsMuted)
            {
                switch (slider.Value)
                {
                    case double val when val == 0:
                        volumeIcon.Glyph = "\ue74f";
                        break;
                    case double val when val > 0 && val <= 50:
                        volumeIcon.Glyph = "\ue993";
                        break;
                    case double val when val <= 99:
                        volumeIcon.Glyph = "\ue994";
                        break;
                    case double val when val == 100:
                        volumeIcon.Glyph = "\ue995";
                        break;
                }
            }
        }

        private void debugCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            DebugHelper.DebugEnabled = true;
            //Frame contentFrame = Window.Current.Content as Frame;
            //MainPage mp = contentFrame.Content as MainPage;
            //mp.DebugEnabled = true;
        }

        private void debugCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            DebugHelper.DebugEnabled = false;
            //Frame contentFrame = Window.Current.Content as Frame;
            //MainPage mp = contentFrame.Content as MainPage;
            //mp.DebugEnabled = false;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(MainMenu));
        }
    }
}
