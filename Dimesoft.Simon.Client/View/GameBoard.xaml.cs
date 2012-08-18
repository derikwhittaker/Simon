using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dimesoft.Simon.Client.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Dimesoft.Simon.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameBoard : Page
    {
        public GameBoard()
        {
            this.InitializeComponent();

            DataContext = new GameBoardViewModel();

            TopLeftButton.ButtonColor = Colors.Green;
            TopRightButton.ButtonColor = Colors.Red;
            BottomRightButton.ButtonColor = Colors.Blue;
            BottomLeftButton.ButtonColor = Colors.Yellow;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void BottomRightButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.BottomRightButton.IsLit = true;
        }
    }
}
