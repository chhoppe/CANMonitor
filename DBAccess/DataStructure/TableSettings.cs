using SQLite;

namespace CANMonitor.DB.DataStructure
{
    public class TableSettings
    {

        [PrimaryKey]
        public string Name
        {
            get;
            set;
        }
        public string StringValue
        {
            get;
            set;
        }
        public double DoubleValue
        {
            get;
            set;
        }
        public int IntValue
        {
            get;
            set;
        }
    }
}
