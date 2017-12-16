using System;
using System.Threading.Tasks;

using UniTube.Framework;
using UniTube.Framework.Utils;
using UniTube.Services.SettingsServices;
using UniTube.Views;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UniTube
{
    sealed partial class App : MvvmApplication
    {
        public App()
        {
            InitializeComponent();

            ExtendedSplashScreenFactory = (s) => new Splash(s);

            var settings = SettingsService.Instance;
            if (settings.AppTheme != ElementTheme.Default)
            {
                RequestedTheme = settings.AppTheme.ToApplicationTheme();
            }
        }

        private async void OnDisplayOrientationChanged(DisplayInformation sender, object args)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                if (sender.CurrentOrientation == DisplayOrientations.Landscape || sender.CurrentOrientation == DisplayOrientations.LandscapeFlipped)
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    if (statusBar != null)
                    {
                        await statusBar.HideAsync();
                    }
                }
                else
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    if (statusBar != null)
                    {
                        await statusBar.ShowAsync();
                    }
                }
            }
        }

        protected override UIElement CreateShell(Frame rootFrame) => new Controls.LoadingView
        {
            Content = rootFrame,
            RingForeground = XamlUtils.GetResource("MainColorBrush", default(SolidColorBrush))
        };

        protected override IAsyncAction OnInitializeAsync(IActivatedEventArgs args) => Task.Run(async () =>
        {
            var keys = PageKeys<Pages>();
            keys.TryAdd(Pages.History, typeof(HistoryPage));
            keys.TryAdd(Pages.Home, typeof(HomePage));
            keys.TryAdd(Pages.Master, typeof(MasterPage));
            keys.TryAdd(Pages.Saved, typeof(SavedPage));
            keys.TryAdd(Pages.Search, typeof(SearchPage));
            keys.TryAdd(Pages.Settings, typeof(SettingsPage));
            keys.TryAdd(Pages.Trending, typeof(TrendingPage));
            keys.TryAdd(Pages.Video, typeof(VideoPage));

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            var display = DisplayInformation.GetForCurrentView();
            display.OrientationChanged += OnDisplayOrientationChanged;

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.BackgroundColor = "#0000".ToColor();
                    titleBar.ForegroundColor = "#FFF".ToColor();

                    titleBar.InactiveBackgroundColor = "#0000".ToColor();
                    titleBar.InactiveForegroundColor = "#FFF".ToColor();

                    titleBar.ButtonBackgroundColor = "#0000".ToColor();
                    titleBar.ButtonForegroundColor = "#FFF".ToColor();

                    titleBar.ButtonHoverBackgroundColor = "#19FFFFFF".ToColor();
                    titleBar.ButtonHoverForegroundColor = "#FFF".ToColor();

                    titleBar.ButtonPressedBackgroundColor = "#33FFFFFF".ToColor();
                    titleBar.ButtonPressedForegroundColor = "#FFF".ToColor();

                    titleBar.ButtonInactiveBackgroundColor = "#0000".ToColor();
                    titleBar.ButtonInactiveForegroundColor = "#FFF".ToColor();
                }
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 0;

                    statusBar.ForegroundColor = "#B2FFFFFF".ToColor();

                    if (display.CurrentOrientation == DisplayOrientations.Landscape || display.CurrentOrientation == DisplayOrientations.LandscapeFlipped)
                    {
                        await statusBar.HideAsync();
                    }
                }
            }

            RegisterBackgroundTask();
        }).AsAsyncAction();

        protected override IAsyncAction OnStartAsync(LaunchActivatedEventArgs args) => Task.Run(() =>
        {
            NavigationService.Navigate(Pages.Master);
        }).AsAsyncAction();

        private void RegisterBackgroundTask()
        {
            var taskRegistered = false;
            var liveTileTaskName = "LiveTileTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == liveTileTaskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder
                {
                    Name = liveTileTaskName,
                    TaskEntryPoint = "UniTube.Tasks.LiveTileTask",
                    CancelOnConditionLoss = true,
                    IsNetworkRequested = true
                };
                builder.SetTrigger(new TimeTrigger(15, false));
                builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                builder.AddCondition(new SystemCondition(SystemConditionType.BackgroundWorkCostNotHigh));
                var task = builder.Register();
            }
        }
    }
}
