namespace SmartFileExtract
{
    partial class frmStealthy
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
            this.dotStat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dotStat
            // 
            this.dotStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dotStat.ForeColor = System.Drawing.Color.Teal;
            this.dotStat.Location = new System.Drawing.Point(82, 0);
            this.dotStat.Name = "dotStat";
            this.dotStat.Size = new System.Drawing.Size(37, 23);
            this.dotStat.TabIndex = 0;
            this.dotStat.Text = ".";
            this.dotStat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dotStat.DoubleClick += new System.EventHandler(this.dotStat_DoubleClick);
            // 
            // frmStealthy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 24);
            this.ControlBox = false;
            this.Controls.Add(this.dotStat);
            this.Name = "frmStealthy";
            this.Opacity = 0.05D;
            this.Load += new System.EventHandler(this.frmStealthy_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label dotStat;
    }
}