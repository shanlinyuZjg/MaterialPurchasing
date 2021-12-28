
namespace Global.Warehouse
{
    partial class UserManage
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
            this.tbUserID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnAddUser = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dgvUser = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.UserCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbInternalNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.dgvGroupFile = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.FileCheck = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cbbType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.btnInActivate = new DevComponents.DotNetBar.ButtonX();
            this.btnRefreshGroupFile = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupFile)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUserID
            // 
            // 
            // 
            // 
            this.tbUserID.Border.Class = "TextBoxBorder";
            this.tbUserID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUserID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUserID.Location = new System.Drawing.Point(504, 7);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.PreventEnterBeep = true;
            this.tbUserID.Size = new System.Drawing.Size(52, 21);
            this.tbUserID.TabIndex = 19;
            // 
            // btnAddUser
            // 
            this.btnAddUser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddUser.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddUser.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAddUser.Location = new System.Drawing.Point(945, 7);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(41, 23);
            this.btnAddUser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddUser.TabIndex = 18;
            this.btnAddUser.Text = "增加";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(443, 7);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 17;
            this.labelX3.Text = "用户代码";
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserCheck});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUser.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvUser.Location = new System.Drawing.Point(448, 37);
            this.dgvUser.Name = "dgvUser";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUser.RowHeadersWidth = 20;
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUser.Size = new System.Drawing.Size(626, 581);
            this.dgvUser.TabIndex = 20;
            // 
            // UserCheck
            // 
            this.UserCheck.HeaderText = "选择";
            this.UserCheck.Name = "UserCheck";
            this.UserCheck.Width = 35;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(562, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(29, 23);
            this.labelX1.TabIndex = 17;
            this.labelX1.Text = "姓名";
            // 
            // tbUserName
            // 
            // 
            // 
            // 
            this.tbUserName.Border.Class = "TextBoxBorder";
            this.tbUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUserName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUserName.Location = new System.Drawing.Point(597, 7);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.PreventEnterBeep = true;
            this.tbUserName.Size = new System.Drawing.Size(64, 21);
            this.tbUserName.TabIndex = 19;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(674, 7);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 23);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "内部编号";
            // 
            // tbInternalNumber
            // 
            // 
            // 
            // 
            this.tbInternalNumber.Border.Class = "TextBoxBorder";
            this.tbInternalNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInternalNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbInternalNumber.Location = new System.Drawing.Point(732, 7);
            this.tbInternalNumber.Name = "tbInternalNumber";
            this.tbInternalNumber.PreventEnterBeep = true;
            this.tbInternalNumber.Size = new System.Drawing.Size(64, 21);
            this.tbInternalNumber.TabIndex = 19;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(811, 7);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(29, 23);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "类型";
            // 
            // dgvGroupFile
            // 
            this.dgvGroupFile.AllowUserToAddRows = false;
            this.dgvGroupFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvGroupFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvGroupFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroupFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGroupFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileCheck});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGroupFile.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGroupFile.EnableHeadersVisualStyles = false;
            this.dgvGroupFile.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvGroupFile.Location = new System.Drawing.Point(12, 37);
            this.dgvGroupFile.Name = "dgvGroupFile";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroupFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGroupFile.RowHeadersWidth = 20;
            this.dgvGroupFile.RowTemplate.Height = 23;
            this.dgvGroupFile.Size = new System.Drawing.Size(430, 582);
            this.dgvGroupFile.TabIndex = 25;
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
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(12, 7);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(158, 23);
            this.labelX7.TabIndex = 17;
            this.labelX7.Text = "班组及追溯文件编号、版本";
            this.labelX7.Click += new System.EventHandler(this.labelX7_Click);
            // 
            // cbbType
            // 
            this.cbbType.DisplayMember = "Text";
            this.cbbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbType.FormattingEnabled = true;
            this.cbbType.ItemHeight = 15;
            this.cbbType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cbbType.Location = new System.Drawing.Point(853, 7);
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(63, 21);
            this.cbbType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbType.TabIndex = 26;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "P|包材";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "M|原料";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "All|全部";
            // 
            // btnInActivate
            // 
            this.btnInActivate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInActivate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInActivate.Font = new System.Drawing.Font("宋体", 9F);
            this.btnInActivate.Location = new System.Drawing.Point(1033, 7);
            this.btnInActivate.Name = "btnInActivate";
            this.btnInActivate.Size = new System.Drawing.Size(41, 23);
            this.btnInActivate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInActivate.TabIndex = 18;
            this.btnInActivate.Text = "删除";
            this.btnInActivate.Click += new System.EventHandler(this.btnInActivate_Click);
            // 
            // btnRefreshGroupFile
            // 
            this.btnRefreshGroupFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefreshGroupFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefreshGroupFile.Font = new System.Drawing.Font("宋体", 9F);
            this.btnRefreshGroupFile.Location = new System.Drawing.Point(176, 7);
            this.btnRefreshGroupFile.Name = "btnRefreshGroupFile";
            this.btnRefreshGroupFile.Size = new System.Drawing.Size(41, 23);
            this.btnRefreshGroupFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefreshGroupFile.TabIndex = 18;
            this.btnRefreshGroupFile.Text = "刷新";
            this.btnRefreshGroupFile.Click += new System.EventHandler(this.btnRefreshGroupFile_Click);
            // 
            // UserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 650);
            this.Controls.Add(this.cbbType);
            this.Controls.Add(this.dgvGroupFile);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.tbInternalNumber);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbUserID);
            this.Controls.Add(this.btnInActivate);
            this.Controls.Add(this.btnRefreshGroupFile);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX3);
            this.DoubleBuffered = true;
            this.Name = "UserManage";
            this.Text = "UserManage";
            this.Load += new System.EventHandler(this.UserManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbUserID;
        private DevComponents.DotNetBar.ButtonX btnAddUser;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvUser;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbUserName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInternalNumber;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvGroupFile;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbType;
        private DevComponents.DotNetBar.ButtonX btnInActivate;
        private DevComponents.DotNetBar.ButtonX btnRefreshGroupFile;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UserCheck;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn FileCheck;
    }
}