
namespace Global.Finance
{
    partial class InvoiceVerify
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.tbYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.tbMonth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbTaxType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.tnYes = new DevComponents.DotNetBar.ButtonX();
            this.tbInvoiceNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbTotalAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbPrepareToMatchAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbTaxAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbTaxCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbVATRate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.dtpFinish = new System.Windows.Forms.DateTimePicker();
            this.dtpError = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.btnViewErrorRecord = new DevComponents.DotNetBar.ButtonX();
            this.btnViewRecord = new DevComponents.DotNetBar.ButtonX();
            this.dgvRecord = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.Check = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle61.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle61.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle61.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle61.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle61.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle61;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle62.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle62.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle62.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle62.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle62.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle62;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(13, 38);
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle63.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle63.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle63.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle63.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle63.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle63.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle63;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowHeadersWidth = 20;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(1307, 690);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellClick);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(1270, 12);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(49, 23);
            this.btnConfirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfirm.TabIndex = 33;
            this.btnConfirm.Text = "核销";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(300, 10);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(42, 21);
            this.labelX6.TabIndex = 32;
            this.labelX6.Text = "VAT码";
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(59, 10);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(63, 21);
            this.labelX8.TabIndex = 32;
            this.labelX8.Text = "会计期间";
            // 
            // tbYear
            // 
            // 
            // 
            // 
            this.tbYear.Border.Class = "TextBoxBorder";
            this.tbYear.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbYear.Location = new System.Drawing.Point(125, 8);
            this.tbYear.Name = "tbYear";
            this.tbYear.PreventEnterBeep = true;
            this.tbYear.Size = new System.Drawing.Size(43, 23);
            this.tbYear.TabIndex = 31;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(13, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 33;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tbMonth
            // 
            // 
            // 
            // 
            this.tbMonth.Border.Class = "TextBoxBorder";
            this.tbMonth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMonth.Location = new System.Drawing.Point(184, 8);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.PreventEnterBeep = true;
            this.tbMonth.Size = new System.Drawing.Size(28, 23);
            this.tbMonth.TabIndex = 31;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(169, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(12, 21);
            this.labelX1.TabIndex = 32;
            this.labelX1.Text = "-";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(216, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(39, 21);
            this.labelX2.TabIndex = 32;
            this.labelX2.Text = "类型";
            // 
            // cbbTaxType
            // 
            this.cbbTaxType.DisplayMember = "Text";
            this.cbbTaxType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTaxType.FormattingEnabled = true;
            this.cbbTaxType.ItemHeight = 15;
            this.cbbTaxType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbbTaxType.Location = new System.Drawing.Point(260, 10);
            this.cbbTaxType.Name = "cbbTaxType";
            this.cbbTaxType.Size = new System.Drawing.Size(34, 21);
            this.cbbTaxType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbTaxType.TabIndex = 34;
            this.cbbTaxType.Text = "I";
            // 
            // comboItem1
            // 
            this.comboItem1.ImagePosition = System.Windows.Forms.HorizontalAlignment.Center;
            this.comboItem1.Text = "I";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "D";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Location = new System.Drawing.Point(8, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1340, 766);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Metro;
            this.tabControl1.TabIndex = 35;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.btnRefresh);
            this.tabControlPanel1.Controls.Add(this.cbbTaxType);
            this.tabControlPanel1.Controls.Add(this.dgvDetail);
            this.tabControlPanel1.Controls.Add(this.labelX1);
            this.tabControlPanel1.Controls.Add(this.labelX6);
            this.tabControlPanel1.Controls.Add(this.labelX3);
            this.tabControlPanel1.Controls.Add(this.labelX5);
            this.tabControlPanel1.Controls.Add(this.labelX10);
            this.tabControlPanel1.Controls.Add(this.labelX4);
            this.tabControlPanel1.Controls.Add(this.labelX9);
            this.tabControlPanel1.Controls.Add(this.btnMakeAllChecked);
            this.tabControlPanel1.Controls.Add(this.tnYes);
            this.tabControlPanel1.Controls.Add(this.btnConfirm);
            this.tabControlPanel1.Controls.Add(this.labelX8);
            this.tabControlPanel1.Controls.Add(this.tbMonth);
            this.tabControlPanel1.Controls.Add(this.labelX2);
            this.tabControlPanel1.Controls.Add(this.tbInvoiceNumber);
            this.tabControlPanel1.Controls.Add(this.tbTotalAmount);
            this.tabControlPanel1.Controls.Add(this.tbYear);
            this.tabControlPanel1.Controls.Add(this.tbPrepareToMatchAmount);
            this.tabControlPanel1.Controls.Add(this.tbTaxAmount);
            this.tabControlPanel1.Controls.Add(this.tbTaxCode);
            this.tabControlPanel1.Controls.Add(this.tbVATRate);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1340, 739);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.tabControlPanel1.Click += new System.EventHandler(this.tabControlPanel1_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(373, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(58, 21);
            this.labelX3.TabIndex = 32;
            this.labelX3.Text = "VAT税率";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(975, 14);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(34, 21);
            this.labelX5.TabIndex = 32;
            this.labelX5.Text = "票号";
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX10.Location = new System.Drawing.Point(789, 12);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(78, 21);
            this.labelX10.TabIndex = 32;
            this.labelX10.Text = "匹配总金额";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(599, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(92, 21);
            this.labelX4.TabIndex = 32;
            this.labelX4.Text = "匹配金额合计";
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX9.Location = new System.Drawing.Point(480, 11);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(35, 21);
            this.labelX9.TabIndex = 32;
            this.labelX9.Text = "税额";
            // 
            // tnYes
            // 
            this.tnYes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.tnYes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.tnYes.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tnYes.Location = new System.Drawing.Point(1212, 12);
            this.tnYes.Name = "tnYes";
            this.tnYes.Size = new System.Drawing.Size(49, 23);
            this.tnYes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tnYes.TabIndex = 33;
            this.tnYes.Text = "确认";
            this.tnYes.Click += new System.EventHandler(this.tnYes_Click);
            // 
            // tbInvoiceNumber
            // 
            // 
            // 
            // 
            this.tbInvoiceNumber.Border.Class = "TextBoxBorder";
            this.tbInvoiceNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbInvoiceNumber.Location = new System.Drawing.Point(1013, 12);
            this.tbInvoiceNumber.MaxLength = 20;
            this.tbInvoiceNumber.Name = "tbInvoiceNumber";
            this.tbInvoiceNumber.PreventEnterBeep = true;
            this.tbInvoiceNumber.Size = new System.Drawing.Size(127, 23);
            this.tbInvoiceNumber.TabIndex = 31;
            this.tbInvoiceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInvoiceNumber_KeyPress);
            // 
            // tbTotalAmount
            // 
            // 
            // 
            // 
            this.tbTotalAmount.Border.Class = "TextBoxBorder";
            this.tbTotalAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTotalAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTotalAmount.Location = new System.Drawing.Point(875, 12);
            this.tbTotalAmount.Name = "tbTotalAmount";
            this.tbTotalAmount.PreventEnterBeep = true;
            this.tbTotalAmount.Size = new System.Drawing.Size(92, 23);
            this.tbTotalAmount.TabIndex = 31;
            // 
            // tbPrepareToMatchAmount
            // 
            // 
            // 
            // 
            this.tbPrepareToMatchAmount.Border.Class = "TextBoxBorder";
            this.tbPrepareToMatchAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPrepareToMatchAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPrepareToMatchAmount.Location = new System.Drawing.Point(692, 11);
            this.tbPrepareToMatchAmount.Name = "tbPrepareToMatchAmount";
            this.tbPrepareToMatchAmount.PreventEnterBeep = true;
            this.tbPrepareToMatchAmount.Size = new System.Drawing.Size(91, 23);
            this.tbPrepareToMatchAmount.TabIndex = 31;
            // 
            // tbTaxAmount
            // 
            // 
            // 
            // 
            this.tbTaxAmount.Border.Class = "TextBoxBorder";
            this.tbTaxAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaxAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTaxAmount.Location = new System.Drawing.Point(516, 11);
            this.tbTaxAmount.Name = "tbTaxAmount";
            this.tbTaxAmount.PreventEnterBeep = true;
            this.tbTaxAmount.Size = new System.Drawing.Size(77, 23);
            this.tbTaxAmount.TabIndex = 31;
            // 
            // tbTaxCode
            // 
            // 
            // 
            // 
            this.tbTaxCode.Border.Class = "TextBoxBorder";
            this.tbTaxCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaxCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTaxCode.Location = new System.Drawing.Point(343, 9);
            this.tbTaxCode.Name = "tbTaxCode";
            this.tbTaxCode.PreventEnterBeep = true;
            this.tbTaxCode.Size = new System.Drawing.Size(24, 23);
            this.tbTaxCode.TabIndex = 31;
            this.tbTaxCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTaxCode_KeyPress);
            // 
            // tbVATRate
            // 
            // 
            // 
            // 
            this.tbVATRate.Border.Class = "TextBoxBorder";
            this.tbVATRate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVATRate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbVATRate.Location = new System.Drawing.Point(434, 10);
            this.tbVATRate.Name = "tbVATRate";
            this.tbVATRate.PreventEnterBeep = true;
            this.tbVATRate.Size = new System.Drawing.Size(40, 23);
            this.tbVATRate.TabIndex = 31;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "等待处理";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.labelX11);
            this.tabControlPanel2.Controls.Add(this.labelX7);
            this.tabControlPanel2.Controls.Add(this.dtpFinish);
            this.tabControlPanel2.Controls.Add(this.dtpError);
            this.tabControlPanel2.Controls.Add(this.dtpStart);
            this.tabControlPanel2.Controls.Add(this.btnViewErrorRecord);
            this.tabControlPanel2.Controls.Add(this.btnViewRecord);
            this.tabControlPanel2.Controls.Add(this.dgvRecord);
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1339, 739);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX11.Location = new System.Drawing.Point(166, 10);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(37, 21);
            this.labelX11.TabIndex = 36;
            this.labelX11.Text = "结束";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(14, 10);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(37, 21);
            this.labelX7.TabIndex = 36;
            this.labelX7.Text = "开始";
            // 
            // dtpFinish
            // 
            this.dtpFinish.Location = new System.Drawing.Point(209, 10);
            this.dtpFinish.Name = "dtpFinish";
            this.dtpFinish.Size = new System.Drawing.Size(107, 21);
            this.dtpFinish.TabIndex = 35;
            this.dtpFinish.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dtpError
            // 
            this.dtpError.Location = new System.Drawing.Point(437, 10);
            this.dtpError.Name = "dtpError";
            this.dtpError.Size = new System.Drawing.Size(107, 21);
            this.dtpError.TabIndex = 35;
            this.dtpError.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(54, 10);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(103, 21);
            this.dtpStart.TabIndex = 35;
            // 
            // btnViewErrorRecord
            // 
            this.btnViewErrorRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewErrorRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewErrorRecord.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnViewErrorRecord.Location = new System.Drawing.Point(550, 10);
            this.btnViewErrorRecord.Name = "btnViewErrorRecord";
            this.btnViewErrorRecord.Size = new System.Drawing.Size(77, 23);
            this.btnViewErrorRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewErrorRecord.TabIndex = 34;
            this.btnViewErrorRecord.Text = "报错查看";
            this.btnViewErrorRecord.Click += new System.EventHandler(this.btnViewErrorRecord_Click);
            // 
            // btnViewRecord
            // 
            this.btnViewRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewRecord.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnViewRecord.Location = new System.Drawing.Point(331, 10);
            this.btnViewRecord.Name = "btnViewRecord";
            this.btnViewRecord.Size = new System.Drawing.Size(69, 23);
            this.btnViewRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewRecord.TabIndex = 34;
            this.btnViewRecord.Text = "查 找";
            this.btnViewRecord.Click += new System.EventHandler(this.btnViewRecord_Click);
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle64.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle64.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle64.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle64.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle64.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle64.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle64;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle65.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle65.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle65.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle65.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle65.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle65.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle65;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvRecord.Location = new System.Drawing.Point(14, 35);
            this.dgvRecord.Name = "dgvRecord";
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle66.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle66.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle66.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle66;
            this.dgvRecord.RowHeadersVisible = false;
            this.dgvRecord.RowHeadersWidth = 20;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.Size = new System.Drawing.Size(1306, 693);
            this.dgvRecord.TabIndex = 1;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "历史记录";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42))))));
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMakeAllChecked.Location = new System.Drawing.Point(1154, 12);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(49, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 33;
            this.btnMakeAllChecked.Text = "全选";
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // Check
            // 
            this.Check.Checked = true;
            this.Check.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Check.CheckValue = "N";
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 65;
            // 
            // InvoiceVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 770);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Name = "InvoiceVerify";
            this.Text = "应付核销 V1.9";
            this.Load += new System.EventHandler(this.InvoiceVerify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX tbYear;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMonth;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTaxType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVATRate;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPrepareToMatchAmount;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceNumber;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTotalAmount;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaxAmount;
        private DevComponents.DotNetBar.ButtonX tnYes;
        private System.Windows.Forms.DateTimePicker dtpError;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private DevComponents.DotNetBar.ButtonX btnViewRecord;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvRecord;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX btnViewErrorRecord;
        private System.Windows.Forms.DateTimePicker dtpFinish;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaxCode;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Check;
    }
}