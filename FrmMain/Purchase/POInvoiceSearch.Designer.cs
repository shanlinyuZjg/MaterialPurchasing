
namespace Global.Purchase
{
    partial class POInvoiceSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPODetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tbItem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.tbPONumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rbtnItemNumber = new System.Windows.Forms.RadioButton();
            this.rbtnItemDescription = new System.Windows.Forms.RadioButton();
            this.rbtnPONumber = new System.Windows.Forms.RadioButton();
            this.tbLineNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPODetail
            // 
            this.dgvPODetail.AllowUserToAddRows = false;
            this.dgvPODetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPODetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPODetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPODetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPODetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPODetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPODetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPODetail.EnableHeadersVisualStyles = false;
            this.dgvPODetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvPODetail.Location = new System.Drawing.Point(12, 32);
            this.dgvPODetail.Name = "dgvPODetail";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPODetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPODetail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPODetail.RowTemplate.Height = 23;
            this.dgvPODetail.Size = new System.Drawing.Size(1090, 569);
            this.dgvPODetail.TabIndex = 31;
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 41;
            // 
            // tbItem
            // 
            // 
            // 
            // 
            this.tbItem.Border.Class = "TextBoxBorder";
            this.tbItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbItem.Location = new System.Drawing.Point(399, 6);
            this.tbItem.Name = "tbItem";
            this.tbItem.PreventEnterBeep = true;
            this.tbItem.Size = new System.Drawing.Size(95, 21);
            this.tbItem.TabIndex = 28;
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(958, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(51, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 30;
            this.btnView.Text = "查询";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(1039, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(51, 23);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tbPONumber
            // 
            // 
            // 
            // 
            this.tbPONumber.Border.Class = "TextBoxBorder";
            this.tbPONumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPONumber.Location = new System.Drawing.Point(564, 6);
            this.tbPONumber.Name = "tbPONumber";
            this.tbPONumber.PreventEnterBeep = true;
            this.tbPONumber.Size = new System.Drawing.Size(95, 21);
            this.tbPONumber.TabIndex = 28;
            // 
            // rbtnItemNumber
            // 
            this.rbtnItemNumber.AutoSize = true;
            this.rbtnItemNumber.Location = new System.Drawing.Point(12, 8);
            this.rbtnItemNumber.Name = "rbtnItemNumber";
            this.rbtnItemNumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnItemNumber.TabIndex = 32;
            this.rbtnItemNumber.TabStop = true;
            this.rbtnItemNumber.Text = "物料代码";
            this.rbtnItemNumber.UseVisualStyleBackColor = true;
            // 
            // rbtnItemDescription
            // 
            this.rbtnItemDescription.AutoSize = true;
            this.rbtnItemDescription.Location = new System.Drawing.Point(113, 8);
            this.rbtnItemDescription.Name = "rbtnItemDescription";
            this.rbtnItemDescription.Size = new System.Drawing.Size(71, 16);
            this.rbtnItemDescription.TabIndex = 32;
            this.rbtnItemDescription.TabStop = true;
            this.rbtnItemDescription.Text = "物料描述";
            this.rbtnItemDescription.UseVisualStyleBackColor = true;
            // 
            // rbtnPONumber
            // 
            this.rbtnPONumber.AutoSize = true;
            this.rbtnPONumber.Location = new System.Drawing.Point(214, 8);
            this.rbtnPONumber.Name = "rbtnPONumber";
            this.rbtnPONumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnPONumber.TabIndex = 32;
            this.rbtnPONumber.TabStop = true;
            this.rbtnPONumber.Text = "采购单号";
            this.rbtnPONumber.UseVisualStyleBackColor = true;
            // 
            // tbLineNumber
            // 
            // 
            // 
            // 
            this.tbLineNumber.Border.Class = "TextBoxBorder";
            this.tbLineNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLineNumber.Location = new System.Drawing.Point(709, 5);
            this.tbLineNumber.Name = "tbLineNumber";
            this.tbLineNumber.PreventEnterBeep = true;
            this.tbLineNumber.Size = new System.Drawing.Size(47, 21);
            this.tbLineNumber.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "采购单号";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "物料代码或描述";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(677, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "行号";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(762, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "行号为空时，查询订单所有信息";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // POInvoiceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 613);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnPONumber);
            this.Controls.Add(this.rbtnItemDescription);
            this.Controls.Add(this.rbtnItemNumber);
            this.Controls.Add(this.dgvPODetail);
            this.Controls.Add(this.tbLineNumber);
            this.Controls.Add(this.tbPONumber);
            this.Controls.Add(this.tbItem);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnView);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "POInvoiceSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发票查询";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.POInvoiceSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvPODetail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private DevComponents.DotNetBar.Controls.TextBoxX tbItem;
        private DevComponents.DotNetBar.ButtonX btnView;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPONumber;
        private System.Windows.Forms.RadioButton rbtnItemNumber;
        private System.Windows.Forms.RadioButton rbtnItemDescription;
        private System.Windows.Forms.RadioButton rbtnPONumber;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLineNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}