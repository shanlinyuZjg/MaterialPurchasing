
namespace Global.Purchase
{
    partial class ManageInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbVendorName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.dgvNumber = new System.Windows.Forms.DataGridView();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbInvoiceNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbTotalAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSeveral = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbEndLineNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbStartLineNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.tbInvoiceTaxedAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbInvoiceAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbInvoiceNumber2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // tbVendorName
            // 
            // 
            // 
            // 
            this.tbVendorName.Border.Class = "TextBoxBorder";
            this.tbVendorName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendorName.DisabledBackColor = System.Drawing.Color.White;
            this.tbVendorName.Location = new System.Drawing.Point(60, 6);
            this.tbVendorName.Name = "tbVendorName";
            this.tbVendorName.PreventEnterBeep = true;
            this.tbVendorName.Size = new System.Drawing.Size(86, 21);
            this.tbVendorName.TabIndex = 0;
            this.tbVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(3, 6);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(54, 21);
            this.labelX13.TabIndex = 39;
            this.labelX13.Text = "供应商名";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(779, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(56, 23);
            this.btnConfirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfirm.TabIndex = 42;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgvNumber
            // 
            this.dgvNumber.AllowUserToAddRows = false;
            this.dgvNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNumber.Location = new System.Drawing.Point(3, 33);
            this.dgvNumber.MultiSelect = false;
            this.dgvNumber.Name = "dgvNumber";
            this.dgvNumber.RowHeadersVisible = false;
            this.dgvNumber.RowTemplate.Height = 23;
            this.dgvNumber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvNumber.Size = new System.Drawing.Size(143, 564);
            this.dgvNumber.TabIndex = 43;
            this.dgvNumber.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumber_CellClick);
            this.dgvNumber.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumber_CellContentClick);
            this.dgvNumber.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumber_CellContentDoubleClick);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(162, 59);
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(956, 538);
            this.dgvDetail.TabIndex = 44;
            this.dgvDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellContentClick);
            this.dgvDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellValueChanged);
            this.dgvDetail.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDetail_RowPostPaint);
            // 
            // Check
            // 
            this.Check.Checked = true;
            this.Check.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Check.CheckValue = null;
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 50;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(779, 33);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 42;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(235, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 21);
            this.labelX1.TabIndex = 39;
            this.labelX1.Text = "发票号";
            // 
            // tbInvoiceNumber
            // 
            // 
            // 
            // 
            this.tbInvoiceNumber.Border.Class = "TextBoxBorder";
            this.tbInvoiceNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceNumber.DisabledBackColor = System.Drawing.Color.White;
            this.tbInvoiceNumber.Location = new System.Drawing.Point(283, 4);
            this.tbInvoiceNumber.Name = "tbInvoiceNumber";
            this.tbInvoiceNumber.PreventEnterBeep = true;
            this.tbInvoiceNumber.Size = new System.Drawing.Size(101, 21);
            this.tbInvoiceNumber.TabIndex = 40;
            this.tbInvoiceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInvoiceNumber_KeyPress);
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Location = new System.Drawing.Point(162, 4);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(50, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 42;
            this.btnMakeAllChecked.Text = "全选";
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(366, 35);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 21);
            this.labelX2.TabIndex = 39;
            this.labelX2.Text = "累计金额";
            // 
            // tbTotalAmount
            // 
            // 
            // 
            // 
            this.tbTotalAmount.Border.Class = "TextBoxBorder";
            this.tbTotalAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTotalAmount.DisabledBackColor = System.Drawing.Color.White;
            this.tbTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.tbTotalAmount.Location = new System.Drawing.Point(424, 35);
            this.tbTotalAmount.Name = "tbTotalAmount";
            this.tbTotalAmount.PreventEnterBeep = true;
            this.tbTotalAmount.ReadOnly = true;
            this.tbTotalAmount.Size = new System.Drawing.Size(133, 21);
            this.tbTotalAmount.TabIndex = 40;
            this.tbTotalAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorName_KeyPress);
            // 
            // btnSeveral
            // 
            this.btnSeveral.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSeveral.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSeveral.Location = new System.Drawing.Point(701, 2);
            this.btnSeveral.Name = "btnSeveral";
            this.btnSeveral.Size = new System.Drawing.Size(56, 23);
            this.btnSeveral.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSeveral.TabIndex = 42;
            this.btnSeveral.Text = "多票确认";
            this.btnSeveral.Click += new System.EventHandler(this.btnSeveral_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.Red;
            this.labelX3.Location = new System.Drawing.Point(848, 4);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(267, 21);
            this.labelX3.TabIndex = 39;
            this.labelX3.Text = "只有一张发票或多张发票最后一张时，点击确认";
            // 
            // tbEndLineNumber
            // 
            // 
            // 
            // 
            this.tbEndLineNumber.Border.Class = "TextBoxBorder";
            this.tbEndLineNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbEndLineNumber.Location = new System.Drawing.Point(313, 33);
            this.tbEndLineNumber.Name = "tbEndLineNumber";
            this.tbEndLineNumber.PreventEnterBeep = true;
            this.tbEndLineNumber.Size = new System.Drawing.Size(47, 21);
            this.tbEndLineNumber.TabIndex = 45;
            this.tbEndLineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndLineNumber_KeyPress);
            // 
            // tbStartLineNumber
            // 
            // 
            // 
            // 
            this.tbStartLineNumber.Border.Class = "TextBoxBorder";
            this.tbStartLineNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbStartLineNumber.Location = new System.Drawing.Point(211, 33);
            this.tbStartLineNumber.Name = "tbStartLineNumber";
            this.tbStartLineNumber.PreventEnterBeep = true;
            this.tbStartLineNumber.Size = new System.Drawing.Size(47, 21);
            this.tbStartLineNumber.TabIndex = 46;
            this.tbStartLineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartLineNumber_KeyPress);
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(264, 33);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(43, 23);
            this.labelX8.TabIndex = 47;
            this.labelX8.Text = "结束行";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(162, 33);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(43, 23);
            this.labelX7.TabIndex = 48;
            this.labelX7.Text = "起始行";
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(557, 3);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(32, 23);
            this.labelX11.TabIndex = 51;
            this.labelX11.Text = "税额";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(390, 3);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(60, 23);
            this.labelX9.TabIndex = 52;
            this.labelX9.Text = "发票金额";
            // 
            // tbInvoiceTaxedAmount
            // 
            // 
            // 
            // 
            this.tbInvoiceTaxedAmount.Border.Class = "TextBoxBorder";
            this.tbInvoiceTaxedAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceTaxedAmount.Location = new System.Drawing.Point(595, 3);
            this.tbInvoiceTaxedAmount.Name = "tbInvoiceTaxedAmount";
            this.tbInvoiceTaxedAmount.PreventEnterBeep = true;
            this.tbInvoiceTaxedAmount.Size = new System.Drawing.Size(80, 21);
            this.tbInvoiceTaxedAmount.TabIndex = 49;
            // 
            // tbInvoiceAmount
            // 
            // 
            // 
            // 
            this.tbInvoiceAmount.Border.Class = "TextBoxBorder";
            this.tbInvoiceAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceAmount.Location = new System.Drawing.Point(456, 3);
            this.tbInvoiceAmount.Name = "tbInvoiceAmount";
            this.tbInvoiceAmount.PreventEnterBeep = true;
            this.tbInvoiceAmount.Size = new System.Drawing.Size(95, 21);
            this.tbInvoiceAmount.TabIndex = 50;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModify.Location = new System.Drawing.Point(1044, 32);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(56, 23);
            this.btnModify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnModify.TabIndex = 42;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(858, 35);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(79, 21);
            this.labelX4.TabIndex = 39;
            this.labelX4.Text = "修改后发票号";
            // 
            // tbInvoiceNumber2
            // 
            // 
            // 
            // 
            this.tbInvoiceNumber2.Border.Class = "TextBoxBorder";
            this.tbInvoiceNumber2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceNumber2.DisabledBackColor = System.Drawing.Color.White;
            this.tbInvoiceNumber2.Location = new System.Drawing.Point(940, 33);
            this.tbInvoiceNumber2.Name = "tbInvoiceNumber2";
            this.tbInvoiceNumber2.PreventEnterBeep = true;
            this.tbInvoiceNumber2.Size = new System.Drawing.Size(101, 21);
            this.tbInvoiceNumber2.TabIndex = 40;
            // 
            // ManageInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 654);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.tbInvoiceTaxedAmount);
            this.Controls.Add(this.tbInvoiceAmount);
            this.Controls.Add(this.tbEndLineNumber);
            this.Controls.Add(this.tbStartLineNumber);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.dgvNumber);
            this.Controls.Add(this.tbTotalAmount);
            this.Controls.Add(this.tbInvoiceNumber2);
            this.Controls.Add(this.tbInvoiceNumber);
            this.Controls.Add(this.tbVendorName);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.btnMakeAllChecked);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSeveral);
            this.Controls.Add(this.btnConfirm);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ManageInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发票到货管理";
            this.Load += new System.EventHandler(this.ManageInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbVendorName;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private System.Windows.Forms.DataGridView dgvNumber;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Check;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceNumber;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTotalAmount;
        private DevComponents.DotNetBar.ButtonX btnSeveral;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbEndLineNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX tbStartLineNumber;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceTaxedAmount;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceAmount;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceNumber2;
    }
}