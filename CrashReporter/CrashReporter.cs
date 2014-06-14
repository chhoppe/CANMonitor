using System;
using System.ComponentModel;
using System.Linq;

namespace QoSCalc.Common
{
    /// <summary>
    /// Class to create CrashReport
    /// </summary>
    public class CrashReporter
    {
        #region Constants
        /// <summary>
        /// Filename in which the report is saved.
        /// </summary>
        private const string CRASH_REPORT_FILENAME = "crash.txt";
        #endregion
        #region Properties
        /// <summary>
        /// Filename and FilePath (WorkingDir)
        /// </summary>
        static private string CrashFilename
        {
            get
            {
                return System.IO.Path.Combine(Environment.CurrentDirectory, CRASH_REPORT_FILENAME);
            }
        }
        /// <summary>
        /// Get the hole report as string
        /// </summary>
        /// <returns>string containing the report or null</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static public string Report
        {
            get
            {
                try
                {
                    if (CrashReportExist)
                    {
                        return System.IO.File.ReadAllText(CrashFilename);
                    }
                    else
                        return null; //no report to copy exist
                }
                catch
                {
                    return null; //unsuccessfull
                }
            }
        }
        /// <summary>
        /// Indicates that a new Crash report exist
        /// </summary>
        static public bool CrashReportExist
        {
            get
            {
                if (System.IO.File.Exists(CrashFilename))
                    return true;
                return false;
            }
        }
        /// <summary>
        /// Enviroment Information will be included in the Report
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeEnviroment
        {
            get;
            set;
        }
        /// <summary>
        /// Enviroment Information will be included in the Report
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeStackTrace
        {
            get;
            set;
        }
        /// <summary>
        /// Exception Information will be included in the Report, if existing
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeException
        {
            get;
            set;
        }
        /// <summary>
        /// Information about user created Assembllies will be included in the Report
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeUserAssemblies
        {
            get;
            set;
        }
        /// <summary>
        /// Information about all loaded Assembllies will be included in the Report
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeAllAssemblies
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Constructor
        /// </summary>
        public CrashReporter ( )
        {
            InitDefaults( );
        }
        /// <summary>
        /// Sets all Properties to default values
        /// </summary>
        private void InitDefaults ( )
        {
            System.Reflection.PropertyInfo[] props = this.GetType( ).GetProperties(System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static);

            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];

                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    object[] defaultValueAttribute =
                        prop.GetCustomAttributes(typeof(DefaultValueAttribute), true);

                    if (defaultValueAttribute != null && defaultValueAttribute.Length > 0)
                    {
                        DefaultValueAttribute dva =
                            defaultValueAttribute[0] as DefaultValueAttribute;

                        if (dva != null)
                            prop.SetValue(this, dva.Value, null);
                    }
                }
            }
        }
        /// <summary>
        /// Creates a Crash Report containing all informations
        /// </summary>
        public void CreateCrashReport ( )
        {
            CreateCrashReport(null, null);
        }
        /// <summary>
        /// Creates a Crash Report containing all informations, including the Exceptions that where raised
        /// </summary>
        /// <param name="e">Exceptions to include into the report.</param>
        public void CreateCrashReport (Exception exept)
        {
            CreateCrashReport(null, exept);
        }
        /// <summary>
        /// Creates a Crash Report containing all informations, including the Exceptions that where raised and the source
        /// </summary>
        /// <param name="source">where this crash report is called from</param>
        /// <param name="exept">Exceptions to include into the report.</param>
        public void CreateCrashReport (string source, Exception exept)
        {
            Exception tmpexept = exept;
            System.IO.TextWriter tw = new System.IO.StreamWriter(CrashFilename, true);
            #region header
            tw.WriteLine("----------------------------");
            tw.WriteLine("----------------------------");
            tw.WriteLine("CRASH Report");
            tw.WriteLine("  created at: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
            tw.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, "  created by: {0}", source ?? ""));
            #endregion header
            if (IncludeException)
            {
                tw.WriteLine("----------------------------");
                tw.WriteLine("Exceptionlist");
                while (tmpexept != null)
                {
                    tw.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, "  {0}: {1}", tmpexept.Source ?? "", tmpexept.Message ?? ""));
                    tmpexept = tmpexept.InnerException;
                }
            }
            if (IncludeStackTrace)
            {
                tw.WriteLine("----------------------------");
                tw.WriteLine("StackTrace");
                tw.WriteLine(Environment.StackTrace);
            }
            System.Reflection.Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies( );
            if (IncludeUserAssemblies)
            {
                tw.WriteLine("----------------------------");
                tw.WriteLine("Used User Assemblies:");
                foreach (System.Reflection.Assembly ass in Assemblies.Where(a => a.FullName.StartsWith("QoSCalc", StringComparison.Ordinal)).ToArray( ))
                {
                    tw.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, "  {0}, Loc:{1}", ass.FullName ?? "", ass.Location ?? ""));
                    foreach (System.Reflection.AssemblyName ass2 in ass.GetReferencedAssemblies( ))
                    {
                        tw.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, "    {0}, Compatible:{1}", ass2.FullName ?? "", ass2.VersionCompatibility.ToString( ) ?? ""));
                    }
                }
            }
            if (IncludeAllAssemblies)
            {
                tw.WriteLine("----------------------------");
                tw.WriteLine("All Assemblies:");
                foreach (System.Reflection.Assembly ass in Assemblies)
                {
                    tw.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, "  {0}, Loc:{1}", ass.FullName ?? "", ass.Location ?? ""));
                }
            }

            tw.WriteLine( );
            tw.Flush( );
            tw.Close( );

        }
        #endregion
        #region static Methods
        /// <summary>
        /// Removes the CrashReport
        /// </summary>
        static public void RemoveCrashReport ( )
        {
            if (CrashReportExist)
                System.IO.File.Delete(CrashFilename);


        }
        /// <summary>
        /// saves the report file to specified place
        /// </summary>
        /// <param name="path">path to save at</param>
        /// <returns>true if successfull</returns>
        static public bool SaveReport (string path)
        {
            return SaveReport(path, CRASH_REPORT_FILENAME);
        }
        /// <summary>
        /// saves the report file to specified place
        /// </summary>
        /// <param name="path">path to save at</param>
        /// <param name="filename">new filename</param>
        /// <returns>true if successfull</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static public bool SaveReport (string path, string fileName)
        {
            try
            {
                if (CrashReportExist)
                {
                    System.IO.File.Copy(CrashFilename, System.IO.Path.Combine(path, fileName));
                    return true;
                }
                else
                    return false; //no report to copy exist
            }
            catch
            {
                return false; //unsuccessfull
            }
        }
        /// <summary>
        /// moves the report file to specified place
        /// </summary>
        /// <param name="path">path to save at</param>
        /// <param name="filename">new filename</param>
        /// <returns>true if successfull</returns>
        static public bool MoveReport (string path)
        {
            return MoveReport(path, CRASH_REPORT_FILENAME);
        }
        /// <summary>
        /// moves the report file to specified place
        /// </summary>
        /// <param name="path">path to save at</param>
        /// <returns>true if successfull</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static public bool MoveReport (string path, string fileName)
        {
            try
            {
                if (CrashReportExist && SaveReport(path, fileName))
                {
                    RemoveCrashReport( );
                    return true;
                }
                else
                    return false; //no report to copy exist
            }
            catch
            {
                return false; //unsuccessfull
            }
        }
        #endregion
    }
}
