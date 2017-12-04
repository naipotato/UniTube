using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Media.Core;
using Windows.UI.Xaml.Navigation;

namespace UniTube.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {
        private string Id { get; set; }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Id = parameter is string ? parameter as string : string.Empty;

            return Task.CompletedTask;
        }

        private MediaSource GetVideoSource()
        {
            var youtube = VideoLibrary.YouTube.Default;
            var video = youtube.GetVideo($"https://www.youtube.com/watch?v={Id}");
            return MediaSource.CreateFromUri(new Uri(video.Uri));
        }
    }
}
