namespace LBATrainer.objects
{
    partial class frmOptions
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tpLBA1 = new System.Windows.Forms.TabPage();
            this.tpLBA2 = new System.Windows.Forms.TabPage();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.txtDefaultRefreshInterval = new System.Windows.Forms.TextBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblAlwaysOnTop = new System.Windows.Forms.Label();
            this.lblDefaultRefresh = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSaveFilePath = new System.Windows.Forms.TextBox();
            this.lblSavePath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutozoom = new System.Windows.Forms.CheckBox();
            this.tcOptions.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpLBA1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(278, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpGeneral);
            this.tcOptions.Controls.Add(this.tpLBA1);
            this.tcOptions.Controls.Add(this.tpLBA2);
            this.tcOptions.Location = new System.Drawing.Point(12, 12);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(534, 238);
            this.tcOptions.TabIndex = 11;
            // 
            // tpGeneral
            // 
            this.tpGeneral.BackColor = System.Drawing.Color.MistyRose;
            this.tpGeneral.Controls.Add(this.cboLanguage);
            this.tpGeneral.Controls.Add(this.txtDefaultRefreshInterval);
            this.tpGeneral.Controls.Add(this.chkAlwaysOnTop);
            this.tpGeneral.Controls.Add(this.lblLanguage);
            this.tpGeneral.Controls.Add(this.lblAlwaysOnTop);
            this.tpGeneral.Controls.Add(this.lblDefaultRefresh);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(526, 212);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            // 
            // tpLBA1
            // 
            this.tpLBA1.BackColor = System.Drawing.Color.MistyRose;
            this.tpLBA1.Controls.Add(this.chkAutozoom);
            this.tpLBA1.Controls.Add(this.label1);
            this.tpLBA1.Controls.Add(this.btnBrowse);
            this.tpLBA1.Controls.Add(this.txtSaveFilePath);
            this.tpLBA1.Controls.Add(this.lblSavePath);
            this.tpLBA1.Location = new System.Drawing.Point(4, 22);
            this.tpLBA1.Name = "tpLBA1";
            this.tpLBA1.Padding = new System.Windows.Forms.Padding(3);
            this.tpLBA1.Size = new System.Drawing.Size(526, 212);
            this.tpLBA1.TabIndex = 1;
            this.tpLBA1.Text = "LBA1";
            // 
            // tpLBA2
            // 
            this.tpLBA2.Location = new System.Drawing.Point(4, 22);
            this.tpLBA2.Name = "tpLBA2";
            this.tpLBA2.Size = new System.Drawing.Size(526, 212);
            this.tpLBA2.TabIndex = 2;
            this.tpLBA2.Text = "LBA2";
            this.tpLBA2.UseVisualStyleBackColor = true;
            // 
            // cboLanguage
            // 
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(181, 61);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(265, 21);
            this.cboLanguage.TabIndex = 16;
            // 
            // txtDefaultRefreshInterval
            // 
            this.txtDefaultRefreshInterval.Location = new System.Drawing.Point(181, 15);
            this.txtDefaultRefreshInterval.Name = "txtDefaultRefreshInterval";
            this.txtDefaultRefreshInterval.Size = new System.Drawing.Size(100, 20);
            this.txtDefaultRefreshInterval.TabIndex = 15;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(181, 42);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(15, 14);
            this.chkAlwaysOnTop.TabIndex = 14;
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.Location = new System.Drawing.Point(19, 64);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(63, 13);
            this.lblLanguage.TabIndex = 13;
            this.lblLanguage.Text = "Language";
            // 
            // lblAlwaysOnTop
            // 
            this.lblAlwaysOnTop.AutoSize = true;
            this.lblAlwaysOnTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlwaysOnTop.Location = new System.Drawing.Point(19, 42);
            this.lblAlwaysOnTop.Name = "lblAlwaysOnTop";
            this.lblAlwaysOnTop.Size = new System.Drawing.Size(86, 13);
            this.lblAlwaysOnTop.TabIndex = 12;
            this.lblAlwaysOnTop.Text = "Always on top";
            // 
            // lblDefaultRefresh
            // 
            this.lblDefaultRefresh.AutoSize = true;
            this.lblDefaultRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultRefresh.Location = new System.Drawing.Point(19, 18);
            this.lblDefaultRefresh.Name = "lblDefaultRefresh";
            this.lblDefaultRefresh.Size = new System.Drawing.Size(137, 13);
            this.lblDefaultRefresh.TabIndex = 11;
            this.lblDefaultRefresh.Text = "Default refresh interval";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(452, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(65, 20);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtSaveFilePath
            // 
            this.txtSaveFilePath.Location = new System.Drawing.Point(181, 10);
            this.txtSaveFilePath.Name = "txtSaveFilePath";
            this.txtSaveFilePath.Size = new System.Drawing.Size(265, 20);
            this.txtSaveFilePath.TabIndex = 9;
            // 
            // lblSavePath
            // 
            this.lblSavePath.AutoSize = true;
            this.lblSavePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSavePath.Location = new System.Drawing.Point(19, 13);
            this.lblSavePath.Name = "lblSavePath";
            this.lblSavePath.Size = new System.Drawing.Size(121, 13);
            this.lblSavePath.TabIndex = 8;
            this.lblSavePath.Text = "LBA1 Save file Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Disable auto-zoom";
            // 
            // chkAutozoom
            // 
            this.chkAutozoom.AutoSize = true;
            this.chkAutozoom.Location = new System.Drawing.Point(181, 36);
            this.chkAutozoom.Name = "chkAutozoom";
            this.chkAutozoom.Size = new System.Drawing.Size(15, 14);
            this.chkAutozoom.TabIndex = 12;
            this.chkAutozoom.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(550, 292);
            this.ControlBox = false;
            this.Controls.Add(this.tcOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "frmOptions";
            this.Text = "frmOptions";
            this.tcOptions.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpLBA1.ResumeLayout(false);
            this.tpLBA1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tcOptions;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.TextBox txtDefaultRefreshInterval;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblAlwaysOnTop;
        private System.Windows.Forms.Label lblDefaultRefresh;
        private System.Windows.Forms.TabPage tpLBA1;
        private System.Windows.Forms.CheckBox chkAutozoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSaveFilePath;
        private System.Windows.Forms.Label lblSavePath;
        private System.Windows.Forms.TabPage tpLBA2;
    }
}