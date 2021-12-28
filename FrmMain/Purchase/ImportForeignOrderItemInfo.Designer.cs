namespace FrmMain.Purchase
{
    partial class ImportForeignOrderItemInfo
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnShow = new DevComponents.DotNetBar.ButtonX();
            this.btnChooseFile = new DevComponents.DotNetBar.ButtonX();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnOthers = new System.Windows.Forms.RadioButton();
            this.rbtnCarbon = new System.Windows.Forms.RadioButton();
            this.rbtnSpecification = new System.Windows.Forms.RadioButton();
            this.rbtnLabel = new System.Windows.Forms.RadioButton();
            this.rbtnBox = new System.Windows.Forms.RadioButton();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.cbbSheet = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(2, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 21;
            this.labelX1.Text = "选择文件";
            // 
            // tbFilePath
            // 
            // 
            // 
            // 
            this.tbFilePath.Border.Class = "TextBoxBorder";
            this.tbFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFilePath.Location = new System.Drawing.Point(69, 13);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.PreventEnterBeep = true;
            this.tbFilePath.Size = new System.Drawing.Size(354, 21);
            this.tbFilePath.TabIndex = 19;
            // 
            // btnShow
            // 
            this.btnShow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnShow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnShow.Location = new System.Drawing.Point(531, 51);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnShow.TabIndex = 17;
            this.btnShow.Text = "显示";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseFile.Location = new System.Drawing.Point(531, 13);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(75, 23);
            this.btnChooseFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseFile.TabIndex = 18;
            this.btnChooseFile.Text = "选择";
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(2, 115);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(723, 426);
            this.dgvDetail.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnOthers);
            this.groupBox1.Controls.Add(this.rbtnCarbon);
            this.groupBox1.Controls.Add(this.rbtnSpecification);
            this.groupBox1.Controls.Add(this.rbtnLabel);
            this.groupBox1.Controls.Add(this.rbtnBox);
            this.groupBox1.Location = new System.Drawing.Point(2, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 39);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "包材类型选择";
            // 
            // rbtnOthers
            // 
            this.rbtnOthers.AutoSize = true;
            this.rbtnOthers.Location = new System.Drawing.Point(374, 16);
            this.rbtnOthers.Name = "rbtnOthers";
            this.rbtnOthers.Size = new System.Drawing.Size(71, 16);
            this.rbtnOthers.TabIndex = 4;
            this.rbtnOthers.Text = "其他包材";
            this.rbtnOthers.UseVisualStyleBackColor = true;
            // 
            // rbtnCarbon
            // 
            this.rbtnCarbon.AutoSize = true;
            this.rbtnCarbon.Location = new System.Drawing.Point(293, 16);
            this.rbtnCarbon.Name = "rbtnCarbon";
            this.rbtnCarbon.Size = new System.Drawing.Size(47, 16);
            this.rbtnCarbon.TabIndex = 4;
            this.rbtnCarbon.Text = "纸箱";
            this.rbtnCarbon.UseVisualStyleBackColor = true;
            // 
            // rbtnSpecification
            // 
            this.rbtnSpecification.AutoSize = true;
            this.rbtnSpecification.Location = new System.Drawing.Point(194, 17);
            this.rbtnSpecification.Name = "rbtnSpecification";
            this.rbtnSpecification.Size = new System.Drawing.Size(59, 16);
            this.rbtnSpecification.TabIndex = 3;
            this.rbtnSpecification.Text = "说明书";
            this.rbtnSpecification.UseVisualStyleBackColor = true;
            // 
            // rbtnLabel
            // 
            this.rbtnLabel.AutoSize = true;
            this.rbtnLabel.Location = new System.Drawing.Point(99, 17);
            this.rbtnLabel.Name = "rbtnLabel";
            this.rbtnLabel.Size = new System.Drawing.Size(47, 16);
            this.rbtnLabel.TabIndex = 2;
            this.rbtnLabel.Text = "标签";
            this.rbtnLabel.UseVisualStyleBackColor = true;
            // 
            // rbtnBox
            // 
            this.rbtnBox.AutoSize = true;
            this.rbtnBox.Checked = true;
            this.rbtnBox.Location = new System.Drawing.Point(11, 17);
            this.rbtnBox.Name = "rbtnBox";
            this.rbtnBox.Size = new System.Drawing.Size(47, 16);
            this.rbtnBox.TabIndex = 0;
            this.rbtnBox.TabStop = true;
            this.rbtnBox.Text = "纸盒";
            this.rbtnBox.UseVisualStyleBackColor = true;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.Red;
            this.labelX3.Location = new System.Drawing.Point(2, 41);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(498, 23);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "必须按照Excel模板格式进行信息导入，Exce每一行记录不能有空的单元格，没有则填写0";
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Location = new System.Drawing.Point(531, 86);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImport.TabIndex = 25;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cbbSheet
            // 
            this.cbbSheet.FormattingEnabled = true;
            this.cbbSheet.Location = new System.Drawing.Point(432, 12);
            this.cbbSheet.Name = "cbbSheet";
            this.cbbSheet.Size = new System.Drawing.Size(28, 20);
            this.cbbSheet.TabIndex = 22;
            this.cbbSheet.Visible = false;
            // 
            // ImportForeignOrderItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 541);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.cbbSheet);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnChooseFile);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ImportForeignOrderItemInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "外贸物料信息导入";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFilePath;
        private DevComponents.DotNetBar.ButtonX btnShow;
        private DevComponents.DotNetBar.ButtonX btnChooseFile;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnSpecification;
        private System.Windows.Forms.RadioButton rbtnLabel;
        private System.Windows.Forms.RadioButton rbtnBox;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private System.Windows.Forms.RadioButton rbtnCarbon;
        private System.Windows.Forms.ComboBox cbbSheet;
        private System.Windows.Forms.RadioButton rbtnOthers;
    }
}