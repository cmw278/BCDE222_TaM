using System;
using System.Collections.Generic;
using System.Text;

namespace TaM
{
    public class Square : AbstractSquare
    {
        private Walls MyWalls;
        private bool _HasTheseus;
        private bool _HasMinotaur;
        private bool _IsExit;

        public bool Top => MyWalls.HasFlag(Walls.Top);

        public bool Right => MyWalls.HasFlag(Walls.Right);

        public bool Bottom => MyWalls.HasFlag(Walls.Bottom);

        public bool Left => MyWalls.HasFlag(Walls.Left);

        public bool Theseus
        {
            get => _HasTheseus;
            set => _HasTheseus = value;
        }

        public bool Minotaur
        {
            get => _HasMinotaur;
            set => _HasMinotaur = value;
        }

        public bool Exit
        {
            get => _IsExit;
            set => _IsExit = value;
        }

        public Square(bool top, bool left, bool bottom, bool right, bool hasMinotuar, bool hasTheseus, bool isExit)
        {
            if (top) MyWalls |= Walls.Top;
            if (right) MyWalls |= Walls.Right;
            if (bottom) MyWalls |= Walls.Bottom;
            if (left) MyWalls |= Walls.Left;
            _HasTheseus = hasTheseus;
            _HasMinotaur = hasMinotuar;
            _IsExit = isExit;
        }
    }
}
