using LibShared.Crash;
using System;
using System.Windows;

namespace QoSCalc.UserInterfaces.WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        public App ( )
            : base( )
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (CrashReporter.CrashReportExist)
            {
                CrashReporter.ShowMsgBox( );
                Application.Current.Shutdown( );
            }

        }

        /// <summary>
        /// Catch unhandled exceptions thrown on the main UI thread  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="exEventArgs"></param>
        private void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs exEventArgs)
        {
            if (exEventArgs.ExceptionObject == null)
            {
                Application.Current.Shutdown( );
                return;
            }
            Exception exept = (Exception)exEventArgs.ExceptionObject;
            new CrashReporter( ).CreateCrashReport("CurrentDomain_UnhandledException", exept);
            Application.Current.Shutdown( );
        }

        /// <summary>
        /// Catch unhandled exceptions thrown on the main UI thread and allow 
        /// option for user to continue program. 
        /// The OnDispatcherUnhandledException method below for AppDomain.UnhandledException will handle all other exceptions thrown by any thread.
        /// </summary>
        void AppUI_DispatcherUnhandledException (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs exEventArgs)
        {
            exEventArgs.Handled = true;
            if (exEventArgs.Exception == null)
            {
                Application.Current.Shutdown( );
                return;
            }
            Exception exept = (Exception)exEventArgs.Exception;
            new CrashReporter( ).CreateCrashReport("AppUI_DispatcherUnhandledException", exept);
            Application.Current.Shutdown( );
        }

        /// <summary>
        /// Catch unhandled exceptions not thrown by the main UI thread.
        /// The above AppUI_DispatcherUnhandledException method for DispatcherUnhandledException will only handle exceptions thrown by the main UI thread. 
        /// Unhandled exceptions caught by this method typically terminate the runtime.
        /// </summary>
        void OnDispatcherUnhandledException (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs exEventArgs)
        {
            exEventArgs.Handled = true;
            if (exEventArgs.Exception == null)
            {
                Application.Current.Shutdown( );
                return;
            }
            Exception exept = (Exception)exEventArgs.Exception;
            new CrashReporter( ).CreateCrashReport("OnDispatcherUnhandledException", exept);
            Application.Current.Shutdown( );
        }
    }
}
