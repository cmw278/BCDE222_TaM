using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TaM;

namespace TheseusAndTheMinotaur
{
    public sealed partial class LevelPlayerControlBar : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler<LevelPlayerEventArgs> ControlClicked = delegate { };

        public LevelPlayerControlBar()
        {
            this.InitializeComponent();
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is AppBarButton button)
            {
                var direction = GetButtonDirection(button.Name);
                ControlClicked?.Invoke(this, new LevelPlayerEventArgs(direction));
            }
        }

        private Directions GetButtonDirection(string buttonName)
        {
            switch (buttonName)
            {
                case "LevelControl_Up":
                    return Directions.UP;
                case "LevelControl_Down":
                    return Directions.DOWN;
                case "LevelControl_Left":
                    return Directions.LEFT;
                case "LevelControl_Right":
                    return Directions.RIGHT;
                case "LevelControl_Wait":
                    return Directions.PAUSE;
                default:
                    throw new NotImplementedException();
            }
        }

        #region DependencyProperties
        // https://www.tutorialspoint.com/xaml/xaml_dependency_properties.htm
        public static DependencyProperty MoveCountProperty =
            DependencyProperty.Register("MoveCount",
                                        typeof(int),
                                        typeof(LevelPlayerControlBar),
                                        new PropertyMetadata(0));

        public int MoveCount
        {
            private get => (int)GetValue(MoveCountProperty);
            set
            {
                if (value != MoveCount)
                {
                    SetValue(MoveCountProperty, value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MoveCountString"));
                }
            }
        }

        private string MoveCountString => $"{MoveCount} move" + (MoveCount != 1 ? "s" : "");
        #endregion
    }
}
