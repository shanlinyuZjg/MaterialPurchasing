namespace Global.Purchase
{
    partial class OldRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.tbNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgv = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnPONumber = new System.Windows.Forms.RadioButton();
            this.rbtnFONumber = new System.Windows.Forms.RadioButton();
            this.rbtnItemNumber = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(402, 21);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(62, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // tbNumber
            // 
            // 
            // 
            // 
            this.tbNumber.Border.Class = "TextBoxBorder";
            this.tbNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbNumber.Location = new System.Drawing.Point(283, 21);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.PreventEnterBeep = true;
            this.tbNumber.Size = new System.Drawing.Size(100, 21);
            this.tbNumber.TabIndex = 2;
            this.tbNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNumber_KeyPress);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.Location = new System.Drawing.Point(6, 64);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(942, 460);
            this.dgv.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnItemNumber);
            this.groupBox1.Controls.Add(this.rbtnPONumber);
            this.groupBox1.Controls.Add(this.rbtnFONumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 43);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询范围";
            // 
            // rbtnPONumber
            // 
            this.rbtnPONumber.AutoSize = true;
            this.rbtnPONumber.Location = new System.Drawing.Point(89, 15);
            this.rbtnPONumber.Name = "rbtnPONumber";
            this.rbtnPONumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnPONumber.TabIndex = 0;
            this.rbtnPONumber.TabStop = true;
            this.rbtnPONumber.Text = "采购单号";
            this.rbtnPONumber.UseVisualStyleBackColor = true;
            // 
            // rbtnFONumber
            // 
            this.rbtnFONumber.AutoSize = true;
            this.rbtnFONumber.Checked = true;
            this.rbtnFONumber.Location = new System.Drawing.Point(7, 15);
            this.rbtnFONumber.Name = "rbtnFONumber";
            this.rbtnFONumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnFONumber.TabIndex = 0;
            this.rbtnFONumber.TabStop = true;
            this.rbtnFONumber.Text = "联系单号";
            this.rbtnFONumber.UseVisualStyleBackColor = true;
            // 
            // rbtnItemNumber
            // 
            this.rbtnItemNumber.AutoSize = true;
            this.rbtnItemNumber.Location = new System.Drawing.Point(171, 15);
            this.rbtnItemNumber.Name = "rbtnItemNumber";
            this.rbtnItemNumber.Size = new System.Drawing.Size(71, 16);
            this.rbtnItemNumber.TabIndex = 0;
            this.rbtnItemNumber.TabStop = true;
            this.rbtnItemNumber.Text = "物料代码";
            this.rbtnItemNumber.UseVisualStyleBackColor = true;
            // 
            // OldRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 536);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.buttonX1);
            this.DoubleBuffered = true;
            this.Name = "OldRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "旧版本记录查询";
            this.Load += new System.EventHandler(this.OldRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbNumber;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnPONumber;
        private System.Windows.Forms.RadioButton rbtnFONumber;
        private System.Windows.Forms.RadioButton rbtnItemNumber;
    }
}