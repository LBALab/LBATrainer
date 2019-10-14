namespace LBATrainer
{
    partial class getSaveFileNames
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
            this.btnOkay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInternalFileName = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtInGameName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(77, 74);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(90, 23);
            this.btnOkay.TabIndex = 3;
            this.btnOkay.Text = "Okay";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.BtnOkay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(184, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblInternalFileName
            // 
            this.lblInternalFileName.AutoSize = true;
            this.lblInternalFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalFileName.Location = new System.Drawing.Point(40, 19);
            this.lblInternalFileName.Name = "lblInternalFileName";
            this.lblInternalFileName.Size = new System.Drawing.Size(90, 13);
            this.lblInternalFileName.TabIndex = 2;
            this.lblInternalFileName.Text = "In Game Name";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(40, 48);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(63, 13);
            this.lblFileName.TabIndex = 3;
            this.lblFileName.Text = "File Name";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(137, 45);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(159, 20);
            this.txtFilename.TabIndex = 2;
            // 
            // txtInGameName
            // 
            this.txtInGameName.Location = new System.Drawing.Point(137, 16);
            this.txtInGameName.Name = "txtInGameName";
            this.txtInGameName.Size = new System.Drawing.Size(159, 20);
            this.txtInGameName.TabIndex = 1;
            // 
            // getSaveFileNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 115);
            this.ControlBox = false;
            this.Controls.Add(this.txtInGameName);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblInternalFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "getSaveFileNames";
            this.Text = "getSaveFileNames";
            this.Load += new System.EventHandler(this.GetSaveFileNames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInternalFileName;
        private System.Windows.Forms.Label lblFileName;
        public System.Windows.Forms.TextBox txtFilename;
        public System.Windows.Forms.TextBox txtInGameName;
    }
}