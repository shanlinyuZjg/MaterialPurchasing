namespace Global.Purchase
{
    partial class GetManufacturerInfoByPO
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
            this.tbMN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbMO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbPO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // tbMN
            // 
            // 
            // 
            // 
            this.tbMN.Border.Class = "TextBoxBorder";
            this.tbMN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMN.Location = new System.Drawing.Point(89, 91);
            this.tbMN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMN.Name = "tbMN";
            this.tbMN.PreventEnterBeep = true;
            this.tbMN.Size = new System.Drawing.Size(295, 26);
            this.tbMN.TabIndex = 8;
            // 
            // tbMO
            // 
            // 
            // 
            // 
            this.tbMO.Border.Class = "TextBoxBorder";
            this.tbMO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMO.Location = new System.Drawing.Point(89, 57);
            this.tbMO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMO.Name = "tbMO";
            this.tbMO.PreventEnterBeep = true;
            this.tbMO.Size = new System.Drawing.Size(148, 26);
            this.tbMO.TabIndex = 9;
            // 
            // tbPO
            // 
            // 
            // 
            // 
            this.tbPO.Border.Class = "TextBoxBorder";
            this.tbPO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPO.Location = new System.Drawing.Point(89, 21);
            this.tbPO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPO.Name = "tbPO";
            this.tbPO.PreventEnterBeep = true;
            this.tbPO.Size = new System.Drawing.Size(148, 26);
            this.tbPO.TabIndex = 10;
            this.tbPO.TextChanged += new System.EventHandler(this.tbPO_TextChanged);
            this.tbPO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPO_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 84);
            this.labelX3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(68, 38);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "生产商名称";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 50);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(68, 38);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "生产商代码";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(245, 25);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(154, 22);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "可输入完成后直接回车";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 14);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(68, 38);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "订单号";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(407, 21);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(71, 26);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // GetManufacturerInfoByPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 314);
            this.Controls.Add(this.tbMN);
            this.Controls.Add(this.tbMO);
            this.Controls.Add(this.tbPO);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnSearch);
            this.DoubleBuffered = true;
            this.Name = "GetManufacturerInfoByPO";
            this.Text = "GetManufacturerInfoByPO";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbMN;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMO;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPO;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSearch;
    }
}