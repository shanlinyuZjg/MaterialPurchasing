
namespace Global.Audit
{
    partial class InvoiceAuditMR
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
            this.BtnConfrim = new System.Windows.Forms.Button();
            this.TbInvoiceSelect = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAll = new System.Windows.Forms.Button();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.TBstorageAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbInvoiceNumberS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnReturn = new System.Windows.Forms.Button();
            this.TbVendorID = new System.Windows.Forms.TextBox();
            this.TbVendorName = new System.Windows.Forms.TextBox();
            this.BtnAuditHistory = new System.Windows.Forms.Button();
            this.BtnExportExel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnConfrim
            // 
            this.BtnConfrim.Location = new System.Drawing.Point(1097, 48);
            this.BtnConfrim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnConfrim.Name = "BtnConfrim";
            this.BtnConfrim.Size = new System.Drawing.Size(135, 31);
            this.BtnConfrim.TabIndex = 38;
            this.BtnConfrim.Text = "确认";
            this.BtnConfrim.UseVisualStyleBackColor = true;
            this.BtnConfrim.Click += new System.EventHandler(this.BtnConfrim_Click);
            // 
            // TbInvoiceSelect
            // 
            this.TbInvoiceSelect.Location = new System.Drawing.Point(221, 49);
            this.TbInvoiceSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceSelect.Name = "TbInvoiceSelect";
            this.TbInvoiceSelect.Size = new System.Drawing.Size(170, 26);
            this.TbInvoiceSelect.TabIndex = 31;
            this.TbInvoiceSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbInvoiceSelect_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "发票号查找";
            // 
            // BtnAll
            // 
            this.BtnAll.Location = new System.Drawing.Point(24, 48);
            this.BtnAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(100, 31);
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
            this.DGV2.Location = new System.Drawing.Point(433, 87);
            this.DGV2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV2.Name = "DGV2";
            this.DGV2.ReadOnly = true;
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(795, 453);
            this.DGV2.TabIndex = 28;
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(4, 87);
            this.DGV1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(421, 453);
            this.DGV1.TabIndex = 27;
            this.DGV1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellDoubleClick);
            // 
            // TBstorageAmount
            // 
            this.TBstorageAmount.Location = new System.Drawing.Point(750, 12);
            this.TBstorageAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBstorageAmount.Name = "TBstorageAmount";
            this.TBstorageAmount.ReadOnly = true;
            this.TBstorageAmount.Size = new System.Drawing.Size(200, 26);
            this.TBstorageAmount.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(650, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 41;
            this.label6.Text = "入库总金额";
            // 
            // TbInvoiceNumberS
            // 
            this.TbInvoiceNumberS.Location = new System.Drawing.Point(460, 49);
            this.TbInvoiceNumberS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceNumberS.Name = "TbInvoiceNumberS";
            this.TbInvoiceNumberS.ReadOnly = true;
            this.TbInvoiceNumberS.Size = new System.Drawing.Size(504, 26);
            this.TbInvoiceNumberS.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "发票号";
            // 
            // BtnReturn
            // 
            this.BtnReturn.Location = new System.Drawing.Point(1102, 10);
            this.BtnReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnReturn.Name = "BtnReturn";
            this.BtnReturn.Size = new System.Drawing.Size(100, 31);
            this.BtnReturn.TabIndex = 43;
            this.BtnReturn.Text = "退回";
            this.BtnReturn.UseVisualStyleBackColor = true;
            this.BtnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // TbVendorID
            // 
            this.TbVendorID.Location = new System.Drawing.Point(33, 12);
            this.TbVendorID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbVendorID.Name = "TbVendorID";
            this.TbVendorID.ReadOnly = true;
            this.TbVendorID.Size = new System.Drawing.Size(196, 26);
            this.TbVendorID.TabIndex = 44;
            // 
            // TbVendorName
            // 
            this.TbVendorName.Location = new System.Drawing.Point(238, 12);
            this.TbVendorName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbVendorName.Name = "TbVendorName";
            this.TbVendorName.ReadOnly = true;
            this.TbVendorName.Size = new System.Drawing.Size(404, 26);
            this.TbVendorName.TabIndex = 45;
            // 
            // BtnAuditHistory
            // 
            this.BtnAuditHistory.Location = new System.Drawing.Point(967, 8);
            this.BtnAuditHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAuditHistory.Name = "BtnAuditHistory";
            this.BtnAuditHistory.Size = new System.Drawing.Size(127, 31);
            this.BtnAuditHistory.TabIndex = 46;
            this.BtnAuditHistory.Text = "审计历史";
            this.BtnAuditHistory.UseVisualStyleBackColor = true;
            this.BtnAuditHistory.Click += new System.EventHandler(this.BtnAuditHistory_Click);
            // 
            // BtnExportExel
            // 
            this.BtnExportExel.Location = new System.Drawing.Point(972, 47);
            this.BtnExportExel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExportExel.Name = "BtnExportExel";
            this.BtnExportExel.Size = new System.Drawing.Size(100, 31);
            this.BtnExportExel.TabIndex = 47;
            this.BtnExportExel.Text = "导出";
            this.BtnExportExel.UseVisualStyleBackColor = true;
            this.BtnExportExel.Click += new System.EventHandler(this.BtnExportExel_Click);
            // 
            // InvoiceAuditMR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 576);
            this.Controls.Add(this.BtnExportExel);
            this.Controls.Add(this.BtnAuditHistory);
            this.Controls.Add(this.TbVendorName);
            this.Controls.Add(this.TbVendorID);
            this.Controls.Add(this.BtnReturn);
            this.Controls.Add(this.TBstorageAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TbInvoiceNumberS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnConfrim);
            this.Controls.Add(this.TbInvoiceSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAll);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.DGV1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "InvoiceAuditMR";
            this.Text = "InvoiceAuditMR";
            this.Load += new System.EventHandler(this.InvoiceAuditMR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnConfrim;
        private System.Windows.Forms.TextBox TbInvoiceSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TextBox TBstorageAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbInvoiceNumberS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnReturn;
        private System.Windows.Forms.TextBox TbVendorID;
        private System.Windows.Forms.TextBox TbVendorName;
        private System.Windows.Forms.Button BtnAuditHistory;
        private System.Windows.Forms.Button BtnExportExel;
    }
}