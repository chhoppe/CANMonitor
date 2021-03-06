﻿using SQLite;

namespace CANMonitor.DB.DataStructure
{
    public class TableData
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string TimeStamp
        {
            get;
            set;
        }
        public int Type
        {
            get;
            set;
        }
        public double Value
        {
            get;
            set;
        }

    }
}
