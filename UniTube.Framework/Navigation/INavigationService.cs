using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;

namespace UniTube.Framework.Navigation
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        Type CurrentPageType { get; }
        object CurrentPageParam { get; }

        void ClearHistory();
        void GoBack(NavigationTransitionInfo infoOverride = null);
        void GoForward();
        bool Navigate(string pageToken, object parameter = null, NavigationTransitionInfo infoOverride = null);
        bool Navigate<T>(T key, object parameter = null, NavigationTransitionInfo infoOverride = null) where T : struct, IConvertible;
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
