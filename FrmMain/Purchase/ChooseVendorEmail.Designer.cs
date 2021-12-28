namespace Global.Purchase
{
    partial class ChooseVendorEmail
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
            this.dgvEmail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmail
            // 
            this.dgvEmail.AllowUserToAddRows = false;
            this.dgvEmail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvEmail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmail.Location = new System.Drawing.Point(13, 13);
            this.dgvEmail.Name = "dgvEmail";
            this.dgvEmail.RowTemplate.Height = 23;
            this.dgvEmail.Size = new System.Drawing.Size(322, 362);
            this.dgvEmail.TabIndex = 0;
            this.dgvEmail.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmail_CellContentDoubleClick);
            this.dgvEmail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmail_CellDoubleClick);
            // 
            // ChooseVendorEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 387);
            this.Controls.Add(this.dgvEmail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ChooseVendorEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择供应商邮箱";
            this.Load += new System.EventHandler(this.ChooseVendorEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmail;
    }
}