
namespace NetworkSystemFinder
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.progressBarExcel = new System.Windows.Forms.ProgressBar();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.splitContainerHome = new System.Windows.Forms.SplitContainer();
            this.panelLeftBar = new System.Windows.Forms.Panel();
            this.panelMainButtons = new System.Windows.Forms.Panel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonPrinters = new System.Windows.Forms.Button();
            this.buttonLANDevices = new System.Windows.Forms.Button();
            this.panelHome = new System.Windows.Forms.Panel();
            this.dataViewMain = new System.Windows.Forms.DataGridView();
            this.backgroundWorkerExcel = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHome)).BeginInit();
            this.splitContainerHome.Panel1.SuspendLayout();
            this.splitContainerHome.Panel2.SuspendLayout();
            this.splitContainerHome.SuspendLayout();
            this.panelLeftBar.SuspendLayout();
            this.panelMainButtons.SuspendLayout();
            this.panelHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            resources.ApplyResources(this.splitContainerMain, "splitContainerMain");
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelHeader);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerHome);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.buttonBack);
            this.panelHeader.Controls.Add(this.progressBarExcel);
            this.panelHeader.Controls.Add(this.buttonExcel);
            resources.ApplyResources(this.panelHeader, "panelHeader");
            this.panelHeader.Name = "panelHeader";
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImage = global::NetworkSystemFinder.Properties.Resources._64x64BackArrow;
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // progressBarExcel
            // 
            resources.ApplyResources(this.progressBarExcel, "progressBarExcel");
            this.progressBarExcel.Name = "progressBarExcel";
            // 
            // buttonExcel
            // 
            resources.ApplyResources(this.buttonExcel, "buttonExcel");
            this.buttonExcel.BackColor = System.Drawing.Color.Transparent;
            this.buttonExcel.FlatAppearance.BorderSize = 0;
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.UseVisualStyleBackColor = false;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // splitContainerHome
            // 
            resources.ApplyResources(this.splitContainerHome, "splitContainerHome");
            this.splitContainerHome.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerHome.Name = "splitContainerHome";
            // 
            // splitContainerHome.Panel1
            // 
            this.splitContainerHome.Panel1.Controls.Add(this.panelLeftBar);
            // 
            // splitContainerHome.Panel2
            // 
            this.splitContainerHome.Panel2.Controls.Add(this.panelHome);
            // 
            // panelLeftBar
            // 
            this.panelLeftBar.Controls.Add(this.panelMainButtons);
            resources.ApplyResources(this.panelLeftBar, "panelLeftBar");
            this.panelLeftBar.Name = "panelLeftBar";
            // 
            // panelMainButtons
            // 
            this.panelMainButtons.Controls.Add(this.buttonSettings);
            this.panelMainButtons.Controls.Add(this.buttonPrinters);
            this.panelMainButtons.Controls.Add(this.buttonLANDevices);
            resources.ApplyResources(this.panelMainButtons, "panelMainButtons");
            this.panelMainButtons.Name = "panelMainButtons";
            // 
            // buttonSettings
            // 
            resources.ApplyResources(this.buttonSettings, "buttonSettings");
            this.buttonSettings.Image = global::NetworkSystemFinder.Properties.Resources._32x32SettingsInverted;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonPrinters
            // 
            resources.ApplyResources(this.buttonPrinters, "buttonPrinters");
            this.buttonPrinters.Image = global::NetworkSystemFinder.Properties.Resources._32x32PrinterInverted;
            this.buttonPrinters.Name = "buttonPrinters";
            this.buttonPrinters.UseVisualStyleBackColor = true;
            this.buttonPrinters.Click += new System.EventHandler(this.buttonPrinters_Click);
            // 
            // buttonLANDevices
            // 
            resources.ApplyResources(this.buttonLANDevices, "buttonLANDevices");
            this.buttonLANDevices.Image = global::NetworkSystemFinder.Properties.Resources._32x32PCInverted;
            this.buttonLANDevices.Name = "buttonLANDevices";
            this.buttonLANDevices.UseVisualStyleBackColor = true;
            this.buttonLANDevices.Click += new System.EventHandler(this.buttonLANDevices_Click);
            // 
            // panelHome
            // 
            this.panelHome.Controls.Add(this.dataViewMain);
            resources.ApplyResources(this.panelHome, "panelHome");
            this.panelHome.Name = "panelHome";
            // 
            // dataViewMain
            // 
            this.dataViewMain.AllowUserToAddRows = false;
            this.dataViewMain.AllowUserToDeleteRows = false;
            this.dataViewMain.AllowUserToOrderColumns = true;
            this.dataViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataViewMain, "dataViewMain");
            this.dataViewMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataViewMain.Name = "dataViewMain";
            this.dataViewMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataViewMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataViewMain.RowHeadersVisible = false;
            this.dataViewMain.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataViewMain_RowsAdded);
            this.dataViewMain.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataViewMain_RowsRemoved);
            // 
            // backgroundWorkerExcel
            // 
            this.backgroundWorkerExcel.WorkerReportsProgress = true;
            this.backgroundWorkerExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExcel_DoWork);
            this.backgroundWorkerExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerExcel_ProgressChanged);
            this.backgroundWorkerExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerExcel_RunWorkerCompleted);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "Main";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.splitContainerHome.Panel1.ResumeLayout(false);
            this.splitContainerHome.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHome)).EndInit();
            this.splitContainerHome.ResumeLayout(false);
            this.panelLeftBar.ResumeLayout(false);
            this.panelMainButtons.ResumeLayout(false);
            this.panelHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.SplitContainer splitContainerHome;
        private System.Windows.Forms.Panel panelLeftBar;
        private System.Windows.Forms.Button buttonLANDevices;
        private System.Windows.Forms.Panel panelHome;
        private System.Windows.Forms.DataGridView dataViewMain;
        private System.Windows.Forms.Panel panelMainButtons;
        private System.Windows.Forms.Button buttonExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExcel;
        private System.Windows.Forms.ProgressBar progressBarExcel;
        private System.Windows.Forms.Button buttonPrinters;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSettings;
    }
}

