using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaM;

namespace TaMTests
{
    [TestClass]
    public class GameTests
    {
        Game game;

        public void MakeEmptyGame()
        {
            game = new Game();
        }

        [TestMethod, TestCategory("0Levels")]
        public void EmptyGameHasLevelCountOf0()
        {
            MakeEmptyGame();
            Assert.AreEqual(0, game.LevelCount);
        }

        [TestMethod, TestCategory("0Levels")]
        public void EmptyGameHasHeight0()
        {
            MakeEmptyGame();
            Assert.AreEqual(0, game.LevelHeight);
        }

        [TestMethod, TestCategory("0Levels")]
        public void EmptyGameHasWidth0()
        {
            MakeEmptyGame();
            Assert.AreEqual(0, game.LevelWidth);
        }

        [TestMethod, TestCategory("0Levels")]
        public void EmptyGameHasLevelNameOf_no_levels_loaded()
        {
            MakeEmptyGame();
            string expectedLevelName = "No levels loaded";
            string actualLevelName = game.CurrentLevelName;
            Assert.AreEqual(expectedLevelName, actualLevelName);
        }

        [TestMethod, TestCategory("0Levels")]
        public void EmptyGameHasEmptyNamesList()
        {
            MakeEmptyGame();
            int actualNumberOfNames = game.LevelNames().Count;
            Assert.AreEqual(0, actualNumberOfNames);
        }

        void MakeGameWithOneLevel()
        {
            game = new Game();
            game.AddLevel("level 1", 3, 1, "0000 0001 0002 1011 1010 1110");
        }

        [TestMethod, TestCategory("1level")]
        public void GameWithOneLevelHasLevelCountOf1()
        {
            MakeGameWithOneLevel();
            Assert.AreEqual(1, game.LevelCount);
        }

        [TestMethod, TestCategory("1level")]
        public void GameWithOneLevelHasHeightOfLevel()
        {
            MakeGameWithOneLevel();
            Assert.AreEqual(1, game.LevelHeight);
        }

        [TestMethod, TestCategory("1level")]
        public void GameWithOneLevelHasWidthOfLevel()
        {
            MakeGameWithOneLevel();
            Assert.AreEqual(3, game.LevelWidth);
        }
        [TestMethod, TestCategory("1level")]
        public void GameWithOneLevelHasLevelName()
        {
            MakeGameWithOneLevel();
            string expectedLevelName = "level 1";
            string actuallevelName = game.CurrentLevelName;
            Assert.AreSame(expectedLevelName, actuallevelName);
        }

        [TestMethod, TestCategory("1level")]
        public void GameWithOneLevelHasSingleEntryNamesList()
        {
            MakeGameWithOneLevel();
            int actualNumberOfNames = game.LevelNames().Count;
            Assert.AreEqual(1, actualNumberOfNames);
        }

        void MakeGameWithThreeLevels()
        {
            game = new Game();
            game.AddLevel("level 1", 3, 1, "0000 0001 0002 1011 1010 1110");
            game.AddLevel("level 2", 3, 1, "0000 0001 0002 1011 1010 1110");
            game.AddLevel("level 3", 3, 1, "0000 0001 0002 1011 1010 1110");
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasLevelCountOf3()
        {
            MakeGameWithThreeLevels();
            int expectedLevelCount = 3;
            int actualLevelCount = game.LevelCount;
            Assert.AreEqual(expectedLevelCount, actualLevelCount);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasHeightOfLastLevel()
        {
            MakeGameWithThreeLevels();
            Assert.AreEqual(1, game.LevelHeight);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasWidthOflastLevel()
        {
            MakeGameWithThreeLevels();
            Assert.AreEqual(3, game.LevelWidth);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasLastLevelName()
        {
            MakeGameWithThreeLevels();
            string expectedLevelName = "level 3";
            string actuallevelName = game.CurrentLevelName;
            Assert.AreSame(expectedLevelName, actuallevelName);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasThreeEntryNamesList()
        {
            MakeGameWithThreeLevels();
            int actualNumberOfNames = game.LevelNames().Count;
            Assert.AreEqual(3, actualNumberOfNames);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsHasCorrectNamesList()
        {
            MakeGameWithThreeLevels();
            List<string> actualNames = game.LevelNames();
            List<string> expectedNames = new List<string>();
            expectedNames.Add("level 1");
            expectedNames.Add("level 2");
            expectedNames.Add("level 3");
            CollectionAssert.AreEqual(expectedNames, actualNames);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsCanChangeCurrentLevel()
        {
            MakeGameWithThreeLevels();
            string expectedName = "level 2";
            game.SetLevel("level 2");
            string actualName = game.CurrentLevelName;
            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod, TestCategory("3levels")]
        public void GameWithThreeLevelsDoesNotChangeCurrentLevelIfNameInvalid
        ()
        {
            MakeGameWithThreeLevels();
            string expectedName = "level 3";
            game.SetLevel("level 666");
            string actualName = game.CurrentLevelName;
            Assert.AreSame(expectedName, actualName);
        }

        void MakeGameWithEmptySquare()
        {
            game = new Game();
            game.AddLevel("level 1", 1, 1, "0000 0000 0000 0000");
        }

        [TestMethod, TestCategory("empty_square")]
        public void EmptySquareHasNoTop()
        {
            MakeGameWithEmptySquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = false;
            bool actuallyHas = targetSquare.Top;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("empty_square")]
        public void EmptySquareHasNoRight()
        {
            MakeGameWithEmptySquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = false;
            bool actuallyHas = targetSquare.Right;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("empty_square")]
        public void EmptySquareHasNoBottom()
        {
            MakeGameWithEmptySquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = false;
            bool actuallyHas = targetSquare.Bottom;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("empty_square")]
        public void EmptySquareHasNoLeft()
        {
            MakeGameWithEmptySquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = false;
            bool actuallyHas = targetSquare.Left;
            Assert.AreEqual(expected, actuallyHas);
        }

        void MakeGameWithFullSquare()
        {
            game = new Game();
            game.AddLevel("level 1", 1, 1, "0000 0000 0000 1111");
        }

        [TestMethod, TestCategory("Full_square")]
        public void FullSquareHasTop()
        {
            MakeGameWithFullSquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Top;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("full_square")]
        public void FullSquareHasRight()
        {
            MakeGameWithFullSquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Right;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("full_square")]
        public void FullSquareHasBottom()
        {
            MakeGameWithFullSquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Bottom;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("full_square")]
        public void FullSquareHasLeft()
        {
            MakeGameWithFullSquare();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Left;
            Assert.AreEqual(expected, actuallyHas);
        }

        void MakeSimpleGame()
        {
            game = new Game();
            game.AddLevel("Sinple", 3, 1, "0000 0001 0002 1011 1010 1110");
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasMinotaurInRightPlace()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Minotaur;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasTheseusInRightPlace()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 1);
            bool expected = true;
            bool actuallyHas = targetSquare.Theseus;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasExitInRightPlace()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 2);
            bool expected = true;
            bool actuallyHas = targetSquare.Exit;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasTopWallInSquare0000()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Top;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasLeftWallInSquare0000()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Left;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasNoRightWallInSquare0000()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = false;
            bool actuallyHas = targetSquare.Right;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasBottomWallInSquare0000()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 0);
            bool expected = true;
            bool actuallyHas = targetSquare.Bottom;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasTopWallInSquare0001()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 1);
            bool expected = true;
            bool actuallyHas = targetSquare.Top;
            Assert.AreEqual(expected, actuallyHas);
        }
        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasNoLeftWallInSquare0001()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 1);
            bool expected = false;
            bool actuallyHas = targetSquare.Left;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasNoRightWallInSquare0001()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 1);
            bool expected = false;
            bool actuallyHas = targetSquare.Right;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasBottomWallInSquare0001()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 1);
            bool expected = true;
            bool actuallyHas = targetSquare.Bottom;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasTopWallInSquare0002()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 2);
            bool expected = true;
            bool actuallyHas = targetSquare.Top;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasNoLeftWallInSquare0002()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 2);
            bool expected = false;
            bool actuallyHas = targetSquare.Left;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasRightWallInSquare0002()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 2);
            bool expected = true;
            bool actuallyHas = targetSquare.Right;
            Assert.AreEqual(expected, actuallyHas);
        }

        [TestMethod, TestCategory("simple_game")]
        public void SimpleGameHasBottomWallInSquare0002()
        {
            MakeSimpleGame();
            Square targetSquare = game.WhatIsAt(0, 2);
            bool expected = true;
            bool actuallyHas = targetSquare.Bottom;
            Assert.AreEqual(expected, actuallyHas);
        }
    }
}
