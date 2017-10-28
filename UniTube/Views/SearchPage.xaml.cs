using UniTube.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Views
{
    public sealed partial class SearchPage : Page
    {
        private SearchViewModel ViewModel => (DataContext as SearchViewModel);

        public SearchPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}
