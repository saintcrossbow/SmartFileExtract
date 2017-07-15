namespace SmartFileExtract
{
    partial class frmProgress
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
            this.progressMain = new System.Windows.Forms.ProgressBar();
            this.btnKill = new System.Windows.Forms.Button();
            this.lblStat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressMain
            // 
            this.progressMain.Location = new System.Drawing.Point(22, 24);
            this.progressMain.Name = "progressMain";
            this.progressMain.Size = new System.Drawing.Size(306, 23);
            this.progressMain.TabIndex = 0;
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(253, 68);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(75, 23);
            this.btnKill.TabIndex = 1;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // lblStat
            // 
            this.lblStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStat.Location = new System.Drawing.Point(22, 68);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(225, 23);
            this.lblStat.TabIndex = 2;
            this.lblStat.Text = "Phase: Initialize";
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 109);
            this.ControlBox = false;
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.progressMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProgress";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressMain;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.Label lblStat;
    }
}