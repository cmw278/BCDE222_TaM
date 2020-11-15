using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaM;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Devices.Sensors;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelPlayer : Page
    {
        private const int TILE_SIZE = 200;
        private const int TILE_MARGIN = 12;

        public event EventHandler<LevelPlayerEventArgs> LevelEventTriggered = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private LevelViewModel Maze { get; set; }

        public int CanvasWidth => GetCanvasDimension(Maze.LevelWidth);
        public int CanvasHeight => GetCanvasDimension(Maze.LevelHeight);

        private int _MoveCount;
        public int MoveCount
        {
            private get => _MoveCount;
            set
            {
                if (value != _MoveCount)
                {
                    _MoveCount = value;
                    ControlBar.MoveCount = MoveCount;
                }
            }
        }

        public LevelPlayer()
        {
            this.InitializeComponent();
        }

        #region Events
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Maze = e.Parameter as LevelViewModel;
            Maze.Reset();
            DrawLevel();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            LevelEventTriggered?.Invoke(this, new LevelPlayerEventArgs(LevelAction.PauseGame));
            base.OnNavigatingFrom(e);
        }

        private void LevelControl_Click(object sender, LevelPlayerEventArgs e)
        {
            LevelEventTriggered?.Invoke(sender, e);
        }
        #endregion

        #region Maze display
        private void DrawLevel()
        {
            DrawFloor((int)Z.Floor);
            DrawWalls((int)Z.Walls);
            DrawCharacters((int)Z.Characters);
        }

        private int GetCanvasDimension(int multiplier) => (multiplier *(TILE_SIZE + TILE_MARGIN)) + TILE_MARGIN;

        private Border GenerateTile(AbstractImmutableMazeImage source, int targetLayer)
        {
            var image = GenerateMazeImage(source);
            var border = new Border()
            {
                Child = image,
                Background = GetBackground(source),
                Width = TILE_SIZE,
                Height = TILE_SIZE,
            };
            SetCanvasPosition(source, border);
            Canvas.SetZIndex(border, targetLayer);
            return border;
        }

        private Image GenerateMazeImage(AbstractImmutableMazeImage source)
        {
            if (source.Image != null) source.Image.DecodePixelHeight = TILE_SIZE;
            var image = new Image()
            {
                Source = source.Image,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = TILE_SIZE - TILE_MARGIN,
            };
            return image;
        }

        private Brush GetBackground(AbstractImmutableMazeImage source)
        {
            switch (source)
            {
                case ExitMazeImage _:
                    return new AcrylicBrush()
                    {
                        FallbackColor = MazeColours.SmokyBlack,
                        TintColor = MazeColours.SmokyBlack,
                        TintOpacity = 0.75,
                    };
                case TopWallMazeImage _:
                case RightWallMazeImage _:
                case BottomWallMazeImage _:
                case LeftWallMazeImage _:
                    return new SolidColorBrush()
                    {
                        Color = MazeColours.SmokyBlack,
                    };
                case MinotaurMazeImage _:
                case TheseusMazeImage _:
                    return null;
                default:
                    return new AcrylicBrush()
                    {
                        FallbackColor = MazeColours.CafeNoir,
                        TintColor = MazeColours.CafeNoir,
                        TintOpacity = 0.75,
                    };

            }
        }

        public void SetCanvasPosition(AbstractImmutableMazeImage source, Border border)
        {
            int verticalOffset = GetCanvasDimension(source.Row);
            int horizontalOffset = GetCanvasDimension(source.Column);
            switch (source)
            {
                case TopWallMazeImage _:
                case LeftWallMazeImage _:
                    horizontalOffset -= TILE_MARGIN;
                    verticalOffset -= TILE_MARGIN;
                    break;
                case RightWallMazeImage _:
                    horizontalOffset += TILE_SIZE;
                    verticalOffset -= TILE_MARGIN;
                    break;
                case BottomWallMazeImage _:
                    horizontalOffset -= TILE_MARGIN;
                    verticalOffset += TILE_SIZE;
                    break;
                default:
                    break;
            }
            Canvas.SetTop(border, verticalOffset);
            Canvas.SetLeft(border, horizontalOffset);
        }

        private void DrawFloor(int targetLayer)
        {
            foreach (var source in Maze.Floor)
            {
                var tile = GenerateTile(source, targetLayer);
                MazeViewCanvas.Children.Add(tile);
            }
        }

        private void DrawWalls(int targetLayer)
        {
            foreach (var source in Maze.Walls)
            {
                var tile = GenerateTile(source, targetLayer);
                switch (source)
                {
                    case TopWallMazeImage _:
                    case BottomWallMazeImage _:
                        tile.Height = TILE_MARGIN;
                        tile.Width += TILE_MARGIN * 2;
                        break;
                    case LeftWallMazeImage _:
                    case RightWallMazeImage _:
                        tile.Height += TILE_MARGIN * 2;
                        tile.Width = TILE_MARGIN;
                        break;
                    default:
                        return;
                }
                MazeViewCanvas.Children.Add(tile);
            }
        }

        private void DrawCharacters(int targetLayer)
        {
            foreach (var source in Maze.Characters)
            {
                var tile = GenerateTile(source, targetLayer);
                new CharacterViewBinding(this, tile, source);
                MazeViewCanvas.Children.Add(tile);
            }
        }

        private enum Z
        {
            Floor = 0,
            Walls = 1,
            Characters = 2,
        }

        private static class MazeColours
        {
            public static readonly Color CafeNoir = Color.FromArgb(0xFF, 0x4C, 0x2E, 0x05);
            public static readonly Color SmokyBlack = Color.FromArgb(0xFF, 0x0D, 0x09, 0x04);
        }
        #endregion
    }
}
