using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheseusAndTheMinotaur
{
    public static class TimeSpanAdapterBuilder
    {
        private static readonly AbstractTimeSpanAdapter _Default = new DefaultTimeSpanAdapter();
        private static readonly AbstractTimeSpanAdapter _Detailed = new DetailedTimeSpanAdapter();
        private static readonly AbstractTimeSpanAdapter _Long = new LongTimeSpanAdapter();

        public static AbstractTimeSpanAdapter GetConverter(TimeSpanFormat targetFormat)
        {
            switch (targetFormat)
            {
                case TimeSpanFormat.Default:
                    return _Default;
                case TimeSpanFormat.Detailed:
                    return _Detailed;
                case TimeSpanFormat.Long:
                    return _Long;
                default:
                    throw new NotImplementedException($"{targetFormat} has not been implemented");
            }
        }
    }
}
