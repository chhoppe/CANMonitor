﻿
namespace CANMonitor.DB.DataStructure
{
    public sealed class ValueTemperature : ValueGeneral
    {
        #region Properties

        public double Celsius
        {
            get
            {
                return _value - 273;
            }
            set
            {
                _value = value + 273;
            }
        }
        public double Kelvin
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public double Fahrenheit
        {
            get
            {
                return _value * 1.8 - 459.67;
            }
            set
            {
                _value = (_value + 459.67) * 5 / 9;
            }
        }
        #endregion

        #region Constructor, Operators
        public ValueTemperature()
        {
            _value = 0;
            _datatype = DataValueTypes.Temperature;
        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">other value</param>
        public ValueTemperature (ValueTemperature temp)
            :this()
        {
            _value = temp.Kelvin;
        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">value in Kelvin</param>
        public ValueTemperature (double temp)
            :this()
        {
            _value = temp;
        }
        #endregion

    }
}
