using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;

namespace TheseusAndTheMinotaur
{
    public class TimerViewModel : AbstractViewModel, IDisposable, ITimer
    {
        #region Timer Data
        private TimeSpan _ElapsedTime { get; set; }
        private int _Interval = 250;
        private System.Timers.Timer _Timer;
        #endregion

        #region Public Properties
        /// <summary>
        /// The period between timer ticks, measured in milliseconds.
        /// </summary>
        public int Interval
        {
            get { return _Interval; }
            set { if (UpdateValue(ref _Interval, value)) _Timer.Interval = _Interval; }
        }

        /// <summary>
        /// A boolean flag that represents the status of the timer
        /// </summary>
        public bool IsRunning
        {
            get { return _Timer.Enabled; }
            set
            {
                bool oldValue = IsRunning;
                _Timer.Enabled = value;
                if (IsRunning != oldValue) NotifyChange();
            }
        }

        /// <summary>
        /// Readonly TimeSpan object representing the timer's elapsed time
        /// </summary>
        public TimeSpan Time
        {
            get { return _ElapsedTime; }
        }
        #endregion

        /// <summary>
        /// Initialize a TimerViewModel instance
        /// </summary>
        /// <param name="startTime">Optionally set a non-zero start time. Measured in milliseconds</param>
        /// <param name="startImmediately">Set true to start the timer after initialization</param>
        public TimerViewModel(int startTime = 0, bool startImmediately = false)
        {
            InitializeTimer();
            Reset(startTime, startImmediately);
        }

        /// <summary>
        /// Initialize a TimerViewModel instance
        /// </summary>
        /// <param name="startImmediately">Set true to start the timer after initialization</param>
        public TimerViewModel(bool startImmediately)
        {
            InitializeTimer();
            Reset(startImmediately);
        }

        #region ITimer
        public void Start() => IsRunning = true;

        public void Stop() => IsRunning = false;

        public void Reset(int startTime = 0, bool startImmediately = false)
        {
            Stop();
            _ElapsedTime = new TimeSpan(0, 0, 0, 0, startTime);
            if (startImmediately) Start();
        }
        public void Reset(bool startImmediately)
        {
            Reset(0, startImmediately);
        }
        #endregion

        #region Timing logic

        /// <summary>
        /// Initialise the timer object
        /// </summary>
        private void InitializeTimer()
        {
            Debug.Assert(_Timer == null, "The timer should not be initialised more than once");
            _Timer = new System.Timers.Timer(Interval);
            _Timer.Elapsed += Tick;
        }

        private void Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            _ElapsedTime += new TimeSpan(0, 0, 0, 0, Interval);
            DispatcherHelper.ExecuteOnUIThreadAsync(() => NotifyChange("Time"));
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Stop();
            _Timer.Dispose();
        }
        #endregion
    }
}
