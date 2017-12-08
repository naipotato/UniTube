using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Navigation
{
    public class FrameFacade : IFrameFacade
    {
        private readonly Frame _frame;

        public FrameFacade(Frame frame)
        {
            _frame = frame;

            if (_frame != null)
            {
                _frame.Navigated += OnFrameNavigated;
                _frame.Navigating += OnFrameNavigating;
            }

            var t = new NavigationThemeTransition
            {
                DefaultNavigationTransitionInfo = new EntranceNavigationTransitionInfo()
            };
            _frame.ContentTransitions = new TransitionCollection();
            _frame.ContentTransitions.Add(t);
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<PageStackEntry> BackStack => _frame.BackStack.ToList();

        /// <summary>
        /// 
        /// </summary>
        public int BackStackDepth => _frame.BackStackDepth;

        /// <summary>
        /// 
        /// </summary>
        public bool CanGoBack => _frame.CanGoBack;

        /// <summary>
        /// 
        /// </summary>
        public bool CanGoForward => _frame.CanGoForward;

        /// <summary>
        /// 
        /// </summary>
        public object Content
        {
            get => _frame.Content;
            set => _frame.Content = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public object CurrentPageParam { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Type CurrentPageType { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NavigatedToEventArgs> NavigatedTo;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NavigatingFromEventArgs> NavigatingFrom;

        /// <summary>
        /// 
        /// </summary>
        public void ClearBackStack() => _frame.BackStack.Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyProperty"></param>
        public void ClearValue(DependencyProperty dependencyProperty) => _frame.ClearValue(dependencyProperty);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNavigationState() => _frame.GetNavigationState();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyProperty"></param>
        /// <returns></returns>
        public object GetValue(DependencyProperty dependencyProperty) => _frame.GetValue(dependencyProperty);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoOverride"></param>
        public void GoBack(NavigationTransitionInfo infoOverride = null)
        {
            if (CanGoBack)
            {
                if (infoOverride == null) _frame.GoBack();
                else _frame.GoBack(infoOverride);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoForward()
        {
            if (CanGoForward) _frame.GoForward();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="parameter"></param>
        /// <param name="infoOverride"></param>
        /// <returns></returns>
        public bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (_frame.Navigate(pageType, parameter, infoOverride))
            {
                return pageType.Equals(_frame.Content?.GetType());
            }
            else
            {
                return false;
            }
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            CurrentPageType = e.SourcePageType;
            CurrentPageParam = e.Parameter;

            NavigatedTo?.Invoke(this, new NavigatedToEventArgs(e));
        }

        private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
            => NavigatingFrom?.Invoke(this, new NavigatingFromEventArgs(e));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool RemoveBackStackEntry(PageStackEntry entry) => _frame.BackStack.Remove(entry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationState"></param>
        public void SetNavigationState(string navigationState) => _frame.SetNavigationState(navigationState);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyProperty"></param>
        /// <param name="value"></param>
        public void SetValue(DependencyProperty dependencyProperty, object value)
            => _frame.SetValue(dependencyProperty, value);
    }
}
