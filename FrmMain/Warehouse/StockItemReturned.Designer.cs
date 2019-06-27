namespace Global.Warehouse
{
    partial class StockItemReturned
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
            this.btnRIMakeAllUnchecked = new DevComponents.DotNetBar.ButtonX();
            this.btnRIMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.dgvItemReturnedUnHandledRecod = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.CheckItemReturned = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvItemReturnedRecord = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnItemReturnedFSOperate = new DevComponents.DotNetBar.ButtonX();
            this.btnItemReturnedRefresh = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.tbItemReturnedItemNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.btnSearchRecord = new DevComponents.DotNetBar.ButtonX();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.btnDateSearchRecord = new DevComponents.DotNetBar.ButtonX();
            this.dtpFinish = new System.Windows.Forms.DateTimePicker();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemReturnedUnHandledRecod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemReturnedRecord)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRIMakeAllUnchecked
            // 
            this.btnRIMakeAllUnchecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRIMakeAllUnchecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRIMakeAllUnchecked.Location = new System.Drawing.Point(98, 11);
            this.btnRIMakeAllUnchecked.Name = "btnRIMakeAllUnchecked";
            this.btnRIMakeAllUnchecked.Size = new System.Drawing.Size(61, 23);
            this.btnRIMakeAllUnchecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRIMakeAllUnchecked.TabIndex = 23;
            this.btnRIMakeAllUnchecked.Text = "全部取消";
            this.btnRIMakeAllUnchecked.Click += new System.EventHandler(this.btnRIMakeAllUnchecked_Click);
            // 
            // btnRIMakeAllChecked
            // 
            this.btnRIMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRIMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRIMakeAllChecked.Location = new System.Drawing.Point(12, 11);
            this.btnRIMakeAllChecked.Name = "btnRIMakeAllChecked";
            this.btnRIMakeAllChecked.Size = new System.Drawing.Size(61, 23);
            this.btnRIMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRIMakeAllChecked.TabIndex = 24;
            this.btnRIMakeAllChecked.Text = "全部选中";
            this.btnRIMakeAllChecked.Click += new System.EventHandler(this.btnRIMakeAllChecked_Click);
            // 
            // dgvItemReturnedUnHandledRecod
            // 
            this.dgvItemReturnedUnHandledRecod.AllowUserToAddRows = false;
            this.dgvItemReturnedUnHandledRecod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemReturnedUnHandledRecod.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemReturnedUnHandledRecod.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemReturnedUnHandledRecod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemReturnedUnHandledRecod.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckItemReturned});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemReturnedUnHandledRecod.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemReturnedUnHandledRecod.EnableHeadersVisualStyles = false;
            this.dgvItemReturnedUnHandledRecod.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvItemReturnedUnHandledRecod.Location = new System.Drawing.Point(12, 38);
            this.dgvItemReturnedUnHandledRecod.Name = "dgvItemReturnedUnHandledRecod";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemReturnedUnHandledRecod.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItemReturnedUnHandledRecod.RowTemplate.Height = 23;
            this.dgvItemReturnedUnHandledRecod.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemReturnedUnHandledRecod.Size = new System.Drawing.Size(1231, 304);
            this.dgvItemReturnedUnHandledRecod.TabIndex = 21;
            // 
            // CheckItemReturned
            // 
            this.CheckItemReturned.HeaderText = "选择";
            this.CheckItemReturned.Name = "CheckItemReturned";
            this.CheckItemReturned.Width = 35;
            // 
            // dgvItemReturnedRecord
            // 
            this.dgvItemReturnedRecord.AllowUserToAddRows = false;
            this.dgvItemReturnedRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemReturnedRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemReturnedRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItemReturnedRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemReturnedRecord.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemReturnedRecord.EnableHeadersVisualStyles = false;
            this.dgvItemReturnedRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvItemReturnedRecord.Location = new System.Drawing.Point(12, 442);
            this.dgvItemReturnedRecord.Name = "dgvItemReturnedRecord";
            this.dgvItemReturnedRecord.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemReturnedRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemReturnedRecord.RowTemplate.Height = 23;
            this.dgvItemReturnedRecord.Size = new System.Drawing.Size(1231, 272);
            this.dgvItemReturnedRecord.TabIndex = 22;
            // 
            // btnItemReturnedFSOperate
            // 
            this.btnItemReturnedFSOperate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemReturnedFSOperate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnItemReturnedFSOperate.Location = new System.Drawing.Point(270, 11);
            this.btnItemReturnedFSOperate.Name = "btnItemReturnedFSOperate";
            this.btnItemReturnedFSOperate.Size = new System.Drawing.Size(61, 23);
            this.btnItemReturnedFSOperate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnItemReturnedFSOperate.TabIndex = 16;
            this.btnItemReturnedFSOperate.Text = "四班操作";
            this.btnItemReturnedFSOperate.Click += new System.EventHandler(this.btnItemReturnedFSOperate_Click);
            // 
            // btnItemReturnedRefresh
            // 
            this.btnItemReturnedRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemReturnedRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnItemReturnedRefresh.Location = new System.Drawing.Point(184, 11);
            this.btnItemReturnedRefresh.Name = "btnItemReturnedRefresh";
            this.btnItemReturnedRefresh.Size = new System.Drawing.Size(61, 23);
            this.btnItemReturnedRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnItemReturnedRefresh.TabIndex = 17;
            this.btnItemReturnedRefresh.Text = "刷新";
            this.btnItemReturnedRefresh.Click += new System.EventHandler(this.btnItemReturnedRefresh_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.tbItemReturnedItemNumber);
            this.groupPanel1.Controls.Add(this.labelX16);
            this.groupPanel1.Controls.Add(this.btnSearchRecord);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(13, 352);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(310, 64);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.DashDot;
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
            this.groupPanel1.TabIndex = 20;
            this.groupPanel1.Text = "按照物料代码查找记录";
            // 
            // tbItemReturnedItemNumber
            // 
            // 
            // 
            // 
            this.tbItemReturnedItemNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemReturnedItemNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbItemReturnedItemNumber.Location = new System.Drawing.Point(83, 7);
            this.tbItemReturnedItemNumber.Name = "tbItemReturnedItemNumber";
            this.tbItemReturnedItemNumber.PreventEnterBeep = true;
            this.tbItemReturnedItemNumber.Size = new System.Drawing.Size(137, 17);
            this.tbItemReturnedItemNumber.TabIndex = 6;
            // 
            // labelX16
            // 
            this.labelX16.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(11, 5);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(66, 23);
            this.labelX16.TabIndex = 4;
            this.labelX16.Text = "物料代码";
            // 
            // btnSearchRecord
            // 
            this.btnSearchRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearchRecord.Location = new System.Drawing.Point(242, 4);
            this.btnSearchRecord.Name = "btnSearchRecord";
            this.btnSearchRecord.Size = new System.Drawing.Size(56, 23);
            this.btnSearchRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearchRecord.TabIndex = 5;
            this.btnSearchRecord.Text = "查找";
            this.btnSearchRecord.Click += new System.EventHandler(this.btnSearchRecord_Click);
            // 
            // labelX19
            // 
            this.labelX19.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX19.Location = new System.Drawing.Point(13, 422);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new System.Drawing.Size(143, 23);
            this.labelX19.TabIndex = 18;
            this.labelX19.Text = "默认显示最近20条记录";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.groupPanel2.Controls.Add(this.dtpStart);
            this.groupPanel2.Controls.Add(this.labelX17);
            this.groupPanel2.Controls.Add(this.btnDateSearchRecord);
            this.groupPanel2.Controls.Add(this.dtpFinish);
            this.groupPanel2.Controls.Add(this.labelX18);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Location = new System.Drawing.Point(338, 352);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(508, 64);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.DashDot;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.DashDot;
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
            this.groupPanel2.TabIndex = 19;
            this.groupPanel2.Text = "查找起止日期内记录";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(64, 8);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(107, 21);
            this.dtpStart.TabIndex = 7;
            // 
            // labelX17
            // 
            this.labelX17.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(4, 7);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(55, 23);
            this.labelX17.TabIndex = 4;
            this.labelX17.Text = "开始日期";
            // 
            // btnDateSearchRecord
            // 
            this.btnDateSearchRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDateSearchRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDateSearchRecord.Location = new System.Drawing.Point(427, 7);
            this.btnDateSearchRecord.Name = "btnDateSearchRecord";
            this.btnDateSearchRecord.Size = new System.Drawing.Size(56, 23);
            this.btnDateSearchRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDateSearchRecord.TabIndex = 5;
            this.btnDateSearchRecord.Text = "查找";
            this.btnDateSearchRecord.Click += new System.EventHandler(this.btnDateSearchRecord_Click);
            // 
            // dtpFinish
            // 
            this.dtpFinish.Location = new System.Drawing.Point(287, 7);
            this.dtpFinish.Name = "dtpFinish";
            this.dtpFinish.Size = new System.Drawing.Size(107, 21);
            this.dtpFinish.TabIndex = 7;
            // 
            // labelX18
            // 
            this.labelX18.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(224, 7);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(59, 23);
            this.labelX18.TabIndex = 4;
            this.labelX18.Text = "结束日期";
            // 
            // StockItemReturned
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 726);
            this.Controls.Add(this.btnRIMakeAllUnchecked);
            this.Controls.Add(this.btnRIMakeAllChecked);
            this.Controls.Add(this.dgvItemReturnedUnHandledRecod);
            this.Controls.Add(this.dgvItemReturnedRecord);
            this.Controls.Add(this.btnItemReturnedFSOperate);
            this.Controls.Add(this.btnItemReturnedRefresh);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.labelX19);
            this.Controls.Add(this.groupPanel2);
            this.DoubleBuffered = true;
            this.Name = "StockItemReturned";
            this.Text = "StockItemReturned";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemReturnedUnHandledRecod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemReturnedRecord)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnRIMakeAllUnchecked;
        private DevComponents.DotNetBar.ButtonX btnRIMakeAllChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemReturnedUnHandledRecod;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckItemReturned;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemReturnedRecord;
        private DevComponents.DotNetBar.ButtonX btnItemReturnedFSOperate;
        private DevComponents.DotNetBar.ButtonX btnItemReturnedRefresh;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemReturnedItemNumber;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.ButtonX btnSearchRecord;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.ButtonX btnDateSearchRecord;
        private System.Windows.Forms.DateTimePicker dtpFinish;
        private DevComponents.DotNetBar.LabelX labelX18;
    }
}