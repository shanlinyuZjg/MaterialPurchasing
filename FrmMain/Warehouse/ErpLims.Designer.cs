
namespace Global.Warehouse
{
    partial class ErpLims
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
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.TbUserID = new System.Windows.Forms.TextBox();
            this.TbItemCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbItemName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtExtract = new System.Windows.Forms.Button();
            this.BtHistory = new System.Windows.Forms.Button();
            this.BtSelectAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select});
            this.DGV.Location = new System.Drawing.Point(12, 50);
            this.DGV.Name = "DGV";
            this.DGV.RowTemplate.Height = 23;
            this.DGV.Size = new System.Drawing.Size(1019, 493);
            this.DGV.TabIndex = 0;
            // 
            // Select
            // 
            this.Select.HeaderText = "选择";
            this.Select.Name = "Select";
            this.Select.Width = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "账号";
            // 
            // TbUserID
            // 
            this.TbUserID.Location = new System.Drawing.Point(80, 19);
            this.TbUserID.Name = "TbUserID";
            this.TbUserID.Size = new System.Drawing.Size(62, 21);
            this.TbUserID.TabIndex = 2;
            this.TbUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbUserID_KeyPress);
            // 
            // TbItemCode
            // 
            this.TbItemCode.Location = new System.Drawing.Point(217, 19);
            this.TbItemCode.Name = "TbItemCode";
            this.TbItemCode.Size = new System.Drawing.Size(62, 21);
            this.TbItemCode.TabIndex = 4;
            this.TbItemCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbItemCode_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "物料编码";
            // 
            // TbItemName
            // 
            this.TbItemName.Location = new System.Drawing.Point(347, 19);
            this.TbItemName.Name = "TbItemName";
            this.TbItemName.Size = new System.Drawing.Size(112, 21);
            this.TbItemName.TabIndex = 6;
            this.TbItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbItemName_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "物料描述";
            // 
            // BtExtract
            // 
            this.BtExtract.Location = new System.Drawing.Point(570, 18);
            this.BtExtract.Name = "BtExtract";
            this.BtExtract.Size = new System.Drawing.Size(75, 23);
            this.BtExtract.TabIndex = 7;
            this.BtExtract.Text = "提取";
            this.BtExtract.UseVisualStyleBackColor = true;
            this.BtExtract.Click += new System.EventHandler(this.BtExtract_Click);
            // 
            // BtHistory
            // 
            this.BtHistory.Location = new System.Drawing.Point(691, 18);
            this.BtHistory.Name = "BtHistory";
            this.BtHistory.Size = new System.Drawing.Size(89, 23);
            this.BtHistory.TabIndex = 8;
            this.BtHistory.Text = "历史数据查询";
            this.BtHistory.UseVisualStyleBackColor = true;
            this.BtHistory.Click += new System.EventHandler(this.BtHistory_Click);
            // 
            // BtSelectAll
            // 
            this.BtSelectAll.Location = new System.Drawing.Point(480, 18);
            this.BtSelectAll.Name = "BtSelectAll";
            this.BtSelectAll.Size = new System.Drawing.Size(75, 23);
            this.BtSelectAll.TabIndex = 9;
            this.BtSelectAll.Text = "全选";
            this.BtSelectAll.UseVisualStyleBackColor = true;
            this.BtSelectAll.Click += new System.EventHandler(this.BtSelectAll_Click);
            // 
            // ErpLims
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 555);
            this.Controls.Add(this.BtSelectAll);
            this.Controls.Add(this.BtHistory);
            this.Controls.Add(this.BtExtract);
            this.Controls.Add(this.TbItemName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbItemCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbUserID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV);
            this.Name = "ErpLims";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ErpLims";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ErpLims_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbUserID;
        private System.Windows.Forms.TextBox TbItemCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtExtract;
        private System.Windows.Forms.Button BtHistory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.Button BtSelectAll;
    }
}