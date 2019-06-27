namespace Global.Finance
{
    partial class FinanceAP
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
            this.sgc = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // sgc
            // 
            this.sgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgc.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.sgc.Location = new System.Drawing.Point(0, 0);
            this.sgc.Name = "sgc";
            this.sgc.Size = new System.Drawing.Size(847, 573);
            this.sgc.TabIndex = 0;
            this.sgc.Text = "superGridControl1";
            // 
            // Finance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 573);
            this.Controls.Add(this.sgc);
            this.Name = "Finance";
            this.Text = "Finance";
            this.Load += new System.EventHandler(this.Finance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl sgc;
    }
}