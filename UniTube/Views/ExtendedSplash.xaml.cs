using System;

using UniTube.Core.Authentication;
using UniTube.Dialogs;
using UniTube.Helpers;
using UniTube.Services;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class ExtendedSplash
    {
        internal Rect splashImageRect;
        internal bool dismissed = false;

        private SplashScreen splash;
        private double ScaleFactor;

        public ExtendedSplash(SplashScreen splashScreen, bool loadState)
        {
            InitializeComponent();

            Window.Current.SizeChanged += Current_SizeChanged;

            ScaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            splash = splashScreen;

            if (splash != null)
            {
                splash.Dismissed += Splash_Dismissed;

                splashImageRect = splash.ImageLocation;
                PositionImage();
            }

            RestoreStateAsync(loadState);
        }

        private async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {
                string refreshToken = await ApplicationData.Current.RoamingSettings.ReadAsync<string>("RefreshToken");
                if (!refreshToken.IsNullOrEmpty())
                {
                    App.AuthInfo = await Authenticator.RefreshAccesTokenAsync(refreshToken, new ErrorHttpDialog());
                }

                string lastRootPage = await ApplicationData.Current.RoamingSettings.ReadAsync<string>("LastRootPage");
                string lastContentPage = await ApplicationData.Current.RoamingSettings.ReadAsync<string>("LastContentPage");
                NavigationService.SetMainPage(lastRootPage);
                NavigationService.Navigate(lastContentPage);
            }
        }

        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.Left);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Top);
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                extendedSplashImage.Height = splashImageRect.Height / ScaleFactor;
                extendedSplashImage.Width = splashImageRect.Width / ScaleFactor;
            }
            else
            {
                extendedSplashImage.Height = splashImageRect.Height;
                extendedSplashImage.Width = splashImageRect.Width;
            }
        }

        private async void Splash_Dismissed(SplashScreen sender, object args)
        {
            dismissed = true;

            bool initialized = await ApplicationData.Current.RoamingSettings.ReadAsync<bool>("Initialized");
            if (initialized)
            {
                string refreshToken = await ApplicationData.Current.RoamingSettings.ReadAsync<string>("RefreshToken");
                if (!refreshToken.IsNullOrEmpty())
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        App.AuthInfo = await Authenticator.RefreshAccesTokenAsync(refreshToken, new ErrorHttpDialog());
                    });
                }

                DismissExtendedSplash("MasterPage");
            }
            else
            {
                DismissExtendedSplash("WelcomePage");
            }
        }

        private async void DismissExtendedSplash(string pageName)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                NavigationService.SetMainPage(pageName);
            });
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }
    }
}
