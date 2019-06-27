namespace Global.Purchase
{
    partial class InventoryMaterialReminder
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
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.tbUM = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.tbMinimumQuantity = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbItemDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnPlace = new DevComponents.DotNetBar.ButtonX();
            this.tbItemNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvInventoryItemSetting = new System.Windows.Forms.DataGridView();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItemSetting)).BeginInit();
            this.superTabControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInventory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInventory.Location = new System.Drawing.Point(13, 32);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowTemplate.Height = 23;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1158, 631);
            this.dgvInventory.TabIndex = 3;
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
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Location = new System.Drawing.Point(12, 12);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1183, 707);
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 5;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1,
            this.superTabItem2});
            this.superTabControl1.Text = "superTabControl1";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.groupPanel2);
            this.superTabControlPanel2.Controls.Add(this.dgvInventoryItemSetting);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(1183, 679);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.Controls.Add(this.tbUM);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Controls.Add(this.tbMinimumQuantity);
            this.groupPanel2.Controls.Add(this.tbItemDescription);
            this.groupPanel2.Controls.Add(this.btnPlace);
            this.groupPanel2.Controls.Add(this.tbItemNumber);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Location = new System.Drawing.Point(14, 10);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(1152, 64);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 7;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(541, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(33, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "单位";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(641, 15);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(82, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "库存最低数量";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(184, 15);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "物料描述";
            // 
            // tbUM
            // 
            // 
            // 
            // 
            this.tbUM.Border.Class = "TextBoxBorder";
            this.tbUM.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUM.Location = new System.Drawing.Point(580, 15);
            this.tbUM.Name = "tbUM";
            this.tbUM.PreventEnterBeep = true;
            this.tbUM.ReadOnly = true;
            this.tbUM.Size = new System.Drawing.Size(44, 21);
            this.tbUM.TabIndex = 1;
            this.tbUM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMinimumQuantity_KeyPress);
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(6, 17);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(55, 23);
            this.labelX6.TabIndex = 2;
            this.labelX6.Text = "物料代码";
            // 
            // tbMinimumQuantity
            // 
            // 
            // 
            // 
            this.tbMinimumQuantity.Border.Class = "TextBoxBorder";
            this.tbMinimumQuantity.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMinimumQuantity.Location = new System.Drawing.Point(725, 15);
            this.tbMinimumQuantity.Name = "tbMinimumQuantity";
            this.tbMinimumQuantity.PreventEnterBeep = true;
            this.tbMinimumQuantity.Size = new System.Drawing.Size(113, 21);
            this.tbMinimumQuantity.TabIndex = 1;
            this.tbMinimumQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMinimumQuantity_KeyPress);
            // 
            // tbItemDescription
            // 
            // 
            // 
            // 
            this.tbItemDescription.Border.Class = "TextBoxBorder";
            this.tbItemDescription.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemDescription.Location = new System.Drawing.Point(245, 15);
            this.tbItemDescription.Name = "tbItemDescription";
            this.tbItemDescription.PreventEnterBeep = true;
            this.tbItemDescription.ReadOnly = true;
            this.tbItemDescription.Size = new System.Drawing.Size(290, 21);
            this.tbItemDescription.TabIndex = 1;
            // 
            // btnPlace
            // 
            this.btnPlace.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlace.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlace.Location = new System.Drawing.Point(944, 15);
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.Size = new System.Drawing.Size(75, 23);
            this.btnPlace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlace.TabIndex = 0;
            this.btnPlace.Text = "设置";
            this.btnPlace.Click += new System.EventHandler(this.btnPlace_Click);
            // 
            // tbItemNumber
            // 
            // 
            // 
            // 
            this.tbItemNumber.Border.Class = "TextBoxBorder";
            this.tbItemNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemNumber.Location = new System.Drawing.Point(67, 17);
            this.tbItemNumber.Name = "tbItemNumber";
            this.tbItemNumber.PreventEnterBeep = true;
            this.tbItemNumber.Size = new System.Drawing.Size(100, 21);
            this.tbItemNumber.TabIndex = 1;
            this.tbItemNumber.TextChanged += new System.EventHandler(this.tbItemNumber_TextChanged);
            this.tbItemNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemNumber_KeyPress);
            // 
            // dgvInventoryItemSetting
            // 
            this.dgvInventoryItemSetting.AllowUserToAddRows = false;
            this.dgvInventoryItemSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventoryItemSetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInventoryItemSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInventoryItemSetting.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInventoryItemSetting.Location = new System.Drawing.Point(14, 95);
            this.dgvInventoryItemSetting.Name = "dgvInventoryItemSetting";
            this.dgvInventoryItemSetting.ReadOnly = true;
            this.dgvInventoryItemSetting.RowTemplate.Height = 23;
            this.dgvInventoryItemSetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventoryItemSetting.Size = new System.Drawing.Size(1152, 566);
            this.dgvInventoryItemSetting.TabIndex = 6;
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "库存提醒设置";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.labelX7);
            this.superTabControlPanel1.Controls.Add(this.dgvInventory);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1183, 679);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(13, 3);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(203, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "急需采购物料";
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "物料库存显示";
            // 
            // InventoryMaterialReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 731);
            this.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            this.Name = "InventoryMaterialReminder";
            this.Text = "InventoryMaterialRemind";
            this.Load += new System.EventHandler(this.InventoryMaterialReminder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItemSetting)).EndInit();
            this.superTabControlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvInventory;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMinimumQuantity;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemDescription;
        private DevComponents.DotNetBar.ButtonX btnPlace;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemNumber;
        private System.Windows.Forms.DataGridView dgvInventoryItemSetting;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbUM;
    }
}