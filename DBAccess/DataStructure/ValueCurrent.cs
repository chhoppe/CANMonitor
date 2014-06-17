
namespace CANMonitor.DB.DataStructure
{
    public sealed class ValueCurrent : ValueGeneral
    {
        #region Properties
        #endregion

        #region Constructor, Operators
        public ValueCurrent()
        {
            _value = 0;
            _datatype = DataValueTypes.Current;
        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">value in Kelvin</param>
        public ValueCurrent (double temp)
            :this()
        {
            _value = temp;
        }
        #endregion

    }
}
