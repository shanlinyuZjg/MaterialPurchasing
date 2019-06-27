namespace Global.Finance
{
    partial class RawMaterialPurchaseHistory
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
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.dgvPurchaseHistory = new System.Windows.Forms.DataGridView();
            this.btnProductHistory = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(247, 12);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(122, 21);
            this.endDate.TabIndex = 22;
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(59, 12);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(124, 21);
            this.startDate.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "截止日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "开始日期";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(421, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvPurchaseHistory
            // 
            this.dgvPurchaseHistory.AllowUserToAddRows = false;
            this.dgvPurchaseHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPurchaseHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvPurchaseHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPurchaseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseHistory.Location = new System.Drawing.Point(5, 39);
            this.dgvPurchaseHistory.Name = "dgvPurchaseHistory";
            this.dgvPurchaseHistory.ReadOnly = true;
            this.dgvPurchaseHistory.RowTemplate.Height = 23;
            this.dgvPurchaseHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseHistory.Size = new System.Drawing.Size(903, 542);
            this.dgvPurchaseHistory.TabIndex = 25;
            this.dgvPurchaseHistory.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseHistory_CellContentDoubleClick);
            this.dgvPurchaseHistory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseHistory_CellDoubleClick);
            // 
            // btnProductHistory
            // 
            this.btnProductHistory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnProductHistory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnProductHistory.Location = new System.Drawing.Point(531, 10);
            this.btnProductHistory.Name = "btnProductHistory";
            this.btnProductHistory.Size = new System.Drawing.Size(102, 23);
            this.btnProductHistory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnProductHistory.TabIndex = 26;
            this.btnProductHistory.Text = "查看生产记录";
            this.btnProductHistory.Click += new System.EventHandler(this.btnProductHistory_Click);
            // 
            // RawMaterialPurchaseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 581);
            this.Controls.Add(this.btnProductHistory);
            this.Controls.Add(this.dgvPurchaseHistory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.Name = "RawMaterialPurchaseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "采购记录";
            this.Load += new System.EventHandler(this.RawMaterialPurchaseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.DataGridView dgvPurchaseHistory;
        private DevComponents.DotNetBar.ButtonX btnProductHistory;
    }
}