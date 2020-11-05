using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class DefaultTimeSpanAdapter : AbstractTimeSpanAdapter
    {
        public DefaultTimeSpanAdapter() : base(@"m\:ss") { }
    }
}
