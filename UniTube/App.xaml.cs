using System;

using UniTube.Core.Authentication;
using UniTube.Dialogs;
using UniTube.Helpers;
using UniTube.Services;
using UniTube.Views;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube
{
    sealed partial class App : Application
    {
        /// <summary>
        /// Root frame of the app
        /// </summary>
        internal static Frame RootFrame { get; set; }

        /// <summary>
        /// Content frame of the app
        /// </summary>
        internal static Frame ContentFrame { get; set; }

        /// <summary>
        /// Contains info about the user's session.
        /// </summary>
        internal static AuthResponse AuthInfo { get; set; }

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            Resuming += OnResuming;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.On; // I like so much this sounds
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                ElementSoundPlayer.Volume = 0.5; // They sounds very loud on mobile devices, believe me :s
            }

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var display = DisplayInformation.GetForCurrentView();
                if (display.ScreenWidthInRawPixels >= 1152 && display.ScreenHeightInRawPixels >= 768)
                {
                    ApplicationView.PreferredLaunchViewSize = new Size(1024, 675);
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                }

                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.BackgroundColor = "#00000000".ToColor();
                    titleBar.ForegroundColor = "#FFFFFFFF".ToColor();

                    titleBar.InactiveBackgroundColor = "#00000000".ToColor();
                    titleBar.InactiveForegroundColor = "#FFFFFFFF".ToColor();

                    titleBar.ButtonBackgroundColor = "#00000000".ToColor();
                    titleBar.ButtonForegroundColor = "#FFFFFFFF".ToColor();

                    titleBar.ButtonHoverBackgroundColor = "#19FFFFFF".ToColor();
                    titleBar.ButtonHoverForegroundColor = "#FFFFFFFF".ToColor();

                    titleBar.ButtonPressedBackgroundColor = "#33FFFFFF".ToColor();
                    titleBar.ButtonPressedForegroundColor = "#FFFFFFFF".ToColor();

                    titleBar.ButtonInactiveBackgroundColor = "#00000000".ToColor();
                    titleBar.ButtonInactiveForegroundColor = "#FFFFFFFF".ToColor();
                }
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 0;

                    statusBar.ForegroundColor = "#B2FFFFFF".ToColor();
                }
            }

            #region To erase all previous data
            try
            {
                await ApplicationData.Current.RoamingSettings.ReadAsync<bool>("initialized");
            }
            catch
            {
                await ApplicationData.Current.ClearAsync();
            }
            #endregion

            RootFrame = Window.Current.Content as Frame;

            if (RootFrame == null)
            {
                RootFrame = new Frame
                {
                    ContentTransitions = new TransitionCollection
                    {
                        new NavigationThemeTransition()
                    }
                };

                RootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                    RootFrame.Content = extendedSplash;
                    Window.Current.Content = RootFrame;
                }
            }

            if (RootFrame.Content == null)
            {
                bool initialized = await ApplicationData.Current.RoamingSettings.ReadAsync<bool>("Initialized");
                if (initialized)
                {
                    NavigationService.SetMainPage("MasterPage");
                }
                else
                {
                    NavigationService.SetMainPage("WelcomePage");
                }
            }
            Window.Current.Activate();
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            if (RootFrame != null)
            {
                await ApplicationData.Current.LocalSettings.SaveAsync("LastRootPage", RootFrame.SourcePageType.Name);
            }
            if (ContentFrame != null)
            {
                await ApplicationData.Current.LocalSettings.SaveAsync("LastContentPage", ContentFrame.SourcePageType.Name);
            }

            deferral.Complete();
        }

        private async void OnResuming(object sender, object e)
        {
            string refreshToken = await ApplicationData.Current.RoamingSettings.ReadAsync<string>("RefreshToken");
            if (!refreshToken.IsNullOrEmpty())
            {
                AuthInfo = await Authenticator.RefreshAccesTokenAsync(refreshToken, new ErrorHttpDialog());
            }
        }
    }
}
