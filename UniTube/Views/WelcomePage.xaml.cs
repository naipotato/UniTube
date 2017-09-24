using System;
using UniTube.Services;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Views
{
    public sealed partial class WelcomePage : Page
    {
        private Type _sourcePageType;

        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            Window.Current.SetTitleBar(MainTitleBar);
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            TitleBar.Height = coreTitleBar.Height;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.IsVisibleChanged -= CoreTitleBar_IsVisibleChanged;
            coreTitleBar.LayoutMetricsChanged -= CoreTitleBar_LayoutMetricsChanged;

            if (_sourcePageType == null)
            {
                _sourcePageType = e.SourcePageType;
                e.Cancel = true;

                ExitAnimation.Completed += ExitAnimation_Completed;
                ExitAnimation.Begin();
            }
            else
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("topLayer", TopLayer);
            }
        }

        private void ExitAnimation_Completed(object sender, object e)
        {
            ExitAnimation.Completed -= ExitAnimation_Completed;
            NavigationService.SetMainPage(_sourcePageType.Name, new SuppressNavigationTransitionInfo());
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            TitleBar.Height = sender.Height;
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            TitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}