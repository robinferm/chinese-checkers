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
        public async static void Play()
        {
            using (var mediaPlayer = new MediaPlayer())
            {
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/PieceMove.wav", UriKind.RelativeOrAbsolute));
                await Task.Run(mediaPlayer.Play);
            }
        }
    }
}
