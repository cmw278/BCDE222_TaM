using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// Simplified version of IValueConvertor for model-level adapters
    /// </summary>
    interface IValueAdapter
    {
        object Convert(object value);
        object ConvertBack(object value);
    }
}
