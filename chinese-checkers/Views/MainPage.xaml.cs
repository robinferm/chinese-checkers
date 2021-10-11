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
using chinese_checkers.Core.Helpers;
using chinese_checkers.Helpers;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using chinese_checkers.Core.Enums;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using chinese_checkers.Views.Menu;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using Windows.UI.ViewManagement;
using chinese_checkers.Views.Menu.Dialogs;

namespace chinese_checkers.Views {
    public sealed partial class MainPage : Page {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        //  public DrawHelper CharacterTurn { get; set; }

        GameSession gs;
        CanvasBitmap highlightCharacter;
        CanvasBitmap locationImage;
        CanvasBitmap locationImageRed;
        CanvasBitmap locationImageGreen;
        CanvasBitmap locationImageBlue;
        CanvasBitmap locationImageBlack;
        CanvasBitmap locationImageWhite;
        CanvasBitmap locationImageYellow;
        CanvasBitmap mysteriousPosition;
        CanvasBitmap pieceImageRed, pieceImageGreen, pieceImageBlack, pieceImageWhite, pieceImageBlue, pieceImageYellow;
        CanvasBitmap cursedOverlay;
        CanvasBitmap freezeSelf, halfDamage, doubleDamage, thorns;

        Dictionary<string, CanvasBitmap[]> characterAbilityAnimations;
        Dictionary<string, CanvasBitmap> characterFrames;
        Dictionary<string, CanvasBitmap> characterAbility;
        Windows.Foundation.Point currentPoint;
        Location mouseover = null;

        int abilityAnimtionCounter = 0;

        public bool IsPaused { get; set; } = false;
        public ICharacter PlayerCharacter { get; set; }
        public int NumberOfAI { get; set; }

        public MainPage()
        {
            characterFrames = new Dictionary<string, CanvasBitmap>();
            characterAbility = new Dictionary<string, CanvasBitmap>();
            characterAbilityAnimations = new Dictionary<string, CanvasBitmap[]>();
            InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            canvas.IsFixedTimeStep = true;
            ScalingHelper.SetScale();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ScalingHelper.SetScale();
        }


        // This happens when pressing start game from the start game view
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (GameParams)e.Parameter;
            this.NumberOfAI = parameters.NumberOfAI;
            this.PlayerCharacter = parameters.PlayerCharacter;
            //if (gs == null)
            if (parameters.CreateNewGame == true)
            {
                CreateGameSession();
                parameters.CreateNewGame = false;
            }
            IsPaused = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            IsPaused = true;
        }

        public void CreateGameSession()
        {

            gs = new GameSession(NumberOfAI, PlayerCharacter);
            gs.CurrentlyPlaying.Highlight = true; // It highlights the first player when a new game starts.

        }

        private void canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            gs.AnimateScoreBoard();
            gs.AnimateAbility();
            //UpdateScore();
            gs.AnimateMove();
            //Debug.WriteLine(AnimationHelper.FrameTime);
            if (gs.AnimatedAbility.X != -5000)
            {
                GifHelper.RunGif(characterAbilityAnimations[gs.CurrentlyPlaying.Character.GetType().Name].Count());
            }
            else
            {
                GifHelper.GifCounter = 0;
            }
        }

        public async void UpdateScore()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                gs.CheckForWin();
            });
        }

        private void optionsButtonGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Options));
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var selPiece = gs.CurrentlyPlaying.selectedPiece;
            if (selPiece != null)
            {
                args.DrawingSession.DrawText(selPiece.Id.ToString(), 0, 40, Colors.Black);
            }
            DrawHelper.DrawBoard(sender, args, gs.Board, locationImage, locationImageRed, locationImageGreen, locationImageBlue, locationImageBlack, locationImageWhite, locationImageYellow, mysteriousPosition);

            DrawHelper.DrawPieces(sender, args, gs.Board, pieceImageRed, pieceImageGreen, pieceImageBlack, pieceImageWhite, pieceImageBlue, pieceImageYellow, freezeSelf, halfDamage, doubleDamage, thorns, cursedOverlay);

            if (gs.AnimatedAbility.X == -5000)
            {
                DrawHelper.DrawAvailableMoves(sender, args, gs.CurrentlyPlaying.AvailableMoves);
            }

            args.DrawingSession.DrawText(((int)currentPoint.X).ToString() + ", " + ((int)currentPoint.Y).ToString(), 0, 0, Colors.Black);

            if (gs.CurrentlyPlaying.Paths != null && DebugHelper.DebugEnabled)
            {
                DrawHelper.DrawPaths(sender, args, gs.CurrentlyPlaying.Paths, mouseover);
            }

            if (gs.AnimatedPiece.X != -5000)
            {

                var color = gs.CurrentlyPlaying.NestColor;
                var selectedPiece = gs.CurrentlyPlaying;

                CanvasBitmap img = pieceImageRed;

                switch (color)
                {
                    case NestColor.Red:
                        img = pieceImageRed;
                        break;

                    case NestColor.Blue:
                        img = pieceImageBlue;
                        break;

                    case NestColor.Green:
                        img = pieceImageGreen;
                        break;

                    case NestColor.Yellow:
                        img = pieceImageYellow;
                        break;

                    case NestColor.White:
                        img = pieceImageWhite;
                        break;

                    case NestColor.Black:
                        img = pieceImageBlack;
                        break;
                }
                DrawHelper.DrawAnimationPiece(sender, args, gs.Board, gs.AnimatedPiece, img, freezeSelf, halfDamage, doubleDamage, thorns, gs.Board.Pieces.Find(x => x.Point == gs.Path.Last.Value));
            }

            DrawHelper.DrawCharacterAndAbility(sender, args, gs.Players, characterFrames, characterAbility, highlightCharacter);
            //DrawHelper.DrawAvailableMoves(sender, args, gs.CurrentlyPlaying.AvailableMoves);

            if (ScalingHelper.DesginWidth * ScalingHelper.ScaleWidth > 1200) // Hide scoreboard if window gets too small
            {
                DrawHelper.DrawScoreBoard(sender, args, gs.ScoreBoard);
            }
            //args.DrawingSession.DrawImage(GifHelper.Ability(characterAbilityAnimations, gs.Players[0]));
            if (gs.CurrentlyPlaying.AbilitySelected && gs.AnimatedAbility.X != -5000)
            {

                DrawHelper.DrawAbility(sender, args, gs.CurrentlyPlaying, GifHelper.Ability(characterAbilityAnimations, gs.Players[0]), gs.AnimatedAbility);
                //Debug.WriteLine((AnimationHelper.AbilityCounter / 5).ToString());
            }

            if (gs.GameEnded)
            {
                sender.Paused = true;
                GameEnded();
            }
        }

        private async void GameEnded()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var lastPlayer = gs.ScoreBoard.ScoreBoardEntries.Find(x => x.Player.Placement == null).Player;
                lastPlayer.Placement = gs.ScoreBoard.ScoreBoardEntries.Count();

                ContentDialog dialog = new GameEndedDialog(gs.ScoreBoard);
                await dialog.ShowAsync();
            });

        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            highlightCharacter = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/highlight.png"));

            locationImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/default.png"));
            locationImageRed = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/red.png"));
            locationImageGreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/green.png"));
            locationImageBlue = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/blue.png"));
            locationImageBlack = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/black.png"));
            locationImageWhite = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/white.png"));
            locationImageYellow = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Locations/yellow.png"));

            pieceImageRed = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/red.png"));
            pieceImageGreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/green.png"));
            pieceImageBlue = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/blue.png"));
            pieceImageBlack = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/black.png"));
            pieceImageWhite = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/white.png"));
            pieceImageYellow = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Pieces/yellow.png"));


            freezeSelf = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/buff/Frozen.png"));
            halfDamage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/buff/HalfDamage.png"));
            doubleDamage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/buff/DoubleDamage.png"));

            mysteriousPosition = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/icon/mysterious.png"));

            characterFrames.Add("Mage", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Mage-Frame.png")));
            characterAbility.Add("Mage", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/fireball-ability.png")));

            characterFrames.Add("Druid", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Druid-Frame.png")));
            characterAbility.Add("Druid", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/druid-ability.png")));

            characterFrames.Add("Hunter", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Hunter-Frame.png")));
            characterAbility.Add("Hunter", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/volly-ability.png")));

            characterFrames.Add("Priest", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Priest-Frame.png")));
            characterAbility.Add("Priest", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/heal-ability.png")));

            characterFrames.Add("Warlock", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Warlock-Frame.png")));
            characterAbility.Add("Warlock", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/curse-ability.png")));

            characterFrames.Add("Warrior", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/CharacterFrame/Warrior-Frame.png")));
            characterAbility.Add("Warrior", await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Abilities/battleshout-ability.png")));

            CanvasBitmap[] fireball = new CanvasBitmap[27];
            for (int i = 0; i < fireball.Length; i++)
            {
                fireball[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/fireball/fireball-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Mage", fireball);

            CanvasBitmap[] heal = new CanvasBitmap[25];
            for (int i = 0; i < heal.Length; i++)
            {
                heal[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/heal/heal-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Priest", heal);

            CanvasBitmap[] curse = new CanvasBitmap[30];
            for (int i = 0; i < curse.Length; i++)
            {
                curse[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/curse/curse-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Warlock", curse);
            cursedOverlay = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/curse/curse-10.png"));

            CanvasBitmap[] battleShout = new CanvasBitmap[100];
            for (int i = 0; i < battleShout.Length; i++)
            {
                battleShout[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/battleshout/battleshout-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Warrior", battleShout);

            CanvasBitmap[] thorns_ani = new CanvasBitmap[34];
            for (int i = 0; i < thorns_ani.Length; i++)
            {
                thorns_ani[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/thorns/thorns-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Druid", thorns_ani);
            thorns = thorns_ani.Last();

            CanvasBitmap[] volley = new CanvasBitmap[26];
            for (int i = 0; i < volley.Length; i++)
            {
                volley[i] = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/gifs/volley/volley-" + i.ToString() + ".png"));
            }
            characterAbilityAnimations.Add("Hunter", volley);



        }

        private void canvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var pos = e.GetCurrentPoint(canvas).Position;

            foreach (var L in gs.locations)
            {
                // Get Locations graphical position
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                // If click is on a Location
                if (pos.X >= x && pos.X <= x + (64 * ScalingHelper.ScaleXY) && pos.Y >= y && pos.Y <= y + (64 * ScalingHelper.ScaleXY))
                {
                    // If ability is seleceted
                    if (gs.CurrentlyPlaying.AbilitySelected)
                    {
                        if (gs.CurrentlyPlaying.AvailableMoves.Contains(L))
                        {
                            gs.UseCharacterAbilityWithAnimation(ScalingHelper.CalculateFramePosition(gs.CurrentlyPlaying.NestColor)[1], new Vector2((int)ScalingHelper.CalculateX(L.Point.X, L.Point.Y), (int)ScalingHelper.CalculateY(L.Point.Y)), L);
                        }
                        else
                        {
                            gs.CurrentlyPlaying.DeSelectAbility();
                        }
                    }
                    // If a piece is selected
                    if (gs.CurrentlyPlaying.selectedPiece != null)
                    {
                        // If location is free, then move piece
                        if (gs.CurrentlyPlaying.AvailableMoves.Contains(L))
                        {
                            gs.MovePieceWithAnimation(L);
                        }
                        // Otherwise deselect piece
                        else
                        {
                            gs.CurrentlyPlaying.DeSelectPiece();

                            canvas_PointerPressed(sender, e);
                        }
                    }
                    // If a piece or ability is not selected
                    else
                    {
                        // If clicked location has a piece
                        if (L.PieceId != null)
                        {
                            // If piece have same color as the player
                            if (gs.Board.Pieces.Find(P => P.Id == L.PieceId).NestColor == gs.CurrentlyPlaying.NestColor)
                            {
                                if (gs.AnimatedPiece.X == -5000)
                                {
                                    gs.CurrentlyPlaying.SelectPiece(L, gs.Board);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Vector2 ownAbility = new Vector2(ScalingHelper.CalculateX(0, 12) - (85 * ScalingHelper.ScaleXY), ScalingHelper.CalculateY(12) - (ScalingHelper.ScalingValue / 2) + (128 * .4f * ScalingHelper.ScaleXY));
            // If click is on players ability
            if (pos.X > ownAbility.X && pos.X < ownAbility.X + (128 * .5f * ScalingHelper.ScaleXY) && pos.Y > ownAbility.Y && pos.Y < ownAbility.Y + (128 * .5f * ScalingHelper.ScaleXY))
            {
                if (!gs.CurrentlyPlaying.IsAI && gs.AnimatedPiece.X == -5000)
                {
                    gs.CurrentlyPlaying.SelectAbility(gs.Board);
                    if (gs.CurrentlyPlaying.Character.GetType().Name == "Hunter")
                    {
                        //TODO hunter ability
                        // gs.UseCharacterAbilityWithAnimation(ScalingHelper.CalculateFramePosition(gs.CurrentlyPlaying.NestColor)[1], new Point((int)ScalingHelper.CalculateX(gs.CurrentlyPlaying.AvailableMoves[0].Point.X, gs.CurrentlyPlaying.AvailableMoves[0].Point.Y)));

                    }
                }
            }
            else
            {
                if (gs.CurrentlyPlaying.AbilitySelected)
                {
                    //gs.CurrentlyPlaying.DeSelectAbility();
                }
            }
        }

        private void canvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            currentPoint = e.GetCurrentPoint(canvas).Position;

            foreach (var L in gs.locations)
            {
                //var x = (L.Point.X + 4) * ScalingHelper.ScalingValue + (L.Point.Y * (ScalingHelper.ScalingValue / 2));
                //var y = (L.Point.Y + 4) * ScalingHelper.ScalingValue;
                var x = ScalingHelper.CalculateX(L.Point.X, L.Point.Y);
                var y = ScalingHelper.CalculateY(L.Point.Y);
                if (currentPoint.X >= x && currentPoint.X <= x + (64 * ScalingHelper.ScaleXY) && currentPoint.Y >= y && currentPoint.Y <= y + (64 * ScalingHelper.ScaleXY))
                {
                    mouseover = L;
                    break;
                }
                else
                {
                    mouseover = null;
                }
            }
        }
    }
}
