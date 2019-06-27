namespace Global.Finance
{
    partial class ForeignCurrencyARCD
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
            this.btnChooseFile = new DevComponents.DotNetBar.ButtonX();
            this.tbFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbSheet = new System.Windows.Forms.ComboBox();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.dgvARCD = new System.Windows.Forms.DataGridView();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.tbCustID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbCurrencyReference = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbContractAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.tbCharge = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnLastStepTest = new DevComponents.DotNetBar.ButtonX();
            this.btnWriteToFS = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.tbCharge2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvARCD)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseFile.Location = new System.Drawing.Point(416, 11);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(75, 23);
            this.btnChooseFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseFile.TabIndex = 0;
            this.btnChooseFile.Text = "选择";
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // tbFilePath
            // 
            // 
            // 
            // 
            this.tbFilePath.Border.Class = "TextBoxBorder";
            this.tbFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFilePath.Location = new System.Drawing.Point(80, 12);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.PreventEnterBeep = true;
            this.tbFilePath.Size = new System.Drawing.Size(325, 21);
            this.tbFilePath.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "选择文件";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 38);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(61, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "选择Sheet";
            // 
            // cbbSheet
            // 
            this.cbbSheet.FormattingEnabled = true;
            this.cbbSheet.Location = new System.Drawing.Point(80, 38);
            this.cbbSheet.Name = "cbbSheet";
            this.cbbSheet.Size = new System.Drawing.Size(104, 20);
            this.cbbSheet.TabIndex = 3;
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImport.Location = new System.Drawing.Point(293, 38);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dgvARCD
            // 
            this.dgvARCD.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.dgvARCD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvARCD.Location = new System.Drawing.Point(13, 80);
            this.dgvARCD.Name = "dgvARCD";
            this.dgvARCD.RowTemplate.Height = 23;
            this.dgvARCD.Size = new System.Drawing.Size(794, 390);
            this.dgvARCD.TabIndex = 4;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(1048, 215);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 5;
            this.buttonX1.Text = "测试增加";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // tbCustID
            // 
            // 
            // 
            // 
            this.tbCustID.Border.Class = "TextBoxBorder";
            this.tbCustID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCustID.Location = new System.Drawing.Point(1048, 13);
            this.tbCustID.Name = "tbCustID";
            this.tbCustID.PreventEnterBeep = true;
            this.tbCustID.Size = new System.Drawing.Size(104, 21);
            this.tbCustID.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(981, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "客户代码";
            // 
            // tbCurrencyReference
            // 
            // 
            // 
            // 
            this.tbCurrencyReference.Border.Class = "TextBoxBorder";
            this.tbCurrencyReference.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCurrencyReference.Location = new System.Drawing.Point(1048, 54);
            this.tbCurrencyReference.Name = "tbCurrencyReference";
            this.tbCurrencyReference.PreventEnterBeep = true;
            this.tbCurrencyReference.Size = new System.Drawing.Size(104, 21);
            this.tbCurrencyReference.TabIndex = 1;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(970, 53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(66, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "回款参考号";
            // 
            // tbContractAmount
            // 
            // 
            // 
            // 
            this.tbContractAmount.Border.Class = "TextBoxBorder";
            this.tbContractAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbContractAmount.Location = new System.Drawing.Point(1048, 96);
            this.tbContractAmount.Name = "tbContractAmount";
            this.tbContractAmount.PreventEnterBeep = true;
            this.tbContractAmount.Size = new System.Drawing.Size(104, 21);
            this.tbContractAmount.TabIndex = 1;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(981, 95);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "合同金额";
            // 
            // tbCharge
            // 
            // 
            // 
            // 
            this.tbCharge.Border.Class = "TextBoxBorder";
            this.tbCharge.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCharge.Location = new System.Drawing.Point(1048, 141);
            this.tbCharge.Name = "tbCharge";
            this.tbCharge.PreventEnterBeep = true;
            this.tbCharge.Size = new System.Drawing.Size(104, 21);
            this.tbCharge.TabIndex = 1;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(981, 140);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(55, 23);
            this.labelX6.TabIndex = 2;
            this.labelX6.Text = "手续费";
            // 
            // btnLastStepTest
            // 
            this.btnLastStepTest.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.btnLastStepTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLastStepTest.Location = new System.Drawing.Point(1048, 260);
            this.btnLastStepTest.Name = "btnLastStepTest";
            this.btnLastStepTest.Size = new System.Drawing.Size(75, 23);
            this.btnLastStepTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLastStepTest.TabIndex = 6;
            this.btnLastStepTest.Text = "最后一步";
            this.btnLastStepTest.Click += new System.EventHandler(this.btnLastStepTest_Click);
            // 
            // btnWriteToFS
            // 
            this.btnWriteToFS.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWriteToFS.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnWriteToFS.Location = new System.Drawing.Point(721, 51);
            this.btnWriteToFS.Name = "btnWriteToFS";
            this.btnWriteToFS.Size = new System.Drawing.Size(75, 23);
            this.btnWriteToFS.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnWriteToFS.TabIndex = 7;
            this.btnWriteToFS.Text = "写入四班";
            this.btnWriteToFS.Click += new System.EventHandler(this.btnWriteToFS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(578, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 45);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "币种";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(171, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "欧元";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(89, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "美元";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "人民币";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "工行美元户10",
            "农行一般户15",
            "农行美元户31",
            "农行欧元户38",
            "建行美元户19",
            "中行一般户20",
            "中行美元户21",
            "中行欧元户58",
            "齐商美元户66"});
            this.comboBox1.Location = new System.Drawing.Point(445, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(410, 54);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(29, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "银行";
            // 
            // tbCharge2
            // 
            // 
            // 
            // 
            this.tbCharge2.Border.Class = "TextBoxBorder";
            this.tbCharge2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCharge2.Location = new System.Drawing.Point(1048, 170);
            this.tbCharge2.Name = "tbCharge2";
            this.tbCharge2.PreventEnterBeep = true;
            this.tbCharge2.Size = new System.Drawing.Size(104, 21);
            this.tbCharge2.TabIndex = 1;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(981, 169);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(55, 23);
            this.labelX8.TabIndex = 2;
            this.labelX8.Text = "手续费2";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(628, 51);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 9;
            this.buttonX2.Text = "读取信息";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // ForeignCurrencyARCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 487);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnWriteToFS);
            this.Controls.Add(this.btnLastStepTest);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dgvARCD);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cbbSheet);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbCharge2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tbCharge);
            this.Controls.Add(this.tbContractAmount);
            this.Controls.Add(this.tbCurrencyReference);
            this.Controls.Add(this.tbCustID);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnChooseFile);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForeignCurrencyARCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "外币ARCD导入";
            ((System.ComponentModel.ISupportInitialize)(this.dgvARCD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnChooseFile;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFilePath;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.ComboBox cbbSheet;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private System.Windows.Forms.DataGridView dgvARCD;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbCustID;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbCurrencyReference;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbContractAmount;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbCharge;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX btnLastStepTest;
        private DevComponents.DotNetBar.ButtonX btnWriteToFS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX tbCharge2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}