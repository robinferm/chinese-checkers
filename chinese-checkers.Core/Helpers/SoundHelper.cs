using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace chinese_checkers.Core.Helpers
{
    public static class SoundHelper
    {
        public static readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private static MediaSource pieceSound = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/PieceMove.wav", UriKind.RelativeOrAbsolute));

        public static double Volume { get; set; } = 0.5;
        public async static void Play()
        {
            mediaPlayer.Source = pieceSound;
            mediaPlayer.Volume = Volume;
            await Task.Run(mediaPlayer.Play);
        }
    }
}
