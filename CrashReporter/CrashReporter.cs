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
        private const string CRASH_REPORT_FILENAME = "crash.xml";
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
        static public CrashReport Report
        {
            get
            {
                try
                {
                    if (CrashReportExist)
                    {
                        return CrashReport.Deserialize(CrashFilename);
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
        public bool IncludeEnvironment
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
            GenerateCrashReport(null, null);
        }
        /// <summary>
        /// Creates a Crash Report containing all informations, including the Exceptions that where raised
        /// </summary>
        /// <param name="reason">Exceptions to include into the report.</param>
        public void CreateCrashReport (string reason)
        {
            GenerateCrashReport(reason, null);
        }
        /// <summary>
        /// Creates a Crash Report containing all informations, including the Exceptions that where raised
        /// </summary>
        /// <param name="exept">Exceptions to include into the report.</param>
        public void CreateCrashReport (Exception exept)
        {
            GenerateCrashReport(null, exept);
        }
        /// <summary>
        /// Creates a Crash Report containing all informations, including the Exceptions that where raised and the source
        /// </summary>
        /// <param name="reason">where this crash report is called from</param>
        /// <param name="exept">Exceptions to include into the report.</param>
        public void CreateCrashReport (string reason, Exception exept)
        {
            GenerateCrashReport(reason, exept);
        }
        private void GenerateCrashReport (string reason, Exception exept)
        {
            CrashReport cr = new CrashReport( );
            cr.General = String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "Created: {0}{1}",
                        DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture),
                        Environment.NewLine
                        );

            cr.General += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "Reason: {0}{1}",
                        reason ?? "",
                        Environment.NewLine
                        );


            if (IncludeException)
            {
                cr.Exceptions = "";
                Exception tmpexept = exept;
                while (tmpexept != null)
                {
                    cr.Exceptions += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0}: {1}{2}",
                        tmpexept.Source ?? "",
                        tmpexept.Message ?? "",
                        Environment.NewLine
                        );
                    tmpexept = tmpexept.InnerException;
                }

            }
            if (IncludeStackTrace)
            {
                cr.StackTrace = Environment.StackTrace;
            }
            if (IncludeEnvironment)
            {
                cr.Environment = "";
                foreach (var item in typeof(Environment).GetProperties(System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Static))
                {
                    if (item.Name != "StackTrace")
                    {
                        cr.Environment += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                            "{0}: {1}{2}",
                            item.Name ?? "",
                            item.GetValue(null, null).ToString( ) ?? "",
                            Environment.NewLine
                            );
                    }
                }
            }
            System.Reflection.Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies( );
            if (IncludeUserAssemblies)
            {
                cr.UserAssemblies = "";
                foreach (System.Reflection.Assembly ass in Assemblies.Where(a => a.FullName.StartsWith("QoSCalc", StringComparison.Ordinal)).ToArray( ))
                {
                    cr.UserAssemblies += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                            "{0}, Loc:{1}{2}",
                            ass.FullName ?? "",
                            ass.Location ?? "",
                            Environment.NewLine
                            );
                    foreach (System.Reflection.AssemblyName ass2 in ass.GetReferencedAssemblies( ))
                    {
                        cr.UserAssemblies += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                            "  {0}, Compatible:{1}{2}",
                            ass2.FullName ?? "",
                            ass2.VersionCompatibility.ToString( ) ?? "",
                            Environment.NewLine
                            );
                    }
                }
            }
            if (IncludeAllAssemblies)
            {
                cr.AllAssemblies = "";
                foreach (System.Reflection.Assembly ass in Assemblies)
                {
                    try
                    {
                        cr.AllAssemblies += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0}, Loc:{1}{2}",
                    ass.FullName ?? "",
                    ass.Location ?? "",
                    Environment.NewLine
                    );
                    }
                    catch (NotSupportedException)
                    {
                        cr.AllAssemblies += String.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0}{1}",
                    ass.FullName ?? "",
                    Environment.NewLine
                    );
                    }
                }
            }
            cr.Serialize(CrashFilename);
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
        /// <param name="filename">new filename</param>
        /// <returns>true if successfull</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static public bool SaveReport (string fileName)
        {
            try
            {
                if (CrashReportExist)
                {
                    System.IO.File.Copy(CrashFilename, fileName);
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
        /// <param name="fileName">path to save at</param>
        /// <returns>true if successfull</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static public bool MoveReport (string fileName)
        {
            try
            {
                if (CrashReportExist && SaveReport(fileName))
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
        static public void ShowMsgBox ( )
        {
            if (CrashReporter.CrashReportExist)
            {
                using (var msgbox = new CrashMsgBox( ))
                {
                    msgbox.ShowDialog( );
                }
            }
        }
        #endregion
    }
}
