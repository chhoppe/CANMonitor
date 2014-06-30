using System;
using System.Windows.Data;

namespace CANMonitor.Core
{
    [ValueConversion(typeof(CANMonitor.DB.DataStructure.ValueGeneral), typeof(string))]
    public class DataValueToValueConverter : IValueConverter
    {
        #region IValueConverter Member

        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "-";
            CANMonitor.DB.DataStructure.ValueGeneral val = (CANMonitor.DB.DataStructure.ValueGeneral)value;
            if (val != null)
            {
                switch (val.DataType)
                {
                    case CANMonitor.DB.DataStructure.DataValueTypes.None:
                        return "-";
                    case CANMonitor.DB.DataStructure.DataValueTypes.Voltage:
                        return string.Format("{0:0.00} V", val.DoubleValue);
                    case CANMonitor.DB.DataStructure.DataValueTypes.Temperature:
                        CANMonitor.DB.DataStructure.ValueTemperature val2 = (CANMonitor.DB.DataStructure.ValueTemperature)val;
                        if (val2 != null && CANMonitor.DB.Settings.GlobalSettings != null)
                        {
                            if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Celsius")
                                return string.Format("{0:0.00} °C", val2.Celsius);
                            else if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Kelvin")
                                return string.Format("{0:0.00} K", val2.Kelvin);
                            else if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Fahrenheit")
                                return string.Format("{0:0.00} F", val2.Fahrenheit);
                            else
                                return string.Format("{0:0.00} °C", val2.Celsius);
                        }
                        return "-";
                    case CANMonitor.DB.DataStructure.DataValueTypes.Current:
                        if (val.DoubleValue < 0.95)
                            return string.Format("{0:0.00} mA", val.DoubleValue*100);
                        else
                            return string.Format("{0:0.00} A", val.DoubleValue);
                    default:
                        return "-";
                }
            }
            return "-";
        }

        public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
