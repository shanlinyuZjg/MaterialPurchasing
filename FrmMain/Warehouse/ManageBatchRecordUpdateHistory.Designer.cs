namespace Global.Warehouse
{
    partial class ManageBatchRecordUpdateHistory
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
            this.dgvRUH = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.Btnchazhao = new System.Windows.Forms.Button();
            this.Btnyincang = new System.Windows.Forms.Button();
            this.Btnxianshi = new System.Windows.Forms.Button();
            this.BtnchazhaoAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRUH)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRUH
            // 
            this.dgvRUH.AllowUserToAddRows = false;
            this.dgvRUH.AllowUserToDeleteRows = false;
            this.dgvRUH.AllowUserToResizeColumns = false;
            this.dgvRUH.AllowUserToResizeRows = false;
            this.dgvRUH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRUH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRUH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dgvRUH.Location = new System.Drawing.Point(12, 52);
            this.dgvRUH.Name = "dgvRUH";
            this.dgvRUH.ReadOnly = true;
            this.dgvRUH.RowHeadersVisible = false;
            this.dgvRUH.RowTemplate.Height = 23;
            this.dgvRUH.Size = new System.Drawing.Size(1274, 548);
            this.dgvRUH.TabIndex = 0;
            this.dgvRUH.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRUH_CellClick);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(52, 21);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(109, 21);
            this.dtpDate.TabIndex = 1;
            // 
            // Btnchazhao
            // 
            this.Btnchazhao.Location = new System.Drawing.Point(201, 18);
            this.Btnchazhao.Name = "Btnchazhao";
            this.Btnchazhao.Size = new System.Drawing.Size(75, 23);
            this.Btnchazhao.TabIndex = 2;
            this.Btnchazhao.Text = "按月查找";
            this.Btnchazhao.UseVisualStyleBackColor = true;
            this.Btnchazhao.Click += new System.EventHandler(this.Btnchazhao_Click);
            // 
            // Btnyincang
            // 
            this.Btnyincang.Location = new System.Drawing.Point(745, 18);
            this.Btnyincang.Name = "Btnyincang";
            this.Btnyincang.Size = new System.Drawing.Size(75, 23);
            this.Btnyincang.TabIndex = 3;
            this.Btnyincang.Text = "隐藏";
            this.Btnyincang.UseVisualStyleBackColor = true;
            this.Btnyincang.Visible = false;
            this.Btnyincang.Click += new System.EventHandler(this.Btnyincang_Click);
            // 
            // Btnxianshi
            // 
            this.Btnxianshi.Location = new System.Drawing.Point(854, 18);
            this.Btnxianshi.Name = "Btnxianshi";
            this.Btnxianshi.Size = new System.Drawing.Size(75, 23);
            this.Btnxianshi.TabIndex = 4;
            this.Btnxianshi.Text = "显示";
            this.Btnxianshi.UseVisualStyleBackColor = true;
            this.Btnxianshi.Visible = false;
            this.Btnxianshi.Click += new System.EventHandler(this.Btnxianshi_Click);
            // 
            // BtnchazhaoAll
            // 
            this.BtnchazhaoAll.Location = new System.Drawing.Point(637, 18);
            this.BtnchazhaoAll.Name = "BtnchazhaoAll";
            this.BtnchazhaoAll.Size = new System.Drawing.Size(75, 23);
            this.BtnchazhaoAll.TabIndex = 5;
            this.BtnchazhaoAll.Text = "按月查找";
            this.BtnchazhaoAll.UseVisualStyleBackColor = true;
            this.BtnchazhaoAll.Visible = false;
            this.BtnchazhaoAll.Click += new System.EventHandler(this.BtnchazhaoAll_Click);
            // 
            // ManageBatchRecordUpdateHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 612);
            this.Controls.Add(this.BtnchazhaoAll);
            this.Controls.Add(this.Btnxianshi);
            this.Controls.Add(this.Btnyincang);
            this.Controls.Add(this.Btnchazhao);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dgvRUH);
            this.KeyPreview = true;
            this.Name = "ManageBatchRecordUpdateHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批记录修改历史";
            this.Load += new System.EventHandler(this.ManageBatchRecordUpdateHistory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManageBatchRecordUpdateHistory_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRUH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRUH;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button Btnchazhao;
        private System.Windows.Forms.Button Btnyincang;
        private System.Windows.Forms.Button Btnxianshi;
        private System.Windows.Forms.Button BtnchazhaoAll;
    }
}