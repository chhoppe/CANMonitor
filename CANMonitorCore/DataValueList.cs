using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANMonitor.Core
{
    public class DataValueList : SortedBindingList<CANMonitor.DB.DataStructure.DataValue>
    {
        public DataValueList() : base()
        {
            this.Add(new DB.DataStructure.DataValue(0, "t1", new DB.DataStructure.ValueTemperature(300.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t2", new DB.DataStructure.ValueTemperature(310.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t3", new DB.DataStructure.ValueTemperature(320.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t4", new DB.DataStructure.ValueTemperature(290.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t5", new DB.DataStructure.ValueTemperature(295.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t6", new DB.DataStructure.ValueTemperature(305.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t7", new DB.DataStructure.ValueTemperature(288.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t8", new DB.DataStructure.ValueTemperature(275.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t9", new DB.DataStructure.ValueTemperature(298.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t10", new DB.DataStructure.ValueTemperature(273.3)));
            this.Add(new DB.DataStructure.DataValue(0, "t11", new DB.DataStructure.ValueTemperature(298.3214821)));
            this.Add(new DB.DataStructure.DataValue(0, "t12", new DB.DataStructure.ValueTemperature(299.3)));

            this.Add(new DB.DataStructure.DataValue(0, "v1", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v2", new DB.DataStructure.ValueVoltage(12.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v3", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v4", new DB.DataStructure.ValueVoltage(12.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v5", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v6", new DB.DataStructure.ValueVoltage(12.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v7", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v8", new DB.DataStructure.ValueVoltage(12.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v9", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v10", new DB.DataStructure.ValueVoltage(12.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v11", new DB.DataStructure.ValueVoltage(11.3)));
            this.Add(new DB.DataStructure.DataValue(0, "v12", new DB.DataStructure.ValueVoltage(12.3)));

            this.Add(new DB.DataStructure.DataValue(0, "c1", new DB.DataStructure.ValueCurrent(6.3)));
        }
    }
}
