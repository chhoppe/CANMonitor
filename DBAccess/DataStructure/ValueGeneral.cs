
namespace CANMonitor.DB.DataStructure
{
    public class ValueGeneral 
    {
        protected double _value; // DoubleValue
        protected DataValueTypes _datatype;

        #region Properties
        public DataValueTypes DataType
        { get { return _datatype; } }
        public double DoubleValue
        {get{ return _value;}
        set{_value=value;}}
        #endregion

        #region Constructor, Operators
        public ValueGeneral ( )
        {
            _datatype = DataValueTypes.None;
            _value = 0;
        }
        public ValueGeneral (double value)
            :this()
        {
            _value = value;
        }
        #endregion
    }
}
