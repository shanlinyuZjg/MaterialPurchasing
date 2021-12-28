namespace Global.Purchase
{
    partial class SuperisorWorkArrangement
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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnAssignTask = new DevComponents.DotNetBar.ButtonX();
            this.tbTaskSubject = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dgvAllTask = new System.Windows.Forms.DataGridView();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.btnUnfinished = new DevComponents.DotNetBar.ButtonX();
            this.btnViewFinished = new DevComponents.DotNetBar.ButtonX();
            this.btnViewAll = new DevComponents.DotNetBar.ButtonX();
            this.rtbTaskDetail = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.cbbStaff = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpFinishDate = new System.Windows.Forms.DateTimePicker();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(276, 15);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(105, 21);
            this.dtpStartDate.TabIndex = 0;
            // 
            // btnAssignTask
            // 
            this.btnAssignTask.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAssignTask.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAssignTask.Location = new System.Drawing.Point(498, 87);
            this.btnAssignTask.Name = "btnAssignTask";
            this.btnAssignTask.Size = new System.Drawing.Size(75, 23);
            this.btnAssignTask.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAssignTask.TabIndex = 1;
            this.btnAssignTask.Text = "下达任务";
            this.btnAssignTask.Click += new System.EventHandler(this.btnAssignTask_Click);
            // 
            // tbTaskSubject
            // 
            // 
            // 
            // 
            this.tbTaskSubject.Border.Class = "TextBoxBorder";
            this.tbTaskSubject.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaskSubject.Location = new System.Drawing.Point(66, 49);
            this.tbTaskSubject.Name = "tbTaskSubject";
            this.tbTaskSubject.PreventEnterBeep = true;
            this.tbTaskSubject.Size = new System.Drawing.Size(507, 21);
            this.tbTaskSubject.TabIndex = 2;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(5, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "选择员工";
            // 
            // dgvAllTask
            // 
            this.dgvAllTask.AllowUserToAddRows = false;
            this.dgvAllTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAllTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllTask.Location = new System.Drawing.Point(8, 116);
            this.dgvAllTask.Name = "dgvAllTask";
            this.dgvAllTask.ReadOnly = true;
            this.dgvAllTask.RowTemplate.Height = 23;
            this.dgvAllTask.Size = new System.Drawing.Size(1140, 493);
            this.dgvAllTask.TabIndex = 4;
            // 
            // superTabControl1
            // 
            this.superTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Location = new System.Drawing.Point(4, 12);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1160, 646);
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 5;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem2});
            this.superTabControl1.Text = "superTabControl1";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.btnUnfinished);
            this.superTabControlPanel2.Controls.Add(this.btnViewFinished);
            this.superTabControlPanel2.Controls.Add(this.btnViewAll);
            this.superTabControlPanel2.Controls.Add(this.rtbTaskDetail);
            this.superTabControlPanel2.Controls.Add(this.cbbStaff);
            this.superTabControlPanel2.Controls.Add(this.dgvAllTask);
            this.superTabControlPanel2.Controls.Add(this.dtpFinishDate);
            this.superTabControlPanel2.Controls.Add(this.dtpStartDate);
            this.superTabControlPanel2.Controls.Add(this.btnAssignTask);
            this.superTabControlPanel2.Controls.Add(this.tbTaskSubject);
            this.superTabControlPanel2.Controls.Add(this.labelX5);
            this.superTabControlPanel2.Controls.Add(this.labelX3);
            this.superTabControlPanel2.Controls.Add(this.labelX4);
            this.superTabControlPanel2.Controls.Add(this.labelX2);
            this.superTabControlPanel2.Controls.Add(this.labelX1);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(1160, 618);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // btnUnfinished
            // 
            this.btnUnfinished.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnfinished.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnfinished.Location = new System.Drawing.Point(274, 87);
            this.btnUnfinished.Name = "btnUnfinished";
            this.btnUnfinished.Size = new System.Drawing.Size(75, 23);
            this.btnUnfinished.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnfinished.TabIndex = 6;
            this.btnUnfinished.Text = "查看未完成";
            this.btnUnfinished.Click += new System.EventHandler(this.btnUnfinished_Click);
            // 
            // btnViewFinished
            // 
            this.btnViewFinished.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewFinished.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewFinished.Location = new System.Drawing.Point(152, 87);
            this.btnViewFinished.Name = "btnViewFinished";
            this.btnViewFinished.Size = new System.Drawing.Size(75, 23);
            this.btnViewFinished.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewFinished.TabIndex = 6;
            this.btnViewFinished.Text = "查看已完成";
            this.btnViewFinished.Click += new System.EventHandler(this.btnViewFinished_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewAll.Location = new System.Drawing.Point(36, 87);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(75, 23);
            this.btnViewAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnViewAll.TabIndex = 6;
            this.btnViewAll.Text = "查看所有";
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // rtbTaskDetail
            // 
            // 
            // 
            // 
            this.rtbTaskDetail.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtbTaskDetail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtbTaskDetail.Location = new System.Drawing.Point(654, 13);
            this.rtbTaskDetail.Name = "rtbTaskDetail";
            this.rtbTaskDetail.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
    "134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs18\\par\r\n}\r\n";
            this.rtbTaskDetail.Size = new System.Drawing.Size(494, 97);
            this.rtbTaskDetail.TabIndex = 5;
            // 
            // cbbStaff
            // 
            this.cbbStaff.DisplayMember = "Text";
            this.cbbStaff.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbStaff.FormattingEnabled = true;
            this.cbbStaff.ItemHeight = 15;
            this.cbbStaff.Location = new System.Drawing.Point(66, 15);
            this.cbbStaff.Name = "cbbStaff";
            this.cbbStaff.Size = new System.Drawing.Size(121, 21);
            this.cbbStaff.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbStaff.TabIndex = 1;
            // 
            // dtpFinishDate
            // 
            this.dtpFinishDate.Location = new System.Drawing.Point(468, 16);
            this.dtpFinishDate.Name = "dtpFinishDate";
            this.dtpFinishDate.Size = new System.Drawing.Size(105, 21);
            this.dtpFinishDate.TabIndex = 0;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(593, 45);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "详细内容";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(8, 49);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "任务主题";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(407, 16);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(55, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "截止日期";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(215, 15);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "开始日期";
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "工作安排";
            // 
            // SuperisorWorkArrangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 659);
            this.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            this.Name = "SuperisorWorkArrangement";
            this.Text = "SuperisorWorkArrangement";
            this.Load += new System.EventHandler(this.SuperisorWorkArrangement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private DevComponents.DotNetBar.ButtonX btnAssignTask;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaskSubject;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.DataGridView dgvAllTask;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbStaff;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private System.Windows.Forms.DateTimePicker dtpFinishDate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtbTaskDetail;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnUnfinished;
        private DevComponents.DotNetBar.ButtonX btnViewFinished;
        private DevComponents.DotNetBar.ButtonX btnViewAll;
    }
}