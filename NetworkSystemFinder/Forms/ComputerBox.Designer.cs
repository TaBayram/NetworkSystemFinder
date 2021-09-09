
namespace NetworkSystemFinder.Forms
{
    partial class ComputerBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerBox));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonOpenAll = new System.Windows.Forms.Button();
            this.buttonCloseAll = new System.Windows.Forms.Button();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.buttonQuerySearch = new System.Windows.Forms.Button();
            this.toolTipQuery = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarExcel = new System.Windows.Forms.ProgressBar();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.backgroundWorkerExcel = new System.ComponentModel.BackgroundWorker();
            this.labelQuerySearch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(-1, 38);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(414, 511);
            this.treeView1.TabIndex = 0;
            // 
            // buttonOpenAll
            // 
            this.buttonOpenAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenAll.Location = new System.Drawing.Point(12, 9);
            this.buttonOpenAll.Name = "buttonOpenAll";
            this.buttonOpenAll.Size = new System.Drawing.Size(90, 23);
            this.buttonOpenAll.TabIndex = 1;
            this.buttonOpenAll.Text = "keyOpenAll";
            this.buttonOpenAll.UseVisualStyleBackColor = true;
            this.buttonOpenAll.Click += new System.EventHandler(this.buttonOpenAll_Click);
            // 
            // buttonCloseAll
            // 
            this.buttonCloseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseAll.Location = new System.Drawing.Point(108, 9);
            this.buttonCloseAll.Name = "buttonCloseAll";
            this.buttonCloseAll.Size = new System.Drawing.Size(90, 23);
            this.buttonCloseAll.TabIndex = 2;
            this.buttonCloseAll.Text = "keyCloseAll";
            this.buttonCloseAll.UseVisualStyleBackColor = true;
            this.buttonCloseAll.Click += new System.EventHandler(this.buttonCloseAll_Click);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuery.Location = new System.Drawing.Point(12, 571);
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(390, 20);
            this.textBoxQuery.TabIndex = 3;
            this.toolTipQuery.SetToolTip(this.textBoxQuery, "Example: SELECT * FROM WIN32_PROCESSOR");
            // 
            // buttonQuerySearch
            // 
            this.buttonQuerySearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuerySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuerySearch.Location = new System.Drawing.Point(327, 597);
            this.buttonQuerySearch.Name = "buttonQuerySearch";
            this.buttonQuerySearch.Size = new System.Drawing.Size(75, 23);
            this.buttonQuerySearch.TabIndex = 4;
            this.buttonQuerySearch.Text = "keySearch";
            this.buttonQuerySearch.UseVisualStyleBackColor = true;
            this.buttonQuerySearch.Click += new System.EventHandler(this.buttonQuerySearch_Click);
            // 
            // toolTipQuery
            // 
            this.toolTipQuery.IsBalloon = true;
            // 
            // progressBarExcel
            // 
            this.progressBarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarExcel.Location = new System.Drawing.Point(336, 10);
            this.progressBarExcel.Name = "progressBarExcel";
            this.progressBarExcel.Size = new System.Drawing.Size(36, 23);
            this.progressBarExcel.TabIndex = 6;
            // 
            // buttonExcel
            // 
            this.buttonExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExcel.BackColor = System.Drawing.Color.Transparent;
            this.buttonExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExcel.BackgroundImage")));
            this.buttonExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExcel.FlatAppearance.BorderSize = 0;
            this.buttonExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonExcel.Location = new System.Drawing.Point(378, 9);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(24, 24);
            this.buttonExcel.TabIndex = 5;
            this.buttonExcel.UseVisualStyleBackColor = false;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // backgroundWorkerExcel
            // 
            this.backgroundWorkerExcel.WorkerReportsProgress = true;
            this.backgroundWorkerExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExcel_DoWork);
            this.backgroundWorkerExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerExcel_ProgressChanged);
            this.backgroundWorkerExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerExcel_RunWorkerCompleted);
            // 
            // labelQuerySearch
            // 
            this.labelQuerySearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelQuerySearch.AutoSize = true;
            this.labelQuerySearch.Location = new System.Drawing.Point(13, 552);
            this.labelQuerySearch.Name = "labelQuerySearch";
            this.labelQuerySearch.Size = new System.Drawing.Size(86, 13);
            this.labelQuerySearch.TabIndex = 7;
            this.labelQuerySearch.Text = "keyQuerySearch";
            // 
            // ComputerBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 632);
            this.Controls.Add(this.labelQuerySearch);
            this.Controls.Add(this.progressBarExcel);
            this.Controls.Add(this.buttonExcel);
            this.Controls.Add(this.buttonQuerySearch);
            this.Controls.Add(this.textBoxQuery);
            this.Controls.Add(this.buttonCloseAll);
            this.Controls.Add(this.buttonOpenAll);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ComputerBox";
            this.Text = "MachineInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonOpenAll;
        private System.Windows.Forms.Button buttonCloseAll;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.Button buttonQuerySearch;
        private System.Windows.Forms.ToolTip toolTipQuery;
        private System.Windows.Forms.ProgressBar progressBarExcel;
        private System.Windows.Forms.Button buttonExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExcel;
        private System.Windows.Forms.Label labelQuerySearch;
    }
}