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
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOConfirmItemsDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFOConfirmItemsDetail
            // 
            this.dgvFOConfirmItemsDetail.AllowUserToAddRows = false;
            this.dgvFOConfirmItemsDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.dgvFOConfirmItemsDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFOConfirmItemsDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFOConfirmItemsDetail.Location = new System.Drawing.Point(13, 13);
            this.dgvFOConfirmItemsDetail.Name = "dgvFOConfirmItemsDetail";
            this.dgvFOConfirmItemsDetail.RowTemplate.Height = 23;
            this.dgvFOConfirmItemsDetail.Size = new System.Drawing.Size(926, 450);
            this.dgvFOConfirmItemsDetail.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(98, 469);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(70, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "生成订单";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnManageSpecialItem
            // 
            this.btnManageSpecialItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnManageSpecialItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnManageSpecialItem.Location = new System.Drawing.Point(845, 469);
            this.btnManageSpecialItem.Name = "btnManageSpecialItem";
            this.btnManageSpecialItem.Size = new System.Drawing.Size(94, 23);
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
            this.tbDelayDays.Location = new System.Drawing.Point(656, 468);
            this.tbDelayDays.Name = "tbDelayDays";
            this.tbDelayDays.PreventEnterBeep = true;
            this.tbDelayDays.Size = new System.Drawing.Size(30, 21);
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
            this.tbArrivedDate.Location = new System.Drawing.Point(438, 469);
            this.tbArrivedDate.Name = "tbArrivedDate";
            this.tbArrivedDate.PreventEnterBeep = true;
            this.tbArrivedDate.Size = new System.Drawing.Size(67, 21);
            this.tbArrivedDate.TabIndex = 36;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(511, 469);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(87, 23);
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
            this.labelX2.Location = new System.Drawing.Point(619, 468);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(31, 23);
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
            this.labelX1.Location = new System.Drawing.Point(383, 469);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
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
            this.labelX5.Location = new System.Drawing.Point(193, 469);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(169, 23);
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
            this.labelX3.Location = new System.Drawing.Point(692, 468);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(31, 23);
            this.labelX3.TabIndex = 32;
            this.labelX3.Text = "天";
            // 
            // btnUnPlaceOrderItemMaintain
            // 
            this.btnUnPlaceOrderItemMaintain.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnPlaceOrderItemMaintain.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnPlaceOrderItemMaintain.Location = new System.Drawing.Point(715, 469);
            this.btnUnPlaceOrderItemMaintain.Name = "btnUnPlaceOrderItemMaintain";
            this.btnUnPlaceOrderItemMaintain.Size = new System.Drawing.Size(124, 23);
            this.btnUnPlaceOrderItemMaintain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnPlaceOrderItemMaintain.TabIndex = 37;
            this.btnUnPlaceOrderItemMaintain.Text = "已审核特殊物料维护";
            this.btnUnPlaceOrderItemMaintain.Click += new System.EventHandler(this.btnUnPlaceOrderItemMaintain_Click);
            // 
            // ForeignOrderItemAutomaticPlaceOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 495);
            this.Controls.Add(this.btnUnPlaceOrderItemMaintain);
            this.Controls.Add(this.tbDelayDays);
            this.Controls.Add(this.tbArrivedDate);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.btnManageSpecialItem);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dgvFOConfirmItemsDetail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForeignOrderItemAutomaticPlaceOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动确认下达订单";
            this.Load += new System.EventHandler(this.ForeignOrderItemAutomaticPlaceOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFOConfirmItemsDetail)).EndInit();
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
    }
}