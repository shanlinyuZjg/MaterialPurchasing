namespace Global.Purchase
{
    partial class ItemInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbItemFuzzyName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvItemDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnItemSearch = new DevComponents.DotNetBar.ButtonX();
            this.la = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // tbItemFuzzyName
            // 
            // 
            // 
            // 
            this.tbItemFuzzyName.Border.Class = "TextBoxBorder";
            this.tbItemFuzzyName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItemFuzzyName.Location = new System.Drawing.Point(69, 6);
            this.tbItemFuzzyName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbItemFuzzyName.Name = "tbItemFuzzyName";
            this.tbItemFuzzyName.PreventEnterBeep = true;
            this.tbItemFuzzyName.Size = new System.Drawing.Size(208, 21);
            this.tbItemFuzzyName.TabIndex = 0;
            this.tbItemFuzzyName.TextChanged += new System.EventHandler(this.tbItemFuzzyName_TextChanged);
            this.tbItemFuzzyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemFuzzyName_KeyPress);
            // 
            // dgvItemDetail
            // 
            this.dgvItemDetail.AllowUserToAddRows = false;
            this.dgvItemDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvItemDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItemDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvItemDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemDetail.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvItemDetail.EnableHeadersVisualStyles = false;
            this.dgvItemDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvItemDetail.Location = new System.Drawing.Point(7, 37);
            this.dgvItemDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvItemDetail.Name = "dgvItemDetail";
            this.dgvItemDetail.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvItemDetail.RowTemplate.Height = 23;
            this.dgvItemDetail.Size = new System.Drawing.Size(479, 565);
            this.dgvItemDetail.TabIndex = 6;
            this.dgvItemDetail.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetail_CellContentDoubleClick);
            this.dgvItemDetail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetail_CellDoubleClick);
            // 
            // btnItemSearch
            // 
            this.btnItemSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnItemSearch.Location = new System.Drawing.Point(285, 9);
            this.btnItemSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnItemSearch.Name = "btnItemSearch";
            this.btnItemSearch.Size = new System.Drawing.Size(52, 21);
            this.btnItemSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnItemSearch.TabIndex = 4;
            this.btnItemSearch.Text = "查询";
            this.btnItemSearch.Click += new System.EventHandler(this.btnItemSearch_Click);
            // 
            // la
            // 
            this.la.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.la.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.la.Location = new System.Drawing.Point(7, 5);
            this.la.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.la.Name = "la";
            this.la.Size = new System.Drawing.Size(54, 26);
            this.la.TabIndex = 5;
            this.la.Text = "物料名称";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.Red;
            this.labelX1.Location = new System.Drawing.Point(345, 6);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(141, 26);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "双击后自动带回物料信息";
            // 
            // ItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 607);
            this.Controls.Add(this.tbItemFuzzyName);
            this.Controls.Add(this.dgvItemDetail);
            this.Controls.Add(this.btnItemSearch);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.la);
            this.DoubleBuffered = true;
            this.Name = "ItemInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料信息查询";
            this.Load += new System.EventHandler(this.ItemInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbItemFuzzyName;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemDetail;
        private DevComponents.DotNetBar.ButtonX btnItemSearch;
        private DevComponents.DotNetBar.LabelX la;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}