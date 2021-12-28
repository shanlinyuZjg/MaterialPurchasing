
namespace Global.Purchase
{
    partial class DeptItemRequirement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.dgvItemRequirement = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbVendorName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbbVendorNumber = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbPricePreTax = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.BtTableExport = new DevComponents.DotNetBar.ButtonX();
            this.BtAllSelect = new DevComponents.DotNetBar.ButtonX();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.dgvItemRequirement1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewCheckBoxXColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.btnRefresh1 = new DevComponents.DotNetBar.ButtonX();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnMonth = new System.Windows.Forms.RadioButton();
            this.rbtnDay = new System.Windows.Forms.RadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(11, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefreshPO_Click);
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(406, 10);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(55, 23);
            this.labelX10.TabIndex = 10;
            this.labelX10.Text = "供应商码";
            // 
            // dgvItemRequirement
            // 
            this.dgvItemRequirement.AllowUserToAddRows = false;
            this.dgvItemRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemRequirement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItemRequirement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemRequirement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItemRequirement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemRequirement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemRequirement.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItemRequirement.EnableHeadersVisualStyles = false;
            this.dgvItemRequirement.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvItemRequirement.Location = new System.Drawing.Point(16, 43);
            this.dgvItemRequirement.Name = "dgvItemRequirement";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemRequirement.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvItemRequirement.RowHeadersWidth = 20;
            this.dgvItemRequirement.RowTemplate.Height = 23;
            this.dgvItemRequirement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemRequirement.Size = new System.Drawing.Size(1145, 612);
            this.dgvItemRequirement.TabIndex = 12;
            // 
            // Check
            // 
            this.Check.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Check.Checked = true;
            this.Check.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Check.CheckValue = "N";
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 54;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(1084, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(48, 23);
            this.btnConfirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfirm.TabIndex = 13;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(170, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(123, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "供应商名(模糊查询)";
            // 
            // tbVendorName
            // 
            // 
            // 
            // 
            this.tbVendorName.Border.Class = "TextBoxBorder";
            this.tbVendorName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendorName.Location = new System.Drawing.Point(299, 10);
            this.tbVendorName.Name = "tbVendorName";
            this.tbVendorName.PreventEnterBeep = true;
            this.tbVendorName.Size = new System.Drawing.Size(94, 21);
            this.tbVendorName.TabIndex = 11;
            this.tbVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // cbbVendorNumber
            // 
            this.cbbVendorNumber.DisplayMember = "Text";
            this.cbbVendorNumber.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbVendorNumber.FormattingEnabled = true;
            this.cbbVendorNumber.ItemHeight = 15;
            this.cbbVendorNumber.Location = new System.Drawing.Point(467, 10);
            this.cbbVendorNumber.Name = "cbbVendorNumber";
            this.cbbVendorNumber.Size = new System.Drawing.Size(198, 21);
            this.cbbVendorNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbVendorNumber.TabIndex = 14;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(680, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 23);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "含税单价";
            // 
            // tbPricePreTax
            // 
            // 
            // 
            // 
            this.tbPricePreTax.Border.Class = "TextBoxBorder";
            this.tbPricePreTax.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPricePreTax.Location = new System.Drawing.Point(742, 10);
            this.tbPricePreTax.Name = "tbPricePreTax";
            this.tbPricePreTax.PreventEnterBeep = true;
            this.tbPricePreTax.Size = new System.Drawing.Size(94, 21);
            this.tbPricePreTax.TabIndex = 11;
            this.tbPricePreTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(849, 10);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "备注信息";
            // 
            // tbRemark
            // 
            // 
            // 
            // 
            this.tbRemark.Border.Class = "TextBoxBorder";
            this.tbRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbRemark.Location = new System.Drawing.Point(911, 10);
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.PreventEnterBeep = true;
            this.tbRemark.Size = new System.Drawing.Size(167, 21);
            this.tbRemark.TabIndex = 11;
            this.tbRemark.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1173, 693);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.BtTableExport);
            this.tabControlPanel1.Controls.Add(this.BtAllSelect);
            this.tabControlPanel1.Controls.Add(this.dgvItemRequirement);
            this.tabControlPanel1.Controls.Add(this.cbbVendorNumber);
            this.tabControlPanel1.Controls.Add(this.labelX10);
            this.tabControlPanel1.Controls.Add(this.btnConfirm);
            this.tabControlPanel1.Controls.Add(this.labelX1);
            this.tabControlPanel1.Controls.Add(this.btnRefresh);
            this.tabControlPanel1.Controls.Add(this.labelX2);
            this.tabControlPanel1.Controls.Add(this.tbRemark);
            this.tabControlPanel1.Controls.Add(this.labelX3);
            this.tabControlPanel1.Controls.Add(this.tbPricePreTax);
            this.tabControlPanel1.Controls.Add(this.tbVendorName);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1173, 667);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // BtTableExport
            // 
            this.BtTableExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtTableExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtTableExport.Location = new System.Drawing.Point(115, 10);
            this.BtTableExport.Name = "BtTableExport";
            this.BtTableExport.Size = new System.Drawing.Size(45, 23);
            this.BtTableExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtTableExport.TabIndex = 16;
            this.BtTableExport.Text = "导出";
            this.BtTableExport.Click += new System.EventHandler(this.BtTableExport_Click);
            // 
            // BtAllSelect
            // 
            this.BtAllSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtAllSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtAllSelect.Location = new System.Drawing.Point(62, 10);
            this.BtAllSelect.Name = "BtAllSelect";
            this.BtAllSelect.Size = new System.Drawing.Size(45, 23);
            this.BtAllSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtAllSelect.TabIndex = 15;
            this.BtAllSelect.Text = "全选";
            this.BtAllSelect.Click += new System.EventHandler(this.BtAllSelect_Click);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "待处理";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.groupBox1);
            this.tabControlPanel3.Controls.Add(this.dtpDate);
            this.tabControlPanel3.Controls.Add(this.labelX4);
            this.tabControlPanel3.Controls.Add(this.dgvItemRequirement1);
            this.tabControlPanel3.Controls.Add(this.btnRefresh1);
            this.tabControlPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(1173, 667);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 9;
            this.tabControlPanel3.TabItem = this.tabItem3;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(517, 13);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(153, 23);
            this.labelX4.TabIndex = 16;
            this.labelX4.Text = "状态：1-处理，2-到货";
            // 
            // dgvItemRequirement1
            // 
            this.dgvItemRequirement1.AllowUserToAddRows = false;
            this.dgvItemRequirement1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemRequirement1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItemRequirement1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemRequirement1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvItemRequirement1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemRequirement1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxXColumn1});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemRequirement1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvItemRequirement1.EnableHeadersVisualStyles = false;
            this.dgvItemRequirement1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvItemRequirement1.Location = new System.Drawing.Point(16, 44);
            this.dgvItemRequirement1.Name = "dgvItemRequirement1";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemRequirement1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvItemRequirement1.RowHeadersWidth = 20;
            this.dgvItemRequirement1.RowTemplate.Height = 23;
            this.dgvItemRequirement1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemRequirement1.Size = new System.Drawing.Size(1145, 612);
            this.dgvItemRequirement1.TabIndex = 14;
            // 
            // dataGridViewCheckBoxXColumn1
            // 
            this.dataGridViewCheckBoxXColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewCheckBoxXColumn1.Checked = true;
            this.dataGridViewCheckBoxXColumn1.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.dataGridViewCheckBoxXColumn1.CheckValue = "N";
            this.dataGridViewCheckBoxXColumn1.HeaderText = "选择";
            this.dataGridViewCheckBoxXColumn1.Name = "dataGridViewCheckBoxXColumn1";
            this.dataGridViewCheckBoxXColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxXColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxXColumn1.Width = 54;
            // 
            // btnRefresh1
            // 
            this.btnRefresh1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh1.Location = new System.Drawing.Point(345, 11);
            this.btnRefresh1.Name = "btnRefresh1";
            this.btnRefresh1.Size = new System.Drawing.Size(104, 23);
            this.btnRefresh1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh1.TabIndex = 15;
            this.btnRefresh1.Text = "查找";
            this.btnRefresh1.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel3;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "已处理";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnMonth);
            this.groupBox1.Controls.Add(this.rbtnDay);
            this.groupBox1.Location = new System.Drawing.Point(196, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 35);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日期范围";
            // 
            // rbtnMonth
            // 
            this.rbtnMonth.AutoSize = true;
            this.rbtnMonth.Location = new System.Drawing.Point(76, 15);
            this.rbtnMonth.Name = "rbtnMonth";
            this.rbtnMonth.Size = new System.Drawing.Size(47, 16);
            this.rbtnMonth.TabIndex = 11;
            this.rbtnMonth.Text = "按月";
            this.rbtnMonth.UseVisualStyleBackColor = true;
            // 
            // rbtnDay
            // 
            this.rbtnDay.AutoSize = true;
            this.rbtnDay.Checked = true;
            this.rbtnDay.Location = new System.Drawing.Point(16, 15);
            this.rbtnDay.Name = "rbtnDay";
            this.rbtnDay.Size = new System.Drawing.Size(47, 16);
            this.rbtnDay.TabIndex = 11;
            this.rbtnDay.TabStop = true;
            this.rbtnDay.Text = "按日";
            this.rbtnDay.UseVisualStyleBackColor = true;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(73, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(108, 21);
            this.dtpDate.TabIndex = 18;
            // 
            // DeptItemRequirement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 693);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Name = "DeptItemRequirement";
            this.Text = "DeptItemRequirement";
            this.Load += new System.EventHandler(this.DeptItemRequirement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemRequirement;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVendorName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbVendorNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPricePreTax;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbRemark;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.DotNetBar.ButtonX BtTableExport;
        private DevComponents.DotNetBar.ButtonX BtAllSelect;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Check;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemRequirement1;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn dataGridViewCheckBoxXColumn1;
        private DevComponents.DotNetBar.ButtonX btnRefresh1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnMonth;
        private System.Windows.Forms.RadioButton rbtnDay;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}