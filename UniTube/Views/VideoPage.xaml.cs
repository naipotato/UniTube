using UniTube.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Views
{
    public sealed partial class VideoPage : Page
    {
        public VideoViewModel ViewModel => (DataContext as VideoViewModel);

        public VideoPage()
        {
            InitializeComponent();
        }
    }
}
