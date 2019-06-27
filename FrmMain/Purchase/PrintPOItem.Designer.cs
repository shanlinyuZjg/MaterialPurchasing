namespace Global.Purchase
{
    partial class PrintPOItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPOItem));
            this.axGRPrintViewer1 = new Axgregn6Lib.AxGRPrintViewer();
            this.tbLNS = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbPO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelx1 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnShow = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGRPrintViewer1
            // 
            this.axGRPrintViewer1.Enabled = true;
            this.axGRPrintViewer1.Location = new System.Drawing.Point(1, 12);
            this.axGRPrintViewer1.Name = "axGRPrintViewer1";
            this.axGRPrintViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRPrintViewer1.OcxState")));
            this.axGRPrintViewer1.Size = new System.Drawing.Size(1035, 596);
            this.axGRPrintViewer1.TabIndex = 0;
            // 
            // tbLNS
            // 
            // 
            // 
            // 
            this.tbLNS.Border.Class = "TextBoxBorder";
            this.tbLNS.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLNS.Location = new System.Drawing.Point(290, 632);
            this.tbLNS.Name = "tbLNS";
            this.tbLNS.PreventEnterBeep = true;
            this.tbLNS.ReadOnly = true;
            this.tbLNS.Size = new System.Drawing.Size(69, 21);
            this.tbLNS.TabIndex = 11;
            // 
            // tbLN
            // 
            // 
            // 
            // 
            this.tbLN.Border.Class = "TextBoxBorder";
            this.tbLN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLN.Location = new System.Drawing.Point(470, 632);
            this.tbLN.Name = "tbLN";
            this.tbLN.PreventEnterBeep = true;
            this.tbLN.Size = new System.Drawing.Size(113, 21);
            this.tbLN.TabIndex = 12;
            // 
            // tbPO
            // 
            // 
            // 
            // 
            this.tbPO.Border.Class = "TextBoxBorder";
            this.tbPO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPO.Location = new System.Drawing.Point(90, 632);
            this.tbPO.Name = "tbPO";
            this.tbPO.PreventEnterBeep = true;
            this.tbPO.Size = new System.Drawing.Size(113, 21);
            this.tbPO.TabIndex = 4;
            this.tbPO.Text = "\r\n";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(236, 632);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(61, 21);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "订单行数";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(431, 632);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(49, 21);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "行号";
            // 
            // labelx1
            // 
            // 
            // 
            // 
            this.labelx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelx1.Location = new System.Drawing.Point(34, 632);
            this.labelx1.Name = "labelx1";
            this.labelx1.Size = new System.Drawing.Size(49, 21);
            this.labelx1.TabIndex = 10;
            this.labelx1.Text = "订单号";
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(879, 627);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 28);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出";
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(764, 627);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(91, 28);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印";
            // 
            // btnShow
            // 
            this.btnShow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnShow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnShow.Location = new System.Drawing.Point(652, 627);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(91, 28);
            this.btnShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnShow.TabIndex = 7;
            this.btnShow.Text = "查询";
            // 
            // PrintPOItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 664);
            this.Controls.Add(this.tbLNS);
            this.Controls.Add(this.tbLN);
            this.Controls.Add(this.tbPO);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelx1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.axGRPrintViewer1);
            this.DoubleBuffered = true;
            this.Name = "PrintPOItem";
            this.Text = "PrintPOItem";
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Axgregn6Lib.AxGRPrintViewer axGRPrintViewer1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLNS;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLN;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPO;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelx1;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnShow;
    }
}