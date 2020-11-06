using System;
using System.Collections.Generic;
using System.Text;

namespace TaM
{
    public class Game : ILevelHolder
    {
        private readonly List<Level> AllMyLevels;
        private Level CurrentLevel;
        public int LevelCount => AllMyLevels.Count;
        public int LevelHeight
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.Height;
                else return 0;
            }
        }
        public int LevelWidth
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.Width;
                return 0;
            }
        }
        public string CurrentLevelName
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.Name;
                return "No levels loaded";
            }
        }

        public int MoveCount
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.MoveCount;
                return 0;
            }
        }
        public bool HasMinotaurWon
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.IsTheseusDead();
                return false;
            }
        }
        public bool HasTheseusWon
        {
            get
            {
                if (CurrentLevel != null) return CurrentLevel.HasTheseusEscaped();
                return false;
            }
        }

        public Game()
        {
            AllMyLevels = new List<Level>();
        }
        public List<string> LevelNames()
        {
            List<string> levelNames = new List<string>();
            foreach (Level aLevel in AllMyLevels) levelNames.Add(aLevel.Name);
            return levelNames;
        }
        public void AddLevel(string name, int width, int height, string data)
        {
            Level newLevel = new Level(name, width, height, data);
            AllMyLevels.Add(newLevel);
            CurrentLevel = newLevel;
        }
        public void SetLevel(string targetLevelName)
        {
            foreach (Level level in AllMyLevels)
            {
                if (level.Name == targetLevelName)
                {
                    CurrentLevel = level;
                    break;
                }
            }
        }

        public Square WhatIsAt(int row, int column)
        {
            return CurrentLevel.WhatIsAt(row, column);
        }

        public void MoveTheseus(Directions direction)
        {
            if (CurrentLevel != null) CurrentLevel.MoveTheseus(direction);
        }

        public void MoveMinotaur()
        {
            if (CurrentLevel != null) CurrentLevel.MoveMinotaur();
        }
    }
}
