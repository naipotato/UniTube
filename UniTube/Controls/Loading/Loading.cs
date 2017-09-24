using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    [TemplateVisualState(GroupName = "ActiveStates", Name = ActiveState)]
    [TemplateVisualState(GroupName = "ActiveStates", Name = InactiveState)]
    public partial class Loading : Control
    {
        private const string ActiveState    = "Active";
        private const string InactiveState  = "Inactive";

        public Loading()
        {
            DefaultStyleKey = typeof(Loading);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (IsActive)
            {
                VisualStateManager.GoToState(this, ActiveState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, InactiveState, true);
            }
        }

        private void OnIsActiveChanged(bool newValue)
        {
            if (newValue)
            {
                VisualStateManager.GoToState(this, ActiveState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, InactiveState, true);
            }
        }
    }
}
