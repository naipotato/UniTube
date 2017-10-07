using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    public partial class Toolbar
    {
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            nameof(Actions), typeof(IList<IToolbarElement>), typeof(Toolbar), new PropertyMetadata(new List<IToolbarElement>()));

        public static readonly DependencyProperty AlwaysShowHeaderProperty = DependencyProperty.Register(
            nameof(AlwaysShowHeader), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty AutoSuggestBoxProperty = DependencyProperty.Register(
            nameof(AutoSuggestBox), typeof(AutoSuggestBox), typeof(Toolbar), new PropertyMetadata(default(AutoSuggestBox), OnAutoSuggestBoxChanged));

        public static readonly DependencyProperty AutoSuggestBoxWidthProperty = DependencyProperty.Register(
            nameof(AutoSuggestBoxWidth), typeof(double), typeof(Toolbar), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty IsAutoSuggestBoxActivatedProperty = DependencyProperty.Register(
            nameof(IsAutoSuggestBoxActivated), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool), OnIsAutoSuggestBoxActivatedChanged));

        public static readonly DependencyProperty IsPaneToggleButtonVisibleProperty = DependencyProperty.Register(
            nameof(IsPaneToggleButtonVisible), typeof(bool), typeof(Toolbar), new PropertyMetadata(default(bool), OnIsPaneToggleButtonVisibleChanged));

        public static readonly DependencyProperty LargeModeThresholdWidthProperty = DependencyProperty.Register(
            nameof(LargeModeThresholdWidth), typeof(double), typeof(Toolbar), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            nameof(Mode), typeof(ToolbarMode), typeof(Toolbar), new PropertyMetadata(default(ToolbarMode), OnModeChanged));

        public static readonly DependencyProperty PaneToggleButtonCommandProperty = DependencyProperty.Register(
            nameof(PaneToggleButtonCommand), typeof(ICommand), typeof(Toolbar), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty PaneToggleButtonStyleProperty = DependencyProperty.Register(
            nameof(PaneToggleButtonStyle), typeof(Style), typeof(Toolbar), new PropertyMetadata(default(Style)));

        private ToolbarButton AutoSuggestButton
        {
            get { return _autoSuggestButton; }
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
            get { return _autoSuggestBoxPresenter; }
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
            get { return _paneToggleButton; }
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

        public IList<IToolbarElement> Actions
        {
            get { return (IList<IToolbarElement>)GetValue(ActionsProperty); }
            set { SetValue(ActionsProperty, value); }
        }

        public bool AlwaysShowHeader
        {
            get { return (bool)GetValue(AlwaysShowHeaderProperty); }
            set { SetValue(AlwaysShowHeaderProperty, value); }
        }

        public AutoSuggestBox AutoSuggestBox
        {
            get { return (AutoSuggestBox)GetValue(AutoSuggestBoxProperty); }
            set { SetValue(AutoSuggestBoxProperty, value); }
        }

        public double AutoSuggestBoxWidth
        {
            get { return (double)GetValue(AutoSuggestBoxWidthProperty); }
            set { SetValue(AutoSuggestBoxWidthProperty, value); }
        }

        public bool IsAutoSuggestBoxActivated
        {
            get { return (bool)GetValue(IsAutoSuggestBoxActivatedProperty); }
            set { SetValue(IsAutoSuggestBoxActivatedProperty, value); }
        }

        public bool IsPaneToggleButtonVisible
        {
            get { return (bool)GetValue(IsPaneToggleButtonVisibleProperty); }
            set { SetValue(IsPaneToggleButtonVisibleProperty, value); }
        }

        public double LargeModeThresholdWidth
        {
            get { return (double)GetValue(LargeModeThresholdWidthProperty); }
            set { SetValue(LargeModeThresholdWidthProperty, value); }
        }

        public ToolbarMode Mode
        {
            get { return (ToolbarMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public ICommand PaneToggleButtonCommand
        {
            get { return (ICommand)GetValue(PaneToggleButtonCommandProperty); }
            set { SetValue(PaneToggleButtonCommandProperty, value); }
        }

        public Style PaneToggleButtonStyle
        {
            get { return (Style)GetValue(PaneToggleButtonStyleProperty); }
            set { SetValue(PaneToggleButtonStyleProperty, value); }
        }

        private static void OnAutoSuggestBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolbar = d as Toolbar;
            toolbar.OnAutoSuggestBoxChanged((AutoSuggestBox)e.NewValue);
        }

        private static void OnIsAutoSuggestBoxActivatedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolbar = d as Toolbar;
            toolbar.OnIsAutoSuggestBoxActivatedChanged((bool)e.NewValue);
        }

        private static void OnIsPaneToggleButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolbar = d as Toolbar;
            toolbar.OnIsPaneToggleButtonVisibleChanged((bool)e.NewValue);
        }

        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolbar = d as Toolbar;
            toolbar.OnModeChanged((ToolbarMode)e.NewValue);
        }
    }
}
