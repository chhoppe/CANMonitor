using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Shared.Crash;

namespace QoSCalc.UserInterfaces.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static Options Opt;
        public static Progress Progr;

        public App()
            :base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (CrashReporter.CrashReportExist)
            {
                CrashReporter.ShowMsgBox( );
                Application.Current.Shutdown( );
            }
        }

        private void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject == null)
            {
                Application.Current.Shutdown();
                return;
            }
            Exception ex = (Exception)e.ExceptionObject;
            PrintCrashReport("CurrentDomain_UnhandledException", ex);
        }

        /// <summary>
        /// Catch unhandled exceptions thrown on the main UI thread and allow 
        /// option for user to continue program. 
        /// The OnDispatcherUnhandledException method below for AppDomain.UnhandledException will handle all other exceptions thrown by any thread.
        /// </summary>
        void AppUI_DispatcherUnhandledException (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception == null)
            {
                Application.Current.Shutdown();
                return;
            }
            PrintCrashReport("AppUI_DispatcherUnhandledException", e.Exception);
            Application.Current.Shutdown();
            e.Handled = true;
        }

        /// <summary>
        /// Catch unhandled exceptions not thrown by the main UI thread.
        /// The above AppUI_DispatcherUnhandledException method for DispatcherUnhandledException will only handle exceptions thrown by the main UI thread. 
        /// Unhandled exceptions caught by this method typically terminate the runtime.
        /// </summary>
        void OnDispatcherUnhandledException (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception == null)
            {
                Application.Current.Shutdown();
                return;
            }
            PrintCrashReport("OnDispatcherUnhandledException", e.Exception);
            Application.Current.Shutdown();
            e.Handled = true;
        }


    }

}