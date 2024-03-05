
namespace Global.Purchase
{
    partial class PurchaseOrderInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.DtpStart = new System.Windows.Forms.DateTimePicker();
            this.DtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TbVendorNumber = new System.Windows.Forms.TextBox();
            this.TbVendorName = new System.Windows.Forms.TextBox();
            this.BtExportExcel = new System.Windows.Forms.Button();
            this.CbQuantity = new System.Windows.Forms.CheckBox();
            this.TbBuyer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(12, 84);
            this.DGV1.Name = "DGV1";
            this.DGV1.ReadOnly = true;
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(1153, 478);
            this.DGV1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "采购订单下达日期从";
            // 
            // DtpStart
            // 
            this.DtpStart.Location = new System.Drawing.Point(141, 15);
            this.DtpStart.Name = "DtpStart";
            this.DtpStart.Size = new System.Drawing.Size(108, 21);
            this.DtpStart.TabIndex = 2;
            // 
            // DtpEnd
            // 
            this.DtpEnd.Location = new System.Drawing.Point(281, 15);
            this.DtpEnd.Name = "DtpEnd";
            this.DtpEnd.Size = new System.Drawing.Size(108, 21);
            this.DtpEnd.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "至";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "供应商码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(376, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "供应商名";
            // 
            // TbVendorNumber
            // 
            this.TbVendorNumber.Location = new System.Drawing.Point(266, 53);
            this.TbVendorNumber.Name = "TbVendorNumber";
            this.TbVendorNumber.Size = new System.Drawing.Size(100, 21);
            this.TbVendorNumber.TabIndex = 7;
            this.TbVendorNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbVendorNumber_KeyDown);
            // 
            // TbVendorName
            // 
            this.TbVendorName.Location = new System.Drawing.Point(435, 53);
            this.TbVendorName.Name = "TbVendorName";
            this.TbVendorName.Size = new System.Drawing.Size(190, 21);
            this.TbVendorName.TabIndex = 8;
            this.TbVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbVendorName_KeyPress);
            // 
            // BtExportExcel
            // 
            this.BtExportExcel.Location = new System.Drawing.Point(649, 52);
            this.BtExportExcel.Name = "BtExportExcel";
            this.BtExportExcel.Size = new System.Drawing.Size(75, 23);
            this.BtExportExcel.TabIndex = 9;
            this.BtExportExcel.Text = "导出";
            this.BtExportExcel.UseVisualStyleBackColor = true;
            this.BtExportExcel.Click += new System.EventHandler(this.BtExportExcel_Click);
            // 
            // CbQuantity
            // 
            this.CbQuantity.AutoSize = true;
            this.CbQuantity.Location = new System.Drawing.Point(424, 17);
            this.CbQuantity.Name = "CbQuantity";
            this.CbQuantity.Size = new System.Drawing.Size(192, 16);
            this.CbQuantity.TabIndex = 10;
            this.CbQuantity.Text = "包含入库数量大于等于订单数量";
            this.CbQuantity.UseVisualStyleBackColor = true;
            // 
            // TbBuyer
            // 
            this.TbBuyer.Location = new System.Drawing.Point(756, 15);
            this.TbBuyer.Name = "TbBuyer";
            this.TbBuyer.Size = new System.Drawing.Size(100, 21);
            this.TbBuyer.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(654, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "采购员四班账号:";
            // 
            // PurchaseOrderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 574);
            this.Controls.Add(this.TbBuyer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbQuantity);
            this.Controls.Add(this.BtExportExcel);
            this.Controls.Add(this.TbVendorName);
            this.Controls.Add(this.TbVendorNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DtpEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DtpStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV1);
            this.Name = "PurchaseOrderInfo";
            this.Text = "采购订单查询";
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DtpStart;
        private System.Windows.Forms.DateTimePicker DtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbVendorNumber;
        private System.Windows.Forms.TextBox TbVendorName;
        private System.Windows.Forms.Button BtExportExcel;
        private System.Windows.Forms.CheckBox CbQuantity;
        private System.Windows.Forms.TextBox TbBuyer;
        private System.Windows.Forms.Label label5;
    }
}