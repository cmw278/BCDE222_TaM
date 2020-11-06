using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheseusAndTheMinotaur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelSelector : Page
    {
        public event EventHandler<LevelSelectedEventArgs> LevelSelected = delegate { };

        private List<string> LevelNames { get; set; }
        public LevelSelector()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var gameController = e.Parameter as GameController ?? new GameController();
            LevelNames = gameController.LevelList;
            base.OnNavigatedTo(e);
        }

        private void LevelItem_Click(object sender, RoutedEventArgs e)
        {
            var targetLevel = sender as Button;
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs(targetLevel.Content as string));
        }
    }
}
