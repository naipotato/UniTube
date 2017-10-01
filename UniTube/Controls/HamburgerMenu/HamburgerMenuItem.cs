using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UniTube.Controls
{
    [TemplateVisualState(GroupName = "PaneStates", Name = NotClosedCompactState)]
    [TemplateVisualState(GroupName = "PaneStates", Name = ClosedCompactState)]
    public partial class HamburgerMenuItem : RadioButton
    {
        private const string NotClosedCompactState  = "NotClosedCompact";
        private const string ClosedCompactState     = "ClosedCompact";

        private HamburgerMenu _hamburgerMenuHost;

        public HamburgerMenuItem()
        {
            DefaultStyleKey = typeof(HamburgerMenuItem);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is HamburgerMenu))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
            {
                _hamburgerMenuHost = parent as HamburgerMenu;

                CompactPaneLength = _hamburgerMenuHost.CompactPaneLength;

                _hamburgerMenuHost.RegisterPropertyChangedCallback(HamburgerMenu.IsPaneOpenProperty, (s, e) =>
                {
                    OnPaneToggled();
                });

                _hamburgerMenuHost.DisplayModeChanged += (s, e) =>
                {
                    OnPaneToggled();
                };

                OnPaneToggled();
            }
        }

        private void OnPaneToggled()
        {
            if (_hamburgerMenuHost.IsPaneOpen)
            {
                VisualStateManager.GoToState(this, NotClosedCompactState, false);
            }
            else if (_hamburgerMenuHost.DisplayMode == HamburgerMenuDisplayMode.Compact ||
                _hamburgerMenuHost.DisplayMode == HamburgerMenuDisplayMode.Expanded)
            {
                VisualStateManager.GoToState(this, ClosedCompactState, false);
            }
        }
    }
}
