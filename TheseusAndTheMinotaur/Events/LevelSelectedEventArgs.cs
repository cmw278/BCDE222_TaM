using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public class LevelSelectedEventArgs : EventArgs
    {
        public readonly string TargetLevel;

        public LevelSelectedEventArgs(string targetLevel)
        {
            TargetLevel = targetLevel;
        }
    }
}
