using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class LeftWallLevelTile : AbstractImmutableLevelTile
    {
        public LeftWallLevelTile(int row, int column) : base(row, column) { }
    }
}
