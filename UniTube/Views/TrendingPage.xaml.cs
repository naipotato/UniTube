using UniTube.Core.Resources;
using UniTube.ViewModels;

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

        private void TrendingGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var video = e.ClickedItem as Video;
            TrendingGridView.PrepareConnectedAnimation("videoThumbnail", e.ClickedItem, "Thumbnail");
            ViewModel.Locator.MasterViewModel.NavigationService.Navigate(Pages.Video, video, new SuppressNavigationTransitionInfo());
        }
    }
}
