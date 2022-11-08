
namespace Global.Warehouse
{
    partial class ManageBatchRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbFileNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbItemDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnUpdate = new DevComponents.DotNetBar.ButtonX();
            this.btnChangeApply = new DevComponents.DotNetBar.ButtonX();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnOccupyFileNumber = new DevComponents.DotNetBar.ButtonX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnMonth = new System.Windows.Forms.RadioButton();
            this.rbtnDay = new System.Windows.Forms.RadioButton();
            this.btnBatchPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllChecked = new DevComponents.DotNetBar.ButtonX();
            this.btnPreview = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdateRecord = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RevisionReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(5, 41);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(1301, 596);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellClick);
            this.dgvDetail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellDoubleClick);
            this.dgvDetail.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDetail_CellMouseUp);
            // 
            // Check
            // 
            this.Check.Checked = true;
            this.Check.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Check.CheckValue = null;
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 35;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(5, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 24);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "流水号";
            // 
            // tbFileNumber
            // 
            // 
            // 
            // 
            this.tbFileNumber.Border.Class = "TextBoxBorder";
            this.tbFileNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFileNumber.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tbFileNumber.Location = new System.Drawing.Point(49, 9);
            this.tbFileNumber.Name = "tbFileNumber";
            this.tbFileNumber.PreventEnterBeep = true;
            this.tbFileNumber.Size = new System.Drawing.Size(110, 21);
            this.tbFileNumber.TabIndex = 3;
            this.tbFileNumber.Text = "输入后按回车键";
            this.tbFileNumber.Click += new System.EventHandler(this.tbFileNumber_Click);
            this.tbFileNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFileNumber_KeyPress);
            // 
            // tbItemDesc
            // 
            // 
            // 
            // 
            this.tbItemDesc.Border.Class = "TextBoxBorder";
            this.tbItemDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemDesc.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tbItemDesc.Location = new System.Drawing.Point(224, 9);
            this.tbItemDesc.Name = "tbItemDesc";
            this.tbItemDesc.PreventEnterBeep = true;
            this.tbItemDesc.Size = new System.Drawing.Size(140, 21);
            this.tbItemDesc.TabIndex = 3;
            this.tbItemDesc.Text = "输入后按回车键";
            this.tbItemDesc.Click += new System.EventHandler(this.tbItemDesc_Click);
            this.tbItemDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemDesc_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(165, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(54, 24);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "物料描述";
            // 
            // btnUpdate
            // 
            this.btnUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdate.Location = new System.Drawing.Point(1138, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(48, 23);
            this.btnUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnChangeApply
            // 
            this.btnChangeApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangeApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChangeApply.Location = new System.Drawing.Point(1245, 8);
            this.btnChangeApply.Name = "btnChangeApply";
            this.btnChangeApply.Size = new System.Drawing.Size(66, 23);
            this.btnChangeApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChangeApply.TabIndex = 1;
            this.btnChangeApply.Text = "变动申请";
            this.btnChangeApply.Visible = false;
            this.btnChangeApply.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(372, 9);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(108, 21);
            this.dtpDate.TabIndex = 8;
            // 
            // btnOccupyFileNumber
            // 
            this.btnOccupyFileNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOccupyFileNumber.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOccupyFileNumber.Location = new System.Drawing.Point(1192, 9);
            this.btnOccupyFileNumber.Name = "btnOccupyFileNumber";
            this.btnOccupyFileNumber.Size = new System.Drawing.Size(47, 23);
            this.btnOccupyFileNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOccupyFileNumber.TabIndex = 1;
            this.btnOccupyFileNumber.Text = "占号";
            this.btnOccupyFileNumber.Visible = false;
            this.btnOccupyFileNumber.Click += new System.EventHandler(this.btnOccupyFileNumber_Click);
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(620, 10);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(48, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 1;
            this.btnView.Text = "查找";
            this.btnView.Click += new System.EventHandler(this.btnView_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnMonth);
            this.groupBox1.Controls.Add(this.rbtnDay);
            this.groupBox1.Location = new System.Drawing.Point(483, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 35);
            this.groupBox1.TabIndex = 10;
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
            // btnBatchPrint
            // 
            this.btnBatchPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBatchPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBatchPrint.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBatchPrint.Location = new System.Drawing.Point(796, 10);
            this.btnBatchPrint.Name = "btnBatchPrint";
            this.btnBatchPrint.Size = new System.Drawing.Size(84, 23);
            this.btnBatchPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBatchPrint.TabIndex = 7;
            this.btnBatchPrint.Text = "打印(可多选)";
            this.btnBatchPrint.Click += new System.EventHandler(this.btnBatchPrint_Click);
            // 
            // btnMakeAllChecked
            // 
            this.btnMakeAllChecked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllChecked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllChecked.Location = new System.Drawing.Point(676, 10);
            this.btnMakeAllChecked.Name = "btnMakeAllChecked";
            this.btnMakeAllChecked.Size = new System.Drawing.Size(48, 23);
            this.btnMakeAllChecked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllChecked.TabIndex = 1;
            this.btnMakeAllChecked.Text = "全选";
            this.btnMakeAllChecked.Click += new System.EventHandler(this.btnMakeAllChecked_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPreview.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPreview.Location = new System.Drawing.Point(737, 10);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(48, 23);
            this.btnPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnUpdateRecord
            // 
            this.btnUpdateRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdateRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdateRecord.Location = new System.Drawing.Point(1063, 9);
            this.btnUpdateRecord.Name = "btnUpdateRecord";
            this.btnUpdateRecord.Size = new System.Drawing.Size(69, 23);
            this.btnUpdateRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdateRecord.TabIndex = 11;
            this.btnUpdateRecord.Text = "修改历史";
            this.btnUpdateRecord.Click += new System.EventHandler(this.btnUpdateRecord_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click);
            // 
            // RevisionReason
            // 
            // 
            // 
            // 
            this.RevisionReason.Border.Class = "TextBoxBorder";
            this.RevisionReason.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RevisionReason.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.RevisionReason.Location = new System.Drawing.Point(886, 11);
            this.RevisionReason.Name = "RevisionReason";
            this.RevisionReason.PreventEnterBeep = true;
            this.RevisionReason.Size = new System.Drawing.Size(171, 21);
            this.RevisionReason.TabIndex = 12;
            this.RevisionReason.Text = "请输入修订原因";
            this.RevisionReason.Click += new System.EventHandler(this.RevisionReason_Click);
            // 
            // ManageBatchRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 642);
            this.Controls.Add(this.RevisionReason);
            this.Controls.Add(this.btnUpdateRecord);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnBatchPrint);
            this.Controls.Add(this.tbItemDesc);
            this.Controls.Add(this.tbFileNumber);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnMakeAllChecked);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnOccupyFileNumber);
            this.Controls.Add(this.btnChangeApply);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ManageBatchRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批记录管理";
            this.Load += new System.EventHandler(this.ManageBatchRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFileNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItemDesc;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Check;
        private DevComponents.DotNetBar.ButtonX btnUpdate;
        private DevComponents.DotNetBar.ButtonX btnChangeApply;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private DevComponents.DotNetBar.ButtonX btnOccupyFileNumber;
        private DevComponents.DotNetBar.ButtonX btnView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnDay;
        private System.Windows.Forms.RadioButton rbtnMonth;
        private DevComponents.DotNetBar.ButtonX btnBatchPrint;
        private DevComponents.DotNetBar.ButtonX btnMakeAllChecked;
        private DevComponents.DotNetBar.ButtonX btnPreview;
        private DevComponents.DotNetBar.ButtonX btnUpdateRecord;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.TextBoxX RevisionReason;
    }
}