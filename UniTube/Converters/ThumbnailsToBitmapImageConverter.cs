using System;
using UniTube.Core.Resources;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace UniTube.Converters
{
    public class ThumbnailsToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Thumbnails)
            {
                var thumbnails = (value as Thumbnails);
                if (thumbnails.MaxRes != null)
                {
                    return new BitmapImage(new Uri(thumbnails.MaxRes.Url));
                }
                else if (thumbnails.Standard != null)
                {
                    return new BitmapImage(new Uri(thumbnails.Standard.Url));
                }
                else if (thumbnails.High != null)
                {
                    return new BitmapImage(new Uri(thumbnails.High.Url));
                }
                else if (thumbnails.Medium != null)
                {
                    return new BitmapImage(new Uri(thumbnails.Medium.Url));
                }
                else if (thumbnails.Default != null)
                {
                    return new BitmapImage(new Uri(thumbnails.Default.Url));
                }
                else
                {
                    return new BitmapImage();
                }
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
