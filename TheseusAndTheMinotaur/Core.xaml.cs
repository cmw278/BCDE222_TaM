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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Core : Page, INotifyPropertyChanged
    {
        public Core()
        {
            this.InitializeComponent();
            AttachPropertyChangeObserver();
        }

        #region Public properties
        public bool TimerActive
        {
            get { return LevelTimer.IsRunning; }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Attach transparent notifier to any observable properties
        /// </summary>
        private void AttachPropertyChangeObserver()
        {
            LevelTimer.PropertyChanged += TransparentPropertyChangeNotifier;
        }

        private void NotifyChange(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Transparent PropertyChange handler to propogate observable properties to any observers of this object
        /// </summary>
        private void TransparentPropertyChangeNotifier(object sender, PropertyChangedEventArgs eventArgs)
        {
            System.Reflection.PropertyInfo[] myProperties = typeof(Core).GetProperties();
            if (myProperties.Any((property) => eventArgs.PropertyName == property.Name))
            {
                NotifyChange(eventArgs.PropertyName);
            }
            else
            {
                NotifyTranslater(sender, eventArgs);
            }
        }

        /// <summary>
        /// PropertyChange translater to propogate observable properties to any observers of this object
        /// </summary>
        private void NotifyTranslater(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (sender is Timer)
            {
                switch (eventArgs.PropertyName)
                {
                    case "IsRunning":
                        NotifyChange("TimerActive");
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        private void CoreNavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
