namespace 原生程序测试
{
    partial class 物料计算
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
            this.btnGet = new System.Windows.Forms.Button();
            this.dgvBOM = new System.Windows.Forms.DataGridView();
            this.tbItemNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCurrentQuantity = new System.Windows.Forms.DataGridView();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvActualRequireQuantity = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvCurrentRequireQuantity = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActualRequireQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentRequireQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(362, 7);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "计算";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // dgvBOM
            // 
            this.dgvBOM.AllowUserToAddRows = false;
            this.dgvBOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBOM.Location = new System.Drawing.Point(12, 66);
            this.dgvBOM.Name = "dgvBOM";
            this.dgvBOM.RowTemplate.Height = 23;
            this.dgvBOM.Size = new System.Drawing.Size(788, 599);
            this.dgvBOM.TabIndex = 1;
            // 
            // tbItemNumber
            // 
            this.tbItemNumber.Location = new System.Drawing.Point(58, 9);
            this.tbItemNumber.Name = "tbItemNumber";
            this.tbItemNumber.Size = new System.Drawing.Size(100, 21);
            this.tbItemNumber.TabIndex = 2;
            this.tbItemNumber.TextChanged += new System.EventHandler(this.tbItemNumber_TextChanged);
            this.tbItemNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbItemNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "代码";
            // 
            // dgvCurrentQuantity
            // 
            this.dgvCurrentQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCurrentQuantity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrentQuantity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentQuantity.Location = new System.Drawing.Point(956, 66);
            this.dgvCurrentQuantity.Name = "dgvCurrentQuantity";
            this.dgvCurrentQuantity.RowTemplate.Height = 23;
            this.dgvCurrentQuantity.Size = new System.Drawing.Size(179, 321);
            this.dgvCurrentQuantity.TabIndex = 1;
            // 
            // tbQuantity
            // 
            this.tbQuantity.Location = new System.Drawing.Point(232, 9);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(100, 21);
            this.tbQuantity.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数量";
            // 
            // dgvActualRequireQuantity
            // 
            this.dgvActualRequireQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActualRequireQuantity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActualRequireQuantity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActualRequireQuantity.Location = new System.Drawing.Point(956, 405);
            this.dgvActualRequireQuantity.Name = "dgvActualRequireQuantity";
            this.dgvActualRequireQuantity.RowTemplate.Height = 23;
            this.dgvActualRequireQuantity.Size = new System.Drawing.Size(179, 260);
            this.dgvActualRequireQuantity.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(954, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "物料库存量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "BOM清单";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(954, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "需求数量";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dgvCurrentRequireQuantity
            // 
            this.dgvCurrentRequireQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvCurrentRequireQuantity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrentRequireQuantity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentRequireQuantity.Location = new System.Drawing.Point(850, 405);
            this.dgvCurrentRequireQuantity.Name = "dgvCurrentRequireQuantity";
            this.dgvCurrentRequireQuantity.RowTemplate.Height = 23;
            this.dgvCurrentRequireQuantity.Size = new System.Drawing.Size(100, 260);
            this.dgvCurrentRequireQuantity.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(848, 390);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "本次需求数量";
            // 
            // 物料计算
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 702);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbItemNumber);
            this.Controls.Add(this.dgvActualRequireQuantity);
            this.Controls.Add(this.dgvCurrentQuantity);
            this.Controls.Add(this.dgvCurrentRequireQuantity);
            this.Controls.Add(this.dgvBOM);
            this.Controls.Add(this.btnGet);
            this.Name = "物料计算";
            this.Text = "物料计算";
            this.Load += new System.EventHandler(this.物料计算_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActualRequireQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentRequireQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.DataGridView dgvBOM;
        private System.Windows.Forms.TextBox tbItemNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCurrentQuantity;
        private System.Windows.Forms.TextBox tbQuantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvActualRequireQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvCurrentRequireQuantity;
        private System.Windows.Forms.Label label6;
    }
}