namespace Global.Finance.CustomsAudit
{
    partial class RawMaterialForProductShipped
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
            this.dgvShipHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShipHistory
            // 
            this.dgvShipHistory.AllowUserToAddRows = false;
            this.dgvShipHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvShipHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvShipHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvShipHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShipHistory.Location = new System.Drawing.Point(0, 12);
            this.dgvShipHistory.Name = "dgvShipHistory";
            this.dgvShipHistory.ReadOnly = true;
            this.dgvShipHistory.RowTemplate.Height = 23;
            this.dgvShipHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShipHistory.Size = new System.Drawing.Size(809, 515);
            this.dgvShipHistory.TabIndex = 24;
            // 
            // RawMaterialForProductShipped
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 527);
            this.Controls.Add(this.dgvShipHistory);
            this.DoubleBuffered = true;
            this.Name = "RawMaterialForProductShipped";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "销售记录";
            this.Load += new System.EventHandler(this.RawMaterialForProductShipped_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvShipHistory;
    }
}