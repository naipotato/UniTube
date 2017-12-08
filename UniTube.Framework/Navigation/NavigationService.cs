using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniTube.Framework.AppModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Navigation
{
    public class NavigationService : INavigationService
    {
        private IFrameFacade _frame;
        private readonly Func<string, Type> _navigationResolver;
        private readonly ISessionStateService _sessionStateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <param name="resolver">The navigation resolver.</param>
        public NavigationService(IFrameFacade frame, ISessionStateService sessionStateService, Func<string, Type> resolver = null)
        {
            _frame = frame;
            _navigationResolver = resolver;
            _sessionStateService = sessionStateService;

            if (frame != null)
            {
                _frame.NavigatingFrom += OnFrameNavigatingFrom;
                _frame.NavigatedTo += OnFrameNavigatedTo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type CurrentPageType => _frame.CurrentPageType;

        /// <summary>
        /// 
        /// </summary>
        public object CurrentPageParam => _frame.CurrentPageParam;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack() => _frame.CanGoBack();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanGoForward() => _frame.CanGoForward();

        /// <summary>
        /// 
        /// </summary>
        public void ClearHistory() => _frame.ClearBackStack();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoOverride"></param>
        public void GoBack(NavigationTransitionInfo infoOverride = null) => _frame.GoBack(infoOverride);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoOverride"></param>
        public void GoForward(NavigationTransitionInfo infoOverride = null) => _frame.GoForward(infoOverride);

        /// <summary>
        /// Navigate to the page with the specified key, passing the specified parameter.
        /// </summary>
        /// <typeparam name="T">An enum that identifies the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="infoOverride">The navigation transition.</param>
        /// <returns><c>true</c> if the navigation was successful; otherwise, <c>false</c>.</returns>
        public IAsyncOperation<bool> NavigateAsync<T>(T key, object parameter = null, NavigationTransitionInfo infoOverride = null)
            where T : struct, IConvertible => Task.Run(() =>
            {
                var keys = MvvmApplication.Current.PageKeys<T>();

                if (!keys.ContainsKey(key))
                    throw new KeyNotFoundException(key.ToString());

                var page = keys[key];

                if ((page.FullName == CurrentPageType?.FullName) && (parameter == CurrentPageParam))
                    return false;

                if ((page.FullName == CurrentPageType?.FullName) && (parameter?.Equals(CurrentPageParam) ?? false))
                    return false;

                return _frame.Navigate(page, parameter, infoOverride);
            }).AsAsyncOperation();

        /// <summary>
        /// Navigate to the page with the specified page token, passing the specified parameter.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="infoOverride">The navigation transition.</param>
        /// <returns><c>true</c> if the navigation was successful; otherwise, <c>false</c>.</returns>
        public IAsyncOperation<bool> NavigateAsync(string pageToken, object parameter = null, NavigationTransitionInfo infoOverride = null)
            => Task.Run(() =>
            {
                if (_navigationResolver == null)
                    throw new InvalidOperationException();

                var pageType = _navigationResolver(pageToken);

                if (pageType == null)
                {
                    throw new ArgumentException("Unable to resolve", nameof(pageToken));
                }

                if ((pageType.FullName == CurrentPageType?.FullName) && (parameter == CurrentPageParam))
                    return false;

                if ((pageType.FullName == CurrentPageType?.FullName) && (parameter?.Equals(CurrentPageParam) ?? false))
                    return false;

                return _frame.Navigate(pageType, parameter, infoOverride);
            }).AsAsyncOperation();

        private void OnFrameNavigatedTo(object sender, NavigatedToEventArgs e) => NavigateToCurrentViewModel(e);

        private void OnFrameNavigatingFrom(object sender, NavigatingFromEventArgs e)
            => NavigateFromCurrentViewModel(e, false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="parameter"></param>
        public void RemoveAllPages(string pageToken = null, object parameter = null)
        {
            if (pageToken != null)
            {
                IEnumerable<PageStackEntry> pages;
                var pageType = _navigationResolver(pageToken);
                if (parameter != null)
                {
                    pages = _frame.BackStack.Where((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
                }
                else
                {
                    pages = _frame.BackStack.Where((x) => x.SourcePageType == pageType);
                }

                foreach (var page in pages)
                {
                    _frame.RemoveBackStackEntry(page);
                }
            }
            else
            {
                _frame.ClearBackStack();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        public void RemoveAllPages<T>(T key, object parameter = null) where T : struct, IConvertible
        {
            IEnumerable<PageStackEntry> pages;

            var keys = MvvmApplication.Current.PageKeys<T>();

            if (!keys.ContainsKey(key))
                throw new KeyNotFoundException(key.ToString());

            var pageType = keys[key];

            if (parameter != null)
            {
                pages = _frame.BackStack.Where((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
            }
            else
            {
                pages = _frame.BackStack.Where((x) => x.SourcePageType == pageType);
            }

            foreach (var page in pages)
            {
                _frame.RemoveBackStackEntry(page);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="parameter"></param>
        public void RemoveFirstPage(string pageToken = null, object parameter = null)
        {
            PageStackEntry page;
            if (pageToken != null)
            {
                var pageType = _navigationResolver(pageToken);
                if (parameter != null)
                {
                    page = _frame.BackStack.FirstOrDefault((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
                }
                else
                {
                    page = _frame.BackStack.FirstOrDefault((x) => x.SourcePageType == pageType);
                }
            }
            else
            {
                page = _frame.BackStack.FirstOrDefault();
            }

            if (page != null)
            {
                _frame.RemoveBackStackEntry(page);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        public void RemoveFirstPage<T>(T key, object parameter = null) where T : struct, IConvertible
        {
            PageStackEntry page;

            var keys = MvvmApplication.Current.PageKeys<T>();

            if (!keys.ContainsKey(key))
                throw new KeyNotFoundException(key.ToString());

            var pageType = keys[key];
            
            if (parameter != null)
            {
                page = _frame.BackStack.FirstOrDefault((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
            }
            else
            {
                page = _frame.BackStack.FirstOrDefault((x) => x.SourcePageType == pageType);
            }

            if (page != null)
            {
                _frame.RemoveBackStackEntry(page);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="parameter"></param>
        public void RemoveLastPage(string pageToken = null, object parameter = null)
        {
            PageStackEntry page;
            if (pageToken != null)
            {
                var pageType = _navigationResolver(pageToken);
                if (parameter != null)
                {
                    page = _frame.BackStack.LastOrDefault((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
                }
                else
                {
                    page = _frame.BackStack.LastOrDefault((x) => x.SourcePageType == pageType);
                }
            }
            else
            {
                page = _frame.BackStack.LastOrDefault();
            }

            if (page != null)
            {
                _frame.RemoveBackStackEntry(page);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        public void RemoveLastPage<T>(T key, object parameter = null) where T : struct, IConvertible
        {
            PageStackEntry page;

            var keys = MvvmApplication.Current.PageKeys<T>();

            if (!keys.ContainsKey(key))
                throw new KeyNotFoundException(key.ToString());

            var pageType = keys[key];

            if (parameter != null)
            {
                page = _frame.BackStack.LastOrDefault((x) => x.SourcePageType == pageType && x.Parameter.Equals(parameter));
            }
            else
            {
                page = _frame.BackStack.LastOrDefault((x) => x.SourcePageType == pageType);
            }

            if (page != null)
            {
                _frame.RemoveBackStackEntry(page);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RestoreSavedNavigation()
        {
            NavigateToCurrentViewModel(new NavigatedToEventArgs()
            {
                NavigationMode = NavigationMode.Refresh,
                Parameter = CurrentPageParam
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Suspending()
        {
            NavigateFromCurrentViewModel(new NavigatingFromEventArgs(), true);
        }

        private void NavigateToCurrentViewModel(NavigatedToEventArgs e)
        {
            var frameState = _sessionStateService.GetSessionStateForFrame(_frame);
            var viewModelKey = $"ViewModel-{_frame.BackStackDepth}";
            
            if (e.NavigationMode == NavigationMode.New)
            {
                var nextViewModelKey = viewModelKey;
                var nextViewModelIndex = _frame.BackStackDepth;
                while (frameState.Remove(nextViewModelKey))
                {
                    nextViewModelIndex++;
                    nextViewModelKey = $"ViewModel-{nextViewModelIndex}";
                }
            }

            var newView = _frame.Content as FrameworkElement;
            if (newView == null) return;
            var newViewModel = newView.DataContext as INavigationAware;
            if (newViewModel != null)
            {
                Dictionary<string, object> viewModelState;
                if (frameState.ContainsKey(viewModelKey))
                {
                    viewModelState = frameState[viewModelKey] as Dictionary<string, object>;
                }
                else
                {
                    viewModelState = new Dictionary<string, object>();
                }
                newViewModel.OnNavigatedTo(e, viewModelState);
                frameState[viewModelKey] = viewModelState;
            }
        }

        private void NavigateFromCurrentViewModel(NavigatingFromEventArgs e, bool suspending)
        {
            var departingView = _frame.Content as FrameworkElement;
            if (departingView == null) return;
            var frameState = _sessionStateService.GetSessionStateForFrame(_frame);
            var departingViewModel = departingView.DataContext as INavigationAware;

            var viewModelKey = $"ViewModel-{_frame.BackStackDepth}";
            if (departingViewModel != null)
            {
                var viewModelState = frameState.ContainsKey(viewModelKey) ? frameState[viewModelKey] as Dictionary<string, object> : null;

                departingViewModel.OnNavigatingFrom(e, viewModelState, suspending);
            }
        }
    }
}
