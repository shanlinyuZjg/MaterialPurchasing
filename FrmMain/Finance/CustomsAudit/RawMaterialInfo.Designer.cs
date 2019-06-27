namespace Global.Finance
{
    partial class RawMaterialInfo
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
            this.tbItemNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbItemDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbUM = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnInventory = new DevComponents.DotNetBar.ButtonX();
            this.btnPurchaseHistory = new DevComponents.DotNetBar.ButtonX();
            this.dgvInventoryItem = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItem)).BeginInit();
            this.SuspendLayout();
            // 
            // tbItemNumber
            // 
            // 
            // 
            // 
            this.tbItemNumber.Border.Class = "TextBoxBorder";
            this.tbItemNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemNumber.Location = new System.Drawing.Point(58, 12);
            this.tbItemNumber.Name = "tbItemNumber";
            this.tbItemNumber.PreventEnterBeep = true;
            this.tbItemNumber.Size = new System.Drawing.Size(111, 21);
            this.tbItemNumber.TabIndex = 0;
            this.tbItemNumber.TextChanged += new System.EventHandler(this.tbItemNumber_TextChanged);
            this.tbItemNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemNumber_KeyPress);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(2, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "物料代码";
            // 
            // tbItemDescription
            // 
            // 
            // 
            // 
            this.tbItemDescription.Border.Class = "TextBoxBorder";
            this.tbItemDescription.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemDescription.Location = new System.Drawing.Point(246, 12);
            this.tbItemDescription.Name = "tbItemDescription";
            this.tbItemDescription.PreventEnterBeep = true;
            this.tbItemDescription.ReadOnly = true;
            this.tbItemDescription.Size = new System.Drawing.Size(266, 21);
            this.tbItemDescription.TabIndex = 1;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(188, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "物料描述";
            // 
            // tbUM
            // 
            // 
            // 
            // 
            this.tbUM.Border.Class = "TextBoxBorder";
            this.tbUM.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUM.Location = new System.Drawing.Point(549, 12);
            this.tbUM.Name = "tbUM";
            this.tbUM.PreventEnterBeep = true;
            this.tbUM.ReadOnly = true;
            this.tbUM.Size = new System.Drawing.Size(34, 21);
            this.tbUM.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(518, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(30, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "单位";
            // 
            // btnInventory
            // 
            this.btnInventory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInventory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInventory.Location = new System.Drawing.Point(605, 12);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(69, 23);
            this.btnInventory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInventory.TabIndex = 0;
            this.btnInventory.Text = "查看库存";
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnPurchaseHistory
            // 
            this.btnPurchaseHistory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPurchaseHistory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPurchaseHistory.Location = new System.Drawing.Point(693, 12);
            this.btnPurchaseHistory.Name = "btnPurchaseHistory";
            this.btnPurchaseHistory.Size = new System.Drawing.Size(69, 23);
            this.btnPurchaseHistory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPurchaseHistory.TabIndex = 0;
            this.btnPurchaseHistory.Text = "采购记录";
            this.btnPurchaseHistory.Click += new System.EventHandler(this.btnPurchaseHistory_Click);
            // 
            // dgvInventoryItem
            // 
            this.dgvInventoryItem.AllowUserToAddRows = false;
            this.dgvInventoryItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInventoryItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvInventoryItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInventoryItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryItem.Location = new System.Drawing.Point(2, 41);
            this.dgvInventoryItem.Name = "dgvInventoryItem";
            this.dgvInventoryItem.ReadOnly = true;
            this.dgvInventoryItem.RowTemplate.Height = 23;
            this.dgvInventoryItem.Size = new System.Drawing.Size(993, 559);
            this.dgvInventoryItem.TabIndex = 3;
            // 
            // RawMaterialInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 604);
            this.Controls.Add(this.dgvInventoryItem);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbUM);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.tbItemDescription);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tbItemNumber);
            this.Controls.Add(this.btnPurchaseHistory);
            this.Controls.Add(this.btnInventory);
            this.DoubleBuffered = true;
            this.Name = "RawMaterialInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料信息查询";
            this.Load += new System.EventHandler(this.RawMaterialInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemNumber;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemDescription;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbUM;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnInventory;
        private DevComponents.DotNetBar.ButtonX btnPurchaseHistory;
        private System.Windows.Forms.DataGridView dgvInventoryItem;
    }
}