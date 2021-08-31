
namespace NetworkSystemFinder.Forms
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxIPEnd = new System.Windows.Forms.TextBox();
            this.textBoxIPStart = new System.Windows.Forms.TextBox();
            this.labelIPStart = new System.Windows.Forms.Label();
            this.labelIPEnd = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.comboBoxTheme = new System.Windows.Forms.ComboBox();
            this.labelTheme = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.labelWarningWARNING = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDone.Location = new System.Drawing.Point(172, 317);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(150, 32);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "keyDone";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(16, 317);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(150, 32);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "keyCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxIPEnd
            // 
            this.textBoxIPEnd.Location = new System.Drawing.Point(12, 70);
            this.textBoxIPEnd.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIPEnd.MaxLength = 15;
            this.textBoxIPEnd.Name = "textBoxIPEnd";
            this.textBoxIPEnd.Size = new System.Drawing.Size(150, 20);
            this.textBoxIPEnd.TabIndex = 10;
            this.textBoxIPEnd.Text = "10.61.6";
            // 
            // textBoxIPStart
            // 
            this.textBoxIPStart.Location = new System.Drawing.Point(12, 30);
            this.textBoxIPStart.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIPStart.MaxLength = 15;
            this.textBoxIPStart.Name = "textBoxIPStart";
            this.textBoxIPStart.Size = new System.Drawing.Size(150, 20);
            this.textBoxIPStart.TabIndex = 9;
            this.textBoxIPStart.Text = "10.61.4";
            // 
            // labelIPStart
            // 
            this.labelIPStart.AutoSize = true;
            this.labelIPStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelIPStart.Location = new System.Drawing.Point(11, 11);
            this.labelIPStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelIPStart.Name = "labelIPStart";
            this.labelIPStart.Size = new System.Drawing.Size(78, 13);
            this.labelIPStart.TabIndex = 11;
            this.labelIPStart.Text = "keyRemIPStart";
            // 
            // labelIPEnd
            // 
            this.labelIPEnd.AutoSize = true;
            this.labelIPEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelIPEnd.Location = new System.Drawing.Point(11, 54);
            this.labelIPEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelIPEnd.Name = "labelIPEnd";
            this.labelIPEnd.Size = new System.Drawing.Size(75, 13);
            this.labelIPEnd.TabIndex = 12;
            this.labelIPEnd.Text = "keyRemIPEnd";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(12, 118);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxUser.MaxLength = 64;
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(150, 20);
            this.textBoxUser.TabIndex = 13;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(12, 158);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPassword.MaxLength = 64;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(150, 20);
            this.textBoxPassword.TabIndex = 14;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUser.Location = new System.Drawing.Point(11, 102);
            this.labelUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(68, 13);
            this.labelUser.TabIndex = 15;
            this.labelUser.Text = "keyRemUser";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPassword.Location = new System.Drawing.Point(10, 142);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(92, 13);
            this.labelPassword.TabIndex = 16;
            this.labelPassword.Text = "keyRemPassword";
            // 
            // comboBoxTheme
            // 
            this.comboBoxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTheme.FormattingEnabled = true;
            this.comboBoxTheme.Location = new System.Drawing.Point(13, 207);
            this.comboBoxTheme.Name = "comboBoxTheme";
            this.comboBoxTheme.Size = new System.Drawing.Size(149, 21);
            this.comboBoxTheme.TabIndex = 17;
            // 
            // labelTheme
            // 
            this.labelTheme.AutoSize = true;
            this.labelTheme.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTheme.Location = new System.Drawing.Point(9, 190);
            this.labelTheme.Margin = new System.Windows.Forms.Padding(5, 10, 5, 1);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(57, 13);
            this.labelTheme.TabIndex = 18;
            this.labelTheme.Text = "keyTheme";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "keyLanguage";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "English (EN)",
            "Turkish (TR)"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(13, 258);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(149, 21);
            this.comboBoxLanguage.TabIndex = 19;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // labelWarningWARNING
            // 
            this.labelWarningWARNING.AutoSize = true;
            this.labelWarningWARNING.Location = new System.Drawing.Point(12, 286);
            this.labelWarningWARNING.Name = "labelWarningWARNING";
            this.labelWarningWARNING.Size = new System.Drawing.Size(84, 13);
            this.labelWarningWARNING.TabIndex = 21;
            this.labelWarningWARNING.Text = "keyNeedRestart";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(334, 361);
            this.Controls.Add(this.labelWarningWARNING);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.comboBoxTheme);
            this.Controls.Add(this.textBoxIPEnd);
            this.Controls.Add(this.textBoxIPStart);
            this.Controls.Add(this.labelIPStart);
            this.Controls.Add(this.labelIPEnd);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonDone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(350, 400);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "keySettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxIPEnd;
        private System.Windows.Forms.TextBox textBoxIPStart;
        private System.Windows.Forms.Label labelIPStart;
        private System.Windows.Forms.Label labelIPEnd;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.ComboBox comboBoxTheme;
        private System.Windows.Forms.Label labelTheme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label labelWarningWARNING;
    }
}