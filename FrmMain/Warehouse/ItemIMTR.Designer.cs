namespace Global.Warehouse
{
    partial class ItemIMTR
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
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbItemNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnIMTR = new DevComponents.DotNetBar.ButtonX();
            this.btnError = new DevComponents.DotNetBar.ButtonX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeALlChecked = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(12, 44);
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1208, 529);
            this.dgvDetail.TabIndex = 8;
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 41;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 17;
            this.labelX1.Text = "物料代码";
            // 
            // tbItemNumber
            // 
            // 
            // 
            // 
            this.tbItemNumber.Border.Class = "TextBoxBorder";
            this.tbItemNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemNumber.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tbItemNumber.Location = new System.Drawing.Point(73, 12);
            this.tbItemNumber.Name = "tbItemNumber";
            this.tbItemNumber.PreventEnterBeep = true;
            this.tbItemNumber.Size = new System.Drawing.Size(84, 23);
            this.tbItemNumber.TabIndex = 18;
            this.tbItemNumber.TextChanged += new System.EventHandler(this.tbItemNumber_TextChanged);
            this.tbItemNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemNumber_KeyPress);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.ForeColor = System.Drawing.Color.DeepPink;
            this.labelX2.Location = new System.Drawing.Point(242, 15);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(184, 23);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "注意：此处只能由I移为O状态";
            // 
            // btnIMTR
            // 
            this.btnIMTR.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIMTR.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnIMTR.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnIMTR.Location = new System.Drawing.Point(492, 15);
            this.btnIMTR.Name = "btnIMTR";
            this.btnIMTR.Size = new System.Drawing.Size(50, 23);
            this.btnIMTR.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnIMTR.TabIndex = 19;
            this.btnIMTR.Text = "移库";
            this.btnIMTR.Click += new System.EventHandler(this.btnIMTR_Click);
            // 
            // btnError
            // 
            this.btnError.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnError.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnError.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnError.Location = new System.Drawing.Point(568, 15);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(50, 23);
            this.btnError.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnError.TabIndex = 19;
            this.btnError.Text = "报错";
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnView.Location = new System.Drawing.Point(163, 12);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(50, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 19;
            this.btnView.Text = "查询";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnMakeALlChecked
            // 
            this.btnMakeALlChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeALlChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeALlChecked.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnMakeALlChecked.Location = new System.Drawing.Point(416, 15);
            this.btnMakeALlChecked.Name = "btnMakeALlChecked";
            this.btnMakeALlChecked.Size = new System.Drawing.Size(50, 23);
            this.btnMakeALlChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeALlChecked.TabIndex = 19;
            this.btnMakeALlChecked.Text = "全选";
            this.btnMakeALlChecked.Click += new System.EventHandler(this.btnMakeALlChecked_Click);
            // 
            // ItemIMTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 585);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnMakeALlChecked);
            this.Controls.Add(this.btnIMTR);
            this.Controls.Add(this.tbItemNumber);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.Name = "ItemIMTR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "移库";
            this.Load += new System.EventHandler(this.ItemIMTR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemNumber;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnIMTR;
        private DevComponents.DotNetBar.ButtonX btnError;
        private DevComponents.DotNetBar.ButtonX btnView;
        private DevComponents.DotNetBar.ButtonX btnMakeALlChecked;
    }
}