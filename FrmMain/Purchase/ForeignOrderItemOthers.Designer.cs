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
            this.cbUnit = new System.Windows.Forms.CheckBox();
            this.rbtnAluminumBag = new System.Windows.Forms.RadioButton();
            this.rbtnAluminum = new System.Windows.Forms.RadioButton();
            this.rbtnCap = new System.Windows.Forms.RadioButton();
            this.rbtnFoil = new System.Windows.Forms.RadioButton();
            this.rbtnSealLabel = new System.Windows.Forms.RadioButton();
            this.rbtnBucketLabel = new System.Windows.Forms.RadioButton();
            this.rbtnBoxLabel = new System.Windows.Forms.RadioButton();
            this.rbtnCarbonLabel = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbArea = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
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
            this.btnSubmit.Location = new System.Drawing.Point(725, 23);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUnit);
            this.groupBox1.Controls.Add(this.rbtnAluminumBag);
            this.groupBox1.Controls.Add(this.rbtnAluminum);
            this.groupBox1.Controls.Add(this.rbtnCap);
            this.groupBox1.Controls.Add(this.rbtnFoil);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 38);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbUnit
            // 
            this.cbUnit.AutoSize = true;
            this.cbUnit.Location = new System.Drawing.Point(280, 16);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(15, 14);
            this.cbUnit.TabIndex = 2;
            this.cbUnit.UseVisualStyleBackColor = true;
            // 
            // rbtnAluminumBag
            // 
            this.rbtnAluminumBag.AutoSize = true;
            this.rbtnAluminumBag.Location = new System.Drawing.Point(128, 16);
            this.rbtnAluminumBag.Name = "rbtnAluminumBag";
            this.rbtnAluminumBag.Size = new System.Drawing.Size(59, 16);
            this.rbtnAluminumBag.TabIndex = 1;
            this.rbtnAluminumBag.TabStop = true;
            this.rbtnAluminumBag.Text = "铝箔袋";
            this.rbtnAluminumBag.UseVisualStyleBackColor = true;
            this.rbtnAluminumBag.CheckedChanged += new System.EventHandler(this.rbtnAluminumBag_CheckedChanged);
            // 
            // rbtnAluminum
            // 
            this.rbtnAluminum.AutoSize = true;
            this.rbtnAluminum.Location = new System.Drawing.Point(75, 16);
            this.rbtnAluminum.Name = "rbtnAluminum";
            this.rbtnAluminum.Size = new System.Drawing.Size(47, 16);
            this.rbtnAluminum.TabIndex = 0;
            this.rbtnAluminum.TabStop = true;
            this.rbtnAluminum.Text = "铝箔";
            this.rbtnAluminum.UseVisualStyleBackColor = true;
            this.rbtnAluminum.CheckedChanged += new System.EventHandler(this.rbtnAluminumFoil_CheckedChanged);
            // 
            // rbtnCap
            // 
            this.rbtnCap.AutoSize = true;
            this.rbtnCap.Location = new System.Drawing.Point(193, 16);
            this.rbtnCap.Name = "rbtnCap";
            this.rbtnCap.Size = new System.Drawing.Size(59, 16);
            this.rbtnCap.TabIndex = 0;
            this.rbtnCap.TabStop = true;
            this.rbtnCap.Text = "铝塑盖";
            this.rbtnCap.UseVisualStyleBackColor = true;
            this.rbtnCap.CheckedChanged += new System.EventHandler(this.rbtnCap_CheckedChanged);
            // 
            // rbtnFoil
            // 
            this.rbtnFoil.AutoSize = true;
            this.rbtnFoil.Location = new System.Drawing.Point(10, 16);
            this.rbtnFoil.Name = "rbtnFoil";
            this.rbtnFoil.Size = new System.Drawing.Size(59, 16);
            this.rbtnFoil.TabIndex = 0;
            this.rbtnFoil.TabStop = true;
            this.rbtnFoil.Text = "复合膜";
            this.rbtnFoil.UseVisualStyleBackColor = true;
            this.rbtnFoil.CheckedChanged += new System.EventHandler(this.rbtnOthers_CheckedChanged);
            // 
            // rbtnSealLabel
            // 
            this.rbtnSealLabel.AutoSize = true;
            this.rbtnSealLabel.Location = new System.Drawing.Point(219, 15);
            this.rbtnSealLabel.Name = "rbtnSealLabel";
            this.rbtnSealLabel.Size = new System.Drawing.Size(59, 16);
            this.rbtnSealLabel.TabIndex = 0;
            this.rbtnSealLabel.TabStop = true;
            this.rbtnSealLabel.Text = "封口签";
            this.rbtnSealLabel.UseVisualStyleBackColor = true;
            this.rbtnSealLabel.CheckedChanged += new System.EventHandler(this.rbtnSealLabel_CheckedChanged);
            // 
            // rbtnBucketLabel
            // 
            this.rbtnBucketLabel.AutoSize = true;
            this.rbtnBucketLabel.Location = new System.Drawing.Point(148, 15);
            this.rbtnBucketLabel.Name = "rbtnBucketLabel";
            this.rbtnBucketLabel.Size = new System.Drawing.Size(47, 16);
            this.rbtnBucketLabel.TabIndex = 0;
            this.rbtnBucketLabel.TabStop = true;
            this.rbtnBucketLabel.Text = "桶签";
            this.rbtnBucketLabel.UseVisualStyleBackColor = true;
            this.rbtnBucketLabel.CheckedChanged += new System.EventHandler(this.rbtnBucketLabel_CheckedChanged);
            // 
            // rbtnBoxLabel
            // 
            this.rbtnBoxLabel.AutoSize = true;
            this.rbtnBoxLabel.Location = new System.Drawing.Point(77, 15);
            this.rbtnBoxLabel.Name = "rbtnBoxLabel";
            this.rbtnBoxLabel.Size = new System.Drawing.Size(47, 16);
            this.rbtnBoxLabel.TabIndex = 0;
            this.rbtnBoxLabel.TabStop = true;
            this.rbtnBoxLabel.Text = "盒签";
            this.rbtnBoxLabel.UseVisualStyleBackColor = true;
            this.rbtnBoxLabel.CheckedChanged += new System.EventHandler(this.rbtnBoxLabel_CheckedChanged);
            // 
            // rbtnCarbonLabel
            // 
            this.rbtnCarbonLabel.AutoSize = true;
            this.rbtnCarbonLabel.Location = new System.Drawing.Point(6, 15);
            this.rbtnCarbonLabel.Name = "rbtnCarbonLabel";
            this.rbtnCarbonLabel.Size = new System.Drawing.Size(47, 16);
            this.rbtnCarbonLabel.TabIndex = 0;
            this.rbtnCarbonLabel.TabStop = true;
            this.rbtnCarbonLabel.Text = "箱签";
            this.rbtnCarbonLabel.UseVisualStyleBackColor = true;
            this.rbtnCarbonLabel.CheckedChanged += new System.EventHandler(this.rbtnCarbonLabel_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbArea);
            this.groupBox2.Controls.Add(this.rbtnSealLabel);
            this.groupBox2.Controls.Add(this.rbtnCarbonLabel);
            this.groupBox2.Controls.Add(this.rbtnBucketLabel);
            this.groupBox2.Controls.Add(this.rbtnBoxLabel);
            this.groupBox2.Location = new System.Drawing.Point(360, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 38);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // cbArea
            // 
            this.cbArea.AutoSize = true;
            this.cbArea.Location = new System.Drawing.Point(308, 16);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(15, 14);
            this.cbArea.TabIndex = 2;
            this.cbArea.UseVisualStyleBackColor = true;
            // 
            // ForeignOrderItemOthers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 299);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ForeignOrderItemOthers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他类包材";
            this.Load += new System.EventHandler(this.ForeignOrderItemOthers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetail;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnCap;
        private System.Windows.Forms.RadioButton rbtnFoil;
        private System.Windows.Forms.RadioButton rbtnAluminum;
        private System.Windows.Forms.RadioButton rbtnSealLabel;
        private System.Windows.Forms.RadioButton rbtnBucketLabel;
        private System.Windows.Forms.RadioButton rbtnBoxLabel;
        private System.Windows.Forms.RadioButton rbtnCarbonLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnAluminumBag;
        private System.Windows.Forms.CheckBox cbUnit;
        private System.Windows.Forms.CheckBox cbArea;
    }
}