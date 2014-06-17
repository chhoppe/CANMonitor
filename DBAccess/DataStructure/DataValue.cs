namespace CANMonitor.DB.DataStructure
{
    public class DataValue
    {

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
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
    }
}
