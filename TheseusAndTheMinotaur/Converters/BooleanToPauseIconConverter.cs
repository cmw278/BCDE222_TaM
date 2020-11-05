using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace TheseusAndTheMinotaur
{
    public class BooleanToPauseIconConverter : IValueConverter
    {
        private static IconElement _PlayIcon = new SymbolIcon(Symbol.Play);
        private static IconElement _PauseIcon = new SymbolIcon(Symbol.Pause);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value) return _PauseIcon;
            else return _PlayIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case Symbol.Pause:
                    return true;
                case Symbol.Play:
                    return false;
                default:
                    throw new NotSupportedException($"Can not convert {(Symbol)value} to bool");
            }
        }
    }
}
