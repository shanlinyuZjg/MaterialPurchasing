namespace Global.Purchase
{
    partial class WorkArrangement
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
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbFinished = new System.Windows.Forms.RadioButton();
            this.rbUnFinished = new System.Windows.Forms.RadioButton();
            this.dgvTask = new System.Windows.Forms.DataGridView();
            this.tbTaskSubject = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.rtbTaskDetail = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.rtbFinishedTaskComment = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Location = new System.Drawing.Point(861, 401);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "完成提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(348, 18);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 4;
            this.btnView.Text = "查看";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Controls.Add(this.rbFinished);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.rbUnFinished);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1149, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(250, 22);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(71, 16);
            this.rbAll.TabIndex = 0;
            this.rbAll.Text = "显示全部";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbFinished
            // 
            this.rbFinished.AutoSize = true;
            this.rbFinished.Location = new System.Drawing.Point(128, 22);
            this.rbFinished.Name = "rbFinished";
            this.rbFinished.Size = new System.Drawing.Size(59, 16);
            this.rbFinished.TabIndex = 0;
            this.rbFinished.Text = "已完成";
            this.rbFinished.UseVisualStyleBackColor = true;
            // 
            // rbUnFinished
            // 
            this.rbUnFinished.AutoSize = true;
            this.rbUnFinished.Checked = true;
            this.rbUnFinished.Location = new System.Drawing.Point(6, 22);
            this.rbUnFinished.Name = "rbUnFinished";
            this.rbUnFinished.Size = new System.Drawing.Size(59, 16);
            this.rbUnFinished.TabIndex = 0;
            this.rbUnFinished.TabStop = true;
            this.rbUnFinished.Text = "未完成";
            this.rbUnFinished.UseVisualStyleBackColor = true;
            // 
            // dgvTask
            // 
            this.dgvTask.AllowUserToAddRows = false;
            this.dgvTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Location = new System.Drawing.Point(12, 61);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.ReadOnly = true;
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.Size = new System.Drawing.Size(576, 572);
            this.dgvTask.TabIndex = 8;
            this.dgvTask.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellClick);
            this.dgvTask.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellContentClick);
            this.dgvTask.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellContentDoubleClick);
            this.dgvTask.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTask_CellDoubleClick);
            // 
            // tbTaskSubject
            // 
            // 
            // 
            // 
            this.tbTaskSubject.Border.Class = "TextBoxBorder";
            this.tbTaskSubject.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaskSubject.Location = new System.Drawing.Point(652, 61);
            this.tbTaskSubject.Name = "tbTaskSubject";
            this.tbTaskSubject.PreventEnterBeep = true;
            this.tbTaskSubject.Size = new System.Drawing.Size(507, 21);
            this.tbTaskSubject.TabIndex = 9;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(594, 61);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "任务主题";
            // 
            // rtbTaskDetail
            // 
            // 
            // 
            // 
            this.rtbTaskDetail.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtbTaskDetail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtbTaskDetail.Location = new System.Drawing.Point(652, 101);
            this.rtbTaskDetail.Name = "rtbTaskDetail";
            this.rtbTaskDetail.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
    "134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs18\\par\r\n}\r\n";
            this.rtbTaskDetail.Size = new System.Drawing.Size(507, 159);
            this.rtbTaskDetail.TabIndex = 12;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(594, 133);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 11;
            this.labelX5.Text = "详细内容";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(594, 298);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 26);
            this.labelX1.TabIndex = 11;
            this.labelX1.Text = "完成情况";
            // 
            // rtbFinishedTaskComment
            // 
            // 
            // 
            // 
            this.rtbFinishedTaskComment.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtbFinishedTaskComment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtbFinishedTaskComment.Location = new System.Drawing.Point(652, 266);
            this.rtbFinishedTaskComment.Name = "rtbFinishedTaskComment";
            this.rtbFinishedTaskComment.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
    "134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs18\\par\r\n}\r\n";
            this.rtbFinishedTaskComment.Size = new System.Drawing.Size(507, 129);
            this.rtbFinishedTaskComment.TabIndex = 12;
            // 
            // WorkArrangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 638);
            this.Controls.Add(this.rtbFinishedTaskComment);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.rtbTaskDetail);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.tbTaskSubject);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "WorkArrangement";
            this.Text = "WorkArrangement";
            this.Load += new System.EventHandler(this.WorkArrangement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ButtonX btnView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbFinished;
        private System.Windows.Forms.RadioButton rbUnFinished;
        private System.Windows.Forms.DataGridView dgvTask;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaskSubject;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtbTaskDetail;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtbFinishedTaskComment;
    }
}