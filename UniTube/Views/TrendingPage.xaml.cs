using UniTube.Core.Resources;
using UniTube.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Views
{
    public sealed partial class TrendingPage : Page
    {
        private TrendingViewModel ViewModel => (DataContext as TrendingViewModel);

        public TrendingPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
    }
}
