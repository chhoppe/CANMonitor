namespace CANMonitor.DB.DataStructure
{
    public class DataValue
    {
        public DataValue()
        {}
        public DataValue(System.UInt32 pID, string pName, ValueGeneral pValue)
            : this(pID, pName, System.DateTime.Now, pValue)
        {
        }
        public DataValue(System.UInt32 pID, string pName, System.DateTime pTimeStamp, ValueGeneral pValue)
        {
            ID = pID;
            Name = pName;
            Value = pValue;
            TimeStamp = pTimeStamp;
        }
        public System.UInt32 ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public System.DateTime TimeStamp
        {
            get;
            set;
        }
        public DataValueTypes Type
        {
            get 
            {
                if (Value != null)
                    return Value.DataType;
                else
                    return DataValueTypes.None;
            }
        }
        public ValueGeneral Value
        {
            get;
            set;
        }
    }
}
