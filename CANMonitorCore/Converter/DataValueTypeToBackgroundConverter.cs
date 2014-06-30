using System;
using System.Windows.Data;

namespace CANMonitor.Core
{

    [ValueConversion(typeof(CANMonitor.DB.DataStructure.DataValueTypes), typeof(System.Windows.Media.Color))]
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
                    return System.Windows.Media.Color.FromRgb(255, 0, 0);
                case CANMonitor.DB.DataStructure.DataValueTypes.Voltage:
                    return System.Windows.Media.Color.FromRgb(0, 0, 64);
                case CANMonitor.DB.DataStructure.DataValueTypes.Temperature:
                    return System.Windows.Media.Color.FromRgb(0, 64, 0);
                case CANMonitor.DB.DataStructure.DataValueTypes.Current:
                    return System.Windows.Media.Color.FromRgb(64, 0, 0);
                default:
                    return System.Windows.Media.Color.FromRgb(255, 0, 0);
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
