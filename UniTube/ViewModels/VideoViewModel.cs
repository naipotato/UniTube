using System.Collections.Generic;
using System.Threading.Tasks;

using Template10.Mvvm;

using UniTube.Core.Resources;

using Windows.UI.Xaml.Navigation;

namespace UniTube.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {
        private Video video;

        public Video Video
        {
            get => video;
            set => Set(ref video, value);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is Video)
            {
                Video = parameter as Video;
            }
            return Task.CompletedTask;
        }
    }
}
