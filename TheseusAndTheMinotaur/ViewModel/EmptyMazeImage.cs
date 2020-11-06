using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class EmptyMazeImage : AbstractImmutableMazeImage
    {
        public EmptyMazeImage(int row, int column) : base(row, column) { }
    }
}
