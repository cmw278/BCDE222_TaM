using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;

namespace TheseusAndTheMinotaur.ViewModel
{
    public class LevelViewModel : AbstractViewModel
    {
        private readonly Game _Game;
        private List<AbstractImmutableMazeImage> _Floor;
        private List<AbstractImmutableMazeImage> _Walls;
        private TheseusMazeImage _TheseusImage;
        private MinotaurMazeImage _MinotaurImage;

        public LevelViewModel(Game game)
        {
            _Game = game;
            Reset();
        }

        #region Public properties
        public int LevelHeight => _Game.LevelHeight;

        public int LevelWidth => _Game.LevelWidth;
        
        public ImmutableList<AbstractImmutableMazeImage> Floor => _Floor.ToImmutableList();

        public ImmutableList<AbstractImmutableMazeImage> Walls => _Walls.ToImmutableList();

        public ImmutableList<AbstractMutableMazeImage> Characters => new AbstractMutableMazeImage[] { _TheseusImage, _MinotaurImage }.ToImmutableList();

        #endregion

        #region Initialization
        public void Reset()
        {
            _Floor = new List<AbstractImmutableMazeImage>();
            _Walls = new List<AbstractImmutableMazeImage>();
            _TheseusImage = new TheseusMazeImage();
            _MinotaurImage = new MinotaurMazeImage();

            for (int row = 0; row < _Game.LevelHeight; row++)
            {
                for (int column = 0; column < _Game.LevelWidth; column++)
                {
                    AddLevelItems(row, column);
                }
            }

            Update();
        }

        private void AddLevelItems(int row, int column)
        {
            var square = _Game.WhatIsAt(row, column);
            AddLevelFloor(square, row, column);
            AddLevelWalls(square, row, column);
        }

        private void AddLevelFloor(Square square, int row, int column)
        {
            var floorItem = square.Exit ? (AbstractImmutableMazeImage)new ExitMazeImage(row, column) : new EmptyMazeImage(row, column);
            _Floor.Add(floorItem);
        }

        private void AddLevelWalls(Square square, int row, int column)
        {
            if (square.Bottom) _Walls.Add(new BottomWallMazeImage(row, column));
            if (square.Left) _Walls.Add(new LeftWallMazeImage(row, column));
            if (square.Right) _Walls.Add(new RightWallMazeImage(row, column));
            if (square.Top) _Walls.Add(new TopWallMazeImage(row, column));
        }
        #endregion

        private void Update()
        {
            var theseusLocated = false;
            var minotaurLocated = false;
            for (int row = 0; row < _Game.LevelHeight; row++)
            {
                for (int column = 0; column < _Game.LevelWidth; column++)
                {
                    var square = _Game.WhatIsAt(row, column);
                    if (square.Theseus)
                    {
                        _TheseusImage.MoveTo(row, column);
                        theseusLocated = true;
                    }
                    if (square.Minotaur)
                    {
                        _MinotaurImage.MoveTo(row, column);
                        minotaurLocated = true;
                    }
                    if (theseusLocated && minotaurLocated) return;
                }
            }
        }
    }
}
