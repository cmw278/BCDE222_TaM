using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;
using TheseusAndTheMinotaur.ViewModel;

namespace TheseusAndTheMinotaur
{
    public class GameController : AbstractViewModel
    {
        #region Only one game
        private static readonly Game _Game;

        static GameController()
        {
            if (_Game == null)
            {
                _Game = new Game();
                LoadLevels();
            }
        }
        #endregion

        public readonly LevelViewModel Maze;

        public GameController()
        {
            Maze = new LevelViewModel(_Game);
            Maze.Reset();
        }

        #region Public properties
        public bool IsFinished => _Game.HasMinotaurWon || _Game.HasTheseusWon;

        public bool HasTheseusWon => _Game.HasTheseusWon;

        public string LevelName
        {
            get { return _Game.CurrentLevelName; }
            set { _Game.SetLevel(value); }
        }

        public int MoveCount => _Game.MoveCount;

        public List<string> LevelList => _Game.LevelNames();
        #endregion

        #region Movement
        public void Move(Directions direction)
        {
            if (!IsFinished)
            {
                _Game.MoveTheseus(direction);
                _Game.MoveMinotaur();
                _Game.MoveMinotaur();
                UpdateProperties();
            }
            if (IsFinished) NotifyChange("IsFinished");
        }

        private void UpdateProperties()
        {
            NotifyChange("MoveCount");
            Maze.Reset();
        }
        #endregion

        #region Maze stuff (A-maze-ing!)
        
        #endregion

        #region Load levels
        /// <summary>
        /// Load game levels
        /// </summary>
        private static void LoadLevels()
        {
            _Game.AddLevel("level 1", 3, 1, "0000 0001 0002 1011 1010 1110");
            _Game.AddLevel("level 2", 3, 1, "0000 0001 0002 1011 1010 1110");
            _Game.AddLevel("level 3", 3, 1, "0000 0001 0002 1011 1010 1110");
        }
        #endregion
    }
}
