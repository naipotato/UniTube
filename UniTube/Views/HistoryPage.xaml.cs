using UniTube.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class HistoryPage : Page
    {
        private HistoryViewModel ViewModel => (DataContext as HistoryViewModel);

        public HistoryPage() => InitializeComponent();
    }
}
