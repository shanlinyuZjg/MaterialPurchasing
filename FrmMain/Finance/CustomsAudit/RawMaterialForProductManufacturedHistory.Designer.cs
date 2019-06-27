namespace Global.Finance.CustomsAudit
{
    partial class RawMaterialForProductManufacturedHistory
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
            this.btnProductHistory = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.dgvProductHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProductHistory
            // 
            this.btnProductHistory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnProductHistory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnProductHistory.Location = new System.Drawing.Point(124, 9);
            this.btnProductHistory.Name = "btnProductHistory";
            this.btnProductHistory.Size = new System.Drawing.Size(69, 23);
            this.btnProductHistory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnProductHistory.TabIndex = 28;
            this.btnProductHistory.Text = "销售记录";
            this.btnProductHistory.Click += new System.EventHandler(this.btnProductHistory_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(14, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvProductHistory
            // 
            this.dgvProductHistory.AllowUserToAddRows = false;
            this.dgvProductHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvProductHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvProductHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProductHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductHistory.Location = new System.Drawing.Point(14, 38);
            this.dgvProductHistory.Name = "dgvProductHistory";
            this.dgvProductHistory.RowTemplate.Height = 23;
            this.dgvProductHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductHistory.Size = new System.Drawing.Size(865, 530);
            this.dgvProductHistory.TabIndex = 29;
            this.dgvProductHistory.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductHistory_CellContentDoubleClick);
            this.dgvProductHistory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductHistory_CellDoubleClick);
            // 
            // RawMaterialForProductManufacturedHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 572);
            this.Controls.Add(this.dgvProductHistory);
            this.Controls.Add(this.btnProductHistory);
            this.Controls.Add(this.btnSearch);
            this.DoubleBuffered = true;
            this.Name = "RawMaterialForProductManufacturedHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产记录";
            this.Load += new System.EventHandler(this.RawMaterialForProductManufacturedHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnProductHistory;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.DataGridView dgvProductHistory;
    }
}