namespace Global.Warehouse
{
    partial class ItemIMTRByOrder
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
            this.btnImtr = new DevComponents.DotNetBar.ButtonX();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnError = new DevComponents.DotNetBar.ButtonX();
            this.tbItemNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtpFS = new System.Windows.Forms.DateTimePicker();
            this.btnViewAppointedDate = new DevComponents.DotNetBar.ButtonX();
            this.btnViewByItemNumber = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImtr
            // 
            this.btnImtr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImtr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImtr.Location = new System.Drawing.Point(734, 5);
            this.btnImtr.Name = "btnImtr";
            this.btnImtr.Size = new System.Drawing.Size(50, 23);
            this.btnImtr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImtr.TabIndex = 21;
            this.btnImtr.Text = "移库";
            this.btnImtr.Click += new System.EventHandler(this.btnImtr_Click);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(2, 34);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1136, 614);
            this.dgvDetail.TabIndex = 20;
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 60;
            // 
            // btnError
            // 
            this.btnError.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnError.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnError.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnError.Location = new System.Drawing.Point(800, 5);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(50, 23);
            this.btnError.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnError.TabIndex = 25;
            this.btnError.Text = "报错";
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // tbItemNumber
            // 
            // 
            // 
            // 
            this.tbItemNumber.Border.Class = "TextBoxBorder";
            this.tbItemNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbItemNumber.Location = new System.Drawing.Point(75, 5);
            this.tbItemNumber.Name = "tbItemNumber";
            this.tbItemNumber.PreventEnterBeep = true;
            this.tbItemNumber.Size = new System.Drawing.Size(84, 21);
            this.tbItemNumber.TabIndex = 24;
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
            this.labelX2.Location = new System.Drawing.Point(456, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(170, 23);
            this.labelX2.TabIndex = 22;
            this.labelX2.Text = "注意：此处只能由I移为O状态";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(2, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 21);
            this.labelX1.TabIndex = 23;
            this.labelX1.Text = "物料代码";
            // 
            // dtpFS
            // 
            this.dtpFS.Location = new System.Drawing.Point(245, 5);
            this.dtpFS.Name = "dtpFS";
            this.dtpFS.Size = new System.Drawing.Size(125, 21);
            this.dtpFS.TabIndex = 27;
            // 
            // btnViewAppointedDate
            // 
            this.btnViewAppointedDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewAppointedDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewAppointedDate.Location = new System.Drawing.Point(386, 5);
            this.btnViewAppointedDate.Name = "btnViewAppointedDate";
            this.btnViewAppointedDate.Size = new System.Drawing.Size(50, 23);
            this.btnViewAppointedDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewAppointedDate.TabIndex = 26;
            this.btnViewAppointedDate.Text = "查找";
            this.btnViewAppointedDate.Click += new System.EventHandler(this.btnViewAppointedDate_Click);
            // 
            // btnViewByItemNumber
            // 
            this.btnViewByItemNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewByItemNumber.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewByItemNumber.Location = new System.Drawing.Point(174, 5);
            this.btnViewByItemNumber.Name = "btnViewByItemNumber";
            this.btnViewByItemNumber.Size = new System.Drawing.Size(50, 23);
            this.btnViewByItemNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewByItemNumber.TabIndex = 26;
            this.btnViewByItemNumber.Text = "查找";
            this.btnViewByItemNumber.Click += new System.EventHandler(this.btnViewByItemNumber_Click);
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Location = new System.Drawing.Point(666, 5);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(50, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 21;
            this.btnMakeAllChecked.Text = "全选";
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // ItemIMTRByOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 660);
            this.Controls.Add(this.dtpFS);
            this.Controls.Add(this.btnViewByItemNumber);
            this.Controls.Add(this.btnViewAppointedDate);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.tbItemNumber);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnMakeAllChecked);
            this.Controls.Add(this.btnImtr);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ItemIMTRByOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "订单移动库位";
            this.Load += new System.EventHandler(this.ItemIMTRByOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnImtr;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.ButtonX btnError;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemNumber;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.DateTimePicker dtpFS;
        private DevComponents.DotNetBar.ButtonX btnViewAppointedDate;
        private DevComponents.DotNetBar.ButtonX btnViewByItemNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
    }
}