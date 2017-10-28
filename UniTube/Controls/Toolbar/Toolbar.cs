using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    [TemplateVisualState(GroupName = "AutoSuggestStates", Name = AutoSuggestBoxVisibleState)]
    [TemplateVisualState(GroupName = "AutoSuggestStates", Name = AutoSuggestBoxCollapsedState)]
    [TemplateVisualState(GroupName = "HeaderStates", Name = HeaderVisibleState)]
    [TemplateVisualState(GroupName = "HeaderStates", Name = HeaderCollapsedState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeLargeState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeCompactClosedState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeCompactNotClosedState)]
    [TemplateVisualState(GroupName = "TogglePaneStates", Name = TogglePaneButtonVisibleState)]
    [TemplateVisualState(GroupName = "TogglePaneStates", Name = TogglePaneButtonCollapsedState)]
    [TemplatePart(Name = AutoSuggestButtonPart, Type = typeof(ToolbarButton))]
    [TemplatePart(Name = AutoSuggestBoxPresenterPart, Type = typeof(ContentControl))]
    [TemplatePart(Name = PaneToggleButtonPart, Type = typeof(ToolbarButton))]
    public partial class Toolbar : ContentControl
    {
        private const string AutoSuggestBoxCollapsedState   = "AutoSuggestBoxCollapsed";
        private const string AutoSuggestBoxVisibleState     = "AutoSuggestBoxVisible";
        private const string HeaderCollapsedState           = "HeaderCollapsed";
        private const string HeaderVisibleState             = "HeaderVisible";
        private const string ModeCompactClosedState         = "ModeCompactClosed";
        private const string ModeCompactNotClosedState      = "ModeCompactNotClosed";
        private const string ModeLargeState                 = "ModeLarge";
        private const string TogglePaneButtonCollapsedState = "TogglePaneButtonCollapsed";
        private const string TogglePaneButtonVisibleState   = "TogglePaneButtonVisible";
        private const string AutoSuggestBoxPresenterPart    = "AutoSuggestBoxPresenter";
        private const string AutoSuggestButtonPart          = "AutoSuggestButton";
        private const string PaneToggleButtonPart           = "PaneToggleButton";

        private ToolbarButton   _autoSuggestButton;
        private ContentControl  _autoSuggestBoxPresenter;
        private ToolbarButton   _paneToggleButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="Toolbar"/> class.
        /// </summary>
        public Toolbar()
        {
            DefaultStyleKey = typeof(Toolbar);
            SizeChanged += OnSizeChanged;
        }

        /// <summary>
        /// Occurs when you click the pane toggle button.
        /// </summary>
        public event RoutedEventHandler PaneToggleButtonClick;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AutoSuggestButton = GetTemplateChild(AutoSuggestButtonPart) as ToolbarButton;
            AutoSuggestBoxPresenter = GetTemplateChild(AutoSuggestBoxPresenterPart) as ContentControl;
            PaneToggleButton = GetTemplateChild(PaneToggleButtonPart) as ToolbarButton;

            OnAutoSuggestBoxChanged(AutoSuggestBox);
            OnIsPaneToggleButtonVisibleChanged(IsPaneToggleButtonVisible);
        }

        private void OnAutoSuggestButtonClick(object sender, RoutedEventArgs e) => IsAutoSuggestBoxActivated = true;

        private void OnAutoSuggestBoxChanged(AutoSuggestBox newValue)
            => VisualStateManager.GoToState(this, AutoSuggestBox == null ? AutoSuggestBoxCollapsedState : AutoSuggestBoxVisibleState, false);

        private void OnAutoSuggestBoxPresenterLostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsSearchModeEnabled)
                IsAutoSuggestBoxActivated = false;
        }

        private void OnIsAutoSuggestBoxActivatedChanged(bool newValue)
        {
            UpdateMode(AlwaysShowHeader, newValue, Mode);
            if (newValue)
                AutoSuggestBoxPresenter.Focus(FocusState.Programmatic);
        }

        private void OnModeChanged(ToolbarMode newValue)
            => UpdateMode(AlwaysShowHeader, IsAutoSuggestBoxActivated, newValue);

        private void OnIsPaneToggleButtonVisibleChanged(bool newValue)
            => VisualStateManager.GoToState(this, newValue ? TogglePaneButtonVisibleState : TogglePaneButtonCollapsedState, false);

        private void OnIsSearchModeEnabledChanged(bool newValue) => IsAutoSuggestBoxActivated = newValue;

        private void OnPaneToggleButtonClick(object sender, RoutedEventArgs e)
        {
            PaneToggleButtonClick?.Invoke(this, new RoutedEventArgs());

            if ((PaneToggleButtonCommand?.CanExecute(null)).GetValueOrDefault())
                PaneToggleButtonCommand?.Execute(null);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty) return;

            IsAutoSuggestBoxActivated = IsSearchModeEnabled;

            if (e.NewSize.Width >= LargeModeThresholdWidth)
                Mode = ToolbarMode.Large;
            else
                Mode = ToolbarMode.Compact;
        }

        private void UpdateMode(bool alwaysShowHeader, bool isAutoSuggestBoxActivated, ToolbarMode mode)
        {
            switch (mode)
            {
                case ToolbarMode.Compact:
                    if (isAutoSuggestBoxActivated)
                        VisualStateManager.GoToState(this, ModeCompactNotClosedState, false);
                    else
                        VisualStateManager.GoToState(this, ModeCompactClosedState, false);

                    VisualStateManager.GoToState(this, HeaderVisibleState, false);
                    break;
                case ToolbarMode.Large:
                    VisualStateManager.GoToState(this, ModeLargeState, false);

                    if (alwaysShowHeader)
                        VisualStateManager.GoToState(this, HeaderVisibleState, false);
                    else
                        VisualStateManager.GoToState(this, HeaderCollapsedState, false);
                    break;
                default:
                    break;
            }
        }
    }
}
