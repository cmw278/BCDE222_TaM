using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Core : Page, INotifyPropertyChanged
    {
        public static readonly GameController MyGameController = new GameController();

        public Core()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CoreNavigationFrame.Navigate(typeof(LevelSelector), MyGameController);
        }

        #region Public properties
        public bool TimerIsRunning => LevelTimer.IsRunning;

        public bool ShowBackButton = false;

        private string _PageTitle;
        public string PageTitle
        {
            get => _PageTitle;
            set
            {
                if (UpdateValue(ref _PageTitle, value)) NotifyChange();
            }
        }

        private bool _TimerIsEnabled;
        public bool TimerIsEnabled
        {
            get => _TimerIsEnabled;
            set
            {
                if (UpdateValue(ref _TimerIsEnabled, value)) NotifyChange();
            }
        }

        private bool _TimerIsVisible;
        public bool TimerIsVisible
        {
            get => _TimerIsVisible;
            set
            {
                if (UpdateValue(ref _TimerIsVisible, value)) NotifyChange();
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Update a referenced variable, but only if the new value is different.
        /// </summary>
        /// <param name="old">A reference to a variable</param>
        /// <param name="_new">The new value to be compared and applied</param>
        /// <returns>A boolean indicating whether the variable was changed.</returns>
        private bool UpdateValue<T>(ref T old, T _new)
        {
            if (old != null && old.Equals(_new)) return false;
            old = _new;
            return true;
        }
        #endregion

        #region Navigation

        private void CoreNavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (CoreNavigationFrame.CanGoBack) CoreNavigationFrame.GoBack();
        }

        private void LevelSelector_LevelSelected(object sender, LevelSelectedEventArgs e)
        {
            MyGameController.LevelName = e.TargetLevel;
            CoreNavigationFrame.Navigate(typeof(LevelPlayer), MyGameController.Maze);
        }

        private void CoreNavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (e.Content)
            {
                case LevelSelector page:
                    Navigate_LevelSelector(page);
                    break;
                case LevelPlayer page:
                    Navigate_LevelPlayer(page);
                    break;
                default:
                    return;
            }
        }

        private void Navigate_LevelSelector(LevelSelector page)
        {
            PageTitle = "Select a level";
            TimerIsVisible = false;
            TimerIsEnabled = false;
            page.LevelSelected += LevelSelector_LevelSelected;
        }

        private void Navigate_LevelPlayer(LevelPlayer page)
        {
            PageTitle = MyGameController.LevelName;
            TimerIsVisible = true;
            TimerIsEnabled = true;
            page.LevelEventTriggered += LevelPlayer_EventTriggered;
        }
        #endregion

        #region Gameplay event listeners
        private void LevelPlayer_EventTriggered(object sender, AbstractLevelEventArgs e)
        {
            switch (e)
            {
                case LevelPauseEventArgs _:
                    LevelTimer.Stop();
                    break;
                case LevelResetEventArgs reset:
                    LevelTimer.Reset(reset.StartTime, reset.StartImmediately);
                    break;
                default:
                    return;
            }
        }
        #endregion
    }
}
