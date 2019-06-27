namespace Global.Purchase
{
    partial class ForeignOrderItemChoose
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
            this.dgvFOItem = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOItem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFOItem
            // 
            this.dgvFOItem.AllowUserToAddRows = false;
            this.dgvFOItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvFOItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFOItem.Location = new System.Drawing.Point(2, 12);
            this.dgvFOItem.Name = "dgvFOItem";
            this.dgvFOItem.ReadOnly = true;
            this.dgvFOItem.RowTemplate.Height = 23;
            this.dgvFOItem.Size = new System.Drawing.Size(882, 312);
            this.dgvFOItem.TabIndex = 0;
            this.dgvFOItem.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFOItem_CellContentDoubleClick);
            this.dgvFOItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFOItem_CellDoubleClick);
            // 
            // ForeignOrderItemChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 336);
            this.Controls.Add(this.dgvFOItem);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForeignOrderItemChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "外贸订单物料选择";
            this.Load += new System.EventHandler(this.ForeignOrderItemChoose_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFOItem;
    }
}