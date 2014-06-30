using System.Windows;

namespace CANMonitor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ( )
        {
            InitializeComponent( );
        }

        private void WindowLoaded (object sender, System.Windows.RoutedEventArgs e)
        {
            SettingsControlForm.SettingsFinished += SettingsControlForm_SettingsFinished;
        }

        void SettingsControlForm_SettingsFinished (object sender, bool changed)
        {
            SettingsBorder.Visibility = System.Windows.Visibility.Hidden;
        }

        private void OnBnSettingsClicked (object sender, System.Windows.RoutedEventArgs e)
        {
            SettingsBorder.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
