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
        public static int SoundDuration { get; set; }
        private static MediaSource pieceSound = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/PieceMove.wav", UriKind.RelativeOrAbsolute));
        private static MediaSource priestSound = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/priest.wav", UriKind.RelativeOrAbsolute));

        public static double Volume { get; set; } = 0.5;
        public static void Play(Sound sound)
        {
            switch (sound)
            {
                case Sound.Piece:
                    mediaPlayer.Source = pieceSound;
                    break;
                case Sound.Priest:
                    mediaPlayer.Source = priestSound;
                    break;
            }
            //if (mediaPlayer.PlaybackSession.NaturalDuration)
            //mediaPlayer.Source = pieceSound;
            mediaPlayer.Volume = Volume;
            mediaPlayer.Play();

            SoundDuration = (int)mediaPlayer.PlaybackSession.NaturalDuration.TotalMilliseconds; // .Duration.Value.TotalMilliseconds;
        }
    }

    public enum Sound
    {
        Piece,
        Priest
    }
}
