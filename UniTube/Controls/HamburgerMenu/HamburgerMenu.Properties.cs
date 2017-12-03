using System;

using Template10.Services.NavigationService;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UniTube.Controls
{
    public partial class HamburgerMenu
    {
        /// <summary>
        /// Identifies the <see cref="CompactModeThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactModeThresholdWidthProperty = DependencyProperty.Register(
            nameof(CompactModeThresholdWidth), typeof(double), typeof(HamburgerMenu), new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="CompactPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(
            nameof(CompactPaneLength), typeof(double), typeof(HamburgerMenu), new PropertyMetadata(default(double), OnCompactPaneLengthChanged));

        /// <summary>
        /// Identifies the <see cref="DefaultPageType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultPageTypeProperty = DependencyProperty.Register(
            nameof(DefaultPageType), typeof(Type), typeof(HamburgerMenu), new PropertyMetadata(default(Type)));

        /// <summary>
        /// Identifies the <see cref="DisplayMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(
            nameof(DisplayMode), typeof(HamburgerMenuDisplayMode), typeof(HamburgerMenu), new PropertyMetadata(default(HamburgerMenuDisplayMode), OnDisplayModeChanged));

        /// <summary>
        /// Identifies the <see cref="ExpandedModeThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpandedModeThresholdWidthProperty = DependencyProperty.Register(
            nameof(ExpandedModeThresholdWidth), typeof(double), typeof(HamburgerMenu), new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="IsPaneOpen"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty = DependencyProperty.Register(
            nameof(IsPaneOpen), typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(default(bool), OnIsPaneOpenChanged));

        /// <summary>
        /// Identifies the <see cref="OpenPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OpenPaneLengthProperty = DependencyProperty.Register(
            nameof(OpenPaneLength), typeof(double), typeof(HamburgerMenu), new PropertyMetadata(default(double), OnOpenPaneLengthChanged));

        /// <summary>
        /// Identifies the <see cref="PaneBackground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneBackgroundProperty = DependencyProperty.Register(
            nameof(PaneBackground), typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the <see cref="TemplateSettings"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TemplateSettingsProperty = DependencyProperty.Register(
            nameof(TemplateSettingsProperty), typeof(HamburgerMenuTemplateSettings), typeof(HamburgerMenu), new PropertyMetadata(default(HamburgerMenuTemplateSettings)));

        private FrameFacade FrameFacade
        {
            get => _frameFacade;
            set
            {
                if (_frameFacade != null)
                {
                    _frameFacade.BackRequested -= OnFrameFacadeBackRequested;
                    _frameFacade.ForwardRequested -= OnFrameFacadeForwardRequested;
                    _frameFacade.Navigated -= OnFrameFacadeNavigated;
                }

                _frameFacade = value;

                if (_frameFacade != null)
                {
                    _frameFacade.BackRequested += OnFrameFacadeBackRequested;
                    _frameFacade.ForwardRequested += OnFrameFacadeForwardRequested;
                    _frameFacade.Navigated += OnFrameFacadeNavigated;
                }
            }
        }

        private Rectangle LightDismissLayer
        {
            get => _lightDismissLayer;
            set
            {
                if (_lightDismissLayer != null)
                {
                    _lightDismissLayer.Tapped -= OnLigthDismissLayerTapped;
                }

                _lightDismissLayer = value;

                if (_lightDismissLayer != null)
                {
                    _lightDismissLayer.Tapped += OnLigthDismissLayerTapped;
                }
            }
        }

        private Rectangle PanArea
        {
            get => _panArea;
            set
            {
                if (_panArea != null)
                {
                    _panArea.ManipulationCompleted -= OnPanAreaManipulationCompleted;
                    _panArea.Tapped -= OnLigthDismissLayerTapped;
                }

                _panArea = value;

                if (_panArea != null)
                {
                    _panArea.ManipulationCompleted += OnPanAreaManipulationCompleted;
                    _panArea.Tapped += OnLigthDismissLayerTapped;
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum window width at which the <see cref="HamburgerMenu"/> enters Compact
        /// display mode.
        /// </summary>
        public double CompactModeThresholdWidth
        {
            get => (double)GetValue(CompactModeThresholdWidthProperty);
            set => SetValue(CompactModeThresholdWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="HamburgerMenu"/> pane in its compact display mode.
        /// </summary>
        public double CompactPaneLength
        {
            get => (double)GetValue(CompactPaneLengthProperty);
            set => SetValue(CompactPaneLengthProperty, value);
        }

        public Type DefaultPageType
        {
            get => (Type)GetValue(DefaultPageTypeProperty);
            set => SetValue(DefaultPageTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that specifies how the pane and content areas of a <see cref="HamburgerMenu"/>
        /// are shown.
        /// </summary>
        public HamburgerMenuDisplayMode DisplayMode
        {
            get => (HamburgerMenuDisplayMode)GetValue(DisplayModeProperty);
            set => SetValue(DisplayModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum window width at which the <see cref="HamburgerMenu"/> enters Expanded
        /// display mode.
        /// </summary>
        public double ExpandedModeThresholdWidth
        {
            get => (double)GetValue(ExpandedModeThresholdWidthProperty);
            set => SetValue(ExpandedModeThresholdWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the <see cref="HamburgerMenu"/> pane is expanded to its
        /// full width.
        /// </summary>
        public bool IsPaneOpen
        {
            get => (bool)GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        public INavigationService NavigationService => _navigationService;

        /// <summary>
        /// Gets or sets the width of the <see cref="HamburgerMenu"/> pane when it's fully expanded.
        /// </summary>
        public double OpenPaneLength
        {
            get => (double)GetValue(OpenPaneLengthProperty);
            set => SetValue(OpenPaneLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the background of the pane.
        /// </summary>
        public Brush PaneBackground
        {
            get => (Brush)GetValue(PaneBackgroundProperty);
            set => SetValue(PaneBackgroundProperty, value);
        }

        /// <summary>
        /// Gets an object that provides calculated values that can be referenced as TemplateBinding sources when
        /// defining templates for a <see cref="HamburgerMenu"/> control.
        /// </summary>
        public HamburgerMenuTemplateSettings TemplateSettings
        {
            get => (HamburgerMenuTemplateSettings)GetValue(TemplateSettingsProperty);
            internal set => SetValue(TemplateSettingsProperty, value);
        }

        private static void OnCompactPaneLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var hamburgerMenu = d as HamburgerMenu;
            hamburgerMenu.OnCompactPaneLengthChanged((double)e.NewValue);
        }

        private static void OnDisplayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var hamburgerMenu = d as HamburgerMenu;
            hamburgerMenu.OnDisplayModeChanged((HamburgerMenuDisplayMode)e.NewValue);
        }

        private static void OnIsPaneOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var hamburgerMenu = d as HamburgerMenu;
            hamburgerMenu.OnIsPaneOpenChanged((bool)e.NewValue);
        }

        private static void OnOpenPaneLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var hamburgerMenu = d as HamburgerMenu;
            hamburgerMenu.OnOpenPaneLengthChanged((double)e.NewValue);
        }
    }
}
