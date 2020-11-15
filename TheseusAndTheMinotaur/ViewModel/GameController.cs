using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;

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
                if (_Game.MoveTheseus(direction))
                {
                    _Game.MoveMinotaur();
                    _Game.MoveMinotaur();
                    UpdateProperties();
                }
            }
            if (IsFinished) NotifyChange("IsFinished");
        }

        private void UpdateProperties()
        {
            NotifyChange("MoveCount");
            Maze.Update();
        }
        #endregion

        #region Load levels
        /// <summary>
        /// Load game levels
        /// </summary>
        private static void LoadLevels()
        {
            //                        w, h
            _Game.AddLevel("Level 1", 3, 3, // Author: Robert Abbott
                "0001 " + // Minotaur
                "0201 " + // Theseus
                "0102 " + // Exit
                //    trbl
                "1001 1010 1100 " +
                "0001 1110 0101 " +
                "0011 1010 0110");

            _Game.AddLevel("Level 2",  7, 4, // Author: Robert Abbott
                "0100 " +
                "0102 " +
                "0301 " +
                "1001 1000 1000 1000 1000 1000 1100 " +
                "0101 0111 0001 0000 0000 0000 0100 " +
                "0001 1000 0000 0000 0100 0111 0101 " +
                "0011 0010 0010 0010 0010 1010 0110");

            _Game.AddLevel("Level 3",  3, 4, // Author: Robert Abbott
                "0001 " +
                "0101 " +
                "0102 " +
                "1001 1010 1100 " +
                "0001 1000 0100 " +
                "0111 0001 0100 " +
                "1011 0010 0110");

            _Game.AddLevel("Level 4",  5, 5, // Author: Toby Nelson
                "0104 " +
                "0001 " +
                "0003 " +
                "1101 1001 1000 1010 1100 " +
                "0001 0110 0001 1100 0101 " +
                "0001 1010 0110 0001 0100 " +
                "0001 1000 1100 0111 0101 " +
                "0011 0010 0010 1010 0110");

            _Game.AddLevel("Level 5",  7, 5, // Author: Toby Nelson
                "0302 " +
                "0002 " +
                "0005 " +
                "1001 1000 1000 1100 1001 1110 1101 " +
                "0101 0011 0000 0100 0001 1010 0100 " +
                "0001 1000 0110 0001 0000 1110 0101 " +
                "0001 0100 1011 0110 0111 1001 0100 " +
                "0011 0010 1010 1010 1010 0010 0110");

            _Game.AddLevel("Level 6",  6, 6, // Author: Toby Nelson
                "0204 " +
                "0000 " +
                "0004 " +
                "1011 1000 1000 1000 1100 1101 " +
                "1001 0110 0001 0100 0111 0101 " +
                "0101 1101 0111 0001 1010 0100 " +
                "0101 0001 1010 0100 1001 0100 " +
                "0101 0101 1001 0000 0000 0100 " +
                "0011 0010 0010 0010 0010 0110");

            _Game.AddLevel("Level 7",  6, 6, // Author: Toby Nelson
                "0400 " +
                "0404 " +
                "0001 " +
                "1001 1010 1000 1000 1110 1101 " +
                "0001 1000 0100 0001 1010 0100 " +
                "0001 0000 0100 0001 1100 0101 " +
                "0111 0001 0000 0100 0111 0101 " +
                "1001 0000 0010 0000 1100 0101 " +
                "0011 0010 1010 0010 0010 0110");

            _Game.AddLevel("Level 8",  9, 8, // Author: Toby Nelson
                "0202 " +
                "0000 " +
                "0701 " +
                "1011 1010 1000 1100 1001 1100 1001 1010 1100 " +
                "1001 1100 0111 0101 0101 0101 0001 1110 0101 " +
                "0101 0011 1100 0001 0110 0011 0110 1001 0100 " +
                "0011 1100 0101 0011 1100 1001 1010 0010 0110 " +
                "1101 0101 0011 1000 0110 0011 1010 1010 1100 " +
                "0001 0110 1001 0110 1001 1000 1010 1010 0110 " +
                "0101 1101 0001 1110 0101 0011 1010 1010 1100 " +
                "0111 0011 0010 1010 0010 1110 1011 1010 0110");

            _Game.AddLevel("Level 9",  9, 8, // Author: Robert Abbott
                "0000 " +
                "0008 " +
                "0003 " +
                "1011 1010 1000 1000 1000 1010 1000 1000 1100 " +
                "1001 1000 0000 0000 0000 1000 0000 0000 0100 " +
                "0001 0000 0000 0010 0000 0000 0000 0000 0100 " +
                "0101 0011 0100 1101 0001 0000 0000 0100 0101 " +
                "0101 1001 0000 0000 0000 0000 0000 0110 0101 " +
                "0101 0001 0000 0000 0000 0000 0000 1100 0101 " +
                "0001 0000 0000 0010 0000 0000 0000 0100 0101 " +
                "0011 0010 0010 1010 0010 0010 0010 0010 0110");

            _Game.AddLevel("Level 10", 8, 8, // Author: Toby Nelson
                "0000 " +
                "0101 " +
                "0000 " +
                "1101 1001 1000 1000 1000 1010 1000 1110 " +
                "0001 0000 0000 0000 0000 1000 0100 1101 " +
                "0001 0000 0000 0000 0100 0001 0000 0100 " +
                "0001 0010 0000 0100 0001 0010 0000 0100 " +
                "0001 1000 0100 0001 0000 1000 0000 0100 " +
                "0111 0101 0001 0000 0000 0100 0011 0100 " +
                "1011 0100 0001 0100 0011 0000 1000 0100 " +
                "1011 0010 0010 0010 1110 0011 0010 0110");
        }
        #endregion
    }
}
