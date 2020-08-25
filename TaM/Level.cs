using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TaM
{
    class Level : IMoveableHolder
    {
        public readonly string Name;
        private readonly Square[,] AllMySquares;
        private List<Directions> Moves;

        public int Height => AllMySquares.Length / Width;
        public int Width { get; }
        public int TheseusRow => _TheseusPosition.Y;

        public int TheseusColumn => _TheseusPosition.X;

        public int MinotaurRow => _MinotaurPosition.Y;

        public int MinotaurColumn => _MinotaurPosition.X;

        public int MoveCount => Moves.Count;

        private Point _TheseusPosition;
        private Point _MinotaurPosition;
        private Point _ExitPosition;

        public Level(string name, int width, int height, string data)
        {
            Name = name;
            AllMySquares = new Square[height, width];
            Moves = new List<Directions>();
            Width = width;

            _MinotaurPosition = GetPoint(data.Substring(0, 4));
            _TheseusPosition = GetPoint(data.Substring(5, 4));
            _ExitPosition = GetPoint(data.Substring(10, 4));

            for (int i = 0; i < AllMySquares.Length; i++)
            {
                string squareData = data.Substring(i * 5 + 15, 4);
                bool top = squareData[0] == '1';
                bool right = squareData[1] == '1';
                bool bottom = squareData[2] == '1';
                bool left = squareData[3] == '1';

                Point currentPosition = new Point(i % width, i / width);
                bool hasTheseus = currentPosition.Equals(_TheseusPosition);
                bool hasMinotaur = currentPosition.Equals(_MinotaurPosition);
                bool isExit = currentPosition.Equals(_ExitPosition);

                AllMySquares[currentPosition.Y, currentPosition.X] = new Square(top, left, bottom, right, hasMinotaur, hasTheseus, isExit);
            }
        }

        private Point GetPoint(string positionData)
        {
            int y = int.Parse(positionData.Substring(0, 2));
            int x = int.Parse(positionData.Substring(2, 2));
            return new Point(x, y);
        }
        public bool HasTheseusEscaped()
        {
            throw new NotImplementedException();
        }

        public bool IsTheseusDead()
        {
            throw new NotImplementedException();
        }

        public void MoveTheseus(Directions direction)
        {
            throw new NotImplementedException();
        }

        public Square WhatIsAt(int row, int column)
        {
            return AllMySquares[row, column];
        }
    }
}
