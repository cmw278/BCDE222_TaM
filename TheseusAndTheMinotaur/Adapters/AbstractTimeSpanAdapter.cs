using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// Convert TimeSpan objects to formatted strings
    /// </summary>
    public abstract class AbstractTimeSpanAdapter : IValueAdapter
    {
        protected readonly string _Format;

        /// <summary>
        /// Create a converter with the defined format
        /// </summary>
        /// <param name="format">
        /// The format to be applied using TimeSpan.ToString()
        /// </param>
        protected AbstractTimeSpanAdapter(string format)
        {
            _Format = format;
        }
        public object Convert(object value)
        {
            return ((TimeSpan)value).ToString(_Format, CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value)
        {
            return TimeSpan.ParseExact((string)value, _Format, CultureInfo.CurrentCulture);
        }
    }
}
