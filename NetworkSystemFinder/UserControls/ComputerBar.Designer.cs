
namespace NetworkSystemFinder.UserControls
{
    partial class ComputerBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button buttonLog;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerBar));
            System.Windows.Forms.Button buttonFilter;
            System.Windows.Forms.Button buttonPingDead;
            System.Windows.Forms.Button buttonSearch;
            System.Windows.Forms.Button buttonCancel;
            this.textBoxIPStart = new System.Windows.Forms.TextBox();
            this.textBoxIPEnd = new System.Windows.Forms.TextBox();
            this.labelIPStart = new System.Windows.Forms.Label();
            this.labelIPEnd = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.checkBoxResolveNames = new System.Windows.Forms.CheckBox();
            this.backgroundWorkerPinger = new System.ComponentModel.BackgroundWorker();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            this.labelCount = new System.Windows.Forms.Label();
            this.flowLayoutPanelFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.checkedListBoxColumns = new System.Windows.Forms.CheckedListBox();
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.textBoxMachineName = new System.Windows.Forms.TextBox();
            this.labelPcName = new System.Windows.Forms.Label();
            this.tabPageFilter = new System.Windows.Forms.TabPage();
            this.timerMouseControl = new System.Windows.Forms.Timer(this.components);
            buttonLog = new System.Windows.Forms.Button();
            buttonFilter = new System.Windows.Forms.Button();
            buttonPingDead = new System.Windows.Forms.Button();
            buttonSearch = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            this.tabControlLeft.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLog
            // 
            resources.ApplyResources(buttonLog, "buttonLog");
            buttonLog.Name = "buttonLog";
            buttonLog.UseVisualStyleBackColor = true;
            buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // buttonFilter
            // 
            resources.ApplyResources(buttonFilter, "buttonFilter");
            buttonFilter.Name = "buttonFilter";
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonPingDead
            // 
            resources.ApplyResources(buttonPingDead, "buttonPingDead");
            buttonPingDead.Name = "buttonPingDead";
            buttonPingDead.UseVisualStyleBackColor = true;
            buttonPingDead.Click += new System.EventHandler(this.buttonPingDead_Click);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(buttonSearch, "buttonSearch");
            buttonSearch.Name = "buttonSearch";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxIPStart
            // 
            resources.ApplyResources(this.textBoxIPStart, "textBoxIPStart");
            this.textBoxIPStart.Name = "textBoxIPStart";
            // 
            // textBoxIPEnd
            // 
            resources.ApplyResources(this.textBoxIPEnd, "textBoxIPEnd");
            this.textBoxIPEnd.Name = "textBoxIPEnd";
            // 
            // labelIPStart
            // 
            resources.ApplyResources(this.labelIPStart, "labelIPStart");
            this.labelIPStart.Name = "labelIPStart";
            // 
            // labelIPEnd
            // 
            resources.ApplyResources(this.labelIPEnd, "labelIPEnd");
            this.labelIPEnd.Name = "labelIPEnd";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // labelUser
            // 
            resources.ApplyResources(this.labelUser, "labelUser");
            this.labelUser.Name = "labelUser";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // textBoxUser
            // 
            resources.ApplyResources(this.textBoxUser, "textBoxUser");
            this.textBoxUser.Name = "textBoxUser";
            // 
            // checkBoxResolveNames
            // 
            resources.ApplyResources(this.checkBoxResolveNames, "checkBoxResolveNames");
            this.checkBoxResolveNames.Checked = true;
            this.checkBoxResolveNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxResolveNames.Name = "checkBoxResolveNames";
            this.checkBoxResolveNames.UseVisualStyleBackColor = true;
            // 
            // backgroundWorkerPinger
            // 
            this.backgroundWorkerPinger.WorkerReportsProgress = true;
            this.backgroundWorkerPinger.WorkerSupportsCancellation = true;
            this.backgroundWorkerPinger.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPinger_DoWork);
            this.backgroundWorkerPinger.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPinger_ProgressChanged);
            this.backgroundWorkerPinger.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPinger_RunWorkerCompleted);
            // 
            // progressBarSearch
            // 
            resources.ApplyResources(this.progressBarSearch, "progressBarSearch");
            this.progressBarSearch.MarqueeAnimationSpeed = 25;
            this.progressBarSearch.Name = "progressBarSearch";
            // 
            // labelCount
            // 
            resources.ApplyResources(this.labelCount, "labelCount");
            this.labelCount.Name = "labelCount";
            // 
            // flowLayoutPanelFilter
            // 
            resources.ApplyResources(this.flowLayoutPanelFilter, "flowLayoutPanelFilter");
            this.flowLayoutPanelFilter.Name = "flowLayoutPanelFilter";
            // 
            // checkedListBoxColumns
            // 
            resources.ApplyResources(this.checkedListBoxColumns, "checkedListBoxColumns");
            this.checkedListBoxColumns.CheckOnClick = true;
            this.checkedListBoxColumns.FormattingEnabled = true;
            this.checkedListBoxColumns.Name = "checkedListBoxColumns";
            this.checkedListBoxColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxColumns_ItemCheck);
            this.checkedListBoxColumns.MouseEnter += new System.EventHandler(this.checkedListBoxColumns_MouseEnter);
            this.checkedListBoxColumns.MouseLeave += new System.EventHandler(this.checkedListBoxColumns_MouseLeave);
            // 
            // tabControlLeft
            // 
            resources.ApplyResources(this.tabControlLeft, "tabControlLeft");
            this.tabControlLeft.Controls.Add(this.tabPageSearch);
            this.tabControlLeft.Controls.Add(this.tabPageFilter);
            this.tabControlLeft.Multiline = true;
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.SelectedIndex = 0;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(buttonPingDead);
            this.tabPageSearch.Controls.Add(this.textBoxMachineName);
            this.tabPageSearch.Controls.Add(this.labelPcName);
            this.tabPageSearch.Controls.Add(this.textBoxIPEnd);
            this.tabPageSearch.Controls.Add(buttonSearch);
            this.tabPageSearch.Controls.Add(this.checkBoxResolveNames);
            this.tabPageSearch.Controls.Add(this.textBoxIPStart);
            this.tabPageSearch.Controls.Add(this.labelIPStart);
            this.tabPageSearch.Controls.Add(this.labelIPEnd);
            this.tabPageSearch.Controls.Add(buttonCancel);
            this.tabPageSearch.Controls.Add(this.textBoxUser);
            this.tabPageSearch.Controls.Add(buttonLog);
            this.tabPageSearch.Controls.Add(this.textBoxPassword);
            this.tabPageSearch.Controls.Add(this.labelUser);
            this.tabPageSearch.Controls.Add(this.labelPassword);
            resources.ApplyResources(this.tabPageSearch, "tabPageSearch");
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // textBoxMachineName
            // 
            resources.ApplyResources(this.textBoxMachineName, "textBoxMachineName");
            this.textBoxMachineName.Name = "textBoxMachineName";
            // 
            // labelPcName
            // 
            resources.ApplyResources(this.labelPcName, "labelPcName");
            this.labelPcName.Name = "labelPcName";
            // 
            // tabPageFilter
            // 
            this.tabPageFilter.Controls.Add(this.checkedListBoxColumns);
            this.tabPageFilter.Controls.Add(buttonFilter);
            this.tabPageFilter.Controls.Add(this.flowLayoutPanelFilter);
            resources.ApplyResources(this.tabPageFilter, "tabPageFilter");
            this.tabPageFilter.Name = "tabPageFilter";
            this.tabPageFilter.UseVisualStyleBackColor = true;
            // 
            // timerMouseControl
            // 
            this.timerMouseControl.Tick += new System.EventHandler(this.timerMouseControl_Tick);
            // 
            // ComputerBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlLeft);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.progressBarSearch);
            this.Name = "ComputerBar";
            this.VisibleChanged += new System.EventHandler(this.ComputerBar_VisibleChanged);
            this.tabControlLeft.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            this.tabPageFilter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIPStart;
        private System.Windows.Forms.TextBox textBoxIPEnd;
        private System.Windows.Forms.Label labelIPStart;
        private System.Windows.Forms.Label labelIPEnd;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.CheckBox checkBoxResolveNames;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPinger;
        private System.Windows.Forms.ProgressBar progressBarSearch;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFilter;
        private System.Windows.Forms.CheckedListBox checkedListBoxColumns;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabPageFilter;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TextBox textBoxMachineName;
        private System.Windows.Forms.Label labelPcName;
        private System.Windows.Forms.Timer timerMouseControl;
    }
}
