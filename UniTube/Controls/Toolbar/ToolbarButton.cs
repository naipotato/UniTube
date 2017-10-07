using Windows.UI.Xaml.Controls;

namespace UniTube.Controls
{
    public class ToolbarButton : Button, IToolbarElement
    {
        public ToolbarButton()
        {
            DefaultStyleKey = typeof(ToolbarButton);
        }
    }
}
