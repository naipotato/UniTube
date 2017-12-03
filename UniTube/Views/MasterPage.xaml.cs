using UniTube.ViewModels;

using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Views
{
    public sealed partial class MasterPage : Page
    {
        private MasterViewModel ViewModel => (DataContext as MasterViewModel);

        public MasterPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            Loaded += OnMasterPageLoaded;
        }

        private void OnMasterPageLoaded(object sender, RoutedEventArgs e)
            => ViewModel.MasterNavigationService = HamburgerMenu.NavigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            Window.Current.SetTitleBar(MainTitleBar);
            coreTitleBar.IsVisibleChanged += OnCoreTitleBarIsVisibleChanged;
            coreTitleBar.LayoutMetricsChanged += OnCoreTitleBarLayoutMetricsChanged;
            TitleBar.Height = coreTitleBar.Height;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.IsVisibleChanged -= OnCoreTitleBarIsVisibleChanged;
            coreTitleBar.LayoutMetricsChanged -= OnCoreTitleBarLayoutMetricsChanged;
        }

        private void OnCoreTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
            => TitleBar.Height = sender.Height;

        private void OnCoreTitleBarIsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
            => TitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
    }
}
