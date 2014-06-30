using System;
using System.Windows.Data;

namespace CANMonitor.Core
{

    [ValueConversion(typeof(CANMonitor.DB.DataStructure.DataValueTypes), typeof(System.Windows.Media.Brush))]
    public class DataValueTypeToBackgroundConverter : IValueConverter
    {
        #region IValueConverter Member

        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            CANMonitor.DB.DataStructure.DataValueTypes ltype = (CANMonitor.DB.DataStructure.DataValueTypes)value;

            switch (ltype)
            {
                case CANMonitor.DB.DataStructure.DataValueTypes.None:
                    return System.Windows.Media.Brushes.Red;
                case CANMonitor.DB.DataStructure.DataValueTypes.Voltage:
                    return System.Windows.Media.Brushes.LightBlue;
                case CANMonitor.DB.DataStructure.DataValueTypes.Temperature:
                    return System.Windows.Media.Brushes.LightGreen;
                case CANMonitor.DB.DataStructure.DataValueTypes.Current:
                    return System.Windows.Media.Brushes.LightYellow;
                default:
                    return System.Windows.Media.Brushes.Red;
            }
        }

        public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            System.Windows.Media.Color clr = (System.Windows.Media.Color)value;
            if (clr.R == 64)
                return CANMonitor.DB.DataStructure.DataValueTypes.Current;
            else if (clr.B == 64)
                return CANMonitor.DB.DataStructure.DataValueTypes.Voltage;
            else if (clr.G == 64)
                return CANMonitor.DB.DataStructure.DataValueTypes.Temperature;
            else
                return CANMonitor.DB.DataStructure.DataValueTypes.None;
        }

        #endregion
    }
}
