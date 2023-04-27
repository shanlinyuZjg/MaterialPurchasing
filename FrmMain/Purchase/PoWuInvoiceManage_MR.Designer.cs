
namespace Global.Purchase
{
    partial class PoWuInvoiceManage_MR
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
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.TbVendorID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAll = new System.Windows.Forms.Button();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.TbVendorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbInvoiceNumberS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnDel = new System.Windows.Forms.Button();
            this.RowStart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RowEnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnAllSelect = new System.Windows.Forms.Button();
            this.TbAmount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(999, 6);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(90, 23);
            this.BtnSubmit.TabIndex = 38;
            this.BtnSubmit.Text = "有票确认";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // TbVendorID
            // 
            this.TbVendorID.Location = new System.Drawing.Point(148, 7);
            this.TbVendorID.Name = "TbVendorID";
            this.TbVendorID.Size = new System.Drawing.Size(62, 21);
            this.TbVendorID.TabIndex = 31;
            this.TbVendorID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbVendorID_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "供应商码";
            // 
            // BtnAll
            // 
            this.BtnAll.Location = new System.Drawing.Point(12, 6);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(75, 23);
            this.BtnAll.TabIndex = 29;
            this.BtnAll.Text = "全部";
            this.BtnAll.UseVisualStyleBackColor = true;
            this.BtnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // DGV2
            // 
            this.DGV2.AllowUserToAddRows = false;
            this.DGV2.AllowUserToDeleteRows = false;
            this.DGV2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.DGV2.Location = new System.Drawing.Point(357, 32);
            this.DGV2.Name = "DGV2";
            this.DGV2.ReadOnly = true;
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(834, 523);
            this.DGV2.TabIndex = 28;
            this.DGV2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV2_CellClick);
            this.DGV2.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DGV2_RowPostPaint);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 35;
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(12, 32);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(337, 523);
            this.DGV1.TabIndex = 27;
            this.DGV1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellDoubleClick);
            this.DGV1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DGV1_RowPostPaint);
            // 
            // TbVendorName
            // 
            this.TbVendorName.Location = new System.Drawing.Point(272, 7);
            this.TbVendorName.Name = "TbVendorName";
            this.TbVendorName.Size = new System.Drawing.Size(80, 21);
            this.TbVendorName.TabIndex = 40;
            this.TbVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbVendorName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "供应商名";
            // 
            // TbInvoiceNumberS
            // 
            this.TbInvoiceNumberS.Location = new System.Drawing.Point(409, 7);
            this.TbInvoiceNumberS.Name = "TbInvoiceNumberS";
            this.TbInvoiceNumberS.Size = new System.Drawing.Size(205, 21);
            this.TbInvoiceNumberS.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "发票号";
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(1105, 6);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(45, 23);
            this.BtnDel.TabIndex = 43;
            this.BtnDel.Text = "删除";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // RowStart
            // 
            this.RowStart.Location = new System.Drawing.Point(737, 7);
            this.RowStart.Name = "RowStart";
            this.RowStart.Size = new System.Drawing.Size(46, 21);
            this.RowStart.TabIndex = 47;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(694, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "起始行";
            // 
            // RowEnd
            // 
            this.RowEnd.Location = new System.Drawing.Point(834, 7);
            this.RowEnd.Name = "RowEnd";
            this.RowEnd.Size = new System.Drawing.Size(46, 21);
            this.RowEnd.TabIndex = 48;
            this.RowEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RowEnd_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(790, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "结束行";
            // 
            // BtnAllSelect
            // 
            this.BtnAllSelect.Location = new System.Drawing.Point(619, 6);
            this.BtnAllSelect.Name = "BtnAllSelect";
            this.BtnAllSelect.Size = new System.Drawing.Size(57, 23);
            this.BtnAllSelect.TabIndex = 44;
            this.BtnAllSelect.Text = "全选";
            this.BtnAllSelect.UseVisualStyleBackColor = true;
            this.BtnAllSelect.Click += new System.EventHandler(this.BtnAllSelect_Click);
            // 
            // TbAmount
            // 
            this.TbAmount.Location = new System.Drawing.Point(886, 7);
            this.TbAmount.Name = "TbAmount";
            this.TbAmount.ReadOnly = true;
            this.TbAmount.Size = new System.Drawing.Size(97, 21);
            this.TbAmount.TabIndex = 49;
            // 
            // PoWuInvoiceManage_MR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 567);
            this.Controls.Add(this.TbAmount);
            this.Controls.Add(this.RowStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RowEnd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnAllSelect);
            this.Controls.Add(this.BtnDel);
            this.Controls.Add(this.TbInvoiceNumberS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbVendorName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.TbVendorID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAll);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.DGV1);
            this.Name = "PoWuInvoiceManage_MR";
            this.Text = "无票管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PoWuInvoiceManage_MR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.TextBox TbVendorID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TextBox TbVendorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbInvoiceNumberS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.TextBox RowStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RowEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnAllSelect;
        private System.Windows.Forms.TextBox TbAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
    }
}