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
        private static readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private static MediaSource pieceSound = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/PieceMove.wav", UriKind.RelativeOrAbsolute));
        
        public async static void Play()
        {
            using (mediaPlayer)
            {
                mediaPlayer.Source = pieceSound;
                await Task.Run(mediaPlayer.Play);
            }
        }
    }
}
