using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
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
            MyGameController.PropertyChanged += MyGameController_PropertyChanged;
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

        private void LevelSelector_LevelSelected(object sender, LevelSelectedEventArgs e)
        {
            MyGameController.LevelName = e.TargetLevel;
            CoreNavigationFrame.Navigate(typeof(LevelPlayer), MyGameController.Maze);
        }

        private void Navigate_LevelSelector(LevelSelector page)
        {
            PageTitle = "Select a level";
            LevelTimer.PropertyChanged -= LevelTimer_PropertyChanged;
            CoreNavigationFrame.Visibility = Visibility.Visible;
            TimerIsVisible = false;
            TimerIsEnabled = false;
            page.LevelSelected += LevelSelector_LevelSelected;
        }

        private void Navigate_LevelPlayer(LevelPlayer page)
        {
            PageTitle = MyGameController.LevelName;
            LevelTimer.Reset();
            TimerIsVisible = true;
            TimerIsEnabled = ! MyGameController.IsFinished;
            LevelTimer.PropertyChanged += LevelTimer_PropertyChanged;
            page.LevelEventTriggered += LevelPlayer_EventTriggered;
            if (TimerIsEnabled) LevelTimer.Start();
        }
        #endregion

        #region Gameplay event listeners
        private void LevelPlayer_EventTriggered(object sender, LevelPlayerEventArgs e)
        {
            switch (e.Action)
            {
                case LevelAction.PauseGame:
                    LevelTimer.Stop();
                    break;
                case LevelAction.Reset:
                    LevelTimer.Reset();
                    break;
                case LevelAction.Move:
                    if (TimerIsRunning) MyGameController.Move(e.Direction);
                    break;
                default:
                    return;
            }
        }

        private void MyGameController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsFinished":
                    Game_IsFinishedHandler();
                    return;
                case "MoveCount":
                    Game_MoveCountHandler();
                    return;
                default:
                    return;
            }
        }

        private void LevelTimer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsRunning":
                    if (TimerIsEnabled)
                    {
                        if (TimerIsRunning) CoreNavigationFrame.Visibility = Visibility.Visible;
                        else CoreNavigationFrame.Visibility = Visibility.Collapsed;
                    }
                    return;
                default:
                    return;
            }
        }

        private void Game_IsFinishedHandler()
        {
            if (MyGameController.IsFinished)
            {
                TimerIsEnabled = false;
                LevelTimer.Stop();
            }
        }

        private void Game_MoveCountHandler()
        {
            if (CoreNavigationFrame.Content is LevelPlayer page)
            {
                page.MoveCount = MyGameController.MoveCount;
            }
        }
        #endregion
    }
}
