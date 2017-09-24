using System;
using UniTube.Views;
using Windows.UI.Xaml.Media.Animation;

namespace UniTube.Services
{
    public static class NavigationService
    {
        public static void Navigate(string pageName)
        {
            switch (pageName)
            {
                default:
                    break;
            }
        }

        public static void SetMainPage(string pageName, NavigationTransitionInfo infoOverride = null)
        {
            switch (pageName)
            {
                case "MasterPage":
                    if (App.RootFrame.SourcePageType == typeof(MasterPage)) break;
                    App.RootFrame.Navigate(typeof(MasterPage), null, infoOverride);
                    break;
                case "WelcomePage":
                    if (App.RootFrame.SourcePageType == typeof(WelcomePage)) break;
                    App.RootFrame.Navigate(typeof(WelcomePage), null, infoOverride);
                    break;
                default:
                    break;
            }
        }
    }
}
