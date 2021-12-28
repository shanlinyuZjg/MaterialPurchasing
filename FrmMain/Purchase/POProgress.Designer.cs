namespace Global.Purchase
{
    partial class POProgress
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
            this.tbItem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.tbVendor = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbForeignNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbDateStart = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbDateFinish = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rbItem = new System.Windows.Forms.RadioButton();
            this.rbVendor = new System.Windows.Forms.RadioButton();
            this.rbForeignNumber = new System.Windows.Forms.RadioButton();
            this.rbDate = new System.Windows.Forms.RadioButton();
            this.dgvPOItemProgress = new System.Windows.Forms.DataGridView();
            this.tbExportFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.btnExportToExcel = new DevComponents.DotNetBar.ButtonX();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItemProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // tbItem
            // 
            // 
            // 
            // 
            this.tbItem.Border.Class = "TextBoxBorder";
            this.tbItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItem.Location = new System.Drawing.Point(144, 3);
            this.tbItem.Name = "tbItem";
            this.tbItem.PreventEnterBeep = true;
            this.tbItem.Size = new System.Drawing.Size(122, 21);
            this.tbItem.TabIndex = 5;
            this.tbItem.TextChanged += new System.EventHandler(this.tbItem_TextChanged);
            this.tbItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItem_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(918, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(45, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbVendor
            // 
            // 
            // 
            // 
            this.tbVendor.Border.Class = "TextBoxBorder";
            this.tbVendor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendor.Location = new System.Drawing.Point(389, 3);
            this.tbVendor.Name = "tbVendor";
            this.tbVendor.PreventEnterBeep = true;
            this.tbVendor.Size = new System.Drawing.Size(122, 21);
            this.tbVendor.TabIndex = 7;
            this.tbVendor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendor_KeyPress);
            // 
            // tbForeignNumber
            // 
            // 
            // 
            // 
            this.tbForeignNumber.Border.Class = "TextBoxBorder";
            this.tbForeignNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbForeignNumber.Location = new System.Drawing.Point(587, 2);
            this.tbForeignNumber.Name = "tbForeignNumber";
            this.tbForeignNumber.PreventEnterBeep = true;
            this.tbForeignNumber.Size = new System.Drawing.Size(70, 21);
            this.tbForeignNumber.TabIndex = 7;
            this.tbForeignNumber.TextChanged += new System.EventHandler(this.textBoxX2_TextChanged);
            this.tbForeignNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbForeignNumber_KeyPress);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(706, 3);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(34, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "开始";
            // 
            // tbDateStart
            // 
            // 
            // 
            // 
            this.tbDateStart.Border.Class = "TextBoxBorder";
            this.tbDateStart.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDateStart.Location = new System.Drawing.Point(739, 3);
            this.tbDateStart.Name = "tbDateStart";
            this.tbDateStart.PreventEnterBeep = true;
            this.tbDateStart.Size = new System.Drawing.Size(70, 21);
            this.tbDateStart.TabIndex = 7;
            this.tbDateStart.TextChanged += new System.EventHandler(this.textBoxX2_TextChanged);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(811, 4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(29, 23);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "结束";
            // 
            // tbDateFinish
            // 
            // 
            // 
            // 
            this.tbDateFinish.Border.Class = "TextBoxBorder";
            this.tbDateFinish.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDateFinish.Location = new System.Drawing.Point(841, 3);
            this.tbDateFinish.Name = "tbDateFinish";
            this.tbDateFinish.PreventEnterBeep = true;
            this.tbDateFinish.Size = new System.Drawing.Size(70, 21);
            this.tbDateFinish.TabIndex = 7;
            this.tbDateFinish.TextChanged += new System.EventHandler(this.textBoxX2_TextChanged);
            this.tbDateFinish.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDateFinish_KeyPress);
            // 
            // rbItem
            // 
            this.rbItem.AutoSize = true;
            this.rbItem.Location = new System.Drawing.Point(4, 6);
            this.rbItem.Name = "rbItem";
            this.rbItem.Size = new System.Drawing.Size(143, 16);
            this.rbItem.TabIndex = 8;
            this.rbItem.TabStop = true;
            this.rbItem.Text = "查询内容(代码或名称)";
            this.rbItem.UseVisualStyleBackColor = true;
            this.rbItem.CheckedChanged += new System.EventHandler(this.rbItem_CheckedChanged);
            // 
            // rbVendor
            // 
            this.rbVendor.AutoSize = true;
            this.rbVendor.Location = new System.Drawing.Point(270, 6);
            this.rbVendor.Name = "rbVendor";
            this.rbVendor.Size = new System.Drawing.Size(119, 16);
            this.rbVendor.TabIndex = 8;
            this.rbVendor.TabStop = true;
            this.rbVendor.Text = "供应商代码或名称";
            this.rbVendor.UseVisualStyleBackColor = true;
            this.rbVendor.CheckedChanged += new System.EventHandler(this.rbVendor_CheckedChanged);
            // 
            // rbForeignNumber
            // 
            this.rbForeignNumber.AutoSize = true;
            this.rbForeignNumber.Location = new System.Drawing.Point(514, 6);
            this.rbForeignNumber.Name = "rbForeignNumber";
            this.rbForeignNumber.Size = new System.Drawing.Size(71, 16);
            this.rbForeignNumber.TabIndex = 8;
            this.rbForeignNumber.TabStop = true;
            this.rbForeignNumber.Text = "外贸单号";
            this.rbForeignNumber.UseVisualStyleBackColor = true;
            this.rbForeignNumber.CheckedChanged += new System.EventHandler(this.rbForeignNumber_CheckedChanged);
            // 
            // rbDate
            // 
            this.rbDate.AutoSize = true;
            this.rbDate.Location = new System.Drawing.Point(661, 5);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(47, 16);
            this.rbDate.TabIndex = 8;
            this.rbDate.TabStop = true;
            this.rbDate.Text = "日期";
            this.rbDate.UseVisualStyleBackColor = true;
            this.rbDate.CheckedChanged += new System.EventHandler(this.rbDate_CheckedChanged);
            // 
            // dgvPOItemProgress
            // 
            this.dgvPOItemProgress.AllowUserToAddRows = false;
            this.dgvPOItemProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPOItemProgress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPOItemProgress.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvPOItemProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPOItemProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPOItemProgress.Location = new System.Drawing.Point(4, 31);
            this.dgvPOItemProgress.Name = "dgvPOItemProgress";
            this.dgvPOItemProgress.RowTemplate.Height = 23;
            this.dgvPOItemProgress.Size = new System.Drawing.Size(1232, 542);
            this.dgvPOItemProgress.TabIndex = 9;
            // 
            // tbExportFilePath
            // 
            // 
            // 
            // 
            this.tbExportFilePath.Border.Class = "TextBoxBorder";
            this.tbExportFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbExportFilePath.Location = new System.Drawing.Point(1172, 2);
            this.tbExportFilePath.Name = "tbExportFilePath";
            this.tbExportFilePath.PreventEnterBeep = true;
            this.tbExportFilePath.Size = new System.Drawing.Size(71, 21);
            this.tbExportFilePath.TabIndex = 43;
            this.tbExportFilePath.Text = "D:\\PO.xlsx";
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(1139, 2);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(29, 23);
            this.labelX11.TabIndex = 42;
            this.labelX11.Text = "路径";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportToExcel.Location = new System.Drawing.Point(1096, 2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(37, 23);
            this.btnExportToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportToExcel.TabIndex = 41;
            this.btnExportToExcel.Text = "导出";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(971, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(109, 21);
            this.dateTimePicker1.TabIndex = 44;
            // 
            // POProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 585);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.tbExportFilePath);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.dgvPOItemProgress);
            this.Controls.Add(this.rbDate);
            this.Controls.Add(this.rbForeignNumber);
            this.Controls.Add(this.rbVendor);
            this.Controls.Add(this.rbItem);
            this.Controls.Add(this.tbDateFinish);
            this.Controls.Add(this.tbDateStart);
            this.Controls.Add(this.tbForeignNumber);
            this.Controls.Add(this.tbVendor);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbItem);
            this.Controls.Add(this.btnSearch);
            this.DoubleBuffered = true;
            this.Name = "POProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POProgress";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.POProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItemProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX tbItem;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVendor;
        private DevComponents.DotNetBar.Controls.TextBoxX tbForeignNumber;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbDateStart;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbDateFinish;
        private System.Windows.Forms.RadioButton rbItem;
        private System.Windows.Forms.RadioButton rbVendor;
        private System.Windows.Forms.RadioButton rbForeignNumber;
        private System.Windows.Forms.RadioButton rbDate;
        private System.Windows.Forms.DataGridView dgvPOItemProgress;
        private DevComponents.DotNetBar.Controls.TextBoxX tbExportFilePath;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX btnExportToExcel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}