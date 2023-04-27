
namespace Global.Purchase
{
    partial class POInvoice_MR
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
            this.label1 = new System.Windows.Forms.Label();
            this.TbVendorNameSelect = new System.Windows.Forms.TextBox();
            this.Dgv1 = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TbVendorNameRO = new System.Windows.Forms.TextBox();
            this.Dgv2 = new System.Windows.Forms.DataGridView();
            this.BtnGet = new System.Windows.Forms.Button();
            this.TbInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbAmount = new System.Windows.Forms.TextBox();
            this.TbTax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbInvoiceAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnInvoiceConfrim = new System.Windows.Forms.Button();
            this.TbVendorNumberSelect = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.BtnInvoiceManage = new System.Windows.Forms.Button();
            this.BtnInvoiceSelect = new System.Windows.Forms.Button();
            this.RowStart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RowEnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnYOUInvoiceConfrim = new System.Windows.Forms.Button();
            this.BtnWuInvoiceManage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "供应商名";
            // 
            // TbVendorNameSelect
            // 
            this.TbVendorNameSelect.Location = new System.Drawing.Point(202, 12);
            this.TbVendorNameSelect.Name = "TbVendorNameSelect";
            this.TbVendorNameSelect.Size = new System.Drawing.Size(122, 21);
            this.TbVendorNameSelect.TabIndex = 1;
            this.TbVendorNameSelect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbVendorNameSelect_KeyPress);
            // 
            // Dgv1
            // 
            this.Dgv1.AllowUserToAddRows = false;
            this.Dgv1.AllowUserToDeleteRows = false;
            this.Dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.Dgv1.Location = new System.Drawing.Point(12, 45);
            this.Dgv1.Name = "Dgv1";
            this.Dgv1.ReadOnly = true;
            this.Dgv1.RowTemplate.Height = 23;
            this.Dgv1.Size = new System.Drawing.Size(1085, 286);
            this.Dgv1.TabIndex = 2;
            this.Dgv1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv1_CellClick);
            this.Dgv1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv1_RowPostPaint);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 35;
            // 
            // TbVendorNameRO
            // 
            this.TbVendorNameRO.Location = new System.Drawing.Point(328, 12);
            this.TbVendorNameRO.Name = "TbVendorNameRO";
            this.TbVendorNameRO.ReadOnly = true;
            this.TbVendorNameRO.Size = new System.Drawing.Size(297, 21);
            this.TbVendorNameRO.TabIndex = 3;
            // 
            // Dgv2
            // 
            this.Dgv2.AllowUserToAddRows = false;
            this.Dgv2.AllowUserToDeleteRows = false;
            this.Dgv2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv2.Location = new System.Drawing.Point(12, 367);
            this.Dgv2.Name = "Dgv2";
            this.Dgv2.ReadOnly = true;
            this.Dgv2.RowTemplate.Height = 23;
            this.Dgv2.Size = new System.Drawing.Size(1085, 285);
            this.Dgv2.TabIndex = 5;
            // 
            // BtnGet
            // 
            this.BtnGet.Location = new System.Drawing.Point(13, 339);
            this.BtnGet.Name = "BtnGet";
            this.BtnGet.Size = new System.Drawing.Size(75, 23);
            this.BtnGet.TabIndex = 6;
            this.BtnGet.Text = "获取";
            this.BtnGet.UseVisualStyleBackColor = true;
            this.BtnGet.Click += new System.EventHandler(this.BtnGet_Click);
            // 
            // TbInvoiceNumber
            // 
            this.TbInvoiceNumber.Location = new System.Drawing.Point(567, 340);
            this.TbInvoiceNumber.Name = "TbInvoiceNumber";
            this.TbInvoiceNumber.Size = new System.Drawing.Size(126, 21);
            this.TbInvoiceNumber.TabIndex = 8;
            this.TbInvoiceNumber.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(520, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "发票号";
            this.label2.Visible = false;
            // 
            // TbAmount
            // 
            this.TbAmount.Location = new System.Drawing.Point(95, 340);
            this.TbAmount.Name = "TbAmount";
            this.TbAmount.ReadOnly = true;
            this.TbAmount.Size = new System.Drawing.Size(127, 21);
            this.TbAmount.TabIndex = 9;
            // 
            // TbTax
            // 
            this.TbTax.Location = new System.Drawing.Point(733, 340);
            this.TbTax.Name = "TbTax";
            this.TbTax.Size = new System.Drawing.Size(81, 21);
            this.TbTax.TabIndex = 11;
            this.TbTax.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(697, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "税额";
            this.label3.Visible = false;
            // 
            // TbInvoiceAmount
            // 
            this.TbInvoiceAmount.Location = new System.Drawing.Point(929, 340);
            this.TbInvoiceAmount.Name = "TbInvoiceAmount";
            this.TbInvoiceAmount.Size = new System.Drawing.Size(88, 21);
            this.TbInvoiceAmount.TabIndex = 13;
            this.TbInvoiceAmount.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(821, 344);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "不含税发票总金额";
            this.label4.Visible = false;
            // 
            // BtnInvoiceConfrim
            // 
            this.BtnInvoiceConfrim.Location = new System.Drawing.Point(300, 339);
            this.BtnInvoiceConfrim.Name = "BtnInvoiceConfrim";
            this.BtnInvoiceConfrim.Size = new System.Drawing.Size(75, 23);
            this.BtnInvoiceConfrim.TabIndex = 14;
            this.BtnInvoiceConfrim.Text = "无票确认";
            this.BtnInvoiceConfrim.UseVisualStyleBackColor = true;
            this.BtnInvoiceConfrim.Click += new System.EventHandler(this.BtnInvoiceConfrim_Click);
            // 
            // TbVendorNumberSelect
            // 
            this.TbVendorNumberSelect.Location = new System.Drawing.Point(75, 12);
            this.TbVendorNumberSelect.Name = "TbVendorNumberSelect";
            this.TbVendorNumberSelect.Size = new System.Drawing.Size(66, 21);
            this.TbVendorNumberSelect.TabIndex = 16;
            this.TbVendorNumberSelect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbVendorNumberSelect_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "供应商码";
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.Location = new System.Drawing.Point(226, 339);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(54, 23);
            this.BtnExcelExport.TabIndex = 17;
            this.BtnExcelExport.Text = "导出";
            this.BtnExcelExport.UseVisualStyleBackColor = true;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(630, 12);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(57, 23);
            this.BtnSelect.TabIndex = 18;
            this.BtnSelect.Text = "全选";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnInvoiceManage
            // 
            this.BtnInvoiceManage.Location = new System.Drawing.Point(896, 12);
            this.BtnInvoiceManage.Name = "BtnInvoiceManage";
            this.BtnInvoiceManage.Size = new System.Drawing.Size(75, 23);
            this.BtnInvoiceManage.TabIndex = 20;
            this.BtnInvoiceManage.Text = "发票管理";
            this.BtnInvoiceManage.UseVisualStyleBackColor = true;
            this.BtnInvoiceManage.Click += new System.EventHandler(this.BtnInvoiceManage_Click);
            // 
            // BtnInvoiceSelect
            // 
            this.BtnInvoiceSelect.Location = new System.Drawing.Point(1022, 12);
            this.BtnInvoiceSelect.Name = "BtnInvoiceSelect";
            this.BtnInvoiceSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnInvoiceSelect.TabIndex = 21;
            this.BtnInvoiceSelect.Text = "发票查询";
            this.BtnInvoiceSelect.UseVisualStyleBackColor = true;
            this.BtnInvoiceSelect.Click += new System.EventHandler(this.BtnInvoiceSelect_Click);
            // 
            // RowStart
            // 
            this.RowStart.Location = new System.Drawing.Point(735, 13);
            this.RowStart.Name = "RowStart";
            this.RowStart.Size = new System.Drawing.Size(46, 21);
            this.RowStart.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(692, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "起始行";
            // 
            // RowEnd
            // 
            this.RowEnd.Location = new System.Drawing.Point(832, 13);
            this.RowEnd.Name = "RowEnd";
            this.RowEnd.Size = new System.Drawing.Size(46, 21);
            this.RowEnd.TabIndex = 26;
            this.RowEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RowEnd_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(788, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "结束行";
            // 
            // BtnYOUInvoiceConfrim
            // 
            this.BtnYOUInvoiceConfrim.Location = new System.Drawing.Point(1022, 339);
            this.BtnYOUInvoiceConfrim.Name = "BtnYOUInvoiceConfrim";
            this.BtnYOUInvoiceConfrim.Size = new System.Drawing.Size(75, 23);
            this.BtnYOUInvoiceConfrim.TabIndex = 27;
            this.BtnYOUInvoiceConfrim.Text = "有票确认";
            this.BtnYOUInvoiceConfrim.UseVisualStyleBackColor = true;
            this.BtnYOUInvoiceConfrim.Visible = false;
            this.BtnYOUInvoiceConfrim.Click += new System.EventHandler(this.BtnYOUInvoiceConfrim_Click);
            // 
            // BtnWuInvoiceManage
            // 
            this.BtnWuInvoiceManage.Location = new System.Drawing.Point(421, 338);
            this.BtnWuInvoiceManage.Name = "BtnWuInvoiceManage";
            this.BtnWuInvoiceManage.Size = new System.Drawing.Size(75, 23);
            this.BtnWuInvoiceManage.TabIndex = 28;
            this.BtnWuInvoiceManage.Text = "无票管理";
            this.BtnWuInvoiceManage.UseVisualStyleBackColor = true;
            this.BtnWuInvoiceManage.Click += new System.EventHandler(this.BtnWuInvoiceManage_Click);
            // 
            // POInvoice_MR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 659);
            this.Controls.Add(this.BtnWuInvoiceManage);
            this.Controls.Add(this.BtnYOUInvoiceConfrim);
            this.Controls.Add(this.RowStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RowEnd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnInvoiceSelect);
            this.Controls.Add(this.BtnInvoiceManage);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.BtnExcelExport);
            this.Controls.Add(this.TbVendorNumberSelect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnInvoiceConfrim);
            this.Controls.Add(this.TbInvoiceAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TbTax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbAmount);
            this.Controls.Add(this.TbInvoiceNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnGet);
            this.Controls.Add(this.Dgv2);
            this.Controls.Add(this.TbVendorNameRO);
            this.Controls.Add(this.Dgv1);
            this.Controls.Add(this.TbVendorNameSelect);
            this.Controls.Add(this.label1);
            this.Name = "POInvoice_MR";
            this.Text = "POInvoice_MR";
            this.Activated += new System.EventHandler(this.POInvoice_MR_Activated);
            this.Load += new System.EventHandler(this.POInvoice_MR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbVendorNameSelect;
        private System.Windows.Forms.DataGridView Dgv1;
        private System.Windows.Forms.TextBox TbVendorNameRO;
        private System.Windows.Forms.DataGridView Dgv2;
        private System.Windows.Forms.Button BtnGet;
        private System.Windows.Forms.TextBox TbInvoiceNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbAmount;
        private System.Windows.Forms.TextBox TbTax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbInvoiceAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnInvoiceConfrim;
        private System.Windows.Forms.TextBox TbVendorNumberSelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnExcelExport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Button BtnInvoiceManage;
        private System.Windows.Forms.Button BtnInvoiceSelect;
        private System.Windows.Forms.TextBox RowStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RowEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnYOUInvoiceConfrim;
        private System.Windows.Forms.Button BtnWuInvoiceManage;
    }
}