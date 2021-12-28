
namespace Global.Warehouse
{
    partial class FileEditionManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dgvFile = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.FileCheck = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.btnFileEditionAbolish = new DevComponents.DotNetBar.ButtonX();
            this.dgvGroup = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.GroupCheck = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.btnFileEditionAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbTracedFileNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbFileEdition = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbEffectiveDate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnAddGroup = new DevComponents.DotNetBar.ButtonX();
            this.btnInactivateGroup = new DevComponents.DotNetBar.ButtonX();
            this.lblgroup = new DevComponents.DotNetBar.LabelX();
            this.tbGroup = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(70, 21);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "班组信息";
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileCheck});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvFile.EnableHeadersVisualStyles = false;
            this.dgvFile.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFile.Location = new System.Drawing.Point(354, 39);
            this.dgvFile.Name = "dgvFile";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvFile.RowHeadersWidth = 20;
            this.dgvFile.RowTemplate.Height = 23;
            this.dgvFile.Size = new System.Drawing.Size(774, 555);
            this.dgvFile.TabIndex = 11;
            // 
            // FileCheck
            // 
            this.FileCheck.Checked = true;
            this.FileCheck.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.FileCheck.CheckValue = null;
            this.FileCheck.HeaderText = "选择";
            this.FileCheck.Name = "FileCheck";
            this.FileCheck.Width = 35;
            // 
            // btnFileEditionAbolish
            // 
            this.btnFileEditionAbolish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFileEditionAbolish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFileEditionAbolish.Location = new System.Drawing.Point(910, 7);
            this.btnFileEditionAbolish.Name = "btnFileEditionAbolish";
            this.btnFileEditionAbolish.Size = new System.Drawing.Size(54, 23);
            this.btnFileEditionAbolish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFileEditionAbolish.TabIndex = 10;
            this.btnFileEditionAbolish.Text = "作废";
            this.btnFileEditionAbolish.Click += new System.EventHandler(this.btnFileEditionAbolish_Click);
            // 
            // dgvGroup
            // 
            this.dgvGroup.AllowUserToAddRows = false;
            this.dgvGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvGroup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupCheck});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGroup.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvGroup.EnableHeadersVisualStyles = false;
            this.dgvGroup.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvGroup.Location = new System.Drawing.Point(12, 39);
            this.dgvGroup.Name = "dgvGroup";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroup.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvGroup.RowHeadersWidth = 20;
            this.dgvGroup.RowTemplate.Height = 23;
            this.dgvGroup.Size = new System.Drawing.Size(307, 555);
            this.dgvGroup.TabIndex = 11;
            // 
            // GroupCheck
            // 
            this.GroupCheck.Checked = true;
            this.GroupCheck.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.GroupCheck.CheckValue = null;
            this.GroupCheck.HeaderText = "选择";
            this.GroupCheck.Name = "GroupCheck";
            this.GroupCheck.Width = 35;
            // 
            // btnFileEditionAdd
            // 
            this.btnFileEditionAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFileEditionAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFileEditionAdd.Location = new System.Drawing.Point(826, 7);
            this.btnFileEditionAdd.Name = "btnFileEditionAdd";
            this.btnFileEditionAdd.Size = new System.Drawing.Size(54, 23);
            this.btnFileEditionAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFileEditionAdd.TabIndex = 10;
            this.btnFileEditionAdd.Text = "新增";
            this.btnFileEditionAdd.Click += new System.EventHandler(this.btnFileEditionAdd_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(354, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(81, 21);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "追溯文件编号";
            // 
            // tbTracedFileNumber
            // 
            // 
            // 
            // 
            this.tbTracedFileNumber.Border.Class = "TextBoxBorder";
            this.tbTracedFileNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTracedFileNumber.Location = new System.Drawing.Point(441, 10);
            this.tbTracedFileNumber.Name = "tbTracedFileNumber";
            this.tbTracedFileNumber.PreventEnterBeep = true;
            this.tbTracedFileNumber.Size = new System.Drawing.Size(125, 21);
            this.tbTracedFileNumber.TabIndex = 9;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(572, 10);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(31, 21);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "版本";
            // 
            // tbFileEdition
            // 
            // 
            // 
            // 
            this.tbFileEdition.Border.Class = "TextBoxBorder";
            this.tbFileEdition.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFileEdition.Location = new System.Drawing.Point(608, 9);
            this.tbFileEdition.Name = "tbFileEdition";
            this.tbFileEdition.PreventEnterBeep = true;
            this.tbFileEdition.Size = new System.Drawing.Size(64, 21);
            this.tbFileEdition.TabIndex = 9;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(678, 10);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(54, 21);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "生效日期";
            // 
            // tbEffectiveDate
            // 
            // 
            // 
            // 
            this.tbEffectiveDate.Border.Class = "TextBoxBorder";
            this.tbEffectiveDate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbEffectiveDate.Location = new System.Drawing.Point(738, 9);
            this.tbEffectiveDate.Name = "tbEffectiveDate";
            this.tbEffectiveDate.PreventEnterBeep = true;
            this.tbEffectiveDate.Size = new System.Drawing.Size(75, 21);
            this.tbEffectiveDate.TabIndex = 9;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddGroup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddGroup.Location = new System.Drawing.Point(219, 8);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(43, 23);
            this.btnAddGroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddGroup.TabIndex = 10;
            this.btnAddGroup.Text = "新增";
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddNewGroup_Click);
            // 
            // btnInactivateGroup
            // 
            this.btnInactivateGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInactivateGroup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInactivateGroup.Location = new System.Drawing.Point(275, 8);
            this.btnInactivateGroup.Name = "btnInactivateGroup";
            this.btnInactivateGroup.Size = new System.Drawing.Size(44, 23);
            this.btnInactivateGroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInactivateGroup.TabIndex = 10;
            this.btnInactivateGroup.Text = "作废";
            this.btnInactivateGroup.Click += new System.EventHandler(this.btnInactivateGroup_Click);
            // 
            // lblgroup
            // 
            // 
            // 
            // 
            this.lblgroup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblgroup.Location = new System.Drawing.Point(82, 10);
            this.lblgroup.Name = "lblgroup";
            this.lblgroup.Size = new System.Drawing.Size(31, 21);
            this.lblgroup.TabIndex = 8;
            this.lblgroup.Text = "名称";
            // 
            // tbGroup
            // 
            // 
            // 
            // 
            this.tbGroup.Border.Class = "TextBoxBorder";
            this.tbGroup.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGroup.Location = new System.Drawing.Point(118, 9);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.PreventEnterBeep = true;
            this.tbGroup.Size = new System.Drawing.Size(95, 21);
            this.tbGroup.TabIndex = 9;
            // 
            // FileEditionManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 628);
            this.Controls.Add(this.dgvGroup);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.btnFileEditionAbolish);
            this.Controls.Add(this.btnInactivateGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.btnFileEditionAdd);
            this.Controls.Add(this.tbEffectiveDate);
            this.Controls.Add(this.tbGroup);
            this.Controls.Add(this.tbFileEdition);
            this.Controls.Add(this.tbTracedFileNumber);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblgroup);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX2);
            this.DoubleBuffered = true;
            this.Name = "FileEditionManage";
            this.Text = "FileEditionManage";
            this.Load += new System.EventHandler(this.FileEditionManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvFile;
        private DevComponents.DotNetBar.ButtonX btnFileEditionAbolish;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvGroup;
        private DevComponents.DotNetBar.ButtonX btnFileEditionAdd;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTracedFileNumber;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFileEdition;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn FileCheck;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn GroupCheck;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbEffectiveDate;
        private DevComponents.DotNetBar.ButtonX btnAddGroup;
        private DevComponents.DotNetBar.ButtonX btnInactivateGroup;
        private DevComponents.DotNetBar.LabelX lblgroup;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGroup;
    }
}