using System;

using Template10.Common;
using Template10.Services.NavigationService;

using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UniTube.Controls
{
    /// <summary>
    /// Represents a container that enables navigation of app content. It has a menu pane for navigation commands.
    /// </summary>
    [TemplateVisualState(GroupName = "DisplayModeStates",   Name = ClosedCompactState)]
    [TemplateVisualState(GroupName = "DisplayModeStates",   Name = ClosedMinimalState)]
    [TemplateVisualState(GroupName = "DisplayModeStates",   Name = OpenCompactState)]
    [TemplateVisualState(GroupName = "DisplayModeStates",   Name = OpenExpandedState)]
    [TemplateVisualState(GroupName = "DisplayModeStates",   Name = OpenMinimalState)]
    [TemplatePart(Name = PaneClipRectanglePart, Type = typeof(RectangleGeometry))]
    [TemplatePart(Name = LightDismissLayerPart, Type = typeof(Rectangle))]
    [TemplatePart(Name = PanAreaPart,           Type = typeof(Rectangle))]
    [TemplatePart(Name = ContentFramePart,      Type = typeof(Frame))]
    public partial class HamburgerMenu : ContentControl
    {
        private const string ClosedCompactState     = "ClosedCompact";
        private const string ClosedMinimalState     = "ClosedMinimal";
        private const string OpenCompactState       = "OpenCompact";
        private const string OpenExpandedState      = "OpenExpanded";
        private const string OpenMinimalState       = "OpenMinimal";
        private const string PaneClipRectanglePart  = "PaneClipRectangle";
        private const string LightDismissLayerPart  = "LightDismissLayer";
        private const string PanAreaPart            = "PanArea";
        private const string ContentFramePart       = "ContentFrame";

        private Frame               _contentFrame;
        private FrameFacade         _frameFacade;
        private Rectangle           _lightDismissLayer;
        private INavigationService  _navigationService;
        private Rectangle           _panArea;

        /// <summary>
        /// Initializes a new instance of the <see cref="HamburgerMenu"/> class.
        /// </summary>
        public HamburgerMenu()
        {
            DefaultStyleKey = typeof(HamburgerMenu);

            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        /// Occurs when the <see cref="DisplayMode"/> property changes.
        /// </summary>
        public event TypedEventHandler<HamburgerMenu, HamburgerMenuDisplayModeChangedEventArgs> DisplayModeChanged;

        /// <summary>
        /// Occurs when the content in which it is navigated is found and available, even if it has not finished loading.
        /// </summary>
        public event TypedEventHandler<HamburgerMenu, NavigatedEventArgs> Navigated;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TemplateSettings = new HamburgerMenuTemplateSettings(CompactPaneLength, OpenPaneLength);

            UpdateDisplayMode(DisplayMode, IsPaneOpen);

            LightDismissLayer = (Rectangle)GetTemplateChild(LightDismissLayerPart);
            PanArea = (Rectangle)GetTemplateChild(PanAreaPart);
            _contentFrame = (Frame)GetTemplateChild(ContentFramePart);

            if (_contentFrame != null)
            {
                _navigationService = BootStrapper.Current.NavigationServiceFactory(BootStrapper.BackButton.Attach, BootStrapper.ExistingContent.Exclude, _contentFrame);
                FrameFacade = _navigationService.FrameFacade;

                if (DefaultPageType != null)
                    _navigationService.Navigate(DefaultPageType);
            }
        }

        private void OnBootStrapperBackRequested(object sender, HandledEventArgs e)
        {
            if (IsPaneOpen && DisplayMode != HamburgerMenuDisplayMode.Expanded)
            {
                e.Handled = true;
                IsPaneOpen = false;
            }
        }

        private void OnCompactPaneLengthChanged(double newValue) => TemplateSettings = new HamburgerMenuTemplateSettings(newValue, OpenPaneLength);

        private void OnDisplayModeChanged(HamburgerMenuDisplayMode newValue)
        {
            DisplayModeChanged?.Invoke(this, new HamburgerMenuDisplayModeChangedEventArgs { DisplayMode = newValue });
            UpdateDisplayMode(newValue, IsPaneOpen);
        }

        private void OnFrameFacadeBackRequested(object sender, HandledEventArgs e)
        {
            if (_navigationService.CanGoBack)
            {
                e.Handled = true;
                _navigationService.GoBack();
            }
        }

        private void OnFrameFacadeForwardRequested(object sender, HandledEventArgs e)
        {
            if (_navigationService.CanGoForward)
            {
                e.Handled = true;
                _navigationService.GoForward();
            }
        }

        private void OnFrameFacadeNavigated(object sender, NavigatedEventArgs e) => Navigated?.Invoke(this, e);

        private void OnIsPaneOpenChanged(bool newValue) => UpdateDisplayMode(DisplayMode, newValue);

        private void OnLigthDismissLayerTapped(object sender, TappedRoutedEventArgs e) => IsPaneOpen = false;

        private void OnLoaded(object sender, RoutedEventArgs e) => BootStrapper.BackRequested += OnBootStrapperBackRequested;

        private void OnOpenPaneLengthChanged(double newValue)
            => TemplateSettings = new HamburgerMenuTemplateSettings(CompactPaneLength, newValue);

        private void OnPanAreaManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var x = e.Velocities.Linear.X;

            if (x <= -0.1)
                IsPaneOpen = false;
            else if (x > -0.1 && x < 0.1)
                IsPaneOpen = !(Math.Abs(e.Position.X) > Math.Abs(TemplateSettings.OpenPaneLength) / 2);
            else
                IsPaneOpen = true;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty)
                return;

            if (e.NewSize.Width >= ExpandedModeThresholdWidth)
            {
                IsPaneOpen = true;
                DisplayMode = HamburgerMenuDisplayMode.Expanded;
            }
            else if (e.NewSize.Width >= CompactModeThresholdWidth)
            {
                IsPaneOpen = false;
                DisplayMode = HamburgerMenuDisplayMode.Compact;
            }
            else
            {
                IsPaneOpen = false;
                DisplayMode = HamburgerMenuDisplayMode.Minimal;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) => BootStrapper.BackRequested -= OnBootStrapperBackRequested;

        private INavigable ResolveForPage(Page page)
        {
            if (!(page.DataContext is INavigable) | page.DataContext == null)
            {
                var viewModel = BootStrapper.Current.ResolveForPage(page, _navigationService as NavigationService);
                if (viewModel != null)
                {
                    page.DataContext = viewModel;
                    return viewModel;
                }
            }
            return page.DataContext as INavigable;
        }

        private void UpdateDisplayMode(HamburgerMenuDisplayMode displayMode, bool isPaneOpen)
        {
            switch (displayMode)
            {
                case HamburgerMenuDisplayMode.Compact:
                    if (isPaneOpen)
                        VisualStateManager.GoToState(this, OpenCompactState, true);
                    else
                        VisualStateManager.GoToState(this, ClosedCompactState, true);
                    break;
                case HamburgerMenuDisplayMode.Expanded:
                    if (isPaneOpen)
                        VisualStateManager.GoToState(this, OpenExpandedState, true);
                    else
                        VisualStateManager.GoToState(this, ClosedCompactState, true);
                    break;
                case HamburgerMenuDisplayMode.Minimal:
                    if (isPaneOpen)
                        VisualStateManager.GoToState(this, OpenMinimalState, true);
                    else
                        VisualStateManager.GoToState(this, ClosedMinimalState, true);
                    break;
                default:
                    break;
            }
        }
    }
}
