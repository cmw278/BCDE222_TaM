using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class LevelResetEventArgs : AbstractLevelEventArgs
    {
        public readonly int StartTime;

        public readonly bool StartImmediately;

        public LevelResetEventArgs(int startTime = 0, bool startImmediately = false)
        {
            StartTime = startTime;
            StartImmediately = startImmediately;
        }

        public LevelResetEventArgs(bool startImmediately) : this(0, startImmediately) { }
    }
}
