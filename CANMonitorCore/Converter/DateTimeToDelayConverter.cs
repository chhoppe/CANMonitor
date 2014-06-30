using System;
using System.Windows.Data;

namespace CANMonitor.Core
{

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeToDelayConverter : IValueConverter
    {
        #region IValueConverter Member

        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            DateTime dt = (DateTime)value;

            if (dt != null)
            {
                TimeSpan delay = DateTime.Now - dt;
                if (delay.Days > 1)
                    return string.Format("{0} days", delay.Days);
                else if (delay.Hours > 1)
                    return string.Format("{0} h", delay.Hours);
                else if (delay.Minutes > 1)
                    return string.Format("{0} min", delay.Minutes);
                else if (delay.Seconds > 1)
                    return string.Format("{0} s", delay.Seconds);
                else if (delay.Milliseconds >= 1)
                    return string.Format("{0} ms", delay.Milliseconds);
                else
                    return "";
            }
            return "";
        }

        public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
