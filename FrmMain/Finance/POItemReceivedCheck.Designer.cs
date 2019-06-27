namespace Global.Finance
{
    partial class POItemReceivedCheck
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
            this.dgvPORecord = new System.Windows.Forms.DataGridView();
            this.dgvPORV = new System.Windows.Forms.DataGridView();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnSearchPORV = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtpSearchDatePORV = new System.Windows.Forms.DateTimePicker();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dtpSearchDatePORecord = new System.Windows.Forms.DateTimePicker();
            this.btnSearchPORecord = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPORecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPORV)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPORecord
            // 
            this.dgvPORecord.AllowUserToAddRows = false;
            this.dgvPORecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPORecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPORecord.Location = new System.Drawing.Point(9, 356);
            this.dgvPORecord.Name = "dgvPORecord";
            this.dgvPORecord.ReadOnly = true;
            this.dgvPORecord.RowTemplate.Height = 23;
            this.dgvPORecord.Size = new System.Drawing.Size(1270, 318);
            this.dgvPORecord.TabIndex = 0;
            // 
            // dgvPORV
            // 
            this.dgvPORV.AllowUserToAddRows = false;
            this.dgvPORV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPORV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPORV.Location = new System.Drawing.Point(12, 42);
            this.dgvPORV.Name = "dgvPORV";
            this.dgvPORV.ReadOnly = true;
            this.dgvPORV.RowTemplate.Height = 23;
            this.dgvPORV.Size = new System.Drawing.Size(1270, 279);
            this.dgvPORV.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(1168, 329);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "buttonX1";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(966, 327);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "labelX1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1047, 329);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 3;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(184, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "PORV入库记录(默认显示前一天)";
            // 
            // btnSearchPORV
            // 
            this.btnSearchPORV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchPORV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearchPORV.Location = new System.Drawing.Point(377, 8);
            this.btnSearchPORV.Name = "btnSearchPORV";
            this.btnSearchPORV.Size = new System.Drawing.Size(53, 23);
            this.btnSearchPORV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearchPORV.TabIndex = 1;
            this.btnSearchPORV.Text = "查询";
            this.btnSearchPORV.Click += new System.EventHandler(this.btnSearchPORV_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(193, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "日期选择";
            // 
            // dtpSearchDatePORV
            // 
            this.dtpSearchDatePORV.Location = new System.Drawing.Point(256, 9);
            this.dtpSearchDatePORV.Name = "dtpSearchDatePORV";
            this.dtpSearchDatePORV.Size = new System.Drawing.Size(107, 21);
            this.dtpSearchDatePORV.TabIndex = 4;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 329);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(81, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "采购入库记录";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(193, 326);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "日期选择";
            // 
            // dtpSearchDatePORecord
            // 
            this.dtpSearchDatePORecord.Location = new System.Drawing.Point(256, 326);
            this.dtpSearchDatePORecord.Name = "dtpSearchDatePORecord";
            this.dtpSearchDatePORecord.Size = new System.Drawing.Size(107, 21);
            this.dtpSearchDatePORecord.TabIndex = 4;
            // 
            // btnSearchPORecord
            // 
            this.btnSearchPORecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchPORecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearchPORecord.Location = new System.Drawing.Point(377, 326);
            this.btnSearchPORecord.Name = "btnSearchPORecord";
            this.btnSearchPORecord.Size = new System.Drawing.Size(53, 23);
            this.btnSearchPORecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearchPORecord.TabIndex = 1;
            this.btnSearchPORecord.Text = "查询";
            this.btnSearchPORecord.Click += new System.EventHandler(this.btnSearchPORecord_Click);
            // 
            // POItemReceivedCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 686);
            this.Controls.Add(this.dtpSearchDatePORecord);
            this.Controls.Add(this.dtpSearchDatePORV);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnSearchPORecord);
            this.Controls.Add(this.btnSearchPORV);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dgvPORV);
            this.Controls.Add(this.dgvPORecord);
            this.DoubleBuffered = true;
            this.Name = "POItemReceivedCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "采购物料入库审核";
            this.Load += new System.EventHandler(this.POItemReceivedCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPORecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPORV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPORecord;
        private System.Windows.Forms.DataGridView dgvPORV;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TextBox textBox1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnSearchPORV;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DateTimePicker dtpSearchDatePORV;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DateTimePicker dtpSearchDatePORecord;
        private DevComponents.DotNetBar.ButtonX btnSearchPORecord;
    }
}