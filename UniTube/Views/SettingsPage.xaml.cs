using UniTube.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel => DataContext as SettingsViewModel;

        public SettingsPage() => InitializeComponent();
    }
}
