using UniTube.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class MenuPage : Page
    {
        private MasterViewModel ViewModel => (DataContext as MasterViewModel);

        public MenuPage() => InitializeComponent();
    }
}
