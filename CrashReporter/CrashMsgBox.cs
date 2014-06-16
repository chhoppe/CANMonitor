using System;
using System.Windows.Forms;

namespace Shared.Crash
{
    public partial class CrashMsgBox : Form
    {
        private System.Resources.ResourceManager LocRM;

        public CrashMsgBox ( )
        {

            InitializeComponent( );
        }

        private void Form_Load (object sender, System.EventArgs e)
        {
            crashReportBindingSource.DataSource = CrashReporter.Report;
            LocRM = new System.Resources.ResourceManager(typeof(CrashMsgBox).FullName, typeof(CrashMsgBox).Assembly);
        }

        private void OnBnIngoreClicked (object sender, System.EventArgs e)
        {
            CrashReporter.RemoveCrashReport( );
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void OnBnSaveClicked (object sender, System.EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog( );
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.Filter = LocRM.GetString("saveFileDialogFilter.Text");
            sfd.FilterIndex = 1;
            sfd.FileName = "crash.xml";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    CrashReporter.SaveReport(sfd.FileName);
                    CrashReporter.RemoveCrashReport( );
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch
                {

                }
            }
        }

    }
}
