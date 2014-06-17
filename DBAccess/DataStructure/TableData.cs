using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANMonitor.DB.DataStructure
{
    public class TableData
    {
        List<DataValue> _list;

        public TableData()
        {
            _list = new List<DataValue>();
        }
        public void AddValue(DataValue value)
        {
            _list.Add(value);

            //_latest.Find(delegate(DataValue s ) { return s.Name == value.Name; } )

        }
        public DataValue GetLatest(string Name)
        {
            return _list.FindLast(delegate(DataValue s) { return s.Name == Name; });
        }
        public List<DataValue> GetAll(string Name)
        {
            return _list.FindAll(delegate(DataValue s) { return s.Name == Name; });
        }

        public List<String> GetNames()
        {
            List<String> names = new List<String>();
            foreach (DataValue item in _list)
            {
                if (!names.Exists(delegate(string s) { return s == item.Name; }))
                {
                    names.Add(item.Name);
                }
            }
            return names;
        }
        public List<String> GetNames(DataValueTypes datatype)
        {
            List<String> names = new List<String>();
            foreach (DataValue item in _list.Where(l => l.Type == datatype))
            {
                if (!names.Exists(delegate(string s) { return s == item.Name; }))
                {
                    names.Add(item.Name);
                }
            }
            return names;
        }
    }
}
