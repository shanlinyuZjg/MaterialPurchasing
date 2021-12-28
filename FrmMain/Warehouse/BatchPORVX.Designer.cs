namespace Global.Warehouse
{
    partial class BatchPORVX
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbPONumberOut = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnDoit = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbLineNumberOut = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbPONumberIn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbLineNumberIn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.tbStatus = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.tbPromisedDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPONumberOut
            // 
            // 
            // 
            // 
            this.tbPONumberOut.Border.Class = "TextBoxBorder";
            this.tbPONumberOut.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPONumberOut.Location = new System.Drawing.Point(111, 7);
            this.tbPONumberOut.Name = "tbPONumberOut";
            this.tbPONumberOut.PreventEnterBeep = true;
            this.tbPONumberOut.Size = new System.Drawing.Size(122, 21);
            this.tbPONumberOut.TabIndex = 14;
            this.tbPONumberOut.TextChanged += new System.EventHandler(this.tbPONumberOut_TextChanged);
            this.tbPONumberOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumberOut_KeyPress);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(12, 36);
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(1233, 531);
            this.dgvDetail.TabIndex = 13;
            this.dgvDetail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellDoubleClick);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.Width = 41;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 23);
            this.labelX1.TabIndex = 12;
            this.labelX1.Text = "退出的采购单号";
            // 
            // btnDoit
            // 
            this.btnDoit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDoit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDoit.Location = new System.Drawing.Point(1006, 7);
            this.btnDoit.Name = "btnDoit";
            this.btnDoit.Size = new System.Drawing.Size(55, 23);
            this.btnDoit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDoit.TabIndex = 11;
            this.btnDoit.Text = "操作";
            this.btnDoit.Click += new System.EventHandler(this.btnDoit_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(239, 7);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(31, 23);
            this.labelX2.TabIndex = 12;
            this.labelX2.Text = "行号";
            // 
            // tbLineNumberOut
            // 
            // 
            // 
            // 
            this.tbLineNumberOut.Border.Class = "TextBoxBorder";
            this.tbLineNumberOut.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLineNumberOut.Location = new System.Drawing.Point(276, 7);
            this.tbLineNumberOut.Name = "tbLineNumberOut";
            this.tbLineNumberOut.PreventEnterBeep = true;
            this.tbLineNumberOut.Size = new System.Drawing.Size(46, 21);
            this.tbLineNumberOut.TabIndex = 14;
            this.tbLineNumberOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLineNumberOut_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(426, 7);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(94, 23);
            this.labelX3.TabIndex = 12;
            this.labelX3.Text = "领入的采购单号";
            // 
            // tbPONumberIn
            // 
            // 
            // 
            // 
            this.tbPONumberIn.Border.Class = "TextBoxBorder";
            this.tbPONumberIn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPONumberIn.Location = new System.Drawing.Point(526, 7);
            this.tbPONumberIn.Name = "tbPONumberIn";
            this.tbPONumberIn.PreventEnterBeep = true;
            this.tbPONumberIn.Size = new System.Drawing.Size(122, 21);
            this.tbPONumberIn.TabIndex = 14;
            this.tbPONumberIn.TextChanged += new System.EventHandler(this.tbPONumberIn_TextChanged);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(664, 7);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(31, 23);
            this.labelX4.TabIndex = 12;
            this.labelX4.Text = "行号";
            // 
            // tbLineNumberIn
            // 
            // 
            // 
            // 
            this.tbLineNumberIn.Border.Class = "TextBoxBorder";
            this.tbLineNumberIn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLineNumberIn.Location = new System.Drawing.Point(701, 7);
            this.tbLineNumberIn.Name = "tbLineNumberIn";
            this.tbLineNumberIn.PreventEnterBeep = true;
            this.tbLineNumberIn.Size = new System.Drawing.Size(46, 21);
            this.tbLineNumberIn.TabIndex = 14;
            this.tbLineNumberIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLineNumberIn_KeyPress);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(328, 7);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(31, 23);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "状态";
            // 
            // tbStatus
            // 
            // 
            // 
            // 
            this.tbStatus.Border.Class = "TextBoxBorder";
            this.tbStatus.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbStatus.Location = new System.Drawing.Point(365, 7);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.PreventEnterBeep = true;
            this.tbStatus.Size = new System.Drawing.Size(30, 21);
            this.tbStatus.TabIndex = 14;
            this.tbStatus.Text = "I";
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(764, 7);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(61, 23);
            this.labelX13.TabIndex = 12;
            this.labelX13.Text = "交货日期";
            // 
            // tbPromisedDate
            // 
            // 
            // 
            // 
            this.tbPromisedDate.Border.Class = "TextBoxBorder";
            this.tbPromisedDate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPromisedDate.Location = new System.Drawing.Point(831, 7);
            this.tbPromisedDate.Name = "tbPromisedDate";
            this.tbPromisedDate.PreventEnterBeep = true;
            this.tbPromisedDate.Size = new System.Drawing.Size(92, 21);
            this.tbPromisedDate.TabIndex = 14;
            // 
            // BatchPORVX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 579);
            this.Controls.Add(this.tbPONumberIn);
            this.Controls.Add(this.tbPromisedDate);
            this.Controls.Add(this.tbLineNumberIn);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.tbLineNumberOut);
            this.Controls.Add(this.tbPONumberOut);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnDoit);
            this.DoubleBuffered = true;
            this.Name = "BatchPORVX";
            this.Text = "批量调整订单";
            this.Load += new System.EventHandler(this.BatchPORVX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbPONumberOut;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnDoit;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLineNumberOut;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPONumberIn;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLineNumberIn;
        private DevComponents.DotNetBar.Controls.TextBoxX tbStatus;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPromisedDate;
        private DevComponents.DotNetBar.LabelX labelX13;
    }
}