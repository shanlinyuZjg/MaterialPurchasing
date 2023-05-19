
namespace Global.Finance
{
    partial class InvoiceVerifyMR
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
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.BtnAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TbInvoiceSelect = new System.Windows.Forms.TextBox();
            this.BtnInvoiceVerify = new System.Windows.Forms.Button();
            this.TbInvoiceAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TbTax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbInvoiceNumberS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TBstorageAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbInvoiceAllAmount = new System.Windows.Forms.TextBox();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbTaxType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbVATRate = new System.Windows.Forms.TextBox();
            this.tbTaxCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.BtnReturn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnHistory = new System.Windows.Forms.Button();
            this.BtnError = new System.Windows.Forms.Button();
            this.BtnManualAPID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(11, 89);
            this.DGV1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(449, 487);
            this.DGV1.TabIndex = 0;
            this.DGV1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellDoubleClick);
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
            this.DGV2.Location = new System.Drawing.Point(468, 89);
            this.DGV2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV2.Name = "DGV2";
            this.DGV2.ReadOnly = true;
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(1128, 487);
            this.DGV2.TabIndex = 1;
            // 
            // BtnAll
            // 
            this.BtnAll.Location = new System.Drawing.Point(17, 53);
            this.BtnAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(100, 31);
            this.BtnAll.TabIndex = 2;
            this.BtnAll.Text = "全部";
            this.BtnAll.UseVisualStyleBackColor = true;
            this.BtnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "发票号查找";
            // 
            // TbInvoiceSelect
            // 
            this.TbInvoiceSelect.Location = new System.Drawing.Point(215, 55);
            this.TbInvoiceSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceSelect.Name = "TbInvoiceSelect";
            this.TbInvoiceSelect.Size = new System.Drawing.Size(161, 26);
            this.TbInvoiceSelect.TabIndex = 4;
            this.TbInvoiceSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbInvoiceSelect_KeyDown);
            // 
            // BtnInvoiceVerify
            // 
            this.BtnInvoiceVerify.Location = new System.Drawing.Point(1376, 53);
            this.BtnInvoiceVerify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnInvoiceVerify.Name = "BtnInvoiceVerify";
            this.BtnInvoiceVerify.Size = new System.Drawing.Size(100, 31);
            this.BtnInvoiceVerify.TabIndex = 21;
            this.BtnInvoiceVerify.Text = "发票核销";
            this.BtnInvoiceVerify.UseVisualStyleBackColor = true;
            this.BtnInvoiceVerify.Click += new System.EventHandler(this.BtnInvoiceVerify_Click);
            // 
            // TbInvoiceAmount
            // 
            this.TbInvoiceAmount.Location = new System.Drawing.Point(972, 55);
            this.TbInvoiceAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceAmount.Name = "TbInvoiceAmount";
            this.TbInvoiceAmount.Size = new System.Drawing.Size(156, 26);
            this.TbInvoiceAmount.TabIndex = 20;
            this.TbInvoiceAmount.TextChanged += new System.EventHandler(this.TbInvoiceAmount_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(833, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "不含税发票总金额";
            // 
            // TbTax
            // 
            this.TbTax.Location = new System.Drawing.Point(681, 55);
            this.TbTax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbTax.Name = "TbTax";
            this.TbTax.Size = new System.Drawing.Size(147, 26);
            this.TbTax.TabIndex = 18;
            this.TbTax.TextChanged += new System.EventHandler(this.TbTax_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "总税额";
            // 
            // TbInvoiceNumber
            // 
            this.TbInvoiceNumber.Location = new System.Drawing.Point(454, 55);
            this.TbInvoiceNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceNumber.Name = "TbInvoiceNumber";
            this.TbInvoiceNumber.Size = new System.Drawing.Size(160, 26);
            this.TbInvoiceNumber.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "四班票号";
            // 
            // TbInvoiceNumberS
            // 
            this.TbInvoiceNumberS.Location = new System.Drawing.Point(714, 13);
            this.TbInvoiceNumberS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceNumberS.Name = "TbInvoiceNumberS";
            this.TbInvoiceNumberS.ReadOnly = true;
            this.TbInvoiceNumberS.Size = new System.Drawing.Size(232, 26);
            this.TbInvoiceNumberS.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(652, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "发票号";
            // 
            // TBstorageAmount
            // 
            this.TBstorageAmount.Location = new System.Drawing.Point(1058, 13);
            this.TBstorageAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBstorageAmount.Name = "TBstorageAmount";
            this.TBstorageAmount.ReadOnly = true;
            this.TBstorageAmount.Size = new System.Drawing.Size(193, 26);
            this.TBstorageAmount.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(958, 19);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "入库总金额";
            // 
            // TbInvoiceAllAmount
            // 
            this.TbInvoiceAllAmount.Location = new System.Drawing.Point(1228, 55);
            this.TbInvoiceAllAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TbInvoiceAllAmount.Name = "TbInvoiceAllAmount";
            this.TbInvoiceAllAmount.ReadOnly = true;
            this.TbInvoiceAllAmount.Size = new System.Drawing.Size(144, 26);
            this.TbInvoiceAllAmount.TabIndex = 26;
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(95, 13);
            this.tbYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(81, 26);
            this.tbYear.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = "会计期间";
            // 
            // tbMonth
            // 
            this.tbMonth.Location = new System.Drawing.Point(200, 13);
            this.tbMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.Size = new System.Drawing.Size(79, 26);
            this.tbMonth.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 30;
            this.label8.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(292, 19);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "类型";
            // 
            // cbbTaxType
            // 
            this.cbbTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTaxType.FormattingEnabled = true;
            this.cbbTaxType.Items.AddRange(new object[] {
            "I",
            "D"});
            this.cbbTaxType.Location = new System.Drawing.Point(345, 13);
            this.cbbTaxType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbbTaxType.Name = "cbbTaxType";
            this.cbbTaxType.Size = new System.Drawing.Size(53, 24);
            this.cbbTaxType.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(522, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 36;
            this.label10.Text = "税率";
            // 
            // tbVATRate
            // 
            this.tbVATRate.Location = new System.Drawing.Point(566, 13);
            this.tbVATRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbVATRate.Name = "tbVATRate";
            this.tbVATRate.ReadOnly = true;
            this.tbVATRate.Size = new System.Drawing.Size(79, 26);
            this.tbVATRate.TabIndex = 35;
            // 
            // tbTaxCode
            // 
            this.tbTaxCode.Location = new System.Drawing.Point(460, 13);
            this.tbTaxCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTaxCode.Name = "tbTaxCode";
            this.tbTaxCode.Size = new System.Drawing.Size(55, 26);
            this.tbTaxCode.TabIndex = 34;
            this.tbTaxCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTaxCode_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(412, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 33;
            this.label11.Text = "VAT码";
            // 
            // BtnReturn
            // 
            this.BtnReturn.Location = new System.Drawing.Point(1259, 12);
            this.BtnReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnReturn.Name = "BtnReturn";
            this.BtnReturn.Size = new System.Drawing.Size(100, 31);
            this.BtnReturn.TabIndex = 44;
            this.BtnReturn.Text = "退回";
            this.BtnReturn.UseVisualStyleBackColor = true;
            this.BtnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1134, 61);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 16);
            this.label12.TabIndex = 45;
            this.label12.Text = "发票总金额";
            // 
            // BtnHistory
            // 
            this.BtnHistory.Location = new System.Drawing.Point(1366, 12);
            this.BtnHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnHistory.Name = "BtnHistory";
            this.BtnHistory.Size = new System.Drawing.Size(100, 31);
            this.BtnHistory.TabIndex = 46;
            this.BtnHistory.Text = "历史查询";
            this.BtnHistory.UseVisualStyleBackColor = true;
            this.BtnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // BtnError
            // 
            this.BtnError.Location = new System.Drawing.Point(1474, 11);
            this.BtnError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnError.Name = "BtnError";
            this.BtnError.Size = new System.Drawing.Size(100, 31);
            this.BtnError.TabIndex = 47;
            this.BtnError.Text = "报错信息";
            this.BtnError.UseVisualStyleBackColor = true;
            this.BtnError.Click += new System.EventHandler(this.BtnError_Click);
            // 
            // BtnManualAPID
            // 
            this.BtnManualAPID.Location = new System.Drawing.Point(1493, 53);
            this.BtnManualAPID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnManualAPID.Name = "BtnManualAPID";
            this.BtnManualAPID.Size = new System.Drawing.Size(89, 31);
            this.BtnManualAPID.TabIndex = 48;
            this.BtnManualAPID.Text = "手工核销";
            this.BtnManualAPID.UseVisualStyleBackColor = true;
            this.BtnManualAPID.Click += new System.EventHandler(this.button1_Click);
            // 
            // InvoiceVerifyMR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1612, 608);
            this.Controls.Add(this.BtnManualAPID);
            this.Controls.Add(this.BtnError);
            this.Controls.Add(this.BtnHistory);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.BtnReturn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbVATRate);
            this.Controls.Add(this.tbTaxCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbbTaxType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbMonth);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TbInvoiceAllAmount);
            this.Controls.Add(this.TBstorageAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TbInvoiceNumberS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnInvoiceVerify);
            this.Controls.Add(this.TbInvoiceAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TbTax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbInvoiceNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbInvoiceSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAll);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.DGV1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "InvoiceVerifyMR";
            this.Text = "InvoiceVerifyMR";
            this.Load += new System.EventHandler(this.InvoiceVerifyMR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbInvoiceSelect;
        private System.Windows.Forms.Button BtnInvoiceVerify;
        private System.Windows.Forms.TextBox TbInvoiceAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbTax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbInvoiceNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbInvoiceNumberS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TBstorageAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbInvoiceAllAmount;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbTaxType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbVATRate;
        private System.Windows.Forms.TextBox tbTaxCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BtnReturn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnHistory;
        private System.Windows.Forms.Button BtnError;
        private System.Windows.Forms.Button BtnManualAPID;
    }
}