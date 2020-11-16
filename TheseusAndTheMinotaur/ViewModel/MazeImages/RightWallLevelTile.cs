using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class RightWallLevelTile : AbstractImmutableLevelTile
    {
        public RightWallLevelTile(int row, int column) : base(row, column) { }
    }
}
