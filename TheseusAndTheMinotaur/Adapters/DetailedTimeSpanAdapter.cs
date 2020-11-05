﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public sealed class DetailedTimeSpanAdapter : AbstractTimeSpanAdapter
    {
        public DetailedTimeSpanAdapter() : base(@"m\:ss\.ff") { }
    }
}
