using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;

namespace TheseusAndTheMinotaur
{
    public sealed class LevelPlayerEventArgs : EventArgs
    {
        public readonly LevelAction Action;
        public readonly Directions Direction;

        public LevelPlayerEventArgs(LevelAction action)
        {
            Action = action;
        }

        public LevelPlayerEventArgs(Directions direction)
        {
            Action = LevelAction.Move;
            Direction = direction;
        }

    }
}
