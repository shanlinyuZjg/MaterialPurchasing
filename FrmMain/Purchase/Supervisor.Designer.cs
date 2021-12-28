namespace Global.Purchase
{
    partial class Supervisor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.btnRefreshUnHandledPO = new DevComponents.DotNetBar.ButtonX();
            this.btnCheckPass = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllCanceled = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.dgvPOItemDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgvPO = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.POCheckChoose = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.balloonTip1 = new DevComponents.DotNetBar.BalloonTip();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItemDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPO)).BeginInit();
            this.SuspendLayout();
            // 
            // superTabControl1
            // 
            this.superTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Location = new System.Drawing.Point(2, 12);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 1;
            this.superTabControl1.Size = new System.Drawing.Size(1390, 639);
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 0;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControl1.Text = "外贸物料审核";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.btnRefreshUnHandledPO);
            this.superTabControlPanel1.Controls.Add(this.btnCheckPass);
            this.superTabControlPanel1.Controls.Add(this.btnMakeAllCanceled);
            this.superTabControlPanel1.Controls.Add(this.btnMakeAllChecked);
            this.superTabControlPanel1.Controls.Add(this.labelX4);
            this.superTabControlPanel1.Controls.Add(this.dgvPOItemDetail);
            this.superTabControlPanel1.Controls.Add(this.dgvPO);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1390, 611);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // btnRefreshUnHandledPO
            // 
            this.btnRefreshUnHandledPO.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefreshUnHandledPO.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefreshUnHandledPO.Location = new System.Drawing.Point(87, 7);
            this.btnRefreshUnHandledPO.Name = "btnRefreshUnHandledPO";
            this.btnRefreshUnHandledPO.Size = new System.Drawing.Size(53, 23);
            this.btnRefreshUnHandledPO.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefreshUnHandledPO.TabIndex = 2;
            this.btnRefreshUnHandledPO.Text = "刷新";
            this.btnRefreshUnHandledPO.Click += new System.EventHandler(this.btnRefreshUnHandledPO_Click);
            // 
            // btnCheckPass
            // 
            this.btnCheckPass.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCheckPass.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCheckPass.Location = new System.Drawing.Point(622, 7);
            this.btnCheckPass.Name = "btnCheckPass";
            this.btnCheckPass.Size = new System.Drawing.Size(65, 23);
            this.btnCheckPass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCheckPass.TabIndex = 2;
            this.btnCheckPass.Text = "审核确认";
            this.btnCheckPass.Click += new System.EventHandler(this.btnCheckPass_Click);
            // 
            // btnMakeAllCanceled
            // 
            this.btnMakeAllCanceled.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllCanceled.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllCanceled.Location = new System.Drawing.Point(530, 7);
            this.btnMakeAllCanceled.Name = "btnMakeAllCanceled";
            this.btnMakeAllCanceled.Size = new System.Drawing.Size(65, 23);
            this.btnMakeAllCanceled.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllCanceled.TabIndex = 2;
            this.btnMakeAllCanceled.Text = "全部取消";
            this.btnMakeAllCanceled.Click += new System.EventHandler(this.btnMakeAllCanceled_Click);
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Location = new System.Drawing.Point(439, 7);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(65, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 2;
            this.btnMakeAllChecked.Text = "全部选中";
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(10, 7);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(71, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "待审核订单";
            // 
            // dgvPOItemDetail
            // 
            this.dgvPOItemDetail.AllowUserToAddRows = false;
            this.dgvPOItemDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPOItemDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPOItemDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPOItemDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPOItemDetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPOItemDetail.EnableHeadersVisualStyles = false;
            this.dgvPOItemDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvPOItemDetail.Location = new System.Drawing.Point(439, 36);
            this.dgvPOItemDetail.Name = "dgvPOItemDetail";
            this.dgvPOItemDetail.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPOItemDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPOItemDetail.RowTemplate.Height = 23;
            this.dgvPOItemDetail.Size = new System.Drawing.Size(941, 565);
            this.dgvPOItemDetail.TabIndex = 0;
            // 
            // dgvPO
            // 
            this.dgvPO.AllowUserToAddRows = false;
            this.dgvPO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.POCheckChoose});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPO.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPO.EnableHeadersVisualStyles = false;
            this.dgvPO.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvPO.Location = new System.Drawing.Point(10, 36);
            this.dgvPO.Name = "dgvPO";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPO.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPO.RowTemplate.Height = 23;
            this.dgvPO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPO.Size = new System.Drawing.Size(423, 565);
            this.dgvPO.TabIndex = 0;
            this.dgvPO.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPO_CellContentDoubleClick);
            this.dgvPO.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPO_CellDoubleClick);
            // 
            // POCheckChoose
            // 
            this.POCheckChoose.Checked = true;
            this.POCheckChoose.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.POCheckChoose.CheckValue = "N";
            this.POCheckChoose.HeaderText = "选择";
            this.POCheckChoose.Name = "POCheckChoose";
            this.POCheckChoose.Width = 35;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "订单审核";
            // 
            // Supervisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 653);
            this.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "Supervisor";
            this.Text = "Supervisor";
            this.Load += new System.EventHandler(this.Supervisor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItemDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.BalloonTip balloonTip1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvPOItemDetail;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvPO;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnRefreshUnHandledPO;
        private DevComponents.DotNetBar.ButtonX btnCheckPass;
        private DevComponents.DotNetBar.ButtonX btnMakeAllCanceled;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn POCheckChoose;
    }
}