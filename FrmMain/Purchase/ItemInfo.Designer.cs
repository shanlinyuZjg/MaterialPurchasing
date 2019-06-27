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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbItemFuzzyName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvItemDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnItemSearch = new DevComponents.DotNetBar.ButtonX();
            this.la = new DevComponents.DotNetBar.LabelX();
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
            this.tbItemFuzzyName.Location = new System.Drawing.Point(97, 15);
            this.tbItemFuzzyName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbItemFuzzyName.Name = "tbItemFuzzyName";
            this.tbItemFuzzyName.PreventEnterBeep = true;
            this.tbItemFuzzyName.Size = new System.Drawing.Size(212, 26);
            this.tbItemFuzzyName.TabIndex = 7;
            this.tbItemFuzzyName.TextChanged += new System.EventHandler(this.tbItemFuzzyName_TextChanged);
            this.tbItemFuzzyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemFuzzyName_KeyPress);
            // 
            // dgvItemDetail
            // 
            this.dgvItemDetail.AllowUserToAddRows = false;
            this.dgvItemDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvItemDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItemDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemDetail.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemDetail.EnableHeadersVisualStyles = false;
            this.dgvItemDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvItemDetail.Location = new System.Drawing.Point(13, 51);
            this.dgvItemDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvItemDetail.Name = "dgvItemDetail";
            this.dgvItemDetail.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemDetail.RowTemplate.Height = 23;
            this.dgvItemDetail.Size = new System.Drawing.Size(536, 643);
            this.dgvItemDetail.TabIndex = 6;
            // 
            // btnItemSearch
            // 
            this.btnItemSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnItemSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnItemSearch.Location = new System.Drawing.Point(317, 14);
            this.btnItemSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnItemSearch.Name = "btnItemSearch";
            this.btnItemSearch.Size = new System.Drawing.Size(69, 27);
            this.btnItemSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnItemSearch.TabIndex = 4;
            this.btnItemSearch.Text = "查询";
            // 
            // la
            // 
            this.la.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.la.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.la.Location = new System.Drawing.Point(21, 14);
            this.la.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.la.Name = "la";
            this.la.Size = new System.Drawing.Size(68, 26);
            this.la.TabIndex = 5;
            this.la.Text = "物料名称";
            // 
            // ItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 708);
            this.Controls.Add(this.tbItemFuzzyName);
            this.Controls.Add(this.dgvItemDetail);
            this.Controls.Add(this.btnItemSearch);
            this.Controls.Add(this.la);
            this.DoubleBuffered = true;
            this.Name = "ItemInfo";
            this.Text = "Item";
            this.Load += new System.EventHandler(this.ItemInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbItemFuzzyName;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvItemDetail;
        private DevComponents.DotNetBar.ButtonX btnItemSearch;
        private DevComponents.DotNetBar.LabelX la;
    }
}