using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;

namespace TheseusAndTheMinotaur
{
    public class BooleanToBackButtonVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value) return NavigationViewBackButtonVisible.Visible;
            else return NavigationViewBackButtonVisible.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch ((NavigationViewBackButtonVisible)value)
            {
                case NavigationViewBackButtonVisible.Collapsed:
                    return false;
                case NavigationViewBackButtonVisible.Visible:
                    return true;
                default:
                    throw new NotSupportedException($"{value} is not supported");
            }
        }
    }
}
