using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;

namespace UniTube.Framework.Navigation
{
    public interface INavigationService
    {
        Type CurrentPageType { get; }
        object CurrentPageParam { get; }

        bool CanGoBack();
        bool CanGoForward();
        void ClearHistory();
        void GoBack(NavigationTransitionInfo infoOverride = null);
        void GoForward(NavigationTransitionInfo infoOverride = null);
        IAsyncOperation<bool> NavigateAsync(string pageToken, object parameter = null, NavigationTransitionInfo infoOverride = null);
        IAsyncOperation<bool> NavigateAsync<T>(T key, object parameter = null, NavigationTransitionInfo infoOverride = null) where T : struct, IConvertible;
        void RemoveAllPages(string pageToken = null, object parameter = null);
        void RemoveAllPages<T>(T key, object parameter = null) where T : struct, IConvertible;
        void RemoveFirstPage(string pageToken = null, object parameter = null);
        void RemoveFirstPage<T>(T key, object parameter = null) where T : struct, IConvertible;
        void RemoveLastPage(string pageToken = null, object parameter = null);
        void RemoveLastPage<T>(T key, object parameter = null) where T : struct, IConvertible;
        void RestoreSavedNavigation();
        void Suspending();
    }
}
