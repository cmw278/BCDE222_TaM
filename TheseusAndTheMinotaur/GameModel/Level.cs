using System;
using System.Collections.Generic;
using System.Drawing;

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
            return _TheseusPosition.Equals(_ExitPosition) && !IsTheseusDead();
        }

        public bool IsTheseusDead()
        {
            return _MinotaurPosition.Equals(_TheseusPosition);
        }

        public void MoveTheseus(Directions direction)
        {
            Point targetPoint = GetTargetPoint(_TheseusPosition, direction);
            Square targetSquare = WhatIsAt(targetPoint.Y, targetPoint.X);
            Square currentSquare = WhatIsAt(TheseusRow, TheseusColumn);
            if (CanMoveToSquare(targetSquare, direction) && CanLeaveSquare(currentSquare, direction))
            {
                currentSquare.Theseus = false;
                targetSquare.Theseus = true;
                _TheseusPosition = targetPoint;
                Moves.Add(direction);
            }
        }

        private Point GetTargetPoint(Point currentPoint, Directions direction)
        {
            Point targetPoint = new Point(currentPoint.X, currentPoint.Y);
            switch (direction)
            {
                case Directions.UP:
                    targetPoint.Y--;
                    break;
                case Directions.DOWN:
                    targetPoint.Y++;
                    break;
                case Directions.LEFT:
                    targetPoint.X--;
                    break;
                case Directions.RIGHT:
                    targetPoint.X++;
                    break;
            }
            return targetPoint;
        }

        private bool CanMoveToSquare(Square targetSquare, Directions direction)
        {
            if (targetSquare is null) return false;
            switch (direction)
            {
                case Directions.UP:
                    return !targetSquare.Bottom;
                case Directions.DOWN:
                    return !targetSquare.Top;
                case Directions.LEFT:
                    return !targetSquare.Right;
                case Directions.RIGHT:
                    return !targetSquare.Left;
                default:
                    return true;
            }
        }

        private bool CanLeaveSquare(Square currentSquare, Directions direction)
        {

            if (currentSquare is null) return false;
            switch (direction)
            {
                case Directions.UP:
                    return !currentSquare.Top;
                case Directions.DOWN:
                    return !currentSquare.Bottom;
                case Directions.LEFT:
                    return !currentSquare.Left;
                case Directions.RIGHT:
                    return !currentSquare.Right;
                default:
                    return true;
            }
        }

        public void MoveMinotaur()
        {
            if (!MoveMinotaurHorizontally()) MoveMinotaurVertically();
        }

        private bool MoveMinotaurVertically()
        {
            if (MinotaurRow > TheseusRow) return MoveMinotaur(Directions.UP);
            else if (MinotaurRow < TheseusRow) return MoveMinotaur(Directions.DOWN);
            else return false;
        }

        private bool MoveMinotaurHorizontally()
        {
            if (MinotaurColumn > TheseusColumn) return MoveMinotaur(Directions.LEFT);
            else if (MinotaurColumn < TheseusColumn) return MoveMinotaur(Directions.RIGHT);
            else return false;
        }

        private bool MoveMinotaur(Directions direction)
        {
            Point targetPoint = GetTargetPoint(_MinotaurPosition, direction);
            Square targetSquare = WhatIsAt(targetPoint.Y, targetPoint.X);
            Square currentSquare = WhatIsAt(MinotaurRow, MinotaurColumn);
            if (CanMoveToSquare(targetSquare, direction) && CanLeaveSquare(currentSquare, direction))
            {
                currentSquare.Minotaur = false;
                targetSquare.Minotaur = true;
                _MinotaurPosition = targetPoint;
                return true;
            }
            return false;
        }

        public Square WhatIsAt(int row, int column)
        {
            if (column < 0 || row < 0 || column >= Width || row >= Height) return null;
            return AllMySquares[row, column];
        }
    }
}
