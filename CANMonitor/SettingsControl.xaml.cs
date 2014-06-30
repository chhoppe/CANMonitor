using System.Windows.Controls;

namespace CANMonitor.GUI
{
    /// <summary>
    /// Interaktionslogik für SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        static public CANMonitor.DB.Settings Settings
        {
            get
            {
                return App.Settings;
            }
        }

        public SettingsControl ( )
        {
            InitializeComponent( );
        }

        private void ControlLoaded (object sender, System.Windows.RoutedEventArgs e)
        {
			this.DataContext = Settings;
            // TODO: Ereignishandlerimplementierung hier einfügen.
        }

        private void OnBnRevertClicked(object sender, System.Windows.RoutedEventArgs e)
        {
        	Settings.Load();
        }

        private void OnBnDefaultsClicked(object sender, System.Windows.RoutedEventArgs e)
        {
        	Settings.LoadDefaults();
        }

        private void OnBnSaveClicked(object sender, System.Windows.RoutedEventArgs e)
        {
			Settings.Save();
        }

    }
}
