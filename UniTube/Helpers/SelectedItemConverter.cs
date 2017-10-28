using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UniTube.Helpers
{
    public class SelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
                return (string)value == (string)parameter;
            else if (value is ElementTheme)
                return ((ElementTheme)value).ToString() == (string)parameter;

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
