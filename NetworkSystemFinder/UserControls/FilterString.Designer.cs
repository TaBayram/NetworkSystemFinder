
namespace NetworkSystemFinder.UserControls
{
    partial class FilterString
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
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonNone = new System.Windows.Forms.Button();
            this.buttonAll = new System.Windows.Forms.Button();
            this.timerMouseControl = new System.Windows.Forms.Timer(this.components);
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(3, 43);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(1);
            this.checkedListBox.MinimumSize = new System.Drawing.Size(180, 0);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(180, 0);
            this.checkedListBox.Sorted = true;
            this.checkedListBox.TabIndex = 0;
            this.checkedListBox.MouseEnter += new System.EventHandler(this.checkedListBox_MouseEnter);
            this.checkedListBox.MouseLeave += new System.EventHandler(this.checkedListBox_MouseLeave);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(3, 17);
            this.textBox.Margin = new System.Windows.Forms.Padding(1);
            this.textBox.MaximumSize = new System.Drawing.Size(300, 4);
            this.textBox.MaxLength = 64;
            this.textBox.MinimumSize = new System.Drawing.Size(180, 20);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(180, 20);
            this.textBox.TabIndex = 1;
            // 
            // groupBox
            // 
            this.groupBox.AutoSize = true;
            this.groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox.Controls.Add(this.buttonNone);
            this.groupBox.Controls.Add(this.buttonAll);
            this.groupBox.Controls.Add(this.checkedListBox);
            this.groupBox.Controls.Add(this.textBox);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(1, 1);
            this.groupBox.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox.Size = new System.Drawing.Size(205, 81);
            this.groupBox.TabIndex = 3;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // buttonNone
            // 
            this.buttonNone.BackgroundImage = global::NetworkSystemFinder.Properties.Resources._16x16selectnone;
            this.buttonNone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonNone.FlatAppearance.BorderSize = 0;
            this.buttonNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNone.Location = new System.Drawing.Point(165, 47);
            this.buttonNone.Name = "buttonNone";
            this.buttonNone.Size = new System.Drawing.Size(18, 18);
            this.buttonNone.TabIndex = 3;
            this.buttonNone.UseVisualStyleBackColor = false;
            this.buttonNone.Visible = false;
            this.buttonNone.Click += new System.EventHandler(this.buttonNone_Click);
            // 
            // buttonAll
            // 
            this.buttonAll.BackgroundImage = global::NetworkSystemFinder.Properties.Resources._16x16selectall;
            this.buttonAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAll.FlatAppearance.BorderSize = 0;
            this.buttonAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAll.Location = new System.Drawing.Point(141, 47);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(18, 18);
            this.buttonAll.TabIndex = 2;
            this.buttonAll.UseVisualStyleBackColor = false;
            this.buttonAll.Visible = false;
            this.buttonAll.Click += new System.EventHandler(this.buttonAll_Click);
            // 
            // timerMouseControl
            // 
            this.timerMouseControl.Tick += new System.EventHandler(this.timerMouseControl_Tick);
            // 
            // FilterString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FilterString";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(207, 83);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Timer timerMouseControl;
        private System.Windows.Forms.Button buttonNone;
        private System.Windows.Forms.Button buttonAll;
    }
}
