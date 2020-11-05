using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class LongTimeSpanAdapter : AbstractTimeSpanAdapter
    {
        public LongTimeSpanAdapter() : base(@"h\:mm\:ss") { }
    }
}
