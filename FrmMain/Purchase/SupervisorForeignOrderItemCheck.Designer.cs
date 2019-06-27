namespace Global.Purchase
{
    partial class SupervisorForeignOrderItemCheck
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
            this.dgvForeginOrderAndItem = new System.Windows.Forms.DataGridView();
            this.dgvForeignOrderDetail = new System.Windows.Forms.DataGridView();
            this.Choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForeginOrderAndItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForeignOrderDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvForeginOrderAndItem
            // 
            this.dgvForeginOrderAndItem.AllowUserToAddRows = false;
            this.dgvForeginOrderAndItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvForeginOrderAndItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvForeginOrderAndItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvForeginOrderAndItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForeginOrderAndItem.Location = new System.Drawing.Point(2, 56);
            this.dgvForeginOrderAndItem.Name = "dgvForeginOrderAndItem";
            this.dgvForeginOrderAndItem.ReadOnly = true;
            this.dgvForeginOrderAndItem.RowTemplate.Height = 23;
            this.dgvForeginOrderAndItem.Size = new System.Drawing.Size(296, 524);
            this.dgvForeginOrderAndItem.TabIndex = 0;
            this.dgvForeginOrderAndItem.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForeginOrderAndItem_CellContentDoubleClick);
            this.dgvForeginOrderAndItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForeginOrderAndItem_CellDoubleClick);
            // 
            // dgvForeignOrderDetail
            // 
            this.dgvForeignOrderDetail.AllowUserToAddRows = false;
            this.dgvForeignOrderDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvForeignOrderDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvForeignOrderDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvForeignOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForeignOrderDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Choose});
            this.dgvForeignOrderDetail.Location = new System.Drawing.Point(304, 56);
            this.dgvForeignOrderDetail.Name = "dgvForeignOrderDetail";
            this.dgvForeignOrderDetail.RowTemplate.Height = 23;
            this.dgvForeignOrderDetail.Size = new System.Drawing.Size(698, 524);
            this.dgvForeignOrderDetail.TabIndex = 0;
            // 
            // Choose
            // 
            this.Choose.HeaderText = "选择";
            this.Choose.Name = "Choose";
            this.Choose.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Choose.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Choose.Width = 54;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Location = new System.Drawing.Point(304, 27);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(185, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "实时刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // SupervisorForeignOrderItemCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 592);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgvForeignOrderDetail);
            this.Controls.Add(this.dgvForeginOrderAndItem);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "SupervisorForeignOrderItemCheck";
            this.Text = "SupervisorForeignOrderItemCheck";
            this.Load += new System.EventHandler(this.SupervisorForeignOrderItemCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForeginOrderAndItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForeignOrderDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvForeginOrderAndItem;
        private System.Windows.Forms.DataGridView dgvForeignOrderDetail;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choose;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
    }
}