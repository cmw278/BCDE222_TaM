using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;

namespace TheseusAndTheMinotaur
{
    public class LevelViewModel
    {
        private readonly Game _Game;
        private List<AbstractImmutableLevelTile> _Floor;
        private List<AbstractImmutableLevelTile> _Walls;
        private TheseusLevelTile _TheseusImage;
        private MinotaurLevelTile _MinotaurImage;

        #region Public properties
        public int LevelHeight => _Game.LevelHeight;

        public int LevelWidth => _Game.LevelWidth;

        public ImmutableList<AbstractImmutableLevelTile> Floor => _Floor.ToImmutableList();

        public ImmutableList<AbstractImmutableLevelTile> Walls => _Walls.ToImmutableList();

        public ImmutableList<AbstractMutableMazeImage> Characters => new AbstractMutableMazeImage[] { _TheseusImage, _MinotaurImage }.ToImmutableList();

        #endregion

        public LevelViewModel(Game game)
        {
            _Game = game;
            Reset();
        }

        #region Initialization
        public void Reset()
        {
            _Floor = new List<AbstractImmutableLevelTile>();
            _Walls = new List<AbstractImmutableLevelTile>();
            _TheseusImage = new TheseusLevelTile();
            _MinotaurImage = new MinotaurLevelTile();

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
            var floorItem = square.Exit ? (AbstractImmutableLevelTile)new ExitLevelTile(row, column) : new EmptyLevelTile(row, column);
            _Floor.Add(floorItem);
        }

        private void AddLevelWalls(Square square, int row, int column)
        {
            if (square.Bottom) _Walls.Add(new BottomWallLevelTile(row, column));
            if (square.Left) _Walls.Add(new LeftWallLevelTile(row, column));
            if (square.Right) _Walls.Add(new RightWallLevelTile(row, column));
            if (square.Top) _Walls.Add(new TopWallLevelTile(row, column));
        }
        #endregion

        public void Update()
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
