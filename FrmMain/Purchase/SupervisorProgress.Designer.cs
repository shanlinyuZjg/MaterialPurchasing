namespace Global.Purchase
{
    partial class SupervisorProgress
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
            this.tbExportFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.rbtnNotDate = new System.Windows.Forms.RadioButton();
            this.rbtnDate = new System.Windows.Forms.RadioButton();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbtnDomestic = new System.Windows.Forms.RadioButton();
            this.rbtnForeign = new System.Windows.Forms.RadioButton();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.btnExportToExcel = new DevComponents.DotNetBar.ButtonX();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.tbSearchItem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSearchItem = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbtnFONumber = new System.Windows.Forms.RadioButton();
            this.rbtnItemDescription = new System.Windows.Forms.RadioButton();
            this.rbtnItemNumber = new System.Windows.Forms.RadioButton();
            this.rbtnVendorName = new System.Windows.Forms.RadioButton();
            this.groupPanel4.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbExportFilePath
            // 
            // 
            // 
            // 
            this.tbExportFilePath.Border.Class = "TextBoxBorder";
            this.tbExportFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbExportFilePath.Location = new System.Drawing.Point(1205, 13);
            this.tbExportFilePath.Name = "tbExportFilePath";
            this.tbExportFilePath.PreventEnterBeep = true;
            this.tbExportFilePath.Size = new System.Drawing.Size(89, 21);
            this.tbExportFilePath.TabIndex = 40;
            this.tbExportFilePath.Text = "D:\\PO.xlsx";
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(1170, 13);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(29, 23);
            this.labelX11.TabIndex = 39;
            this.labelX11.Text = "路径";
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.dtpEndDate);
            this.groupPanel4.Controls.Add(this.dtpStartDate);
            this.groupPanel4.Controls.Add(this.labelX10);
            this.groupPanel4.Controls.Add(this.labelX9);
            this.groupPanel4.Controls.Add(this.rbtnNotDate);
            this.groupPanel4.Controls.Add(this.rbtnDate);
            this.groupPanel4.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel4.Location = new System.Drawing.Point(636, 9);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(426, 30);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel4.TabIndex = 37;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(315, 1);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(102, 21);
            this.dtpEndDate.TabIndex = 12;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(171, 1);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 21);
            this.dtpStartDate.TabIndex = 13;
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(278, 1);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(31, 23);
            this.labelX10.TabIndex = 10;
            this.labelX10.Text = "截止";
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(136, 1);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(35, 23);
            this.labelX9.TabIndex = 11;
            this.labelX9.Text = "开始";
            // 
            // rbtnNotDate
            // 
            this.rbtnNotDate.AutoSize = true;
            this.rbtnNotDate.BackColor = System.Drawing.Color.Transparent;
            this.rbtnNotDate.Checked = true;
            this.rbtnNotDate.Location = new System.Drawing.Point(3, 4);
            this.rbtnNotDate.Name = "rbtnNotDate";
            this.rbtnNotDate.Size = new System.Drawing.Size(71, 16);
            this.rbtnNotDate.TabIndex = 9;
            this.rbtnNotDate.TabStop = true;
            this.rbtnNotDate.Text = "不按日期";
            this.rbtnNotDate.UseVisualStyleBackColor = false;
            // 
            // rbtnDate
            // 
            this.rbtnDate.AutoSize = true;
            this.rbtnDate.BackColor = System.Drawing.Color.Transparent;
            this.rbtnDate.Location = new System.Drawing.Point(76, 4);
            this.rbtnDate.Name = "rbtnDate";
            this.rbtnDate.Size = new System.Drawing.Size(59, 16);
            this.rbtnDate.TabIndex = 9;
            this.rbtnDate.Text = "按日期";
            this.rbtnDate.UseVisualStyleBackColor = false;
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.rbtnDomestic);
            this.groupPanel3.Controls.Add(this.rbtnForeign);
            this.groupPanel3.Controls.Add(this.rbtnAll);
            this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel3.Location = new System.Drawing.Point(470, 9);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(158, 29);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 38;
            // 
            // rbtnDomestic
            // 
            this.rbtnDomestic.AutoSize = true;
            this.rbtnDomestic.BackColor = System.Drawing.Color.Transparent;
            this.rbtnDomestic.Location = new System.Drawing.Point(103, 3);
            this.rbtnDomestic.Name = "rbtnDomestic";
            this.rbtnDomestic.Size = new System.Drawing.Size(47, 16);
            this.rbtnDomestic.TabIndex = 0;
            this.rbtnDomestic.Text = "内销";
            this.rbtnDomestic.UseVisualStyleBackColor = false;
            // 
            // rbtnForeign
            // 
            this.rbtnForeign.AutoSize = true;
            this.rbtnForeign.BackColor = System.Drawing.Color.Transparent;
            this.rbtnForeign.Location = new System.Drawing.Point(54, 3);
            this.rbtnForeign.Name = "rbtnForeign";
            this.rbtnForeign.Size = new System.Drawing.Size(47, 16);
            this.rbtnForeign.TabIndex = 0;
            this.rbtnForeign.Text = "外贸";
            this.rbtnForeign.UseVisualStyleBackColor = false;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.BackColor = System.Drawing.Color.Transparent;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(3, 3);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.TabIndex = 0;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "全部";
            this.rbtnAll.UseVisualStyleBackColor = false;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportToExcel.Location = new System.Drawing.Point(1122, 13);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(42, 23);
            this.btnExportToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportToExcel.TabIndex = 36;
            this.btnExportToExcel.Text = "导出";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // superGridControl1
            // 
            this.superGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(0, 42);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.ColumnAutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            this.superGridControl1.PrimaryGrid.ReadOnly = true;
            this.superGridControl1.Size = new System.Drawing.Size(1294, 549);
            this.superGridControl1.TabIndex = 35;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(4, 13);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(66, 23);
            this.labelX8.TabIndex = 34;
            this.labelX8.Text = "查询内容";
            // 
            // tbSearchItem
            // 
            // 
            // 
            // 
            this.tbSearchItem.Border.Class = "TextBoxBorder";
            this.tbSearchItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbSearchItem.Location = new System.Drawing.Point(76, 13);
            this.tbSearchItem.Name = "tbSearchItem";
            this.tbSearchItem.PreventEnterBeep = true;
            this.tbSearchItem.Size = new System.Drawing.Size(98, 21);
            this.tbSearchItem.TabIndex = 33;
            this.tbSearchItem.TextChanged += new System.EventHandler(this.tbSearchItem_TextChanged);
            this.tbSearchItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchItem_KeyPress);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearchItem.Location = new System.Drawing.Point(1070, 13);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(46, 23);
            this.btnSearchItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearchItem.TabIndex = 32;
            this.btnSearchItem.Text = "查找";
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.rbtnVendorName);
            this.groupPanel1.Controls.Add(this.rbtnFONumber);
            this.groupPanel1.Controls.Add(this.rbtnItemDescription);
            this.groupPanel1.Controls.Add(this.rbtnItemNumber);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(180, 9);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(284, 28);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 41;
            // 
            // rbtnFONumber
            // 
            this.rbtnFONumber.AutoSize = true;
            this.rbtnFONumber.BackColor = System.Drawing.Color.Transparent;
            this.rbtnFONumber.Location = new System.Drawing.Point(112, 3);
            this.rbtnFONumber.Name = "rbtnFONumber";
            this.rbtnFONumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnFONumber.TabIndex = 0;
            this.rbtnFONumber.Text = "联系单号";
            this.rbtnFONumber.UseVisualStyleBackColor = false;
            this.rbtnFONumber.CheckedChanged += new System.EventHandler(this.rbtnFONumber_CheckedChanged);
            // 
            // rbtnItemDescription
            // 
            this.rbtnItemDescription.AutoSize = true;
            this.rbtnItemDescription.BackColor = System.Drawing.Color.Transparent;
            this.rbtnItemDescription.Location = new System.Drawing.Point(59, 3);
            this.rbtnItemDescription.Name = "rbtnItemDescription";
            this.rbtnItemDescription.Size = new System.Drawing.Size(47, 16);
            this.rbtnItemDescription.TabIndex = 0;
            this.rbtnItemDescription.Text = "名称";
            this.rbtnItemDescription.UseVisualStyleBackColor = false;
            // 
            // rbtnItemNumber
            // 
            this.rbtnItemNumber.AutoSize = true;
            this.rbtnItemNumber.BackColor = System.Drawing.Color.Transparent;
            this.rbtnItemNumber.Checked = true;
            this.rbtnItemNumber.Location = new System.Drawing.Point(6, 3);
            this.rbtnItemNumber.Name = "rbtnItemNumber";
            this.rbtnItemNumber.Size = new System.Drawing.Size(47, 16);
            this.rbtnItemNumber.TabIndex = 0;
            this.rbtnItemNumber.TabStop = true;
            this.rbtnItemNumber.Text = "代码";
            this.rbtnItemNumber.UseVisualStyleBackColor = false;
            // 
            // rbtnVendorName
            // 
            this.rbtnVendorName.AutoSize = true;
            this.rbtnVendorName.BackColor = System.Drawing.Color.Transparent;
            this.rbtnVendorName.Location = new System.Drawing.Point(189, 3);
            this.rbtnVendorName.Name = "rbtnVendorName";
            this.rbtnVendorName.Size = new System.Drawing.Size(71, 16);
            this.rbtnVendorName.TabIndex = 0;
            this.rbtnVendorName.Text = "供应商名";
            this.rbtnVendorName.UseVisualStyleBackColor = false;
            this.rbtnVendorName.CheckedChanged += new System.EventHandler(this.rbtnFONumber_CheckedChanged);
            // 
            // SupervisorProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 603);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.tbExportFilePath);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.groupPanel4);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.tbSearchItem);
            this.Controls.Add(this.btnSearchItem);
            this.DoubleBuffered = true;
            this.Name = "SupervisorProgress";
            this.Text = "SupervisorProgress";
            this.Load += new System.EventHandler(this.SupervisorProgress_Load);
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbExportFilePath;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.RadioButton rbtnNotDate;
        private System.Windows.Forms.RadioButton rbtnDate;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private System.Windows.Forms.RadioButton rbtnDomestic;
        private System.Windows.Forms.RadioButton rbtnForeign;
        private System.Windows.Forms.RadioButton rbtnAll;
        private DevComponents.DotNetBar.ButtonX btnExportToExcel;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX tbSearchItem;
        private DevComponents.DotNetBar.ButtonX btnSearchItem;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RadioButton rbtnFONumber;
        private System.Windows.Forms.RadioButton rbtnItemDescription;
        private System.Windows.Forms.RadioButton rbtnItemNumber;
        private System.Windows.Forms.RadioButton rbtnVendorName;
    }
}