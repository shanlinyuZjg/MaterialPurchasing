namespace Global.Purchase
{
    partial class ForeignOrderItemOthers
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
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnAluminumFoil = new System.Windows.Forms.RadioButton();
            this.rbtnPlasticBoard = new System.Windows.Forms.RadioButton();
            this.rbtnCap = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(3, 52);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(971, 246);
            this.dgvDetail.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Location = new System.Drawing.Point(899, 17);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnCap);
            this.groupBox1.Controls.Add(this.rbtnPlasticBoard);
            this.groupBox1.Controls.Add(this.rbtnAluminumFoil);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 38);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbtnAluminumFoil
            // 
            this.rbtnAluminumFoil.AutoSize = true;
            this.rbtnAluminumFoil.Location = new System.Drawing.Point(10, 16);
            this.rbtnAluminumFoil.Name = "rbtnAluminumFoil";
            this.rbtnAluminumFoil.Size = new System.Drawing.Size(71, 16);
            this.rbtnAluminumFoil.TabIndex = 0;
            this.rbtnAluminumFoil.TabStop = true;
            this.rbtnAluminumFoil.Text = "复合铝箔";
            this.rbtnAluminumFoil.UseVisualStyleBackColor = true;
            this.rbtnAluminumFoil.CheckedChanged += new System.EventHandler(this.rbtnAluminumFoil_CheckedChanged);
            // 
            // rbtnPlasticBoard
            // 
            this.rbtnPlasticBoard.AutoSize = true;
            this.rbtnPlasticBoard.Location = new System.Drawing.Point(144, 16);
            this.rbtnPlasticBoard.Name = "rbtnPlasticBoard";
            this.rbtnPlasticBoard.Size = new System.Drawing.Size(59, 16);
            this.rbtnPlasticBoard.TabIndex = 0;
            this.rbtnPlasticBoard.TabStop = true;
            this.rbtnPlasticBoard.Text = "塑料托";
            this.rbtnPlasticBoard.UseVisualStyleBackColor = true;
            this.rbtnPlasticBoard.CheckedChanged += new System.EventHandler(this.rbtnPlasticBoard_CheckedChanged);
            // 
            // rbtnCap
            // 
            this.rbtnCap.AutoSize = true;
            this.rbtnCap.Location = new System.Drawing.Point(285, 16);
            this.rbtnCap.Name = "rbtnCap";
            this.rbtnCap.Size = new System.Drawing.Size(59, 16);
            this.rbtnCap.TabIndex = 0;
            this.rbtnCap.TabStop = true;
            this.rbtnCap.Text = "铝塑盖";
            this.rbtnCap.UseVisualStyleBackColor = true;
            this.rbtnCap.CheckedChanged += new System.EventHandler(this.rbtnCap_CheckedChanged);
            // 
            // ForeignOrderItemOthers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 299);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ForeignOrderItemOthers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他类包材";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetail;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnCap;
        private System.Windows.Forms.RadioButton rbtnPlasticBoard;
        private System.Windows.Forms.RadioButton rbtnAluminumFoil;
    }
}