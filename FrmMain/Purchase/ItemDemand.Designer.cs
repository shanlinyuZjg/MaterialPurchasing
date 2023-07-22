
namespace Global.Purchase
{
    partial class ItemDemand
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvItemRequirement = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnExtract = new System.Windows.Forms.Button();
            this.BtAllSelect = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1162, 569);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.dgvItemRequirement);
            this.tabPage1.Controls.Add(this.btnExtract);
            this.tabPage1.Controls.Add(this.BtAllSelect);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1154, 539);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "计划提取";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(23, 14);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(133, 35);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvItemRequirement
            // 
            this.dgvItemRequirement.AllowUserToAddRows = false;
            this.dgvItemRequirement.AllowUserToDeleteRows = false;
            this.dgvItemRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemRequirement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItemRequirement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemRequirement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dgvItemRequirement.Location = new System.Drawing.Point(12, 59);
            this.dgvItemRequirement.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dgvItemRequirement.Name = "dgvItemRequirement";
            this.dgvItemRequirement.ReadOnly = true;
            this.dgvItemRequirement.RowHeadersVisible = false;
            this.dgvItemRequirement.RowTemplate.Height = 23;
            this.dgvItemRequirement.Size = new System.Drawing.Size(1127, 462);
            this.dgvItemRequirement.TabIndex = 21;
            this.dgvItemRequirement.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemRequirement_CellDoubleClick);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择 ";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 49;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(413, 14);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(133, 35);
            this.btnExtract.TabIndex = 20;
            this.btnExtract.Text = "提取";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // BtAllSelect
            // 
            this.BtAllSelect.Location = new System.Drawing.Point(185, 14);
            this.BtAllSelect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtAllSelect.Name = "BtAllSelect";
            this.BtAllSelect.Size = new System.Drawing.Size(193, 35);
            this.BtAllSelect.TabIndex = 19;
            this.BtAllSelect.Text = "全选/全不选";
            this.BtAllSelect.UseVisualStyleBackColor = true;
            this.BtAllSelect.Click += new System.EventHandler(this.BtAllSelect_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1154, 539);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "信息录入";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(1323, 738);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "订单下达";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ItemDemand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 569);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ItemDemand";
            this.Text = "ItemDemand";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemRequirement)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvItemRequirement;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button BtAllSelect;
    }
}