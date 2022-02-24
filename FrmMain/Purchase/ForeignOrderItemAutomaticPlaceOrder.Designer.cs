namespace Global.Purchase
{
    partial class ForeignOrderItemAutomaticPlaceOrder
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
            this.dgvFOConfirmItemsDetail = new System.Windows.Forms.DataGridView();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnManageSpecialItem = new DevComponents.DotNetBar.ButtonX();
            this.tbDelayDays = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbArrivedDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnUnPlaceOrderItemMaintain = new DevComponents.DotNetBar.ButtonX();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnAllSelect_dgvSpe = new DevComponents.DotNetBar.ButtonX();
            this.BtnAllnotSelect_dgvSpe = new DevComponents.DotNetBar.ButtonX();
            this.BtnSpecialDelete = new DevComponents.DotNetBar.ButtonX();
            this.BtnSpecialYixiada = new DevComponents.DotNetBar.ButtonX();
            this.BtnSpecialRefresh = new DevComponents.DotNetBar.ButtonX();
            this.dgvFOSpeItemsDetail = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOConfirmItemsDetail)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOSpeItemsDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFOConfirmItemsDetail
            // 
            this.dgvFOConfirmItemsDetail.AllowUserToAddRows = false;
            this.dgvFOConfirmItemsDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFOConfirmItemsDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvFOConfirmItemsDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFOConfirmItemsDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFOConfirmItemsDetail.Location = new System.Drawing.Point(11, 8);
            this.dgvFOConfirmItemsDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvFOConfirmItemsDetail.Name = "dgvFOConfirmItemsDetail";
            this.dgvFOConfirmItemsDetail.RowTemplate.Height = 23;
            this.dgvFOConfirmItemsDetail.Size = new System.Drawing.Size(1263, 555);
            this.dgvFOConfirmItemsDetail.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(75, 571);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(93, 31);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "生成订单";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnManageSpecialItem
            // 
            this.btnManageSpecialItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnManageSpecialItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnManageSpecialItem.Location = new System.Drawing.Point(1071, 571);
            this.btnManageSpecialItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnManageSpecialItem.Name = "btnManageSpecialItem";
            this.btnManageSpecialItem.Size = new System.Drawing.Size(125, 31);
            this.btnManageSpecialItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnManageSpecialItem.TabIndex = 2;
            this.btnManageSpecialItem.Text = "特殊物料维护";
            this.btnManageSpecialItem.Click += new System.EventHandler(this.btnManageSpecialItem_Click);
            // 
            // tbDelayDays
            // 
            // 
            // 
            // 
            this.tbDelayDays.Border.Class = "TextBoxBorder";
            this.tbDelayDays.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDelayDays.Location = new System.Drawing.Point(819, 569);
            this.tbDelayDays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbDelayDays.Name = "tbDelayDays";
            this.tbDelayDays.PreventEnterBeep = true;
            this.tbDelayDays.Size = new System.Drawing.Size(40, 26);
            this.tbDelayDays.TabIndex = 35;
            this.tbDelayDays.Text = "12";
            // 
            // tbArrivedDate
            // 
            // 
            // 
            // 
            this.tbArrivedDate.Border.Class = "TextBoxBorder";
            this.tbArrivedDate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbArrivedDate.Location = new System.Drawing.Point(528, 571);
            this.tbArrivedDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbArrivedDate.Name = "tbArrivedDate";
            this.tbArrivedDate.PreventEnterBeep = true;
            this.tbArrivedDate.Size = new System.Drawing.Size(89, 26);
            this.tbArrivedDate.TabIndex = 36;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(625, 571);
            this.labelX6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(116, 31);
            this.labelX6.TabIndex = 31;
            this.labelX6.Text = "MMddyyyy格式";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(769, 569);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(41, 31);
            this.labelX2.TabIndex = 32;
            this.labelX2.Text = "延后";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(455, 571);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(73, 31);
            this.labelX1.TabIndex = 33;
            this.labelX1.Text = "到货日期";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.ForeColor = System.Drawing.Color.DeepPink;
            this.labelX5.Location = new System.Drawing.Point(201, 571);
            this.labelX5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(225, 31);
            this.labelX5.TabIndex = 34;
            this.labelX5.Text = "到货日期和延后天数只填一个";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(867, 569);
            this.labelX3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(41, 31);
            this.labelX3.TabIndex = 32;
            this.labelX3.Text = "天";
            // 
            // btnUnPlaceOrderItemMaintain
            // 
            this.btnUnPlaceOrderItemMaintain.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnPlaceOrderItemMaintain.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnPlaceOrderItemMaintain.Location = new System.Drawing.Point(897, 571);
            this.btnUnPlaceOrderItemMaintain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUnPlaceOrderItemMaintain.Name = "btnUnPlaceOrderItemMaintain";
            this.btnUnPlaceOrderItemMaintain.Size = new System.Drawing.Size(165, 31);
            this.btnUnPlaceOrderItemMaintain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnPlaceOrderItemMaintain.TabIndex = 37;
            this.btnUnPlaceOrderItemMaintain.Text = "已审核特殊物料维护";
            this.btnUnPlaceOrderItemMaintain.Click += new System.EventHandler(this.btnUnPlaceOrderItemMaintain_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1290, 641);
            this.tabControl1.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabPage1.Controls.Add(this.dgvFOConfirmItemsDetail);
            this.tabPage1.Controls.Add(this.btnUnPlaceOrderItemMaintain);
            this.tabPage1.Controls.Add(this.buttonX1);
            this.tabPage1.Controls.Add(this.tbDelayDays);
            this.tabPage1.Controls.Add(this.btnManageSpecialItem);
            this.tabPage1.Controls.Add(this.tbArrivedDate);
            this.tabPage1.Controls.Add(this.labelX5);
            this.tabPage1.Controls.Add(this.labelX6);
            this.tabPage1.Controls.Add(this.labelX1);
            this.tabPage1.Controls.Add(this.labelX3);
            this.tabPage1.Controls.Add(this.labelX2);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1282, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "订单下达";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabPage2.Controls.Add(this.BtnAllSelect_dgvSpe);
            this.tabPage2.Controls.Add(this.BtnAllnotSelect_dgvSpe);
            this.tabPage2.Controls.Add(this.BtnSpecialDelete);
            this.tabPage2.Controls.Add(this.BtnSpecialYixiada);
            this.tabPage2.Controls.Add(this.BtnSpecialRefresh);
            this.tabPage2.Controls.Add(this.dgvFOSpeItemsDetail);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1282, 611);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "特殊物料";
            // 
            // BtnAllSelect_dgvSpe
            // 
            this.BtnAllSelect_dgvSpe.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnAllSelect_dgvSpe.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnAllSelect_dgvSpe.Location = new System.Drawing.Point(73, 571);
            this.BtnAllSelect_dgvSpe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAllSelect_dgvSpe.Name = "BtnAllSelect_dgvSpe";
            this.BtnAllSelect_dgvSpe.Size = new System.Drawing.Size(93, 31);
            this.BtnAllSelect_dgvSpe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnAllSelect_dgvSpe.TabIndex = 6;
            this.BtnAllSelect_dgvSpe.Text = "全选";
            this.BtnAllSelect_dgvSpe.Click += new System.EventHandler(this.BtnAllSelect_dgvSpe_Click);
            // 
            // BtnAllnotSelect_dgvSpe
            // 
            this.BtnAllnotSelect_dgvSpe.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnAllnotSelect_dgvSpe.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnAllnotSelect_dgvSpe.Location = new System.Drawing.Point(213, 571);
            this.BtnAllnotSelect_dgvSpe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAllnotSelect_dgvSpe.Name = "BtnAllnotSelect_dgvSpe";
            this.BtnAllnotSelect_dgvSpe.Size = new System.Drawing.Size(93, 31);
            this.BtnAllnotSelect_dgvSpe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnAllnotSelect_dgvSpe.TabIndex = 5;
            this.BtnAllnotSelect_dgvSpe.Text = "全不选";
            this.BtnAllnotSelect_dgvSpe.Click += new System.EventHandler(this.BtnAllnotSelect_dgvSpe_Click);
            // 
            // BtnSpecialDelete
            // 
            this.BtnSpecialDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSpecialDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSpecialDelete.Location = new System.Drawing.Point(663, 571);
            this.BtnSpecialDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSpecialDelete.Name = "BtnSpecialDelete";
            this.BtnSpecialDelete.Size = new System.Drawing.Size(123, 31);
            this.BtnSpecialDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSpecialDelete.TabIndex = 4;
            this.BtnSpecialDelete.Text = "删除";
            this.BtnSpecialDelete.Click += new System.EventHandler(this.BtnSpecialDelete_Click);
            // 
            // BtnSpecialYixiada
            // 
            this.BtnSpecialYixiada.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSpecialYixiada.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSpecialYixiada.Location = new System.Drawing.Point(493, 571);
            this.BtnSpecialYixiada.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSpecialYixiada.Name = "BtnSpecialYixiada";
            this.BtnSpecialYixiada.Size = new System.Drawing.Size(123, 31);
            this.BtnSpecialYixiada.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSpecialYixiada.TabIndex = 3;
            this.BtnSpecialYixiada.Text = "已下达";
            this.BtnSpecialYixiada.Click += new System.EventHandler(this.BtnSpecialYixiada_Click);
            // 
            // BtnSpecialRefresh
            // 
            this.BtnSpecialRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSpecialRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSpecialRefresh.Location = new System.Drawing.Point(353, 571);
            this.BtnSpecialRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSpecialRefresh.Name = "BtnSpecialRefresh";
            this.BtnSpecialRefresh.Size = new System.Drawing.Size(93, 31);
            this.BtnSpecialRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSpecialRefresh.TabIndex = 2;
            this.BtnSpecialRefresh.Text = "刷新";
            this.BtnSpecialRefresh.Click += new System.EventHandler(this.BtnSpecialRefresh_Click);
            // 
            // dgvFOSpeItemsDetail
            // 
            this.dgvFOSpeItemsDetail.AllowUserToAddRows = false;
            this.dgvFOSpeItemsDetail.AllowUserToDeleteRows = false;
            this.dgvFOSpeItemsDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFOSpeItemsDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFOSpeItemsDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvFOSpeItemsDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFOSpeItemsDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dgvFOSpeItemsDetail.Location = new System.Drawing.Point(11, 8);
            this.dgvFOSpeItemsDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvFOSpeItemsDetail.Name = "dgvFOSpeItemsDetail";
            this.dgvFOSpeItemsDetail.ReadOnly = true;
            this.dgvFOSpeItemsDetail.RowTemplate.Height = 23;
            this.dgvFOSpeItemsDetail.Size = new System.Drawing.Size(1262, 545);
            this.dgvFOSpeItemsDetail.TabIndex = 0;
            this.dgvFOSpeItemsDetail.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFOSpeItemsDetail_CellMouseClick);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 35;
            // 
            // ForeignOrderItemAutomaticPlaceOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 641);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForeignOrderItemAutomaticPlaceOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动确认下达订单";
            this.Load += new System.EventHandler(this.ForeignOrderItemAutomaticPlaceOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOConfirmItemsDetail)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOSpeItemsDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFOConfirmItemsDetail;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnManageSpecialItem;
        private DevComponents.DotNetBar.Controls.TextBoxX tbDelayDays;
        private DevComponents.DotNetBar.Controls.TextBoxX tbArrivedDate;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnUnPlaceOrderItemMaintain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvFOSpeItemsDetail;
        private DevComponents.DotNetBar.ButtonX BtnSpecialDelete;
        private DevComponents.DotNetBar.ButtonX BtnSpecialYixiada;
        private DevComponents.DotNetBar.ButtonX BtnSpecialRefresh;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.ButtonX BtnAllSelect_dgvSpe;
        private DevComponents.DotNetBar.ButtonX BtnAllnotSelect_dgvSpe;
    }
}