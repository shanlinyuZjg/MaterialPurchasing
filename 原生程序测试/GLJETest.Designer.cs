namespace 原生程序测试
{
    partial class GLJETest
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbLatin = new System.Windows.Forms.TextBox();
            this.tbCn = new System.Windows.Forms.TextBox();
            this.tbBatchNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConvertCn2Latin = new System.Windows.Forms.Button();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(633, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "拉丁字符";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "中文";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "查找";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbLatin
            // 
            this.tbLatin.Location = new System.Drawing.Point(692, 6);
            this.tbLatin.Name = "tbLatin";
            this.tbLatin.Size = new System.Drawing.Size(333, 21);
            this.tbLatin.TabIndex = 5;
            // 
            // tbCn
            // 
            this.tbCn.Location = new System.Drawing.Point(282, 5);
            this.tbCn.Name = "tbCn";
            this.tbCn.Size = new System.Drawing.Size(333, 21);
            this.tbCn.TabIndex = 6;
            this.tbCn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCn_KeyPress);
            // 
            // tbBatchNo
            // 
            this.tbBatchNo.Location = new System.Drawing.Point(56, 7);
            this.tbBatchNo.Name = "tbBatchNo";
            this.tbBatchNo.Size = new System.Drawing.Size(70, 21);
            this.tbBatchNo.TabIndex = 0;
            this.tbBatchNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBatchNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "凭证号";
            // 
            // btnConvertCn2Latin
            // 
            this.btnConvertCn2Latin.Location = new System.Drawing.Point(1044, 7);
            this.btnConvertCn2Latin.Name = "btnConvertCn2Latin";
            this.btnConvertCn2Latin.Size = new System.Drawing.Size(75, 23);
            this.btnConvertCn2Latin.TabIndex = 7;
            this.btnConvertCn2Latin.Text = "转换";
            this.btnConvertCn2Latin.UseVisualStyleBackColor = true;
            this.btnConvertCn2Latin.Click += new System.EventHandler(this.btnConvertCn2Latin_Click);
            this.btnConvertCn2Latin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnConvertCn2Latin_KeyPress);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dgvDetail.Location = new System.Drawing.Point(7, 50);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(1296, 586);
            this.dgvDetail.TabIndex = 10;
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 35;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(1142, 7);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // GLJETest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 648);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnConvertCn2Latin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbLatin);
            this.Controls.Add(this.tbBatchNo);
            this.Controls.Add(this.tbCn);
            this.MaximizeBox = false;
            this.Name = "GLJETest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GLJETest";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbLatin;
        private System.Windows.Forms.TextBox tbCn;
        private System.Windows.Forms.TextBox tbBatchNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConvertCn2Latin;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.Button btnUpdate;
    }
}