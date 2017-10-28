using Template10.Common;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    [TemplateVisualState(GroupName = "ActiveStates", Name = LoadingState)]
    [TemplateVisualState(GroupName = "ActiveStates", Name = NotLoadingState)]
    public partial class LoadingView : ContentControl
    {
        private const string LoadingState    = "Loading";
        private const string NotLoadingState  = "NotLoading";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingView"/> class.
        /// </summary>
        public LoadingView()
        {
            DefaultStyleKey = typeof(LoadingView);
            Loaded += OnLoadingViewLoaded;
            Unloaded += OnLoadingViewUnloaded;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (IsLoading)
                VisualStateManager.GoToState(this, LoadingState, true);
            else
                VisualStateManager.GoToState(this, NotLoadingState, true);
        }

        private void OnBootStrapperBackRequested(object sender, HandledEventArgs e)
        {
            if (IsLoading)
                e.Handled = true;
        }

        private void OnIsActiveChanged(bool newValue)
        {
            if (newValue)
                VisualStateManager.GoToState(this, LoadingState, true);
            else
                VisualStateManager.GoToState(this, NotLoadingState, true);
        }

        private void OnLoadingViewLoaded(object sender, RoutedEventArgs e)
            => BootStrapper.BackRequested += OnBootStrapperBackRequested;

        private void OnLoadingViewUnloaded(object sender, RoutedEventArgs e)
            => BootStrapper.BackRequested -= OnBootStrapperBackRequested;
    }
}
