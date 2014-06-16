
using System;
using System.IO;
namespace LibShared.Crash
{
    [Serializable( )]
    public class CrashReport
    {
        #region Properties
        public string General
        {
            get;
            set;
        }
        public string StackTrace
        {
            get;
            set;
        }
        public string Exceptions
        {
            get;
            set;
        }
        public string Environment
        {
            get;
            set;
        }
        public string UserAssemblies
        {
            get;
            set;
        }
        public string AllAssemblies
        {
            get;
            set;
        }
        #endregion
        #region Serializer
        /// <summary>
        /// Schreiben der Einstellungen auf Festplatte
        /// </summary>
        /// <param name="c">zu speicherende Einstellungen</param>
        internal void Serialize (string file)
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(typeof(CrashReport));
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, this);
            writer.Flush( );
            writer.Close( );
        }
        /// <summary>
        /// Einlesen der Einstellungen
        /// </summary>
        /// <returns>Gelesene Einstellungen</returns>
        internal static CrashReport Deserialize (string file)
        {
            System.Xml.Serialization.XmlSerializer xs
                = new System.Xml.Serialization.XmlSerializer(
                    typeof(CrashReport));
            StreamReader reader = File.OpenText(file);
            CrashReport c = (CrashReport)xs.Deserialize(reader);
            reader.Close( );
            return c;
        }
        #endregion
    }
}
