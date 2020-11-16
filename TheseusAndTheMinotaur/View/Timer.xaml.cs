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

namespace TheseusAndTheMinotaur
{
    public sealed partial class Timer : UserControl, ITimer, INotifyPropertyChanged
    {
        #region Public Properties
        public bool IsRunning
        {
            get { return _Timer.IsRunning; }
            set { _Timer.IsRunning = value; }
        }
        #endregion

        public Timer()
        {
            this.InitializeComponent();
            InitializeTimer();
            TimerInterval = 250;
            SetTimeFormat(TimeSpanFormat.Default);
            AttachPropertyChangeObserver();
        }

        #region DependencyProperties
        // https://www.tutorialspoint.com/xaml/xaml_dependency_properties.htm
        public static DependencyProperty TimeFormatProperty =
            DependencyProperty.Register("TimeFormat",
                                        typeof(TimeSpanFormat),
                                        typeof(Timer),
                                        new PropertyMetadata(TimeSpanFormat.Default,
                                                             new PropertyChangedCallback(OnTimeFormatChanged)));
        public static DependencyProperty TimerIntervalProperty =
            DependencyProperty.Register("TimerInterval",
                                        typeof(int),
                                        typeof(Timer),
                                        new PropertyMetadata(250,
                                                             new PropertyChangedCallback(OnTimerIntervalChanged)));
        public TimeSpanFormat TimeFormat
        {
            get { return (TimeSpanFormat)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        public int TimerInterval
        {
            get { return (int)GetValue(TimerIntervalProperty); }
            set { SetValue(TimerIntervalProperty, value); }
        }
        public static void OnTimeFormatChanged(DependencyObject instance, DependencyPropertyChangedEventArgs e)
        {
            ((Timer)instance)?.OnTimeFormatChanged(e);
        }

        public void OnTimeFormatChanged(DependencyPropertyChangedEventArgs e)
        {
            SetTimeFormat(TimeFormat);
        }
        public static void OnTimerIntervalChanged(DependencyObject instance, DependencyPropertyChangedEventArgs e)
        {
            ((Timer)instance)?.OnTimerIntervalChanged(e);
        }

        public void OnTimerIntervalChanged(DependencyPropertyChangedEventArgs e)
        {
            _Timer.Interval = TimerInterval;
        }
        #endregion

        #region ITimer
        private TimerViewModel _Timer;

        private void InitializeTimer()
        {
            _Timer = new TimerViewModel();
        }

        public void Start() => _Timer.Start();

        public void Stop() => _Timer.Stop();

        public void Reset(int startTime = 0, bool startImmediately = false) => _Timer.Reset(startTime, startImmediately);

        public void Reset(bool startImmediately) => _Timer.Reset(startImmediately);
        #endregion

        #region Time Formatting
        private AbstractTimeSpanAdapter _TimeFormatter;

        public void SetTimeFormat(TimeSpanFormat format)
        {
            _TimeFormatter = TimeSpanAdapterBuilder.GetConverter(format);
        }

        public string Time
        {
            get { return (string)_TimeFormatter?.Convert(_Timer.Time); }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Attach transparent notifier to any observable properties
        /// </summary>
        private void AttachPropertyChangeObserver()
        {
            _Timer.PropertyChanged += TransparentPropertyChangeNotifier;
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
            System.Reflection.PropertyInfo[] myProperties = typeof(Timer).GetProperties();
            if (myProperties.Any((property) => eventArgs.PropertyName == property.Name))
            {
                NotifyChange(eventArgs.PropertyName);
            }
        }
        #endregion

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            IsRunning = !IsRunning;
        }
    }
}
