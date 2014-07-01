using CANMonitor.DB;
using System;
using System.Windows.Data;

namespace CANMonitor.Core
{

    [ValueConversion(typeof(CANMonitor.DB.DataStructure.ValueGeneral), typeof(System.Windows.Media.Brush))]
    public class DataValueTypeToThresholdHighlightConverter : IValueConverter
    {
        #region IValueConverter Member

        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            CANMonitor.DB.DataStructure.ValueGeneral val = (CANMonitor.DB.DataStructure.ValueGeneral)value;
            if (val == null)
                return null;

            switch (val.DataType)
            {
                case CANMonitor.DB.DataStructure.DataValueTypes.None:
                    return System.Windows.Media.Brushes.Red;
                case CANMonitor.DB.DataStructure.DataValueTypes.Voltage:
                    if (val.DoubleValue >= Settings.GlobalSettings.VoltageThresholdLower &&
                        val.DoubleValue <= Settings.GlobalSettings.VoltageThresholdUpper)
                        return System.Windows.Media.Brushes.Black;
                    else
                        return System.Windows.Media.Brushes.Red;
                case CANMonitor.DB.DataStructure.DataValueTypes.Temperature:
                    double tempvalue = 0;
                    CANMonitor.DB.DataStructure.ValueTemperature val2 = (CANMonitor.DB.DataStructure.ValueTemperature)val;
                    if (val2 != null && CANMonitor.DB.Settings.GlobalSettings != null)
                    {
                        if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Celsius")
                            tempvalue = val2.Celsius;
                        else if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Kelvin")
                            tempvalue = val2.Kelvin;
                        else if (CANMonitor.DB.Settings.GlobalSettings.TemperatureUnit == "Fahrenheit")
                            tempvalue = val2.Fahrenheit;
                        else
                            tempvalue = val2.Celsius;
                    }

                    if (tempvalue >= Settings.GlobalSettings.TemperatureThresholdLower &&
                    tempvalue <= Settings.GlobalSettings.TemperatureThresholdUpper)
                        return System.Windows.Media.Brushes.Black;
                    else
                        return System.Windows.Media.Brushes.Red;
                case CANMonitor.DB.DataStructure.DataValueTypes.Current:
                    if (val.DoubleValue >= Settings.GlobalSettings.CurrentThresholdLower &&
                        val.DoubleValue <= Settings.GlobalSettings.CurrentThresholdUpper)
                        return System.Windows.Media.Brushes.Black;
                    else
                        return System.Windows.Media.Brushes.Red;
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
