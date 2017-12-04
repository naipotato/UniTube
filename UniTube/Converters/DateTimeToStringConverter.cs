using System;

using Windows.UI.Xaml.Data;

namespace UniTube.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                DateTime date1 = (DateTime)value;
                TimeSpan timeSpan = date1.Subtract(DateTime.Now);
                if (-(timeSpan.Seconds) <= 59 && timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Days == 0)
                {
                    return "Hace unos segundos";
                }
                if (-(timeSpan.Minutes) <= 10 && -(timeSpan.Minutes) >= 1 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                {
                    return "Hace unos minutos";
                }
                if (-(timeSpan.Minutes) <= 59 && -(timeSpan.Minutes) > 10 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                {
                    return "Hace " + -(timeSpan.Minutes) + " minutos";
                }
                if (-(timeSpan.Hours) == 1 && timeSpan.Days == 0)
                {
                    return "Hace una hora";
                }
                if (-(timeSpan.Hours) <= 23 && -(timeSpan.Minutes) < 59 && -(timeSpan.Hours) > 1 && timeSpan.Days == 0)
                {
                    return "Hace " + -(timeSpan.Hours) + " horas";
                }
                if (-(timeSpan.Days) == 1)
                {
                    return "Hace un dia";
                }
                if (-(timeSpan.Days) <= 7 && -(timeSpan.Days) > 1)
                {
                    return "Hace " + -(timeSpan.Days) + " dias";
                }
                if (-(timeSpan.Days) <= 31 && -(timeSpan.Days) > 7)
                {
                    int week = 1;
                    return "Hace una semana";
                    if (-(timeSpan.Days) > 7)
                    {
                        week = 2;
                        return "Hace " + week + " semanas";
                    }
                    if (-(timeSpan.Days) > 14)
                    {
                        week = 3;
                        return "Hace " + week + " semanas";
                    }
                    if (-(timeSpan.Days) > 28)
                    {
                        week = 4;
                        return "Hace " + week + " semanas";
                    }

                }
                if (-(timeSpan.Days) < 365 && -(timeSpan.Days) > 31)
                {
                    int month = 1;
                    return "Hace un mes";
                    if (-(timeSpan.Days) >= 56)
                    {
                        month = 2;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 84)
                    {
                        month = 3;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 112)
                    {
                        month = 4;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 140)
                    {
                        month = 5;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 168)
                    {
                        month = 6;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 196)
                    {
                        month = 7;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 224)
                    {
                        month = 8;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 252)
                    {
                        month = 9;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 280)
                    {
                        month = 10;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 308)
                    {
                        month = 11;
                        return "Hace " + month + " meses";
                    }
                    if (-(timeSpan.Days) >= 336)
                    {
                        month = 12;
                        return "Hace " + month + " meses";
                    }
                }
            }
            else if (value is null)
            {
                return "None";
            }

            throw new NotImplementedException();

        }
        

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
