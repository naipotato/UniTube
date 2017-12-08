using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Navigation
{
    public interface IFrameFacade
    {
        IReadOnlyList<PageStackEntry> BackStack { get; }
        int BackStackDepth { get; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        object Content { get; }
        object CurrentPageParam { get; }
        Type CurrentPageType { get; }

        event EventHandler<NavigatedToEventArgs> NavigatedTo;
        event EventHandler<NavigatingFromEventArgs> NavigatingFrom;

        void ClearBackStack();
        void ClearValue(DependencyProperty dependencyProperty);
        string GetNavigationState();
        object GetValue(DependencyProperty dependencyProperty);
        void GoBack(NavigationTransitionInfo infoOverride = null);
        void GoForward();
        bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null);
        bool RemoveBackStackEntry(PageStackEntry entry);
        void SetNavigationState(string navigationState);
        void SetValue(DependencyProperty dependencyProperty, object value);
    }
}