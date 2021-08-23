
namespace NetworkSystemFinder.UserControls
{
    partial class FilterNumber
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.labelMax = new System.Windows.Forms.Label();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.labelMin = new System.Windows.Forms.Label();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.labelMax);
            this.groupBox.Controls.Add(this.textBoxMax);
            this.groupBox.Controls.Add(this.labelMin);
            this.groupBox.Controls.Add(this.textBoxMin);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox.Size = new System.Drawing.Size(205, 75);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(92, 18);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(44, 13);
            this.labelMax.TabIndex = 3;
            this.labelMax.Text = "keyMax";
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(95, 34);
            this.textBoxMax.MaxLength = 12;
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(85, 20);
            this.textBoxMax.TabIndex = 2;
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(4, 18);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(41, 13);
            this.labelMin.TabIndex = 1;
            this.labelMin.Text = "keyMin";
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(4, 34);
            this.textBoxMin.MaxLength = 12;
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(85, 20);
            this.textBoxMin.TabIndex = 0;
            // 
            // FilterNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "FilterNumber";
            this.Size = new System.Drawing.Size(205, 75);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.TextBox textBoxMin;
    }
}
