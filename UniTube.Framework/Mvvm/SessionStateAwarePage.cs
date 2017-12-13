using System;
using System.Collections.Generic;
using UniTube.Framework.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Mvvm
{
    public class SessionStateAwarePage : Page
    {
        private string _pageKey;

        public static Func<IFrameFacade, IDictionary<string, object>> GetSessionStateForFrame { get; set; }

        protected virtual void LoadState(object navigationParameter, Dictionary<string, object> pageState) { }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var frameFacade = new FrameFacade(Frame);
            var frameState = GetSessionStateForFrame(frameFacade);
            var pageState = new Dictionary<string, object>();
            SaveState(pageState);
            frameState[_pageKey] = pageState;
        }

        protected override void OnNavigatedTo(NavigationEventArgs navigationEventArgs)
        {
            if (navigationEventArgs == null) throw new ArgumentNullException(nameof(navigationEventArgs));

            if (_pageKey != null) return;

            var frameFacade = new FrameFacade(Frame);
            var frameState = GetSessionStateForFrame(frameFacade);
            _pageKey = $"Page-{frameFacade.BackStackDepth}";

            if (navigationEventArgs.NavigationMode == NavigationMode.New)
            {
                var nextPageKey = _pageKey;
                var nextPageIndex = frameFacade.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = $"Page-{nextPageIndex}";
                }

                LoadState(navigationEventArgs.Parameter, null);
            }
            else
            {
                LoadState(navigationEventArgs.Parameter, (Dictionary<string, object>)frameState[_pageKey]);
            }
        }

        protected virtual void SaveState(Dictionary<string, object> pageState) { }
    }
}
