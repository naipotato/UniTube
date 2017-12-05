using System;

using Windows.UI.Xaml.Data;

namespace UniTube.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                var timeSpan = DateTime.Now - dateTime;
                if (timeSpan < TimeSpan.FromMinutes(1))
                {
                    return "A few seconds ago";
                }
                else if (timeSpan <= TimeSpan.FromMinutes(10) && timeSpan > TimeSpan.FromMinutes(1))
                {
                    return "A few minutes ago";
                }
                else if (timeSpan < TimeSpan.FromHours(1) && timeSpan > TimeSpan.FromMinutes(10))
                {
                    return $"{timeSpan.Minutes} minutes ago";
                }
                else if (timeSpan == TimeSpan.FromHours(1))
                {
                    return "An hour ago";
                }
                else if (timeSpan < TimeSpan.FromDays(1) && timeSpan > TimeSpan.FromHours(1))
                {
                    return $"{timeSpan.Hours} hours ago";
                }
                else if (timeSpan == TimeSpan.FromDays(1))
                {
                    return "A day ago";
                }
                else if (timeSpan < TimeSpan.FromDays(7) && timeSpan > TimeSpan.FromDays(1))
                {
                    return $"{timeSpan.Days} days ago";
                }
                else if (timeSpan == TimeSpan.FromDays(7))
                {
                    return "A week ago";
                }
                else if (timeSpan < TimeSpan.FromDays(30) && timeSpan > TimeSpan.FromDays(7))
                {
                    return $"{Math.Truncate(timeSpan.Days / 7d)} weeks ago";
                }
                else if (timeSpan == TimeSpan.FromDays(30))
                {
                    return "A month ago";
                }
                else if (timeSpan < TimeSpan.FromDays(365) && timeSpan > TimeSpan.FromDays(30))
                {
                    return $"{Math.Truncate(timeSpan.Days / 30d)} months ago";
                }
                else if (timeSpan == TimeSpan.FromDays(365))
                {
                    return "A year ago";
                }
                else if (timeSpan > TimeSpan.FromDays(365))
                {
                    return $"{Math.Truncate(timeSpan.Days / 365d)} years ago";
                }
            }
            else if (value is null)
            {
                return string.Empty;
            }

            throw new NotImplementedException();
        }
        

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
