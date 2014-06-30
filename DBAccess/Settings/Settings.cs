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

        private string _COMPort;
        private string _TemperatureUnit;

        #endregion
        #region Properties
        [DefaultValue("COM1")]
        public string COMPort
        {
            get
            {
                return _COMPort;
            }

            set
            {
                _COMPort = value;
                OnPropertyChanged("COMPort");
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
        /// loads settings
        /// </summary>
        /// <returns>success</returns>
        public bool Load ( )
        {
            massValueChanges = true;
            // insert code to load settings
            OnPropertyChanged(string.Empty);
            massValueChanges = false;
            return false;
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
