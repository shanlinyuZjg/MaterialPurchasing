namespace Global.Warehouse
{
    partial class WarehousePOItemDetailPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehousePOItemDetailPrint));
            this.axGRPrintViewer1 = new Axgregn6Lib.AxGRPrintViewer();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGRPrintViewer1
            // 
            this.axGRPrintViewer1.Enabled = true;
            this.axGRPrintViewer1.Location = new System.Drawing.Point(1, 1);
            this.axGRPrintViewer1.Name = "axGRPrintViewer1";
            this.axGRPrintViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRPrintViewer1.OcxState")));
            this.axGRPrintViewer1.Size = new System.Drawing.Size(1181, 531);
            this.axGRPrintViewer1.TabIndex = 18;
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(537, 538);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // WarehousePOItemDetailBatchPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 565);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.axGRPrintViewer1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WarehousePOItemDetailBatchPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物管处物料请验批量打印";
         
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Axgregn6Lib.AxGRPrintViewer axGRPrintViewer1;
        private DevComponents.DotNetBar.ButtonX btnPrint;
    }
}