using UniTube.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class SavedPage : Page
    {
        private SavedViewModel ViewModel => (DataContext as SavedViewModel);

        public SavedPage() => InitializeComponent();
    }
}
