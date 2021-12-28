namespace Global.Purchase
{
    partial class ImportDemesticProduct
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
            this.dgvExcelContent = new System.Windows.Forms.DataGridView();
            this.cbbSheetName = new System.Windows.Forms.ComboBox();
            this.tbFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnShowExcelContent = new DevComponents.DotNetBar.ButtonX();
            this.btnChooseFile = new DevComponents.DotNetBar.ButtonX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnPlaceOrder = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnItemPrice = new System.Windows.Forms.RadioButton();
            this.rbtnPurchaseData = new System.Windows.Forms.RadioButton();
            this.rbtnOriginalData = new System.Windows.Forms.RadioButton();
            this.tbExportFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnExportToExcel = new DevComponents.DotNetBar.ButtonX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.btnImportDomesticProductPrice = new DevComponents.DotNetBar.ButtonX();
            this.btnManageItemPrice = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.tbTaxRate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnItemWithoutReview = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelContent)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExcelContent
            // 
            this.dgvExcelContent.AllowUserToAddRows = false;
            this.dgvExcelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExcelContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExcelContent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
            this.dgvExcelContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvExcelContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcelContent.Location = new System.Drawing.Point(0, 79);
            this.dgvExcelContent.MultiSelect = false;
            this.dgvExcelContent.Name = "dgvExcelContent";
            this.dgvExcelContent.RowTemplate.Height = 23;
            this.dgvExcelContent.Size = new System.Drawing.Size(1143, 494);
            this.dgvExcelContent.TabIndex = 24;
            // 
            // cbbSheetName
            // 
            this.cbbSheetName.FormattingEnabled = true;
            this.cbbSheetName.Location = new System.Drawing.Point(75, 41);
            this.cbbSheetName.Name = "cbbSheetName";
            this.cbbSheetName.Size = new System.Drawing.Size(103, 20);
            this.cbbSheetName.TabIndex = 23;
            // 
            // tbFilePath
            // 
            // 
            // 
            // 
            this.tbFilePath.Border.Class = "TextBoxBorder";
            this.tbFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFilePath.Location = new System.Drawing.Point(75, 12);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.PreventEnterBeep = true;
            this.tbFilePath.Size = new System.Drawing.Size(359, 21);
            this.tbFilePath.TabIndex = 22;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(608, 43);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(86, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "查找信息";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnShowExcelContent
            // 
            this.btnShowExcelContent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnShowExcelContent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnShowExcelContent.Location = new System.Drawing.Point(442, 43);
            this.btnShowExcelContent.Name = "btnShowExcelContent";
            this.btnShowExcelContent.Size = new System.Drawing.Size(63, 23);
            this.btnShowExcelContent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnShowExcelContent.TabIndex = 20;
            this.btnShowExcelContent.Text = "显示内容";
            this.btnShowExcelContent.Click += new System.EventHandler(this.btnShowExcelContent_Click);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseFile.Location = new System.Drawing.Point(442, 12);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(63, 23);
            this.btnChooseFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseFile.TabIndex = 21;
            this.btnChooseFile.Text = "选择文件";
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(0, 41);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(64, 23);
            this.labelX8.TabIndex = 17;
            this.labelX8.Text = "选择Sheet";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(0, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 18;
            this.labelX3.Text = "文件路径";
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlaceOrder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlaceOrder.Location = new System.Drawing.Point(794, 41);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(63, 23);
            this.btnPlaceOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlaceOrder.TabIndex = 25;
            this.btnPlaceOrder.Text = "生成订单";
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnItemPrice);
            this.groupBox1.Controls.Add(this.rbtnPurchaseData);
            this.groupBox1.Controls.Add(this.rbtnOriginalData);
            this.groupBox1.Location = new System.Drawing.Point(184, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 32);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // rbtnItemPrice
            // 
            this.rbtnItemPrice.AutoSize = true;
            this.rbtnItemPrice.Location = new System.Drawing.Point(177, 12);
            this.rbtnItemPrice.Name = "rbtnItemPrice";
            this.rbtnItemPrice.Size = new System.Drawing.Size(71, 16);
            this.rbtnItemPrice.TabIndex = 1;
            this.rbtnItemPrice.TabStop = true;
            this.rbtnItemPrice.Text = "价格数据";
            this.rbtnItemPrice.UseVisualStyleBackColor = true;
            this.rbtnItemPrice.CheckedChanged += new System.EventHandler(this.rbtnItemPrice_CheckedChanged);
            // 
            // rbtnPurchaseData
            // 
            this.rbtnPurchaseData.AutoSize = true;
            this.rbtnPurchaseData.Location = new System.Drawing.Point(99, 13);
            this.rbtnPurchaseData.Name = "rbtnPurchaseData";
            this.rbtnPurchaseData.Size = new System.Drawing.Size(71, 16);
            this.rbtnPurchaseData.TabIndex = 0;
            this.rbtnPurchaseData.TabStop = true;
            this.rbtnPurchaseData.Text = "采购数据";
            this.rbtnPurchaseData.UseVisualStyleBackColor = true;
            this.rbtnPurchaseData.CheckedChanged += new System.EventHandler(this.rbtnPurchaseData_CheckedChanged);
            // 
            // rbtnOriginalData
            // 
            this.rbtnOriginalData.AutoSize = true;
            this.rbtnOriginalData.Checked = true;
            this.rbtnOriginalData.Location = new System.Drawing.Point(18, 12);
            this.rbtnOriginalData.Name = "rbtnOriginalData";
            this.rbtnOriginalData.Size = new System.Drawing.Size(71, 16);
            this.rbtnOriginalData.TabIndex = 0;
            this.rbtnOriginalData.TabStop = true;
            this.rbtnOriginalData.Text = "原始数据";
            this.rbtnOriginalData.UseVisualStyleBackColor = true;
            this.rbtnOriginalData.CheckedChanged += new System.EventHandler(this.rbtnOriginalData_CheckedChanged);
            // 
            // tbExportFilePath
            // 
            // 
            // 
            // 
            this.tbExportFilePath.Border.Class = "TextBoxBorder";
            this.tbExportFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbExportFilePath.Location = new System.Drawing.Point(965, 43);
            this.tbExportFilePath.Name = "tbExportFilePath";
            this.tbExportFilePath.PreventEnterBeep = true;
            this.tbExportFilePath.Size = new System.Drawing.Size(178, 21);
            this.tbExportFilePath.TabIndex = 29;
            this.tbExportFilePath.Text = "D:\\PO.xlsx";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportToExcel.Location = new System.Drawing.Point(863, 41);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(61, 23);
            this.btnExportToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportToExcel.TabIndex = 28;
            this.btnExportToExcel.Text = "导出文件";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(930, 40);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(29, 23);
            this.labelX10.TabIndex = 27;
            this.labelX10.Text = "路径";
            // 
            // btnImportDomesticProductPrice
            // 
            this.btnImportDomesticProductPrice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportDomesticProductPrice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportDomesticProductPrice.Location = new System.Drawing.Point(518, 43);
            this.btnImportDomesticProductPrice.Name = "btnImportDomesticProductPrice";
            this.btnImportDomesticProductPrice.Size = new System.Drawing.Size(75, 23);
            this.btnImportDomesticProductPrice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportDomesticProductPrice.TabIndex = 31;
            this.btnImportDomesticProductPrice.Text = "价格导入";
            this.btnImportDomesticProductPrice.Click += new System.EventHandler(this.btnImportDomesticProductPrice_Click);
            // 
            // btnManageItemPrice
            // 
            this.btnManageItemPrice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnManageItemPrice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnManageItemPrice.Location = new System.Drawing.Point(518, 12);
            this.btnManageItemPrice.Name = "btnManageItemPrice";
            this.btnManageItemPrice.Size = new System.Drawing.Size(75, 23);
            this.btnManageItemPrice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnManageItemPrice.TabIndex = 32;
            this.btnManageItemPrice.Text = "价格管理";
            this.btnManageItemPrice.Click += new System.EventHandler(this.btnManageItemPrice_Click);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(694, 43);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(29, 23);
            this.labelX7.TabIndex = 27;
            this.labelX7.Text = "税率";
            // 
            // tbTaxRate
            // 
            // 
            // 
            // 
            this.tbTaxRate.Border.Class = "TextBoxBorder";
            this.tbTaxRate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaxRate.Location = new System.Drawing.Point(726, 42);
            this.tbTaxRate.Name = "tbTaxRate";
            this.tbTaxRate.PreventEnterBeep = true;
            this.tbTaxRate.Size = new System.Drawing.Size(56, 21);
            this.tbTaxRate.TabIndex = 33;
            this.tbTaxRate.Text = "0.13";
            // 
            // btnItemWithoutReview
            // 
            this.btnItemWithoutReview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemWithoutReview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnItemWithoutReview.Location = new System.Drawing.Point(608, 12);
            this.btnItemWithoutReview.Name = "btnItemWithoutReview";
            this.btnItemWithoutReview.Size = new System.Drawing.Size(86, 23);
            this.btnItemWithoutReview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnItemWithoutReview.TabIndex = 34;
            this.btnItemWithoutReview.Text = "不审核物料";
            this.btnItemWithoutReview.Click += new System.EventHandler(this.btnItemWithoutReview_Click);
            // 
            // ImportDemesticProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 585);
            this.Controls.Add(this.btnItemWithoutReview);
            this.Controls.Add(this.tbTaxRate);
            this.Controls.Add(this.btnManageItemPrice);
            this.Controls.Add(this.btnImportDomesticProductPrice);
            this.Controls.Add(this.tbExportFilePath);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.dgvExcelContent);
            this.Controls.Add(this.cbbSheetName);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnShowExcelContent);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX3);
            this.DoubleBuffered = true;
            this.Name = "ImportDemesticProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportDemesticProduct";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ImportDemesticProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelContent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExcelContent;
        private System.Windows.Forms.ComboBox cbbSheetName;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFilePath;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.ButtonX btnShowExcelContent;
        private DevComponents.DotNetBar.ButtonX btnChooseFile;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnPlaceOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnPurchaseData;
        private System.Windows.Forms.RadioButton rbtnOriginalData;
        private DevComponents.DotNetBar.Controls.TextBoxX tbExportFilePath;
        private DevComponents.DotNetBar.ButtonX btnExportToExcel;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.ButtonX btnImportDomesticProductPrice;
        private System.Windows.Forms.RadioButton rbtnItemPrice;
        private DevComponents.DotNetBar.ButtonX btnManageItemPrice;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaxRate;
        private DevComponents.DotNetBar.ButtonX btnItemWithoutReview;
    }
}