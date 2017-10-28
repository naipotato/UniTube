using UniTube.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class HomePage : Page
    {
        private HomeViewModel ViewModel => (DataContext as HomeViewModel);

        public HomePage() => InitializeComponent();
    }
}
