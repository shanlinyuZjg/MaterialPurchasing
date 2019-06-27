namespace Global.Purchase
{
    partial class FilePathSetting
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
            this.btnPOChoose = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbPODetailPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbInvoiceDetailPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnPOSave = new DevComponents.DotNetBar.ButtonX();
            this.btnInvoiceChoose = new DevComponents.DotNetBar.ButtonX();
            this.btnInvoiceSave = new DevComponents.DotNetBar.ButtonX();
            this.fbdFilePath = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnPOChoose
            // 
            this.btnPOChoose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPOChoose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPOChoose.Location = new System.Drawing.Point(542, 21);
            this.btnPOChoose.Name = "btnPOChoose";
            this.btnPOChoose.Size = new System.Drawing.Size(57, 23);
            this.btnPOChoose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPOChoose.TabIndex = 0;
            this.btnPOChoose.Text = "选择";
            this.btnPOChoose.Click += new System.EventHandler(this.btnPOChoose_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(7, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(107, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "采购订单导出路径";
            // 
            // tbPODetailPath
            // 
            // 
            // 
            // 
            this.tbPODetailPath.Border.Class = "TextBoxBorder";
            this.tbPODetailPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPODetailPath.Location = new System.Drawing.Point(141, 21);
            this.tbPODetailPath.Name = "tbPODetailPath";
            this.tbPODetailPath.PreventEnterBeep = true;
            this.tbPODetailPath.Size = new System.Drawing.Size(391, 21);
            this.tbPODetailPath.TabIndex = 2;
            this.tbPODetailPath.Tag = "采购订单导出路径";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(7, 60);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(128, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "开具发票明细导出路径";
            // 
            // tbInvoiceDetailPath
            // 
            // 
            // 
            // 
            this.tbInvoiceDetailPath.Border.Class = "TextBoxBorder";
            this.tbInvoiceDetailPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbInvoiceDetailPath.Location = new System.Drawing.Point(141, 60);
            this.tbInvoiceDetailPath.Name = "tbInvoiceDetailPath";
            this.tbInvoiceDetailPath.PreventEnterBeep = true;
            this.tbInvoiceDetailPath.Size = new System.Drawing.Size(391, 21);
            this.tbInvoiceDetailPath.TabIndex = 2;
            this.tbInvoiceDetailPath.Tag = "开具发票明细导出路径";
            // 
            // btnPOSave
            // 
            this.btnPOSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPOSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPOSave.Location = new System.Drawing.Point(614, 21);
            this.btnPOSave.Name = "btnPOSave";
            this.btnPOSave.Size = new System.Drawing.Size(57, 23);
            this.btnPOSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPOSave.TabIndex = 0;
            this.btnPOSave.Text = "保存";
            this.btnPOSave.Click += new System.EventHandler(this.btnPOSave_Click);
            // 
            // btnInvoiceChoose
            // 
            this.btnInvoiceChoose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInvoiceChoose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInvoiceChoose.Location = new System.Drawing.Point(542, 60);
            this.btnInvoiceChoose.Name = "btnInvoiceChoose";
            this.btnInvoiceChoose.Size = new System.Drawing.Size(57, 23);
            this.btnInvoiceChoose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInvoiceChoose.TabIndex = 0;
            this.btnInvoiceChoose.Text = "选择";
            this.btnInvoiceChoose.Click += new System.EventHandler(this.btnInvoiceChoose_Click);
            // 
            // btnInvoiceSave
            // 
            this.btnInvoiceSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInvoiceSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInvoiceSave.Location = new System.Drawing.Point(614, 60);
            this.btnInvoiceSave.Name = "btnInvoiceSave";
            this.btnInvoiceSave.Size = new System.Drawing.Size(57, 23);
            this.btnInvoiceSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInvoiceSave.TabIndex = 0;
            this.btnInvoiceSave.Text = "保存";
            this.btnInvoiceSave.Click += new System.EventHandler(this.btnInvoiceSave_Click);
            // 
            // FilePathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 131);
            this.Controls.Add(this.tbInvoiceDetailPath);
            this.Controls.Add(this.tbPODetailPath);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnInvoiceSave);
            this.Controls.Add(this.btnPOSave);
            this.Controls.Add(this.btnInvoiceChoose);
            this.Controls.Add(this.btnPOChoose);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilePathSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件导出路径设置";
            this.Load += new System.EventHandler(this.FilePathSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnPOChoose;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPODetailPath;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbInvoiceDetailPath;
        private DevComponents.DotNetBar.ButtonX btnPOSave;
        private DevComponents.DotNetBar.ButtonX btnInvoiceChoose;
        private DevComponents.DotNetBar.ButtonX btnInvoiceSave;
        private System.Windows.Forms.FolderBrowserDialog fbdFilePath;
    }
}