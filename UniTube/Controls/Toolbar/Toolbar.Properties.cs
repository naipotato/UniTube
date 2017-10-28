using System.Collections.Generic;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    public partial class Toolbar
    {
        /// <summary>
        /// Identifies the <see cref="Actions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions), typeof(IList<IToolbarElement>), typeof(Toolbar), new PropertyMetadata(new List<IToolbarElement>()));

        /// <summary>
        /// Identifies the <see cref="AlwaysShowHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlwaysShowHeaderProperty = DependencyProperty.Register(
            nameof(AlwaysShowHeader), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Identifies the <see cref="AutoSuggestBox"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AutoSuggestBoxProperty = DependencyProperty.Register(
            nameof(AutoSuggestBox), typeof(AutoSuggestBox), typeof(Toolbar), new PropertyMetadata(default(AutoSuggestBox), OnAutoSuggestBoxChanged));

        /// <summary>
        /// Identifies the <see cref="AutoSuggestBoxWidthProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AutoSuggestBoxWidthProperty = DependencyProperty.Register(
            nameof(AutoSuggestBoxWidth), typeof(double), typeof(Toolbar), new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="IsAutoSuggestBoxActivated"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAutoSuggestBoxActivatedProperty = DependencyProperty.Register(
            nameof(IsAutoSuggestBoxActivated), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool), OnIsAutoSuggestBoxActivatedChanged));

        /// <summary>
        /// Identifies the <see cref="IsPaneToggleButtonVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneToggleButtonVisibleProperty = DependencyProperty.Register(
            nameof(IsPaneToggleButtonVisible), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool), OnIsPaneToggleButtonVisibleChanged));

        /// <summary>
        /// Identifies the <see cref="IsSearchModeEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSearchModeEnabledProperty = DependencyProperty.Register(
            nameof(IsSearchModeEnabled), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool), OnIsSearchModeEnabledChanged));

        /// <summary>
        /// Identifies the <see cref="LargeModeThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LargeModeThresholdWidthProperty = DependencyProperty.Register(
            nameof(LargeModeThresholdWidth), typeof(double), typeof(Toolbar), new PropertyMetadata(default(double)));

        /// <summary>
        /// Identifies the <see cref="ModeProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            nameof(Mode), typeof(ToolbarMode), typeof(Toolbar), new PropertyMetadata(default(ToolbarMode), OnModeChanged));

        /// <summary>
        /// Identifies the <see cref="PaneToggleButtonCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneToggleButtonCommandProperty = DependencyProperty.Register(
            nameof(PaneToggleButtonCommand), typeof(ICommand), typeof(Toolbar), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Identifies the <see cref="PaneToggleButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneToggleButtonStyleProperty = DependencyProperty.Register(
            nameof(PaneToggleButtonStyle), typeof(Style), typeof(Toolbar), new PropertyMetadata(default(Style)));

        private ToolbarButton AutoSuggestButton
        {
            get => _autoSuggestButton;
            set
            {
                if (_autoSuggestButton != null)
                {
                    _autoSuggestButton.Click -= OnAutoSuggestButtonClick;
                }

                _autoSuggestButton = value;

                if (_autoSuggestButton != null)
                {
                    _autoSuggestButton.Click += OnAutoSuggestButtonClick;
                }
            }
        }

        private ContentControl AutoSuggestBoxPresenter
        {
            get => _autoSuggestBoxPresenter;
            set
            {
                if (_autoSuggestBoxPresenter != null)
                {
                    _autoSuggestBoxPresenter.LostFocus -= OnAutoSuggestBoxPresenterLostFocus;
                }

                _autoSuggestBoxPresenter = value;

                if (_autoSuggestBoxPresenter != null)
                {
                    _autoSuggestBoxPresenter.LostFocus += OnAutoSuggestBoxPresenterLostFocus;
                }
            }
        }

        private ToolbarButton PaneToggleButton
        {
            get => _paneToggleButton;
            set
            {
                if (_paneToggleButton != null)
                {
                    _paneToggleButton.Click -= OnPaneToggleButtonClick;
                }

                _paneToggleButton = value;

                if (_paneToggleButton != null)
                {
                    _paneToggleButton.Click += OnPaneToggleButtonClick;
                }
            }
        }

        /// <summary>
        /// Gets or sets a list of actions for the <see cref="Toolbar"/>.
        /// </summary>
        public IList<IToolbarElement> Actions
        {
            get => (IList<IToolbarElement>)GetValue(ActionsProperty);
            set => SetValue(ActionsProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the header is always visible.
        /// </summary>
        public bool AlwaysShowHeader
        {
            get => (bool)GetValue(AlwaysShowHeaderProperty);
            set => SetValue(AlwaysShowHeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Windows.UI.Xaml.Controls.AutoSuggestBox"/> used in <see cref="Toolbar"/>.
        /// </summary>
        public AutoSuggestBox AutoSuggestBox
        {
            get => (AutoSuggestBox)GetValue(AutoSuggestBoxProperty);
            set => SetValue(AutoSuggestBoxProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of <see cref="AutoSuggestBox"/>.
        /// </summary>
        public double AutoSuggestBoxWidth
        {
            get => (double)GetValue(AutoSuggestBoxWidthProperty);
            set => SetValue(AutoSuggestBoxWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether <see cref="AutoSuggestBox"/> has been activated.
        /// </summary>
        public bool IsAutoSuggestBoxActivated
        {
            get => (bool)GetValue(IsAutoSuggestBoxActivatedProperty);
            set => SetValue(IsAutoSuggestBoxActivatedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether pane toggle button is visible.
        /// </summary>
        public bool IsPaneToggleButtonVisible
        {
            get => (bool)GetValue(IsPaneToggleButtonVisibleProperty);
            set => SetValue(IsPaneToggleButtonVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the search mode is enabled.
        /// </summary>
        public bool IsSearchModeEnabled
        {
            get => (bool)GetValue(IsSearchModeEnabledProperty);
            set => SetValue(IsSearchModeEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the width threshold of the large mode.
        /// </summary>
        public double LargeModeThresholdWidth
        {
            get => (double)GetValue(LargeModeThresholdWidthProperty);
            set => SetValue(LargeModeThresholdWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the mode of the <see cref="Toolbar"/>.
        /// </summary>
        public ToolbarMode Mode
        {
            get => (ToolbarMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that is invoked when the pane toggle button is pressed.
        /// </summary>
        public ICommand PaneToggleButtonCommand
        {
            get => (ICommand)GetValue(PaneToggleButtonCommandProperty);
            set => SetValue(PaneToggleButtonCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the style that defines the appearance of the pane toggle button.
        /// </summary>
        public Style PaneToggleButtonStyle
        {
            get => (Style)GetValue(PaneToggleButtonStyleProperty);
            set => SetValue(PaneToggleButtonStyleProperty, value);
        }

        private static void OnAutoSuggestBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var toolbar = d as Toolbar;
            toolbar.OnAutoSuggestBoxChanged((AutoSuggestBox)e.NewValue);
        }

        private static void OnIsAutoSuggestBoxActivatedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var toolbar = d as Toolbar;
            toolbar.OnIsAutoSuggestBoxActivatedChanged((bool)e.NewValue);
        }

        private static void OnIsPaneToggleButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var toolbar = d as Toolbar;
            toolbar.OnIsPaneToggleButtonVisibleChanged((bool)e.NewValue);
        }

        private static void OnIsSearchModeEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var toolbar = d as Toolbar;
            toolbar.OnIsSearchModeEnabledChanged((bool)e.NewValue);
        }

        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var toolbar = d as Toolbar;
            toolbar.OnModeChanged((ToolbarMode)e.NewValue);
        }
    }
}
