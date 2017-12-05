using System;
using UniTube.Core.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.DataTemplateSelectors
{
    public class ResourceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VideoTemplate { get; set; }
        public DataTemplate ChannelTemplate { get; set; }
        public DataTemplate PlaylistTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is Video)
            {
                return VideoTemplate;
            }
            else if (item is Channel)
            {
                return ChannelTemplate;
            }
            else if (item is Playlist)
            {
                return PlaylistTemplate;
            }

            throw new NotImplementedException();
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
            => SelectTemplateCore(item);
    }
}
