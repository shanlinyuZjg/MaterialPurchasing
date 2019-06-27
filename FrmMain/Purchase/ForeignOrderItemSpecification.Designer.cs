namespace Global.Purchase
{
    partial class ForeignOrderItemSpecification
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
            this.cbSpecificationMannual = new System.Windows.Forms.CheckBox();
            this.btnBoxAddWithoutRecord = new DevComponents.DotNetBar.ButtonX();
            this.dgvSpecification = new System.Windows.Forms.DataGridView();
            this.btnBoxSubmit = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnSingleColor = new System.Windows.Forms.RadioButton();
            this.rbtnComplexColor = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecification)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSpecificationMannual
            // 
            this.cbSpecificationMannual.AutoSize = true;
            this.cbSpecificationMannual.Location = new System.Drawing.Point(7, 15);
            this.cbSpecificationMannual.Name = "cbSpecificationMannual";
            this.cbSpecificationMannual.Size = new System.Drawing.Size(54, 18);
            this.cbSpecificationMannual.TabIndex = 15;
            this.cbSpecificationMannual.Text = "手工";
            this.cbSpecificationMannual.UseVisualStyleBackColor = true;
            this.cbSpecificationMannual.CheckedChanged += new System.EventHandler(this.cbSpecificationMannual_CheckedChanged);
            // 
            // btnBoxAddWithoutRecord
            // 
            this.btnBoxAddWithoutRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBoxAddWithoutRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBoxAddWithoutRecord.Location = new System.Drawing.Point(259, 12);
            this.btnBoxAddWithoutRecord.Name = "btnBoxAddWithoutRecord";
            this.btnBoxAddWithoutRecord.Size = new System.Drawing.Size(87, 27);
            this.btnBoxAddWithoutRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBoxAddWithoutRecord.TabIndex = 14;
            this.btnBoxAddWithoutRecord.Text = "无记录添加";
            this.btnBoxAddWithoutRecord.Click += new System.EventHandler(this.btnBoxAddWithoutRecord_Click);
            // 
            // dgvSpecification
            // 
            this.dgvSpecification.AllowUserToAddRows = false;
            this.dgvSpecification.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSpecification.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.dgvSpecification.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecification.Location = new System.Drawing.Point(5, 57);
            this.dgvSpecification.Name = "dgvSpecification";
            this.dgvSpecification.RowTemplate.Height = 23;
            this.dgvSpecification.Size = new System.Drawing.Size(1014, 197);
            this.dgvSpecification.TabIndex = 13;
            // 
            // btnBoxSubmit
            // 
            this.btnBoxSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBoxSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBoxSubmit.Location = new System.Drawing.Point(397, 12);
            this.btnBoxSubmit.Name = "btnBoxSubmit";
            this.btnBoxSubmit.Size = new System.Drawing.Size(87, 27);
            this.btnBoxSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBoxSubmit.TabIndex = 12;
            this.btnBoxSubmit.Text = "多条添加";
            this.btnBoxSubmit.Click += new System.EventHandler(this.btnBoxSubmit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtnComplexColor);
            this.panel1.Controls.Add(this.rbtnSingleColor);
            this.panel1.Location = new System.Drawing.Point(67, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 27);
            this.panel1.TabIndex = 16;
            // 
            // rbtnSingleColor
            // 
            this.rbtnSingleColor.AutoSize = true;
            this.rbtnSingleColor.Location = new System.Drawing.Point(3, 3);
            this.rbtnSingleColor.Name = "rbtnSingleColor";
            this.rbtnSingleColor.Size = new System.Drawing.Size(53, 18);
            this.rbtnSingleColor.TabIndex = 0;
            this.rbtnSingleColor.TabStop = true;
            this.rbtnSingleColor.Text = "单色";
            this.rbtnSingleColor.UseVisualStyleBackColor = true;
            this.rbtnSingleColor.CheckedChanged += new System.EventHandler(this.rbtnSingleColor_CheckedChanged);
            // 
            // rbtnComplexColor
            // 
            this.rbtnComplexColor.AutoSize = true;
            this.rbtnComplexColor.Location = new System.Drawing.Point(87, 3);
            this.rbtnComplexColor.Name = "rbtnComplexColor";
            this.rbtnComplexColor.Size = new System.Drawing.Size(53, 18);
            this.rbtnComplexColor.TabIndex = 0;
            this.rbtnComplexColor.TabStop = true;
            this.rbtnComplexColor.Text = "彩色";
            this.rbtnComplexColor.UseVisualStyleBackColor = true;
            this.rbtnComplexColor.CheckedChanged += new System.EventHandler(this.rbtnComplexColor_CheckedChanged);
            // 
            // ForeignOrderItemSpecification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 257);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbSpecificationMannual);
            this.Controls.Add(this.btnBoxAddWithoutRecord);
            this.Controls.Add(this.dgvSpecification);
            this.Controls.Add(this.btnBoxSubmit);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "ForeignOrderItemSpecification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "说明书选择";
            this.Load += new System.EventHandler(this.ForeignOrderItemSpecification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecification)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSpecificationMannual;
        private DevComponents.DotNetBar.ButtonX btnBoxAddWithoutRecord;
        private System.Windows.Forms.DataGridView dgvSpecification;
        private DevComponents.DotNetBar.ButtonX btnBoxSubmit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnComplexColor;
        private System.Windows.Forms.RadioButton rbtnSingleColor;
    }
}