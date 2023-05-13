
namespace Global.Purchase
{
    partial class PoInvoiceManage_MR
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
            this.TbInvoiceSelect = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAll = new System.Windows.Forms.Button();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.TbVendor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbInvoiceNumberS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnReturnWupiao = new System.Windows.Forms.Button();
            this.TbAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TbTax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbRukuAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(1082, 38);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(90, 23);
            this.BtnSubmit.TabIndex = 38;
            this.BtnSubmit.Text = "提交";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // TbInvoiceSelect
            // 
            this.TbInvoiceSelect.Location = new System.Drawing.Point(165, 7);
            this.TbInvoiceSelect.Name = "TbInvoiceSelect";
            this.TbInvoiceSelect.Size = new System.Drawing.Size(154, 21);
            this.TbInvoiceSelect.TabIndex = 31;
            this.TbInvoiceSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbInvoiceSelect_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "发票号查找";
            // 
            // BtnAll
            // 
            this.BtnAll.Location = new System.Drawing.Point(17, 6);
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
            this.DGV2.Location = new System.Drawing.Point(357, 65);
            this.DGV2.Name = "DGV2";
            this.DGV2.ReadOnly = true;
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(834, 490);
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
            this.DGV1.Location = new System.Drawing.Point(12, 32);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(337, 523);
            this.DGV1.TabIndex = 27;
            this.DGV1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellDoubleClick);
            // 
            // TbVendor
            // 
            this.TbVendor.Location = new System.Drawing.Point(408, 7);
            this.TbVendor.Name = "TbVendor";
            this.TbVendor.ReadOnly = true;
            this.TbVendor.Size = new System.Drawing.Size(322, 21);
            this.TbVendor.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "供应商";
            // 
            // TbInvoiceNumberS
            // 
            this.TbInvoiceNumberS.Location = new System.Drawing.Point(787, 7);
            this.TbInvoiceNumberS.Name = "TbInvoiceNumberS";
            this.TbInvoiceNumberS.ReadOnly = true;
            this.TbInvoiceNumberS.Size = new System.Drawing.Size(289, 21);
            this.TbInvoiceNumberS.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(740, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "发票号";
            // 
            // BtnReturnWupiao
            // 
            this.BtnReturnWupiao.Location = new System.Drawing.Point(1082, 6);
            this.BtnReturnWupiao.Name = "BtnReturnWupiao";
            this.BtnReturnWupiao.Size = new System.Drawing.Size(90, 23);
            this.BtnReturnWupiao.TabIndex = 43;
            this.BtnReturnWupiao.Text = "退回无票";
            this.BtnReturnWupiao.UseVisualStyleBackColor = true;
            this.BtnReturnWupiao.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // TbAmount
            // 
            this.TbAmount.Location = new System.Drawing.Point(634, 39);
            this.TbAmount.Name = "TbAmount";
            this.TbAmount.Size = new System.Drawing.Size(123, 21);
            this.TbAmount.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(527, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "不含税发票总金额";
            // 
            // TbTax
            // 
            this.TbTax.Location = new System.Drawing.Point(393, 39);
            this.TbTax.Name = "TbTax";
            this.TbTax.Size = new System.Drawing.Size(130, 21);
            this.TbTax.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "税额";
            // 
            // TbRukuAmount
            // 
            this.TbRukuAmount.Location = new System.Drawing.Point(839, 39);
            this.TbRukuAmount.Name = "TbRukuAmount";
            this.TbRukuAmount.ReadOnly = true;
            this.TbRukuAmount.Size = new System.Drawing.Size(130, 21);
            this.TbRukuAmount.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(765, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 48;
            this.label6.Text = "入库总金额";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(975, 38);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(90, 23);
            this.BtnPrint.TabIndex = 50;
            this.BtnPrint.Text = "打印";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // PoInvoiceManage_MR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 567);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.TbRukuAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TbAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TbTax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnReturnWupiao);
            this.Controls.Add(this.TbInvoiceNumberS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbVendor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.TbInvoiceSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAll);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.DGV1);
            this.Name = "PoInvoiceManage_MR";
            this.Text = "发票管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PoInvoiceManage_MR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.TextBox TbInvoiceSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TextBox TbVendor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbInvoiceNumberS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnReturnWupiao;
        private System.Windows.Forms.TextBox TbAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbTax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbRukuAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnPrint;
    }
}