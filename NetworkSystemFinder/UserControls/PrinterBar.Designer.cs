
namespace NetworkSystemFinder.UserControls
{
    partial class PrinterBar
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
            System.Windows.Forms.Button buttonPingDead;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterBar));
            System.Windows.Forms.Button buttonSearch;
            System.Windows.Forms.Button buttonCancel;
            System.Windows.Forms.Button buttonLog;
            System.Windows.Forms.Button buttonFilter;
            this.timerMouseControl = new System.Windows.Forms.Timer(this.components);
            this.textBoxMachineName = new System.Windows.Forms.TextBox();
            this.labelPcName = new System.Windows.Forms.Label();
            this.textBoxIPEnd = new System.Windows.Forms.TextBox();
            this.textBoxIPStart = new System.Windows.Forms.TextBox();
            this.labelIPStart = new System.Windows.Forms.Label();
            this.labelIPEnd = new System.Windows.Forms.Label();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.checkBoxResolveNames = new System.Windows.Forms.CheckBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabPageFilter = new System.Windows.Forms.TabPage();
            this.checkedListBoxColumns = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanelFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.labelCount = new System.Windows.Forms.Label();
            this.backgroundWorkerPinger = new System.ComponentModel.BackgroundWorker();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            buttonPingDead = new System.Windows.Forms.Button();
            buttonSearch = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            buttonLog = new System.Windows.Forms.Button();
            buttonFilter = new System.Windows.Forms.Button();
            this.tabPageSearch.SuspendLayout();
            this.tabControlLeft.SuspendLayout();
            this.tabPageFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPingDead
            // 
            buttonPingDead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPingDead.Image = ((System.Drawing.Image)(resources.GetObject("buttonPingDead.Image")));
            buttonPingDead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonPingDead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonPingDead.Location = new System.Drawing.Point(31, 254);
            buttonPingDead.Margin = new System.Windows.Forms.Padding(5);
            buttonPingDead.Name = "buttonPingDead";
            buttonPingDead.Size = new System.Drawing.Size(150, 32);
            buttonPingDead.TabIndex = 16;
            buttonPingDead.Text = "keyPingDead";
            buttonPingDead.UseVisualStyleBackColor = true;
            buttonPingDead.Click += new System.EventHandler(this.buttonPingDead_Click);
            // 
            // buttonSearch
            // 
            buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonSearch.Location = new System.Drawing.Point(31, 212);
            buttonSearch.Margin = new System.Windows.Forms.Padding(5);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new System.Drawing.Size(150, 32);
            buttonSearch.TabIndex = 0;
            buttonSearch.Text = "keySearch";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonCancel
            // 
            buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonCancel.Location = new System.Drawing.Point(123, 181);
            buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(87, 24);
            buttonCancel.TabIndex = 13;
            buttonCancel.Text = "keyCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLog
            // 
            buttonLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonLog.Location = new System.Drawing.Point(2, 181);
            buttonLog.Margin = new System.Windows.Forms.Padding(2);
            buttonLog.Name = "buttonLog";
            buttonLog.Size = new System.Drawing.Size(113, 24);
            buttonLog.TabIndex = 12;
            buttonLog.Text = "keyLog";
            buttonLog.UseVisualStyleBackColor = true;
            buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // buttonFilter
            // 
            buttonFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            buttonFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonFilter.Location = new System.Drawing.Point(137, 652);
            buttonFilter.Margin = new System.Windows.Forms.Padding(5);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new System.Drawing.Size(75, 24);
            buttonFilter.TabIndex = 16;
            buttonFilter.Text = "keyFilter";
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // timerMouseControl
            // 
            this.timerMouseControl.Tick += new System.EventHandler(this.timerMouseControl_Tick);
            // 
            // textBoxMachineName
            // 
            this.textBoxMachineName.Location = new System.Drawing.Point(2, 70);
            this.textBoxMachineName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMachineName.MaxLength = 64;
            this.textBoxMachineName.Name = "textBoxMachineName";
            this.textBoxMachineName.Size = new System.Drawing.Size(200, 20);
            this.textBoxMachineName.TabIndex = 14;
            // 
            // labelPcName
            // 
            this.labelPcName.AutoSize = true;
            this.labelPcName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPcName.Location = new System.Drawing.Point(2, 54);
            this.labelPcName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelPcName.Name = "labelPcName";
            this.labelPcName.Size = new System.Drawing.Size(38, 13);
            this.labelPcName.TabIndex = 15;
            this.labelPcName.Text = "keyPC";
            // 
            // textBoxIPEnd
            // 
            this.textBoxIPEnd.Location = new System.Drawing.Point(110, 29);
            this.textBoxIPEnd.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIPEnd.MaxLength = 15;
            this.textBoxIPEnd.Name = "textBoxIPEnd";
            this.textBoxIPEnd.Size = new System.Drawing.Size(100, 20);
            this.textBoxIPEnd.TabIndex = 2;
            // 
            // textBoxIPStart
            // 
            this.textBoxIPStart.Location = new System.Drawing.Point(2, 30);
            this.textBoxIPStart.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIPStart.MaxLength = 15;
            this.textBoxIPStart.Name = "textBoxIPStart";
            this.textBoxIPStart.Size = new System.Drawing.Size(100, 20);
            this.textBoxIPStart.TabIndex = 1;
            // 
            // labelIPStart
            // 
            this.labelIPStart.AutoSize = true;
            this.labelIPStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelIPStart.Location = new System.Drawing.Point(1, 11);
            this.labelIPStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelIPStart.Name = "labelIPStart";
            this.labelIPStart.Size = new System.Drawing.Size(56, 13);
            this.labelIPStart.TabIndex = 3;
            this.labelIPStart.Text = "keyIPStart";
            // 
            // labelIPEnd
            // 
            this.labelIPEnd.AutoSize = true;
            this.labelIPEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelIPEnd.Location = new System.Drawing.Point(107, 11);
            this.labelIPEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelIPEnd.Name = "labelIPEnd";
            this.labelIPEnd.Size = new System.Drawing.Size(53, 13);
            this.labelIPEnd.TabIndex = 4;
            this.labelIPEnd.Text = "keyIPEnd";
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
            this.tabPageSearch.Location = new System.Drawing.Point(4, 20);
            this.tabPageSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(212, 676);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "keySearch";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxResolveNames
            // 
            this.checkBoxResolveNames.AutoSize = true;
            this.checkBoxResolveNames.Checked = true;
            this.checkBoxResolveNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxResolveNames.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxResolveNames.Location = new System.Drawing.Point(100, 96);
            this.checkBoxResolveNames.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxResolveNames.Name = "checkBoxResolveNames";
            this.checkBoxResolveNames.Size = new System.Drawing.Size(110, 17);
            this.checkBoxResolveNames.TabIndex = 9;
            this.checkBoxResolveNames.Text = "keyResolveName";
            this.checkBoxResolveNames.UseVisualStyleBackColor = true;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(2, 117);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxUser.MaxLength = 64;
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(200, 20);
            this.textBoxUser.TabIndex = 5;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(2, 157);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPassword.MaxLength = 64;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(200, 20);
            this.textBoxPassword.TabIndex = 6;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUser.Location = new System.Drawing.Point(2, 101);
            this.labelUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(46, 13);
            this.labelUser.TabIndex = 7;
            this.labelUser.Text = "keyUser";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPassword.Location = new System.Drawing.Point(1, 141);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(70, 13);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "keyPassword";
            // 
            // tabControlLeft
            // 
            this.tabControlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlLeft.Controls.Add(this.tabPageSearch);
            this.tabControlLeft.Controls.Add(this.tabPageFilter);
            this.tabControlLeft.ItemSize = new System.Drawing.Size(48, 16);
            this.tabControlLeft.Location = new System.Drawing.Point(0, 3);
            this.tabControlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlLeft.Multiline = true;
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.Padding = new System.Drawing.Point(16, 2);
            this.tabControlLeft.SelectedIndex = 0;
            this.tabControlLeft.Size = new System.Drawing.Size(220, 700);
            this.tabControlLeft.TabIndex = 21;
            // 
            // tabPageFilter
            // 
            this.tabPageFilter.Controls.Add(this.checkedListBoxColumns);
            this.tabPageFilter.Controls.Add(buttonFilter);
            this.tabPageFilter.Controls.Add(this.flowLayoutPanelFilter);
            this.tabPageFilter.Location = new System.Drawing.Point(4, 20);
            this.tabPageFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageFilter.Name = "tabPageFilter";
            this.tabPageFilter.Size = new System.Drawing.Size(212, 676);
            this.tabPageFilter.TabIndex = 0;
            this.tabPageFilter.Text = "keyFilter";
            this.tabPageFilter.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxColumns
            // 
            this.checkedListBoxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxColumns.CheckOnClick = true;
            this.checkedListBoxColumns.FormattingEnabled = true;
            this.checkedListBoxColumns.Location = new System.Drawing.Point(2, 3);
            this.checkedListBoxColumns.Name = "checkedListBoxColumns";
            this.checkedListBoxColumns.ScrollAlwaysVisible = true;
            this.checkedListBoxColumns.Size = new System.Drawing.Size(207, 64);
            this.checkedListBoxColumns.TabIndex = 17;
            this.checkedListBoxColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxColumns_ItemCheck);
            this.checkedListBoxColumns.MouseEnter += new System.EventHandler(this.checkedListBoxColumns_MouseEnter);
            this.checkedListBoxColumns.MouseLeave += new System.EventHandler(this.checkedListBoxColumns_MouseLeave);
            // 
            // flowLayoutPanelFilter
            // 
            this.flowLayoutPanelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelFilter.AutoScroll = true;
            this.flowLayoutPanelFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelFilter.Location = new System.Drawing.Point(0, 73);
            this.flowLayoutPanelFilter.Name = "flowLayoutPanelFilter";
            this.flowLayoutPanelFilter.Size = new System.Drawing.Size(212, 560);
            this.flowLayoutPanelFilter.TabIndex = 14;
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.AutoSize = true;
            this.labelCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCount.Location = new System.Drawing.Point(164, 727);
            this.labelCount.Margin = new System.Windows.Forms.Padding(5);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(51, 13);
            this.labelCount.TabIndex = 20;
            this.labelCount.Text = "keyRows";
            this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.progressBarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarSearch.Location = new System.Drawing.Point(4, 705);
            this.progressBarSearch.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarSearch.MarqueeAnimationSpeed = 25;
            this.progressBarSearch.Name = "progressBarSearch";
            this.progressBarSearch.Size = new System.Drawing.Size(212, 10);
            this.progressBarSearch.TabIndex = 19;
            // 
            // PrinterBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlLeft);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.progressBarSearch);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "PrinterBar";
            this.Size = new System.Drawing.Size(220, 742);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            this.tabControlLeft.ResumeLayout(false);
            this.tabPageFilter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerMouseControl;
        private System.Windows.Forms.TextBox textBoxMachineName;
        private System.Windows.Forms.Label labelPcName;
        private System.Windows.Forms.TextBox textBoxIPEnd;
        private System.Windows.Forms.TextBox textBoxIPStart;
        private System.Windows.Forms.Label labelIPStart;
        private System.Windows.Forms.Label labelIPEnd;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.CheckBox checkBoxResolveNames;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabPageFilter;
        private System.Windows.Forms.CheckedListBox checkedListBoxColumns;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFilter;
        private System.Windows.Forms.Label labelCount;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPinger;
        private System.Windows.Forms.ProgressBar progressBarSearch;
    }
}
