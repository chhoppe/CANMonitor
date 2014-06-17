
namespace CANMonitor.DB.DataStructure
{
    public sealed class ValueVoltage : ValueGeneral
    {
        #region Properties
        #endregion

        #region Constructor, Operators
        public ValueVoltage()
        {
            _value = 0;
            _datatype = DataValueTypes.Voltage;
        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">value in Kelvin</param>
        public ValueVoltage (double temp)
            :this()
        {
            _value = temp;
        }
        #endregion

    }
}
