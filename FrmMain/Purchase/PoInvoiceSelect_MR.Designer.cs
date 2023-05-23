
namespace Global.Purchase
{
    partial class PoInvoiceSelect_MR
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
            this.BtnSelect = new System.Windows.Forms.Button();
            this.TbInvoiceS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.TbVendorNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbVendorName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.TbLineNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbPONumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TbItemNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(723, 16);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(59, 23);
            this.BtnSelect.TabIndex = 53;
            this.BtnSelect.Text = "查找";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // TbInvoiceS
            // 
            this.TbInvoiceS.Location = new System.Drawing.Point(367, 17);
            this.TbInvoiceS.Name = "TbInvoiceS";
            this.TbInvoiceS.Size = new System.Drawing.Size(75, 21);
            this.TbInvoiceS.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(324, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "发票号";
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
            this.DGV2.Location = new System.Drawing.Point(362, 81);
            this.DGV2.Name = "DGV2";
            this.DGV2.ReadOnly = true;
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(832, 489);
            this.DGV2.TabIndex = 45;
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(6, 47);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(348, 523);
            this.DGV1.TabIndex = 44;
            this.DGV1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV1_CellDoubleClick);
            // 
            // TbVendorNumber
            // 
            this.TbVendorNumber.Location = new System.Drawing.Point(504, 17);
            this.TbVendorNumber.Name = "TbVendorNumber";
            this.TbVendorNumber.Size = new System.Drawing.Size(69, 21);
            this.TbVendorNumber.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "供应商码";
            // 
            // TbVendorName
            // 
            this.TbVendorName.Location = new System.Drawing.Point(633, 17);
            this.TbVendorName.Name = "TbVendorName";
            this.TbVendorName.Size = new System.Drawing.Size(85, 21);
            this.TbVendorName.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(576, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "供应商名";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(80, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(106, 21);
            this.dateTimePicker1.TabIndex = 58;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(209, 17);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(108, 21);
            this.dateTimePicker2.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 60;
            this.label4.Text = "操作日期从";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 61;
            this.label5.Text = "至";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(788, 17);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(59, 23);
            this.BtnPrint.TabIndex = 62;
            this.BtnPrint.Text = "打印";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // TbLineNumber
            // 
            this.TbLineNumber.Location = new System.Drawing.Point(754, 54);
            this.TbLineNumber.Name = "TbLineNumber";
            this.TbLineNumber.Size = new System.Drawing.Size(58, 21);
            this.TbLineNumber.TabIndex = 68;
            this.TbLineNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbLineNumber_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(722, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 67;
            this.label6.Text = "行号";
            // 
            // TbPONumber
            // 
            this.TbPONumber.Location = new System.Drawing.Point(581, 54);
            this.TbPONumber.Name = "TbPONumber";
            this.TbPONumber.Size = new System.Drawing.Size(117, 21);
            this.TbPONumber.TabIndex = 66;
            this.TbPONumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbPONumber_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(523, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 65;
            this.label7.Text = "采购单号";
            // 
            // TbItemNumber
            // 
            this.TbItemNumber.Location = new System.Drawing.Point(425, 54);
            this.TbItemNumber.Name = "TbItemNumber";
            this.TbItemNumber.Size = new System.Drawing.Size(75, 21);
            this.TbItemNumber.TabIndex = 64;
            this.TbItemNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbItemNumber_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(368, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 63;
            this.label8.Text = "物料编码";
            // 
            // PoInvoiceSelect_MR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 591);
            this.Controls.Add(this.TbLineNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TbPONumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TbItemNumber);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.TbVendorName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbVendorNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.TbInvoiceS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.DGV1);
            this.Name = "PoInvoiceSelect_MR";
            this.Text = "Status:1已提交2审计已审核3财务已核销";
            this.Load += new System.EventHandler(this.PoInvoiceSelect_MR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.TextBox TbInvoiceS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TextBox TbVendorNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbVendorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.TextBox TbLineNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbPONumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TbItemNumber;
        private System.Windows.Forms.Label label8;
    }
}