namespace Global.Warehouse
{
    partial class PrintBatchRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintBatchRecord));
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.axGRPrintViewer1 = new Axgregn6Lib.AxGRPrintViewer();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(626, 642);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(54, 27);
            this.btnConfirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "关闭";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // axGRPrintViewer1
            // 
            this.axGRPrintViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axGRPrintViewer1.Enabled = true;
            this.axGRPrintViewer1.Location = new System.Drawing.Point(12, 12);
            this.axGRPrintViewer1.Name = "axGRPrintViewer1";
            this.axGRPrintViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRPrintViewer1.OcxState")));
            this.axGRPrintViewer1.Size = new System.Drawing.Size(1242, 629);
            this.axGRPrintViewer1.TabIndex = 6;
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.Location = new System.Drawing.Point(469, 642);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(54, 27);
            this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一条";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // PrintBatchRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 671);
            this.Controls.Add(this.axGRPrintViewer1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnConfirm);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "PrintBatchRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看批记录";
            this.Load += new System.EventHandler(this.PrintPO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Axgregn6Lib.AxGRPrintViewer axGRPrintViewer1;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnNext;
    }
}