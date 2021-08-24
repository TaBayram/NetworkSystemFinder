
namespace NetworkSystemFinder.UserControls
{
    partial class IPBar
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
            System.Windows.Forms.Button buttonSearch;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPBar));
            System.Windows.Forms.Button buttonLog;
            System.Windows.Forms.Button buttonCancel;
            System.Windows.Forms.Button buttonFilter;
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkedListBoxColumns = new System.Windows.Forms.CheckedListBox();
            buttonSearch = new System.Windows.Forms.Button();
            buttonLog = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            buttonFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            resources.ApplyResources(buttonSearch, "buttonSearch");
            buttonSearch.Name = "buttonSearch";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonLog
            // 
            resources.ApplyResources(buttonLog, "buttonLog");
            buttonLog.Name = "buttonLog";
            buttonLog.UseVisualStyleBackColor = true;
            buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonFilter
            // 
            resources.ApplyResources(buttonFilter, "buttonFilter");
            buttonFilter.Name = "buttonFilter";
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
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
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // checkedListBoxColumns
            // 
            this.checkedListBoxColumns.CheckOnClick = true;
            this.checkedListBoxColumns.FormattingEnabled = true;
            resources.ApplyResources(this.checkedListBoxColumns, "checkedListBoxColumns");
            this.checkedListBoxColumns.Name = "checkedListBoxColumns";
            this.checkedListBoxColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxColumns_ItemCheck);
            // 
            // IPBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkedListBoxColumns);
            this.Controls.Add(this.checkBoxResolveNames);
            this.Controls.Add(buttonFilter);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(buttonCancel);
            this.Controls.Add(buttonLog);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.progressBarSearch);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.labelIPEnd);
            this.Controls.Add(this.labelIPStart);
            this.Controls.Add(this.textBoxIPEnd);
            this.Controls.Add(this.textBoxIPStart);
            this.Controls.Add(buttonSearch);
            this.Name = "IPBar";
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBoxColumns;
    }
}
