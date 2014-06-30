using CANMonitor.DB.DataStructure;
using System;
using System.ComponentModel;

namespace CANMonitor.DB
{
    public class Settings : System.ComponentModel.INotifyPropertyChanged
    {
        #region private variables
        /// <summary>
        /// indicates that multiple settings will be changed at once at stops raising PropertyChanged Events for each one
        /// </summary>
        private bool massValueChanges = false;
        private System.Windows.Threading.Dispatcher _Dispatcher;
        static public Settings GlobalSettings;
        private string _COMPort;
        private int _SerialPortBaudrate;
        private System.IO.Ports.Parity _SerialPortParity;
        private int _SerialPortDatenbits;
        private System.IO.Ports.StopBits _SerialPortStopBits;

        private string _TemperatureUnit;
        private double _CurrentThresholdUpper;
        private double _CurrentThresholdLower;
        private double _VoltageThresholdUpper;
        private double _VoltageThresholdLower;
        private double _TemperatureThresholdUpper;
        private double _TemperatureThresholdLower;

        #endregion
        #region Properties
        [DefaultValue("COM1")]
        public string SerialPort
        {
            get
            {
                return _COMPort;
            }

            set
            {
                _COMPort = value;
                OnPropertyChanged("SerialPort");
            }
        }
        [DefaultValue(19200)]
        public int SerialPortBaudrate
        {
            get
            {
                return _SerialPortBaudrate;
            }

            set
            {
                _SerialPortBaudrate = value;
                OnPropertyChanged("SerialPortBaudrate");
            }
        }
        [DefaultValue(System.IO.Ports.Parity.None)]
        public System.IO.Ports.Parity SerialPortParity
        {
            get
            {
                return _SerialPortParity;
            }

            set
            {
                _SerialPortParity = value;
                OnPropertyChanged("SerialPortParity");
            }
        }
        [DefaultValue(8)]
        public int SerialPortDatenbits
        {
            get
            {
                return _SerialPortDatenbits;
            }

            set
            {
                _SerialPortDatenbits = value;
                OnPropertyChanged("SerialPortDatenbits");
            }
        }
        [DefaultValue(System.IO.Ports.StopBits.None)]
        public System.IO.Ports.StopBits SerialPortStopBits
        {
            get
            {
                return _SerialPortStopBits;
            }

            set
            {
                _SerialPortStopBits = value;
                OnPropertyChanged("SerialPortStopBits");
            }
        }

        public string[] SerialPortList
        {
            get
            {
                return System.IO.Ports.SerialPort.GetPortNames( );
            }
        }
        [DefaultValue("Celsius")]
        public string TemperatureUnit
        {
            get
            {
                return _TemperatureUnit;
            }

            set
            {
                _TemperatureUnit = value;
                OnPropertyChanged("TemperatureUnit");
            }
        }

        [DefaultValue(30.0)]
        public double TemperatureThresholdLower
        {
            get
            {
                return _TemperatureThresholdLower;
            }

            set
            {
                _TemperatureThresholdLower = value;
                OnPropertyChanged("TemperatureThresholdLower");
            }
        }
        [DefaultValue(45.0)]
        public double TemperatureThresholdUpper
        {
            get
            {
                return _TemperatureThresholdUpper;
            }

            set
            {
                _TemperatureThresholdUpper = value;
                OnPropertyChanged("TemperatureThresholdUpper");
            }
        }
        [DefaultValue(12.5)]
        public double VoltageThresholdLower
        {
            get
            {
                return _VoltageThresholdLower;
            }

            set
            {
                _VoltageThresholdLower = value;
                OnPropertyChanged("VoltageThresholdLower");
            }
        }
        [DefaultValue(11.5)]
        public double VoltageThresholdUpper
        {
            get
            {
                return _VoltageThresholdUpper;
            }

            set
            {
                _VoltageThresholdUpper = value;
                OnPropertyChanged("VoltageThresholdUpper");
            }
        }
        [DefaultValue(5.5)]
        public double CurrentThresholdLower
        {
            get
            {
                return _CurrentThresholdLower;
            }

            set
            {
                _CurrentThresholdLower = value;
                OnPropertyChanged("CurrentThresholdLower");
            }
        }
        [DefaultValue(6.5)]
        public double CurrentThresholdUpper
        {
            get
            {
                return _CurrentThresholdUpper;
            }

            set
            {
                _CurrentThresholdUpper = value;
                OnPropertyChanged("CurrentThresholdUpper");
            }
        }
        #endregion

        #region Property Handling
        /// <summary>
        /// save settings
        /// </summary>
        /// <returns>success</returns>
        public bool Save ( )
        {
            // insert code to save settings
            return false;
        }
        /// <summary>
        /// loads settings from db
        /// </summary>
        /// <returns>success</returns>
        public bool Load ( )
        {
            if (!DatabaseManager.IsConnected)
                return false;
            massValueChanges = true;

            System.Reflection.PropertyInfo[] props = this.GetType( ).GetProperties(System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static);

            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];
                var query = DatabaseManager.Connection.Table<TableSettings>( ).Where(v => String.Equals(v.Name, prop.Name));
                if (query.Count( ) >= 1)
                {


                }
            }
            massValueChanges = false;


            OnPropertyChanged(string.Empty);
            massValueChanges = false;
            return false;
        }

        private void updateValueDB (System.Reflection.PropertyInfo prop)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Settings ( )
        {
            massValueChanges = false;
            LoadDefaults( );
            Load( );
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Settings (System.Windows.Threading.Dispatcher pDispatcher)
            : this( )
        {
            _Dispatcher = pDispatcher;
        }

        /// <summary>
        /// Sets all Properties to default values
        /// </summary>
        public void LoadDefaults ( )
        {
            massValueChanges = true; // indicates that multiple settings will be changed at once at stops raising PropertyChanged Events for each one
            System.Reflection.PropertyInfo[] props = this.GetType( ).GetProperties(System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static);

            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];
                this.LoadDefaults(props[i]);
            }
            massValueChanges = false;
            OnPropertyChanged(string.Empty); // signals all have changed

        }
        /// <summary>
        /// Set one Property back to default value
        /// </summary>
        public void LoadDefaults (string pPropertyName)
        {
            System.Reflection.PropertyInfo prop = this.GetType( ).GetProperty(pPropertyName, System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static);

            LoadDefaults(prop);
        }
        /// <summary>
        /// Set one Property back to default value
        /// </summary>
        public void LoadDefaults (System.Reflection.PropertyInfo pProperty)
        {
            if (pProperty.GetCustomAttributes(true).Length > 0)
            {
                object[] defaultValueAttribute =
                    pProperty.GetCustomAttributes(typeof(DefaultValueAttribute), true);

                if (defaultValueAttribute != null && defaultValueAttribute.Length > 0)
                {
                    DefaultValueAttribute dva =
                        defaultValueAttribute[0] as DefaultValueAttribute;

                    if (dva != null)
                        pProperty.SetValue(this, dva.Value, null);
                }
            }
        }

        /// <summary>
        /// Event to signal a change in one of the properties
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// Method to fire event if Property changed
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged (string name)
        {
            if (!string.IsNullOrEmpty(name) && !massValueChanges)
            {
                System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }

}
