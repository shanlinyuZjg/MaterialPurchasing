﻿namespace Global.Purchase
{
    partial class VendorEmailSetting
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
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbVendorNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbVendorName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnUpdate = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbEmail = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvVendorEmail = new System.Windows.Forms.DataGridView();
            this.EmaiCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.GBemailsend = new System.Windows.Forms.GroupBox();
            this.EmailSend = new System.Windows.Forms.Button();
            this.CKleader = new System.Windows.Forms.CheckBox();
            this.CBme = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorEmail)).BeginInit();
            this.GBemailsend.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(325, 39);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 23);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "增加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(1, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "供应商码";
            // 
            // tbVendorNumber
            // 
            // 
            // 
            // 
            this.tbVendorNumber.Border.Class = "TextBoxBorder";
            this.tbVendorNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendorNumber.Location = new System.Drawing.Point(57, 12);
            this.tbVendorNumber.Name = "tbVendorNumber";
            this.tbVendorNumber.PreventEnterBeep = true;
            this.tbVendorNumber.Size = new System.Drawing.Size(100, 21);
            this.tbVendorNumber.TabIndex = 0;
            this.tbVendorNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVendorNumber_KeyPress);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(178, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(30, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "名称";
            // 
            // tbVendorName
            // 
            // 
            // 
            // 
            this.tbVendorName.Border.Class = "TextBoxBorder";
            this.tbVendorName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVendorName.Location = new System.Drawing.Point(214, 12);
            this.tbVendorName.Name = "tbVendorName";
            this.tbVendorName.PreventEnterBeep = true;
            this.tbVendorName.Size = new System.Drawing.Size(173, 21);
            this.tbVendorName.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdate.Location = new System.Drawing.Point(398, 39);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "删除";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(1, 42);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "电子邮箱";
            // 
            // tbEmail
            // 
            // 
            // 
            // 
            this.tbEmail.Border.Class = "TextBoxBorder";
            this.tbEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbEmail.Location = new System.Drawing.Point(57, 41);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.PreventEnterBeep = true;
            this.tbEmail.Size = new System.Drawing.Size(151, 21);
            this.tbEmail.TabIndex = 2;
            // 
            // dgvVendorEmail
            // 
            this.dgvVendorEmail.AllowUserToAddRows = false;
            this.dgvVendorEmail.AllowUserToDeleteRows = false;
            this.dgvVendorEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVendorEmail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvVendorEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVendorEmail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmaiCheck});
            this.dgvVendorEmail.Location = new System.Drawing.Point(1, 71);
            this.dgvVendorEmail.Name = "dgvVendorEmail";
            this.dgvVendorEmail.ReadOnly = true;
            this.dgvVendorEmail.RowHeadersVisible = false;
            this.dgvVendorEmail.RowTemplate.Height = 23;
            this.dgvVendorEmail.Size = new System.Drawing.Size(997, 508);
            this.dgvVendorEmail.TabIndex = 3;
            this.dgvVendorEmail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVendorEmail_CellClick);
            // 
            // EmaiCheck
            // 
            this.EmaiCheck.HeaderText = "选择  ";
            this.EmaiCheck.Name = "EmaiCheck";
            this.EmaiCheck.ReadOnly = true;
            this.EmaiCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EmaiCheck.Width = 42;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(398, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // GBemailsend
            // 
            this.GBemailsend.Controls.Add(this.label1);
            this.GBemailsend.Controls.Add(this.EmailSend);
            this.GBemailsend.Controls.Add(this.CKleader);
            this.GBemailsend.Controls.Add(this.CBme);
            this.GBemailsend.Location = new System.Drawing.Point(493, 2);
            this.GBemailsend.Name = "GBemailsend";
            this.GBemailsend.Size = new System.Drawing.Size(505, 63);
            this.GBemailsend.TabIndex = 4;
            this.GBemailsend.TabStop = false;
            this.GBemailsend.Visible = false;
            // 
            // EmailSend
            // 
            this.EmailSend.Location = new System.Drawing.Point(135, 18);
            this.EmailSend.Name = "EmailSend";
            this.EmailSend.Size = new System.Drawing.Size(75, 23);
            this.EmailSend.TabIndex = 2;
            this.EmailSend.Text = "发送邮件";
            this.EmailSend.UseVisualStyleBackColor = true;
            this.EmailSend.Click += new System.EventHandler(this.EmailSend_Click);
            // 
            // CKleader
            // 
            this.CKleader.AutoSize = true;
            this.CKleader.Location = new System.Drawing.Point(70, 21);
            this.CKleader.Name = "CKleader";
            this.CKleader.Size = new System.Drawing.Size(48, 16);
            this.CKleader.TabIndex = 1;
            this.CKleader.Text = "领导";
            this.CKleader.UseVisualStyleBackColor = true;
            // 
            // CBme
            // 
            this.CBme.AutoSize = true;
            this.CBme.Checked = true;
            this.CBme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBme.Location = new System.Drawing.Point(10, 21);
            this.CBme.Name = "CBme";
            this.CBme.Size = new System.Drawing.Size(48, 16);
            this.CBme.TabIndex = 0;
            this.CBme.Text = "自己";
            this.CBme.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "请确保Excel附件已导出，且不是打开状态";
            // 
            // VendorEmailSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 582);
            this.Controls.Add(this.GBemailsend);
            this.Controls.Add(this.dgvVendorEmail);
            this.Controls.Add(this.tbVendorName);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.tbVendorNumber);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VendorEmailSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商邮箱设置";
            this.Load += new System.EventHandler(this.VendorEmailSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorEmail)).EndInit();
            this.GBemailsend.ResumeLayout(false);
            this.GBemailsend.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVendorNumber;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVendorName;
        private DevComponents.DotNetBar.ButtonX btnUpdate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbEmail;
        private System.Windows.Forms.DataGridView dgvVendorEmail;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.GroupBox GBemailsend;
        private System.Windows.Forms.Button EmailSend;
        private System.Windows.Forms.CheckBox CKleader;
        private System.Windows.Forms.CheckBox CBme;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EmaiCheck;
        private System.Windows.Forms.Label label1;
    }
}