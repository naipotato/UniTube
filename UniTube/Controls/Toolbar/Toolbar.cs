using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    [TemplateVisualState(GroupName = "AutoSuggestStates", Name = AutoSuggestBoxVisibleState)]
    [TemplateVisualState(GroupName = "AutoSuggestStates", Name = AutoSuggestBoxCollapsedState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeLargeState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeCompactClosedState)]
    [TemplateVisualState(GroupName = "ModeStates", Name = ModeCompactNotClosedState)]
    [TemplateVisualState(GroupName = "HeaderStates", Name = HeaderVisibleState)]
    [TemplateVisualState(GroupName = "HeaderStates", Name = HeaderCollapsedState)]
    [TemplateVisualState(GroupName = "TogglePaneStates", Name = TogglePaneButtonVisibleState)]
    [TemplateVisualState(GroupName = "TogglePaneStates", Name = TogglePaneButtonCollapsedState)]
    [TemplatePart(Name = AutoSuggestButtonPart, Type = typeof(ToolbarButton))]
    [TemplatePart(Name = AutoSuggestBoxPresenterPart, Type = typeof(ContentControl))]
    [TemplatePart(Name = PaneToggleButtonPart, Type = typeof(ToolbarButton))]
    public partial class Toolbar : ContentControl
    {
        private const string AutoSuggestBoxVisibleState = "AutoSuggestBoxVisible";
        private const string AutoSuggestBoxCollapsedState = "AutoSuggestBoxCollapsed";
        private const string ModeLargeState = "ModeLarge";
        private const string ModeCompactClosedState = "ModeCompactClosed";
        private const string ModeCompactNotClosedState = "ModeCompactNotClosed";
        private const string HeaderVisibleState = "HeaderVisible";
        private const string HeaderCollapsedState = "HeaderCollapsed";
        private const string AutoSuggestButtonPart = "AutoSuggestButton";
        private const string AutoSuggestBoxPresenterPart = "AutoSuggestBoxPresenter";
        private const string PaneToggleButtonPart = "PaneToggleButton";
        private const string TogglePaneButtonVisibleState = "TogglPaneButtonVisible";
        private const string TogglePaneButtonCollapsedState = "TogglePaneButtonCollapsed";

        private ToolbarButton _autoSuggestButton;
        private ContentControl _autoSuggestBoxPresenter;
        private ToolbarButton _paneToggleButton;

        public Toolbar()
        {
            DefaultStyleKey = typeof(Toolbar);
            SizeChanged += OnSizeChanged;
        }

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

        private void OnAutoSuggestButtonClick(object sender, RoutedEventArgs e)
        {
            IsAutoSuggestBoxActivated = true;
        }

        private void OnAutoSuggestBoxPresenterLostFocus(object sender, RoutedEventArgs e)
        {
            IsAutoSuggestBoxActivated = false;
        }

        private void OnPaneToggleButtonClick(object sender, RoutedEventArgs e)
        {
            PaneToggleButtonClick?.Invoke(this, new RoutedEventArgs());

            if (PaneToggleButtonCommand.CanExecute(null))
            {
                PaneToggleButtonCommand.Execute(null);
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty) return;

            IsAutoSuggestBoxActivated = false;

            if (e.NewSize.Width >= LargeModeThresholdWidth)
            {
                Mode = ToolbarMode.Large;
            }
            else
            {
                Mode = ToolbarMode.Compact;
            }
        }

        private void OnAutoSuggestBoxChanged(AutoSuggestBox newValue)
        {
            VisualStateManager.GoToState(this, AutoSuggestBox == null ? AutoSuggestBoxCollapsedState : AutoSuggestBoxVisibleState, false);
        }

        private void OnIsAutoSuggestBoxActivatedChanged(bool newValue)
        {
            UpdateMode(AlwaysShowHeader, newValue, Mode);
            if (newValue)
                AutoSuggestBoxPresenter.Focus(FocusState.Programmatic);
        }

        private void OnIsPaneToggleButtonVisibleChanged(bool newValue)
        {
            VisualStateManager.GoToState(this, newValue ? TogglePaneButtonVisibleState : TogglePaneButtonCollapsedState, false);
        }

        private void OnModeChanged(ToolbarMode newValue)
        {
            UpdateMode(AlwaysShowHeader, IsAutoSuggestBoxActivated, newValue);
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
