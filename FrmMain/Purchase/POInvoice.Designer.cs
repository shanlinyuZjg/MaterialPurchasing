namespace Global.Purchase
{
    partial class POInvoice
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
            this.btnJoinInvoice = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbPONumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.superTabItem5 = new DevComponents.DotNetBar.SuperTabItem();
            this.dgvPODetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnPrintDetail = new DevComponents.DotNetBar.ButtonX();
            this.dgvTotal = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tbVendorName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnTotalPO = new System.Windows.Forms.RadioButton();
            this.rbtnSinglePO = new System.Windows.Forms.RadioButton();
            this.tbInvoiceNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnChooseFilePath = new DevComponents.DotNetBar.ButtonX();
            this.tbExportFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnExportToExcel = new DevComponents.DotNetBar.ButtonX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.btnNoInvoice = new DevComponents.DotNetBar.ButtonX();
            this.btnInvoced = new DevComponents.DotNetBar.ButtonX();
            this.tbInvoiced = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnRestrictDate = new System.Windows.Forms.RadioButton();
            this.rbtnNoDate = new System.Windows.Forms.RadioButton();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.tbInvoicedView = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnManageInvoice = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.tbStartLineNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.tbEndLineNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRecover = new DevComponents.DotNetBar.ButtonX();
            this.tbInvoiceAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.tbInvoiceTaxedAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.lblTotalAmount = new DevComponents.DotNetBar.LabelX();
            this.btnInvoiceSearch = new DevComponents.DotNetBar.ButtonX();
            this.TbVendorNumName = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnJoinInvoice
            // 
            this.btnJoinInvoice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnJoinInvoice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnJoinInvoice.Location = new System.Drawing.Point(650, 36);
            this.btnJoinInvoice.Name = "btnJoinInvoice";
            this.btnJoinInvoice.Size = new System.Drawing.Size(64, 23);
            this.btnJoinInvoice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnJoinInvoice.TabIndex = 9;
            this.btnJoinInvoice.Text = "加入开票";
            this.btnJoinInvoice.Visible = false;
            this.btnJoinInvoice.Click += new System.EventHandler(this.btnJoinInvoice_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(1, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "采购单号";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(166, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "供应商名";
            // 
            // tbPONumber
            // 
            // 
            // 
            // 
            this.tbPONumber.Border.Class = "TextBoxBorder";
            this.tbPONumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPONumber.Location = new System.Drawing.Point(65, 9);
            this.tbPONumber.Name = "tbPONumber";
            this.tbPONumber.PreventEnterBeep = true;
            this.tbPONumber.Size = new System.Drawing.Size(82, 21);
            this.tbPONumber.TabIndex = 0;
            this.tbPONumber.TextChanged += new System.EventHandler(this.tbPONumber_TextChanged);
            this.tbPONumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // superTabItem5
            // 
            this.superTabItem5.GlobalItem = false;
            this.superTabItem5.Name = "superTabItem5";
            this.superTabItem5.Text = "批量导入";
            this.superTabItem5.Visible = false;
            // 
            // dgvPODetail
            // 
            this.dgvPODetail.AllowUserToAddRows = false;
            this.dgvPODetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPODetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPODetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPODetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPODetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPODetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPODetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPODetail.EnableHeadersVisualStyles = false;
            this.dgvPODetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvPODetail.Location = new System.Drawing.Point(1, 62);
            this.dgvPODetail.Name = "dgvPODetail";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPODetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPODetail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPODetail.RowTemplate.Height = 23;
            this.dgvPODetail.Size = new System.Drawing.Size(1236, 293);
            this.dgvPODetail.TabIndex = 27;
            this.dgvPODetail.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPODetail_RowPostPaint);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 41;
            // 
            // btnPrintDetail
            // 
            this.btnPrintDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintDetail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintDetail.Location = new System.Drawing.Point(1055, 9);
            this.btnPrintDetail.Name = "btnPrintDetail";
            this.btnPrintDetail.Size = new System.Drawing.Size(65, 23);
            this.btnPrintDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintDetail.TabIndex = 9;
            this.btnPrintDetail.Text = "打印单据";
            this.btnPrintDetail.Visible = false;
            this.btnPrintDetail.Click += new System.EventHandler(this.btnPrintDetail_Click);
            // 
            // dgvTotal
            // 
            this.dgvTotal.AllowUserToAddRows = false;
            this.dgvTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTotal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTotal.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTotal.EnableHeadersVisualStyles = false;
            this.dgvTotal.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvTotal.Location = new System.Drawing.Point(1, 387);
            this.dgvTotal.Name = "dgvTotal";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTotal.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTotal.RowTemplate.Height = 23;
            this.dgvTotal.Size = new System.Drawing.Size(1236, 314);
            this.dgvTotal.TabIndex = 27;
            // 
            // tbVendorName
            // 
            // 
            // 
            // 
            this.tbVendorName.Border.Class = "TextBoxBorder";
            this.tbVendorName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendorName.Location = new System.Drawing.Point(223, 9);
            this.tbVendorName.Name = "tbVendorName";
            this.tbVendorName.PreventEnterBeep = true;
            this.tbVendorName.Size = new System.Drawing.Size(122, 21);
            this.tbVendorName.TabIndex = 1;
            this.tbVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(58, 363);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 20);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "合计开票";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnTotalPO);
            this.groupBox1.Controls.Add(this.rbtnSinglePO);
            this.groupBox1.Location = new System.Drawing.Point(889, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 31);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // rbtnTotalPO
            // 
            this.rbtnTotalPO.AutoSize = true;
            this.rbtnTotalPO.Location = new System.Drawing.Point(86, 9);
            this.rbtnTotalPO.Name = "rbtnTotalPO";
            this.rbtnTotalPO.Size = new System.Drawing.Size(71, 16);
            this.rbtnTotalPO.TabIndex = 0;
            this.rbtnTotalPO.Text = "汇总订单";
            this.rbtnTotalPO.UseVisualStyleBackColor = true;
            // 
            // rbtnSinglePO
            // 
            this.rbtnSinglePO.AutoSize = true;
            this.rbtnSinglePO.Checked = true;
            this.rbtnSinglePO.Location = new System.Drawing.Point(7, 9);
            this.rbtnSinglePO.Name = "rbtnSinglePO";
            this.rbtnSinglePO.Size = new System.Drawing.Size(71, 16);
            this.rbtnSinglePO.TabIndex = 0;
            this.rbtnSinglePO.TabStop = true;
            this.rbtnSinglePO.Text = "单个订单";
            this.rbtnSinglePO.UseVisualStyleBackColor = true;
            // 
            // tbInvoiceNumber
            // 
            // 
            // 
            // 
            this.tbInvoiceNumber.Border.Class = "TextBoxBorder";
            this.tbInvoiceNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceNumber.Location = new System.Drawing.Point(786, 11);
            this.tbInvoiceNumber.Name = "tbInvoiceNumber";
            this.tbInvoiceNumber.PreventEnterBeep = true;
            this.tbInvoiceNumber.Size = new System.Drawing.Size(95, 21);
            this.tbInvoiceNumber.TabIndex = 0;
            this.tbInvoiceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(738, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(42, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "发票号";
            // 
            // btnChooseFilePath
            // 
            this.btnChooseFilePath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseFilePath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseFilePath.Location = new System.Drawing.Point(465, 361);
            this.btnChooseFilePath.Name = "btnChooseFilePath";
            this.btnChooseFilePath.Size = new System.Drawing.Size(48, 23);
            this.btnChooseFilePath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseFilePath.TabIndex = 43;
            this.btnChooseFilePath.Text = "位置";
            this.btnChooseFilePath.Click += new System.EventHandler(this.btnChooseFilePath_Click);
            // 
            // tbExportFilePath
            // 
            // 
            // 
            // 
            this.tbExportFilePath.Border.Class = "TextBoxBorder";
            this.tbExportFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbExportFilePath.Location = new System.Drawing.Point(325, 361);
            this.tbExportFilePath.Name = "tbExportFilePath";
            this.tbExportFilePath.PreventEnterBeep = true;
            this.tbExportFilePath.Size = new System.Drawing.Size(124, 21);
            this.tbExportFilePath.TabIndex = 42;
            this.tbExportFilePath.Text = "D:\\";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportToExcel.Location = new System.Drawing.Point(119, 361);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(163, 23);
            this.btnExportToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportToExcel.TabIndex = 41;
            this.btnExportToExcel.Text = "导出 (导出的表格请勿改动)";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(288, 362);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(29, 23);
            this.labelX10.TabIndex = 40;
            this.labelX10.Text = "路径";
            // 
            // btnNoInvoice
            // 
            this.btnNoInvoice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNoInvoice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNoInvoice.Location = new System.Drawing.Point(603, 361);
            this.btnNoInvoice.Name = "btnNoInvoice";
            this.btnNoInvoice.Size = new System.Drawing.Size(75, 23);
            this.btnNoInvoice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNoInvoice.TabIndex = 43;
            this.btnNoInvoice.Text = "无票确认";
            this.btnNoInvoice.Visible = false;
            this.btnNoInvoice.Click += new System.EventHandler(this.btnNoInvoice_Click);
            // 
            // btnInvoced
            // 
            this.btnInvoced.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInvoced.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInvoced.Location = new System.Drawing.Point(1134, 362);
            this.btnInvoced.Name = "btnInvoced";
            this.btnInvoced.Size = new System.Drawing.Size(63, 23);
            this.btnInvoced.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInvoced.TabIndex = 43;
            this.btnInvoced.Text = "有票确认";
            this.btnInvoced.Visible = false;
            this.btnInvoced.Click += new System.EventHandler(this.btnInvoced_Click);
            // 
            // tbInvoiced
            // 
            // 
            // 
            // 
            this.tbInvoiced.Border.Class = "TextBoxBorder";
            this.tbInvoiced.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiced.Location = new System.Drawing.Point(728, 363);
            this.tbInvoiced.Name = "tbInvoiced";
            this.tbInvoiced.PreventEnterBeep = true;
            this.tbInvoiced.Size = new System.Drawing.Size(95, 21);
            this.tbInvoiced.TabIndex = 0;
            this.tbInvoiced.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(682, 363);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(43, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "发票号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnRestrictDate);
            this.groupBox2.Controls.Add(this.rbtnNoDate);
            this.groupBox2.Location = new System.Drawing.Point(353, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 31);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            // 
            // rbtnRestrictDate
            // 
            this.rbtnRestrictDate.AutoSize = true;
            this.rbtnRestrictDate.Checked = true;
            this.rbtnRestrictDate.Location = new System.Drawing.Point(86, 9);
            this.rbtnRestrictDate.Name = "rbtnRestrictDate";
            this.rbtnRestrictDate.Size = new System.Drawing.Size(107, 16);
            this.rbtnRestrictDate.TabIndex = 0;
            this.rbtnRestrictDate.TabStop = true;
            this.rbtnRestrictDate.Text = "2020.12.01以后";
            this.rbtnRestrictDate.UseVisualStyleBackColor = true;
            // 
            // rbtnNoDate
            // 
            this.rbtnNoDate.AutoSize = true;
            this.rbtnNoDate.Location = new System.Drawing.Point(7, 9);
            this.rbtnNoDate.Name = "rbtnNoDate";
            this.rbtnNoDate.Size = new System.Drawing.Size(71, 16);
            this.rbtnNoDate.TabIndex = 0;
            this.rbtnNoDate.Text = "不限日期";
            this.rbtnNoDate.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Location = new System.Drawing.Point(537, 361);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(44, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 43;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Location = new System.Drawing.Point(566, 36);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(51, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 9;
            this.btnMakeAllChecked.Text = "全选";
            this.btnMakeAllChecked.Visible = false;
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // tbInvoicedView
            // 
            // 
            // 
            // 
            this.tbInvoicedView.Border.Class = "TextBoxBorder";
            this.tbInvoicedView.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoicedView.Location = new System.Drawing.Point(619, 9);
            this.tbInvoicedView.Name = "tbInvoicedView";
            this.tbInvoicedView.PreventEnterBeep = true;
            this.tbInvoicedView.Size = new System.Drawing.Size(95, 21);
            this.tbInvoicedView.TabIndex = 0;
            this.tbInvoicedView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInvoicedView_KeyPress);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(555, 9);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(58, 23);
            this.labelX6.TabIndex = 2;
            this.labelX6.Text = "发票查找";
            // 
            // btnManageInvoice
            // 
            this.btnManageInvoice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnManageInvoice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnManageInvoice.Location = new System.Drawing.Point(1156, 9);
            this.btnManageInvoice.Name = "btnManageInvoice";
            this.btnManageInvoice.Size = new System.Drawing.Size(65, 23);
            this.btnManageInvoice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnManageInvoice.TabIndex = 9;
            this.btnManageInvoice.Text = "开票管理";
            this.btnManageInvoice.Visible = false;
            this.btnManageInvoice.Click += new System.EventHandler(this.btnManageInvoice_Click);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(339, 36);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(43, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "起始行";
            // 
            // tbStartLineNumber
            // 
            // 
            // 
            // 
            this.tbStartLineNumber.Border.Class = "TextBoxBorder";
            this.tbStartLineNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbStartLineNumber.Location = new System.Drawing.Point(388, 36);
            this.tbStartLineNumber.Name = "tbStartLineNumber";
            this.tbStartLineNumber.PreventEnterBeep = true;
            this.tbStartLineNumber.Size = new System.Drawing.Size(47, 21);
            this.tbStartLineNumber.TabIndex = 1;
            this.tbStartLineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartLineNumber_KeyPress);
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(441, 36);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(43, 23);
            this.labelX8.TabIndex = 2;
            this.labelX8.Text = "结束行";
            // 
            // tbEndLineNumber
            // 
            // 
            // 
            // 
            this.tbEndLineNumber.Border.Class = "TextBoxBorder";
            this.tbEndLineNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbEndLineNumber.Location = new System.Drawing.Point(490, 36);
            this.tbEndLineNumber.Name = "tbEndLineNumber";
            this.tbEndLineNumber.PreventEnterBeep = true;
            this.tbEndLineNumber.Size = new System.Drawing.Size(47, 21);
            this.tbEndLineNumber.TabIndex = 1;
            this.tbEndLineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndLineNumber_KeyPress);
            // 
            // btnRecover
            // 
            this.btnRecover.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRecover.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRecover.Location = new System.Drawing.Point(786, 36);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(64, 23);
            this.btnRecover.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRecover.TabIndex = 9;
            this.btnRecover.Text = "恢复状态";
            this.btnRecover.Visible = false;
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            // 
            // tbInvoiceAmount
            // 
            // 
            // 
            // 
            this.tbInvoiceAmount.Border.Class = "TextBoxBorder";
            this.tbInvoiceAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceAmount.Location = new System.Drawing.Point(889, 364);
            this.tbInvoiceAmount.Name = "tbInvoiceAmount";
            this.tbInvoiceAmount.PreventEnterBeep = true;
            this.tbInvoiceAmount.Size = new System.Drawing.Size(95, 21);
            this.tbInvoiceAmount.TabIndex = 0;
            this.tbInvoiceAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(823, 364);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(60, 23);
            this.labelX9.TabIndex = 2;
            this.labelX9.Text = "发票金额";
            // 
            // tbInvoiceTaxedAmount
            // 
            // 
            // 
            // 
            this.tbInvoiceTaxedAmount.Border.Class = "TextBoxBorder";
            this.tbInvoiceTaxedAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceTaxedAmount.Location = new System.Drawing.Point(1028, 364);
            this.tbInvoiceTaxedAmount.Name = "tbInvoiceTaxedAmount";
            this.tbInvoiceTaxedAmount.PreventEnterBeep = true;
            this.tbInvoiceTaxedAmount.Size = new System.Drawing.Size(80, 21);
            this.tbInvoiceTaxedAmount.TabIndex = 0;
            this.tbInvoiceTaxedAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(990, 364);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(32, 23);
            this.labelX11.TabIndex = 2;
            this.labelX11.Text = "税额";
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(889, 36);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(71, 23);
            this.labelX12.TabIndex = 2;
            this.labelX12.Text = "单据总金额";
            // 
            // lblTotalAmount
            // 
            // 
            // 
            // 
            this.lblTotalAmount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTotalAmount.Location = new System.Drawing.Point(972, 36);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(145, 23);
            this.lblTotalAmount.TabIndex = 2;
            // 
            // btnInvoiceSearch
            // 
            this.btnInvoiceSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInvoiceSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInvoiceSearch.Location = new System.Drawing.Point(1156, 36);
            this.btnInvoiceSearch.Name = "btnInvoiceSearch";
            this.btnInvoiceSearch.Size = new System.Drawing.Size(65, 23);
            this.btnInvoiceSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInvoiceSearch.TabIndex = 9;
            this.btnInvoiceSearch.Text = "开票查询";
            this.btnInvoiceSearch.Click += new System.EventHandler(this.btnInvoiceSearch_Click);
            // 
            // TbVendorNumName
            // 
            // 
            // 
            // 
            this.TbVendorNumName.Border.Class = "TextBoxBorder";
            this.TbVendorNumName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TbVendorNumName.Location = new System.Drawing.Point(13, 36);
            this.TbVendorNumName.Name = "TbVendorNumName";
            this.TbVendorNumName.PreventEnterBeep = true;
            this.TbVendorNumName.ReadOnly = true;
            this.TbVendorNumName.Size = new System.Drawing.Size(320, 21);
            this.TbVendorNumName.TabIndex = 44;
            // 
            // POInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 713);
            this.Controls.Add(this.TbVendorNumName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnInvoced);
            this.Controls.Add(this.btnNoInvoice);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnChooseFilePath);
            this.Controls.Add(this.tbExportFilePath);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvTotal);
            this.Controls.Add(this.dgvPODetail);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.tbInvoiceTaxedAmount);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.tbInvoiceAmount);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tbInvoiced);
            this.Controls.Add(this.tbInvoicedView);
            this.Controls.Add(this.tbInvoiceNumber);
            this.Controls.Add(this.tbEndLineNumber);
            this.Controls.Add(this.tbStartLineNumber);
            this.Controls.Add(this.tbVendorName);
            this.Controls.Add(this.tbPONumber);
            this.Controls.Add(this.btnInvoiceSearch);
            this.Controls.Add(this.btnManageInvoice);
            this.Controls.Add(this.btnPrintDetail);
            this.Controls.Add(this.btnMakeAllChecked);
            this.Controls.Add(this.btnRecover);
            this.Controls.Add(this.btnJoinInvoice);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX2);
            this.DoubleBuffered = true;
            this.Name = "POInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POInvoice";
            this.Load += new System.EventHandler(this.POInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnJoinInvoice;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPONumber;
        private DevComponents.DotNetBar.SuperTabItem superTabItem5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvPODetail;
        private DevComponents.DotNetBar.ButtonX btnPrintDetail;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvTotal;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVendorName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnTotalPO;
        private System.Windows.Forms.RadioButton rbtnSinglePO;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceNumber;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnChooseFilePath;
        private DevComponents.DotNetBar.Controls.TextBoxX tbExportFilePath;
        private DevComponents.DotNetBar.ButtonX btnExportToExcel;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.ButtonX btnNoInvoice;
        private DevComponents.DotNetBar.ButtonX btnInvoced;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiced;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnRestrictDate;
        private System.Windows.Forms.RadioButton rbtnNoDate;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoicedView;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX btnManageInvoice;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX tbStartLineNumber;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX tbEndLineNumber;
        private DevComponents.DotNetBar.ButtonX btnRecover;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceAmount;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceTaxedAmount;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.LabelX lblTotalAmount;
        private DevComponents.DotNetBar.ButtonX btnInvoiceSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX TbVendorNumName;
    }
}