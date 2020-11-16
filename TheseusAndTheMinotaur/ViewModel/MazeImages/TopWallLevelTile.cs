using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class TopWallLevelTile : AbstractImmutableLevelTile
    {
        public TopWallLevelTile(int row, int column) : base(row, column) { }
    }
}
