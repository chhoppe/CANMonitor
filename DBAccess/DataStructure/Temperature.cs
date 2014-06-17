
namespace CANMonitor.DB.DataStructure
{
    public sealed class Temperature
    {
        private double _temperature; // Kelvin

        #region Properties
        public double Celsius
        {
            get
            {
                return _temperature + 273;
            }
            set
            {
                _temperature = value - 273;
            }
        }
        public double Kelvin
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
            }
        }
        public double Fahrenheit
        {
            get
            {
                return (Celsius / (5 / 9)) + 32;
            }
            set
            {
                _temperature = 5 / 9 * (value - 32);
            }
        }
        #endregion

        #region Constructor, Operators
        public Temperature ( )
        {

        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">other value</param>
        public Temperature (Temperature temp)
        {
            _temperature = temp.Kelvin;
        }
        /// <summary>
        /// creates new temperature
        /// </summary>
        /// <param name="temp">value in Kelvin</param>
        public Temperature (double temp)
        {
            _temperature = temp;
        }
        public static Temperature operator + (Temperature temp1, Temperature temp2)
        {
            return new Temperature(temp1.Kelvin + temp2.Kelvin);
        }
        public static Temperature operator - (Temperature temp1, Temperature temp2)
        {
            return new Temperature(temp1.Kelvin - temp2.Kelvin);
        }
        public static Temperature operator * (Temperature temp1, Temperature temp2)
        {
            return new Temperature(temp1.Kelvin * temp2.Kelvin);
        }
        public static Temperature operator / (Temperature temp1, Temperature temp2)
        {
            return new Temperature(temp1.Kelvin / temp2.Kelvin);
        }

        public static bool operator < (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin < temp2.Kelvin;
        }
        public static bool operator <= (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin <= temp2.Kelvin;
        }
        public static bool operator > (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin > temp2.Kelvin;
        }
        public static bool operator >= (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin >= temp2.Kelvin;
        }
        public static bool operator == (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin == temp2.Kelvin;
        }
        public static bool operator != (Temperature temp1, Temperature temp2)
        {
            return temp1.Kelvin != temp2.Kelvin;
        }
        public override bool Equals (object obj)
        {
            Temperature r = obj as Temperature;
            if (r != null)
            {
                return r.Kelvin == this.Kelvin;
            }
            return false;
        }
        public override int GetHashCode ( )
        {
            return this.Kelvin.GetHashCode( );
        }

        public static implicit operator Temperature (double val)
        {
            // since the rational equivalant of an int has 1 as denominator
            Temperature rational = new Temperature(val);

            return rational;
        }
        public static explicit operator double (Temperature r)
        {
            return r.Kelvin;
        }

        public override string ToString ( )
        {
            return Kelvin.ToString( );
        }
        #endregion

    }
}
