namespace QoSCalc.Common
{
    partial class CrashMsgBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrashMsgBox));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.saveFileDialogFilter = new System.Windows.Forms.Label();
            this.textBoxGeneral = new System.Windows.Forms.RichTextBox();
            this.buttonIgnore = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabPageExceptions = new System.Windows.Forms.TabPage();
            this.textBoxExceptions = new System.Windows.Forms.RichTextBox();
            this.tabPageStackTrace = new System.Windows.Forms.TabPage();
            this.textBoxStackTrace = new System.Windows.Forms.RichTextBox();
            this.tabPageEnvironment = new System.Windows.Forms.TabPage();
            this.textBoxEnvironment = new System.Windows.Forms.RichTextBox();
            this.tabPageUserAssemblies = new System.Windows.Forms.TabPage();
            this.textBoxUserAssemblies = new System.Windows.Forms.RichTextBox();
            this.tabPageAllAssemblies = new System.Windows.Forms.TabPage();
            this.textBoxAllAssemblies = new System.Windows.Forms.RichTextBox();
            this.crashReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageExceptions.SuspendLayout();
            this.tabPageStackTrace.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageUserAssemblies.SuspendLayout();
            this.tabPageAllAssemblies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crashReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageExceptions);
            this.tabControl1.Controls.Add(this.tabPageStackTrace);
            this.tabControl1.Controls.Add(this.tabPageEnvironment);
            this.tabControl1.Controls.Add(this.tabPageUserAssemblies);
            this.tabControl1.Controls.Add(this.tabPageAllAssemblies);
            this.tabControl1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.crashReportBindingSource, "Environment", true));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Controls.Add(this.saveFileDialogFilter);
            this.tabPageGeneral.Controls.Add(this.textBoxGeneral);
            this.tabPageGeneral.Controls.Add(this.buttonIgnore);
            this.tabPageGeneral.Controls.Add(this.buttonSave);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // saveFileDialogFilter
            // 
            resources.ApplyResources(this.saveFileDialogFilter, "saveFileDialogFilter");
            this.saveFileDialogFilter.Name = "saveFileDialogFilter";
            // 
            // textBoxGeneral
            // 
            resources.ApplyResources(this.textBoxGeneral, "textBoxGeneral");
            this.textBoxGeneral.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "General", true));
            this.textBoxGeneral.Name = "textBoxGeneral";
            this.textBoxGeneral.ReadOnly = true;
            this.textBoxGeneral.Click += new System.EventHandler(this.OnBnIngoreClicked);
            // 
            // buttonIgnore
            // 
            resources.ApplyResources(this.buttonIgnore, "buttonIgnore");
            this.buttonIgnore.Name = "buttonIgnore";
            this.buttonIgnore.UseVisualStyleBackColor = true;
            this.buttonIgnore.Click += new System.EventHandler(this.OnBnIngoreClicked);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.OnBnSaveClicked);
            // 
            // tabPageExceptions
            // 
            resources.ApplyResources(this.tabPageExceptions, "tabPageExceptions");
            this.tabPageExceptions.Controls.Add(this.textBoxExceptions);
            this.tabPageExceptions.Name = "tabPageExceptions";
            this.tabPageExceptions.UseVisualStyleBackColor = true;
            // 
            // textBoxExceptions
            // 
            resources.ApplyResources(this.textBoxExceptions, "textBoxExceptions");
            this.textBoxExceptions.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "Exceptions", true));
            this.textBoxExceptions.Name = "textBoxExceptions";
            this.textBoxExceptions.ReadOnly = true;
            // 
            // tabPageStackTrace
            // 
            resources.ApplyResources(this.tabPageStackTrace, "tabPageStackTrace");
            this.tabPageStackTrace.Controls.Add(this.textBoxStackTrace);
            this.tabPageStackTrace.Name = "tabPageStackTrace";
            this.tabPageStackTrace.UseVisualStyleBackColor = true;
            // 
            // textBoxStackTrace
            // 
            resources.ApplyResources(this.textBoxStackTrace, "textBoxStackTrace");
            this.textBoxStackTrace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "StackTrace", true));
            this.textBoxStackTrace.Name = "textBoxStackTrace";
            this.textBoxStackTrace.ReadOnly = true;
            // 
            // tabPageEnvironment
            // 
            resources.ApplyResources(this.tabPageEnvironment, "tabPageEnvironment");
            this.tabPageEnvironment.Controls.Add(this.textBoxEnvironment);
            this.tabPageEnvironment.Name = "tabPageEnvironment";
            this.tabPageEnvironment.UseVisualStyleBackColor = true;
            // 
            // textBoxEnvironment
            // 
            resources.ApplyResources(this.textBoxEnvironment, "textBoxEnvironment");
            this.textBoxEnvironment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "Environment", true));
            this.textBoxEnvironment.Name = "textBoxEnvironment";
            this.textBoxEnvironment.ReadOnly = true;
            // 
            // tabPageUserAssemblies
            // 
            resources.ApplyResources(this.tabPageUserAssemblies, "tabPageUserAssemblies");
            this.tabPageUserAssemblies.Controls.Add(this.textBoxUserAssemblies);
            this.tabPageUserAssemblies.Name = "tabPageUserAssemblies";
            this.tabPageUserAssemblies.UseVisualStyleBackColor = true;
            // 
            // textBoxUserAssemblies
            // 
            resources.ApplyResources(this.textBoxUserAssemblies, "textBoxUserAssemblies");
            this.textBoxUserAssemblies.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "UserAssemblies", true));
            this.textBoxUserAssemblies.Name = "textBoxUserAssemblies";
            this.textBoxUserAssemblies.ReadOnly = true;
            // 
            // tabPageAllAssemblies
            // 
            resources.ApplyResources(this.tabPageAllAssemblies, "tabPageAllAssemblies");
            this.tabPageAllAssemblies.Controls.Add(this.textBoxAllAssemblies);
            this.tabPageAllAssemblies.Name = "tabPageAllAssemblies";
            this.tabPageAllAssemblies.UseVisualStyleBackColor = true;
            // 
            // textBoxAllAssemblies
            // 
            resources.ApplyResources(this.textBoxAllAssemblies, "textBoxAllAssemblies");
            this.textBoxAllAssemblies.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.crashReportBindingSource, "AllAssemblies", true));
            this.textBoxAllAssemblies.Name = "textBoxAllAssemblies";
            this.textBoxAllAssemblies.ReadOnly = true;
            // 
            // crashReportBindingSource
            // 
            this.crashReportBindingSource.DataSource = typeof(QoSCalc.Common.CrashReport);
            // 
            // CrashMsgBox
            // 
            this.AcceptButton = this.buttonIgnore;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CrashMsgBox";
            this.Load += new System.EventHandler(this.Form_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageExceptions.ResumeLayout(false);
            this.tabPageStackTrace.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageUserAssemblies.ResumeLayout(false);
            this.tabPageAllAssemblies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crashReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageExceptions;
        private System.Windows.Forms.TabPage tabPageStackTrace;
        private System.Windows.Forms.TabPage tabPageUserAssemblies;
        private System.Windows.Forms.TabPage tabPageAllAssemblies;
        private System.Windows.Forms.TabPage tabPageEnvironment;
        private System.Windows.Forms.Button buttonIgnore;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.RichTextBox textBoxGeneral;
        private System.Windows.Forms.RichTextBox textBoxExceptions;
        private System.Windows.Forms.RichTextBox textBoxStackTrace;
        private System.Windows.Forms.RichTextBox textBoxEnvironment;
        private System.Windows.Forms.RichTextBox textBoxUserAssemblies;
        private System.Windows.Forms.RichTextBox textBoxAllAssemblies;
        private System.Windows.Forms.BindingSource crashReportBindingSource;
        private System.Windows.Forms.Label saveFileDialogFilter;
    }
}